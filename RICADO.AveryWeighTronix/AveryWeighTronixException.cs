using System;

namespace RICADO.AveryWeighTronix
{
    public class AveryWeighTronixException : Exception
    {
        #region Constructors

        internal AveryWeighTronixException(string message) : base(message)
        {
        }

        internal AveryWeighTronixException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion
    }
}
