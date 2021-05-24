using System;
using System.Text;
using QRCoder;

namespace Function
{
    public class FunctionHandler
    {
        public string Handle(string input) {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(input, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);
            
            string base64String = Convert.ToBase64String(qrCodeAsPngByteArr, 0, qrCodeAsPngByteArr.Length);
            return "data:image/png;base64," + base64String;
        }
    }
}
