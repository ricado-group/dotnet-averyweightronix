using System;

namespace RICADO.AveryWeighTronix
{
    public class ClearTareWeightResult : RequestResult
    {
        #region Constructor

        internal ClearTareWeightResult(Channels.ProcessMessageResult result) : base(result)
        {
        }

        #endregion
    }
}
