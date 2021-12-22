using System;

namespace RICADO.AveryWeighTronix
{
    public class ZeroCommandResult : RequestResult
    {
        #region Constructor

        internal ZeroCommandResult(Channels.ProcessMessageResult result) : base(result)
        {
        }

        #endregion
    }
}
