
using LearningHub.Infra.Util;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Infra.Services
{
    public class QRCodeService
    {
        public string GenerateQRCodeText(decimal eventId, decimal userId)
        {
            string randomString = Guid.NewGuid().ToString().Substring(0, 6);
            return $"EVENT-{eventId}-USER-{userId}-{randomString}"; 
        }

        public byte[] GenerateQRCodeImage(string qrCodeText)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                using (Bitmap qrBitmap = qrCode.GetGraphic(20))
                using (MemoryStream ms = new MemoryStream())
                {
                    qrBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }

    }
}
