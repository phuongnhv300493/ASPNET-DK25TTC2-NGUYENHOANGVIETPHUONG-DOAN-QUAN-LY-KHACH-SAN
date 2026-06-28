using Microsoft.EntityFrameworkCore;

using datphongkhachsan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using datphongkhachsan.Data.DataModel;

namespace datphongkhachsan.Service
{
    public class DatPhongService : IDatPhong
    {
        private readonly ApplicationDbContext _context;

        public DatPhongService(ApplicationDbContext context)
        {
            _context = context;
        }
        public  int New(DatPhong dp)
        {
            _context.Add(dp);
            _context.SaveChangesAsync();
            int id = dp.Id;
          return id;
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

        public   void Edit(DatPhong dp)
        {
            var model = _context.DatPhongs.First(f => f.Id == dp.Id);
            _context.Entry<DatPhong>(model).State = EntityState.Detached;
            _context.Update(dp);
             _context.SaveChangesAsync();
        }
        public IQueryable<DatPhong> GetAll()
        {
            return _context.DatPhongs;
        }

        public DatPhong GetOneById(int? id)
        {
            return GetAll().FirstOrDefault(dp => dp.Id == id);
        }


        public IQueryable<DatPhong> getLDatPhongByTimeNhanDuKien(DateTime dp)
        {
            return _context.DatPhongs
            .Where(d => d.ThoiGianNhanPhongDuKien.Date == dp.Date);
        }
        public IQueryable<DatPhong> getLDatPhongByCMND(string CMND)
        {
            return _context.DatPhongs.Where(d => d.TenNguoiDat.Contains(CMND));
        }
        //public IQueryable<DatPhong> getLDatPhongBySDT(string SDT)
        //{
        //    return _context.DatPhongs.Where(d => d.SDT.Contains(SDT));
        //}
        //public IQueryable<ChiTietPhongDatPhong> SearchChiTietDatPhongOfPhieuDP(DatPhong datPhong)
        //{
        //    return _context.CaTietPhongDatPhongs
        //    .Where(d => d.DatPhong == datPhong);
        //}
    }
}
