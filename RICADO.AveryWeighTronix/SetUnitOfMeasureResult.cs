using System;

namespace RICADO.AveryWeighTronix
{
    public class SetUnitOfMeasureResult : RequestResult
    {
        #region Constructor

        internal SetUnitOfMeasureResult(Channels.ProcessMessageResult result) : base(result)
        {
        }

        #endregion
    }
}
