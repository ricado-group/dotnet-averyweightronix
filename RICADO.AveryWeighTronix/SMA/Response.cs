using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RICADO.AveryWeighTronix.SMA
{
    internal abstract class Response
    {
        #region Constants

        public const ushort STXLength = 1;
        public const ushort ETXLength = 1;

        #endregion


        #region Private Fields

        private readonly Request _request;

        #endregion


        #region Protected Properties

        protected virtual Request Request => _request;

        #endregion


        #region Public Properties

        public static readonly byte STX = (byte)'\n';

        public static readonly byte ETX = (byte)'\r';

        #endregion


        #region Constructor

#if NETSTANDARD
        protected Response(Request request, byte[] responseMessage)
#else
        protected Response(Request request, Memory<byte> responseMessage)
#endif
        {
            _request = request;

            if (responseMessage.Length < STXLength + ETXLength)
            {
                throw new SMAException("The SMA Response Message Length was too short");
            }

#if NETSTANDARD
            if (responseMessage[0] != STX)
            {
                throw new SMAException("Invalid or Missing STX");
            }

            if (responseMessage[responseMessage.Length - ETXLength] != ETX)
            {
                throw new SMAException("Invalid or Missing ETX");
            }

            string messageString = Encoding.ASCII.GetString(responseMessage.Skip(STXLength).Take(responseMessage.Length - STXLength - ETXLength).ToArray());
#else
            if (responseMessage.Span[0] != STX)
            {
                throw new SMAException("Invalid or Missing STX");
            }

            if (responseMessage.Span[responseMessage.Length - ETXLength] != ETX)
            {
                throw new SMAException("Invalid or Missing ETX");
            }

            string messageString = Encoding.ASCII.GetString(responseMessage.Slice(STXLength, responseMessage.Length - STXLength - ETXLength).ToArray());
#endif

            if (messageString == "?")
            {
                throw new SMAException("The SMA Command is not Supported by the Device");
            }

            if(messageString == "!")
            {
                throw new SMAException("SMA Communication or Protocol Error");
            }

            UnpackMessageDetail(messageString);
        }

        #endregion


        #region Protected Methods

        protected abstract void UnpackMessageDetail(string messageDetail);

        #endregion
    }
}
