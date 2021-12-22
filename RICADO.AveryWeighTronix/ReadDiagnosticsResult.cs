using System;

namespace RICADO.AveryWeighTronix
{
    public class ReadDiagnosticsResult : RequestResult
    {
        #region Private Fields

        private readonly bool _ramError;
        private readonly bool _eepromError;
        private readonly bool _calibrationError;

        #endregion


        #region Public Properties

        public bool RAMError => _ramError;
        public bool EEPROMError => _eepromError;
        public bool CalibrationError => _calibrationError;

        #endregion


        #region Constructor

        internal ReadDiagnosticsResult(Channels.ProcessMessageResult result, bool ramError, bool eepromError, bool calibrationError) : base(result)
        {
            _ramError = ramError;
            _eepromError = eepromError;
            _calibrationError = calibrationError;
        }

        #endregion
    }
}
