using System;

namespace RICADO.AveryWeighTronix.SMA
{
    internal static class Commands
    {
        #region Public Properties

        public static readonly string ReadWeightAndStatus = "W";
        public static readonly string ZeroCommand = "Z";
        public static readonly string TareCommand = "T";
        public static readonly string SetTareWeight = "T";
        public static readonly string ReadTareWeight = "M";
        public static readonly string ClearTareWeight = "C";
        public static readonly string SetUnitOfMeasure = "U";
        public static readonly string ReadDiagnostics = "D";

        #endregion
    }
}
