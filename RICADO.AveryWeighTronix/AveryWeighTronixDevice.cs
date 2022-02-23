using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RICADO.AveryWeighTronix.Channels;

namespace RICADO.AveryWeighTronix
{
    public class AveryWeighTronixDevice : IDisposable
    {
        #region Private Fields

        private readonly ConnectionMethod _connectionMethod;
        private readonly string _remoteHost;
        private readonly int _port;
        private int _timeout;
        private int _retries;

        private bool _isInitialized = false;
        private readonly object _isInitializedLock = new object();

        private IChannel _channel;

        private ProtocolType _protocolType;

        #endregion


        #region Internal Properties

        internal IChannel Channel => _channel;

        #endregion


        #region Public Properties

        public ConnectionMethod ConnectionMethod => _connectionMethod;

        public string RemoteHost => _remoteHost;

        public int Port => _port;

        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }

        public int Retries
        {
            get
            {
                return _retries;
            }
            set
            {
                _retries = value;
            }
        }

        public ProtocolType ProtocolType => _protocolType;

        public bool IsInitialized
        {
            get
            {
                lock (_isInitializedLock)
                {
                    return _isInitialized;
                }
            }
        }

        #endregion


        #region Constructors

        public AveryWeighTronixDevice(ConnectionMethod connectionMethod, ProtocolType protocolType, string remoteHost, int port, int timeout = 2000, int retries = 1)
        {
            _connectionMethod = connectionMethod;

            _protocolType = protocolType;

            if (remoteHost == null)
            {
                throw new ArgumentNullException(nameof(remoteHost), "The Remote Host cannot be Null");
            }

            if (remoteHost.Length == 0)
            {
                throw new ArgumentException("The Remote Host cannot be Empty", nameof(remoteHost));
            }

            _remoteHost = remoteHost;

            if (port <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(port), "The Port cannot be less than 1");
            }

            _port = port;

            if (timeout <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout), "The Timeout Value cannot be less than 1");
            }

            _timeout = timeout;

            if (retries < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(retries), "The Retries Value cannot be Negative");
            }

            _retries = retries;
        }

        #endregion


        #region Public Methods

        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            lock (_isInitializedLock)
            {
                if (_isInitialized == true)
                {
                    return;
                }
            }

            // Initialize the Channel
            if (_connectionMethod == ConnectionMethod.Ethernet)
            {
                try
                {
                    _channel = new EthernetChannel(_remoteHost, _port);

                    await _channel.InitializeAsync(_timeout, cancellationToken);
                }
                catch (ObjectDisposedException)
                {
                    throw new AveryWeighTronixException("Failed to Create the Ethernet Communication Channel for Avery Weigh-Tronix Device '" + _remoteHost + ":" + _port + "' - The underlying Socket Connection has been Closed");
                }
                catch (TimeoutException)
                {
                    throw new AveryWeighTronixException("Failed to Create the Ethernet Communication Channel within the Timeout Period for Avery Weigh-Tronix Device '" + _remoteHost + ":" + _port + "'");
                }
                catch (System.Net.Sockets.SocketException e)
                {
                    throw new AveryWeighTronixException("Failed to Create the Ethernet Communication Channel for Avery Weigh-Tronix Device '" + _remoteHost + ":" + _port + "'", e);
                }
            }

            lock (_isInitializedLock)
            {
                _isInitialized = true;
            }
        }

        public void Dispose()
        {
            if (_channel is EthernetChannel)
            {
                _channel.Dispose();

                _channel = null;
            }

            lock (_isInitializedLock)
            {
                _isInitialized = false;
            }
        }

        public async Task<ClearTareWeightResult> ClearTareWeightAsync(CancellationToken cancellationToken)
        {
            lock (_isInitializedLock)
            {
                if (_isInitialized == false || _channel == null)
                {
                    throw new AveryWeighTronixException("This Avery Weigh-Tronix Device must be Initialized first before any Requests can be Processed");
                }
            }

            SMA.ClearTareWeightRequest request = SMA.ClearTareWeightRequest.CreateNew(this);

            ProcessMessageResult result = await _channel.ProcessMessageAsync(request.BuildMessage(), ProtocolType.SMA, _timeout, _retries, cancellationToken);

            request.ValidateResponseMessage(result.ResponseMessage);

            return new ClearTareWeightResult(result);
        }

        public async Task<ReadDiagnosticsResult> ReadDiagnosticsAsync(CancellationToken cancellationToken)
        {
            lock (_isInitializedLock)
            {
                if (_isInitialized == false || _channel == null)
                {
                    throw new AveryWeighTronixException("This Avery Weigh-Tronix Device must be Initialized first before any Requests can be Processed");
                }
            }

            SMA.ReadDiagnosticsRequest request = SMA.ReadDiagnosticsRequest.CreateNew(this);

            ProcessMessageResult result = await _channel.ProcessMessageAsync(request.BuildMessage(), ProtocolType.SMA, _timeout, _retries, cancellationToken);

            SMA.ReadDiagnosticsResponse response = request.UnpackResponseMessage(result.ResponseMessage);

            return new ReadDiagnosticsResult(result, response.RAMError, response.EEPROMError, response.CalibrationError);
        }

        public async Task<ReadTareWeightResult> ReadTareWeightAsync(CancellationToken cancellationToken)
        {
            lock (_isInitializedLock)
            {
                if (_isInitialized == false || _channel == null)
                {
                    throw new AveryWeighTronixException("This Avery Weigh-Tronix Device must be Initialized first before any Requests can be Processed");
                }
            }

            SMA.ReadTareWeightRequest request = SMA.ReadTareWeightRequest.CreateNew(this);

            ProcessMessageResult result = await _channel.ProcessMessageAsync(request.BuildMessage(), ProtocolType.SMA, _timeout, _retries, cancellationToken);

            SMA.ReadTareWeightResponse response = request.UnpackResponseMessage(result.ResponseMessage);

            return new ReadTareWeightResult(result, response.TareWeight, response.Units);
        }

        public async Task<ReadWeightAndStatusResult> ReadWeightAndStatusAsync(CancellationToken cancellationToken)
        {
            lock (_isInitializedLock)
            {
                if (_isInitialized == false || _channel == null)
                {
                    throw new AveryWeighTronixException("This Avery Weigh-Tronix Device must be Initialized first before any Requests can be Processed");
                }
            }

            SMA.ReadWeightAndStatusRequest request = SMA.ReadWeightAndStatusRequest.CreateNew(this);

            ProcessMessageResult result = await _channel.ProcessMessageAsync(request.BuildMessage(), ProtocolType.SMA, _timeout, _retries, cancellationToken);

            SMA.ReadWeightAndStatusResponse response = request.UnpackResponseMessage(result.ResponseMessage);

            return new ReadWeightAndStatusResult(result, response.StableStatus, response.Weight, response.Type, response.Units, response.OutOfRange);
        }

        public async Task<SetTareWeightResult> SetTareWeightAsync(double tareWeight, CancellationToken cancellationToken)
        {
            lock (_isInitializedLock)
            {
                if (_isInitialized == false || _channel == null)
                {
                    throw new AveryWeighTronixException("This Avery Weigh-Tronix Device must be Initialized first before any Requests can be Processed");
                }
            }

            if(tareWeight < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tareWeight), "The Tare Weight Value cannot be less than Zero");
            }

            if(tareWeight > 99999999.9)
            {
                throw new ArgumentOutOfRangeException(nameof(tareWeight), "The Tare Weight Value cannot be greater than '99999999.9'");
            }

            SMA.SetTareWeightRequest request = SMA.SetTareWeightRequest.CreateNew(this, tareWeight);

            ProcessMessageResult result = await _channel.ProcessMessageAsync(request.BuildMessage(), ProtocolType.SMA, _timeout, _retries, cancellationToken);

            request.ValidateResponseMessage(result.ResponseMessage);

            return new SetTareWeightResult(result);
        }

        public async Task<SetUnitOfMeasureResult> SetUnitOfMeasureAsync(UnitOfMeasure newUnits, CancellationToken cancellationToken)
        {
            lock (_isInitializedLock)
            {
                if (_isInitialized == false || _channel == null)
                {
                    throw new AveryWeighTronixException("This Avery Weigh-Tronix Device must be Initialized first before any Requests can be Processed");
                }
            }

            SMA.SetUnitOfMeasureRequest request = SMA.SetUnitOfMeasureRequest.CreateNew(this, newUnits);

            ProcessMessageResult result = await _channel.ProcessMessageAsync(request.BuildMessage(), ProtocolType.SMA, _timeout, _retries, cancellationToken);

            request.ValidateResponseMessage(result.ResponseMessage);

            return new SetUnitOfMeasureResult(result);
        }

        public async Task<TareCommandResult> TareCommandAsync(CancellationToken cancellationToken)
        {
            lock (_isInitializedLock)
            {
                if (_isInitialized == false || _channel == null)
                {
                    throw new AveryWeighTronixException("This Avery Weigh-Tronix Device must be Initialized first before any Requests can be Processed");
                }
            }

            SMA.TareCommandRequest request = SMA.TareCommandRequest.CreateNew(this);

            ProcessMessageResult result = await _channel.ProcessMessageAsync(request.BuildMessage(), ProtocolType.SMA, _timeout, _retries, cancellationToken);

            request.ValidateResponseMessage(result.ResponseMessage);

            return new TareCommandResult(result);
        }

        public async Task<ZeroCommandResult> ZeroCommandAsync(CancellationToken cancellationToken)
        {
            lock (_isInitializedLock)
            {
                if (_isInitialized == false || _channel == null)
                {
                    throw new AveryWeighTronixException("This Avery Weigh-Tronix Device must be Initialized first before any Requests can be Processed");
                }
            }

            SMA.ZeroCommandRequest request = SMA.ZeroCommandRequest.CreateNew(this);

            ProcessMessageResult result = await _channel.ProcessMessageAsync(request.BuildMessage(), ProtocolType.SMA, _timeout, _retries, cancellationToken);

            request.ValidateResponseMessage(result.ResponseMessage);

            return new ZeroCommandResult(result);
        }

        #endregion


        #region Private Methods



        #endregion
    }
}