using Microsoft.EntityFrameworkCore;
using datphongkhachsan.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using datphongkhachsan.Data.DataModel;

namespace datphongkhachsan.Service
{
    public class ChiTietDatPhongService : IChiTietDatPhong
    {
        private readonly ApplicationDbContext _context;

        public ChiTietDatPhongService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(ChiTietDatPhong obj)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ChiTietDatPhong> GetAll()
        {
            throw new NotImplementedException();
        }

        public ChiTietDatPhong GetOneById(int? id)
        {
            throw new NotImplementedException();
        }

        public void New(ChiTietDatPhong obj)
        {
            throw new NotImplementedException();
        }
    }
}
