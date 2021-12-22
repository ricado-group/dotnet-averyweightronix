using System;
using System.Text.RegularExpressions;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class ReadTareWeightResponse : Response
    {
        #region Constants

        private const string MessageRegex = "^([ZOUEIT\u0020])([0-9])([GTNgn])([M\u0020]).([0-9\u0020\u003A\u002E\\-]{10})(.{3})$";

        #endregion


        #region Private Fields

        private double _tareWeight;
        private UnitOfMeasure _units;

        #endregion


        #region Public Properties

        public double TareWeight => _tareWeight;
        public UnitOfMeasure Units => _units;

        #endregion


        #region Constructor

        protected ReadTareWeightResponse(Request request, Memory<byte> responseMessage) : base(request, responseMessage)
        {
        }

        #endregion


        #region Public Methods

        public static ReadTareWeightResponse UnpackResponseMessage(ReadTareWeightRequest request, Memory<byte> responseMessage)
        {
            return new ReadTareWeightResponse(request, responseMessage);
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
                throw new SMAException("The Read Tare Weight Response Message Format was Invalid");
            }

            WeightType weightType = splitInformation[3].ConvertToWeightType();

            if(weightType != WeightType.Tare)
            {
                throw new SMAException("Failed to Read the Tare Weight. The Device did not report a Tare Weight");
            }

            double weight;

            if(double.TryParse(splitInformation[5], out weight))
            {
                _tareWeight = weight;
                return;
            }

            _tareWeight = 0;
        }

        #endregion
    }
}
