using System.ComponentModel.DataAnnotations;

namespace QRCodeGeneration.Models
{
    public class QRCodeModel
    {
        [Display(Name = "Enter QRCode Text")]
        public string QRCodeText { get; set; }
    }
}
