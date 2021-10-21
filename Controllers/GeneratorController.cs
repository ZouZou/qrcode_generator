using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QRCodeGeneration.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace QRCodeGeneration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratorController : ControllerBase
    {
        private readonly ILogger<GeneratorController> _logger;
        public GeneratorController(ILogger<GeneratorController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQRCode(QRCodeModel qRCode)
        {
            string QrUri = "";
            await Task.Run(() =>
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qRCode.QRCodeText, QRCodeGenerator.ECCLevel.Q);
                Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                var imgType = Base64QRCode.ImageType.Jpeg;
                string qrCodeImageAsBase64 = qrCode.GetGraphic(20, Color.Black, Color.White, true, imgType);
                QrUri = string.Format("data:image/{0};base64,{1}", imgType.ToString().ToLower(), qrCodeImageAsBase64);
            });

            return Ok(QrUri);
        }
    }
}
