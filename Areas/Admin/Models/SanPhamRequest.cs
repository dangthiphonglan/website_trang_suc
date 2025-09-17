using Microsoft.AspNetCore.Http;
using TreasureL.Models;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel;

namespace TreasureL.Areas.Admin.Models
{
    public class SanPhamRequest: SanPham
    {
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
