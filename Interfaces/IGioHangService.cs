using TreasureL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TreasureL.Interfaces
{
    public interface IGioHangService
    {
        List<ThanhToan> GetListThanhToan();

        public Task<bool> ThanhToanNow(Cart cart, ThanhToan tt);
    }
}
