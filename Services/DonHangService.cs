using TreasureL.Interfaces;
using TreasureL.Models;
using System.Collections.Generic;
using System.Linq;

namespace TreasureL.Services
{
    public class DonHangService : IDonHangService
    {
        private readonly TreasureLContext _context;
        public DonHangService(TreasureLContext context)
        {
            _context = context;
        }
        public List<DonHang> GetAll()
        {
            return _context.DonHangs.ToList();
        }

    }
}
