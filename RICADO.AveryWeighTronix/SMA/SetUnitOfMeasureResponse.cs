using System;
using System.Text.RegularExpressions;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class SetUnitOfMeasureResponse : Response
    {
        #region Constants

        private const string MessageRegex = "^([ZOUEIT\u0020])([0-9])([GTNgn])([M\u0020]).([0-9\u0020\u003A\u002E\\-]{10})(.{3})$";

        #endregion


        #region Constructor

        protected SetUnitOfMeasureResponse(Request request, Memory<byte> responseMessage) : base(request, responseMessage)
        {
        }

        #endregion


        #region Public Methods

        public static void ValidateResponseMessage(SetUnitOfMeasureRequest request, Memory<byte> responseMessage)
        {
            _ = new SetUnitOfMeasureResponse(request, responseMessage);
        }

        #endregion


        #region Protected Methods

        protected override void UnpackMessageDetail(string messageDetail)
        {
            string[] splitInformation;

            if (Regex.IsMatch(messageDetail, MessageRegex))
            {
                splitInformation = Regex.Split(messageDetail, MessageRegex);
            }
            else
            {
                throw new SMAException("The Set Unit of Measure Response Message Format was Invalid");
            }

            UnitOfMeasure units = splitInformation[6].ConvertToUnitOfMeasure();

            if(units != (Request as SetUnitOfMeasureRequest)?.NewUnits)
            {
                throw new SMAException("Failed to Set the Unit of Measure. The Device Units did not change");
            }
        }

        #endregion
    }
}
