using System;
using System.Text.RegularExpressions;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class ReadDiagnosticsResponse : Response
    {
        #region Constants

        private const string MessageRegex = "^([R\u0020])([E\u0020])([C\u0020]).$";

        #endregion


        #region Private Fields

        private bool _ramError;
        private bool _eepromError;
        private bool _calibrationError;

        #endregion


        #region Public Properties

        public bool RAMError => _ramError;
        public bool EEPROMError => _eepromError;
        public bool CalibrationError => _calibrationError;

        #endregion


        #region Constructor

#if NETSTANDARD
        protected ReadDiagnosticsResponse(Request request, byte[] responseMessage) : base(request, responseMessage)
        {
        }
#else
        protected ReadDiagnosticsResponse(Request request, Memory<byte> responseMessage) : base(request, responseMessage)
        {
        }
#endif

        #endregion


        #region Public Methods

#if NETSTANDARD
        public static ReadDiagnosticsResponse UnpackResponseMessage(ReadDiagnosticsRequest request, byte[] responseMessage)
        {
            return new ReadDiagnosticsResponse(request, responseMessage);
        }
#else
        public static ReadDiagnosticsResponse UnpackResponseMessage(ReadDiagnosticsRequest request, Memory<byte> responseMessage)
        {
            return new ReadDiagnosticsResponse(request, responseMessage);
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
                throw new SMAException("The Read Diagnostics Response Message Format was Invalid");
            }

            _ramError = splitInformation[1] == "R";

            _eepromError = splitInformation[2] == "E";

            _calibrationError = splitInformation[3] == "C";
        }

        #endregion
    }
}
