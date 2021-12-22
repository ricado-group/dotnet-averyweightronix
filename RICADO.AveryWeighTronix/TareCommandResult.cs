using System;

namespace RICADO.AveryWeighTronix
{
    public class TareCommandResult : RequestResult
    {
        #region Constructor

        internal TareCommandResult(Channels.ProcessMessageResult result) : base(result)
        {
        }

        #endregion
    }
}
