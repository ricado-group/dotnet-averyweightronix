using System;
using System.Text;

namespace RICADO.AveryWeighTronix.SMA
{
    internal abstract class Request
    {
        #region Constants

        public const string STX = "\n";
        public const string ETX = "\r";

        #endregion


        #region Private Fields

        private readonly string _commandCode;

        #endregion


        #region Public Properties

        public string CommandCode => _commandCode;

        #endregion


        #region Constructor

        protected Request(string commandCode)
        {
            _commandCode = commandCode;
        }

        #endregion


        #region Public Methods

#if NETSTANDARD
        public byte[] BuildMessage()
#else
        public ReadOnlyMemory<byte> BuildMessage()
#endif
        {
            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.Append(_commandCode);

            BuildMessageDetail(ref messageBuilder);

            messageBuilder.Insert(0, STX);

            messageBuilder.Append(ETX);

            return Encoding.ASCII.GetBytes(messageBuilder.ToString());
        }

        #endregion


        #region Protected Methods

        protected abstract void BuildMessageDetail(ref StringBuilder messageBuilder);

        #endregion
    }
}
