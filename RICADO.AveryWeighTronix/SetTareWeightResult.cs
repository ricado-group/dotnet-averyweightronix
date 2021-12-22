using System;

namespace RICADO.AveryWeighTronix
{
    public class SetTareWeightResult : RequestResult
    {
        #region Constructor

        internal SetTareWeightResult(Channels.ProcessMessageResult result) : base(result)
        {
        }

        #endregion
    }
}
