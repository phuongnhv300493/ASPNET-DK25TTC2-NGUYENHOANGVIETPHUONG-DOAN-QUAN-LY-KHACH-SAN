using Microsoft.EntityFrameworkCore;
using datphongkhachsan.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using datphongkhachsan.Data.DataModel;

namespace datphongkhachsan.Service
{
    public class ChiTietDichVuDatPhongService : IChiTietDichVuDatPhong
    {
        private readonly ApplicationDbContext _context;

        public ChiTietDichVuDatPhongService(ApplicationDbContext context)
        {
            _context = context;
        }
        public   void New(ChiTietDichVuDatPhong dp)
        {
            _context.Add(dp);
            _context.SaveChangesAsync();
        }
        public  void Delete(int id)
        {
            var dp = GetOneById(id);
            if (dp == null)
            {
                throw new ArgumentException();
            }
            _context.Remove(dp);
            _context.SaveChangesAsync();
        }

        public void Edit(ChiTietDichVuDatPhong dp)
        {
            //////var model = _context.ChiTietDichVuDatPhongs.First(f => f.Id == dp.Id);
            ////_context.Entry<ChiTietDichVuDatPhong>(model).State = EntityState.Detached;
            //_context.Update(dp);
            //_context.SaveChangesAsync();
        }
        public IQueryable<ChiTietDichVuDatPhong> GetAll()
        {
            return _context.ChiTietDichVuDatPhongs.Include(c => c.DatPhong).Include(c => c.DichVu); ;
        }

        public ChiTietDichVuDatPhong GetOneById(int? id)
        {
            //return GetAll().FirstOrDefault(dp => dp.Id == id);
            throw new NotImplementedException();

        }
        public IQueryable<ChiTietDichVuDatPhong> GetByIDPhieuDatPhong(int? id)
        {
            return GetAll().Where(dp => dp.DatPhongId == id);
        }
    }
}
