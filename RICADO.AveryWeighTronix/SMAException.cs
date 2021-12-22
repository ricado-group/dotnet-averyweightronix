using System;

namespace RICADO.AveryWeighTronix
{
    public class SMAException : Exception
    {
        #region Constructors

        internal SMAException(string message) : base(message)
        {
        }

        internal SMAException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion
    }
}
