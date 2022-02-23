using System;
using System.Text;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class ReadDiagnosticsRequest : Request
    {
        #region Constructor

        protected ReadDiagnosticsRequest(string commandCode) : base(commandCode)
        {
        }

        #endregion


        #region Public Methods

#if NETSTANDARD
        public ReadDiagnosticsResponse UnpackResponseMessage(byte[] responseMessage)
        {
            return ReadDiagnosticsResponse.UnpackResponseMessage(this, responseMessage);
        }
#else
        public ReadDiagnosticsResponse UnpackResponseMessage(Memory<byte> responseMessage)
        {
            return ReadDiagnosticsResponse.UnpackResponseMessage(this, responseMessage);
        }
#endif

        public static ReadDiagnosticsRequest CreateNew(AveryWeighTronixDevice device)
        {
            return new ReadDiagnosticsRequest(Commands.ReadDiagnostics);
        }

        #endregion


        #region Protected Methods

        protected override void BuildMessageDetail(ref StringBuilder messageBuilder)
        {
        }

        #endregion
    }
}
