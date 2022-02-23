using System;
using System.Text.RegularExpressions;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class ReadWeightAndStatusResponse : Response
    {
        #region Constants

        private const string MessageRegex = "^([ZOUEIT\u0020])([0-9])([GTNgn])([M\u0020]).([0-9\u0020\u003A\u002E\\-]{10})(.{3})$";

        #endregion


        #region Private Fields

        private bool _stableStatus;
        private double _weight;
        private WeightType _type;
        private UnitOfMeasure _units;
        private bool _outOfRange;

        #endregion


        #region Public Properties

        public bool StableStatus => _stableStatus;
        public double Weight => _weight;
        public WeightType Type => _type;
        public UnitOfMeasure Units => _units;
        public bool OutOfRange => _outOfRange;

        #endregion


        #region Constructor

#if NETSTANDARD
        protected ReadWeightAndStatusResponse(Request request, byte[] responseMessage) : base(request, responseMessage)
        {
        }
#else
        protected ReadWeightAndStatusResponse(Request request, Memory<byte> responseMessage) : base(request, responseMessage)
        {
        }
#endif

        #endregion


        #region Public Methods

#if NETSTANDARD
        public static ReadWeightAndStatusResponse UnpackResponseMessage(ReadWeightAndStatusRequest request, byte[] responseMessage)
        {
            return new ReadWeightAndStatusResponse(request, responseMessage);
        }
#else
        public static ReadWeightAndStatusResponse UnpackResponseMessage(ReadWeightAndStatusRequest request, Memory<byte> responseMessage)
        {
            return new ReadWeightAndStatusResponse(request, responseMessage);
        }
#endif

        #endregion


        #region Protected Methods

        protected override void UnpackMessageDetail(string messageDetail)
        {
            string[] splitInformation;

            if (Regex.IsMatch(messageDetail, MessageRegex))
            {
                splitInformation = Regex.Split(messageDetail, MessageRegex);
            }
            else
            {
                throw new SMAException("The Read Weight and Status Response Message Format was Invalid");
            }

            if (splitInformation[1] == "E")
            {
                throw new SMAException("Failed to Read the Weight and Status. A Zero Error is Active");
            }

            if (splitInformation[1] == "I")
            {
                throw new SMAException("Failed to Read the Weight and Status. An Initial-Zero Error is Active");
            }

            if (splitInformation[1] == "T")
            {
                throw new SMAException("Failed to Read the Weight and Status. A Tare Error is Active");
            }

            _type = splitInformation[3].ConvertToWeightType();

            _stableStatus = splitInformation[4] == "\u0020";

            _units = splitInformation[6].ConvertToUnitOfMeasure();

            if (splitInformation[1] == "O" || splitInformation[1] == "U")
            {
                _outOfRange = true;
            }
            else
            {
                _outOfRange = false;
            }

            double weight;

            if(double.TryParse(splitInformation[5], out weight))
            {
                _weight = weight;
                return;
            }

            _weight = 0;
        }

        #endregion
    }
}
