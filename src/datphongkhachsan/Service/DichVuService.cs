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
    public class DichVuService : IDichVu
    {
        private readonly ApplicationDbContext _context;

        public DichVuService(ApplicationDbContext context)
        {
            _context = context;
        }

		public void  Delete(int id)
		{
            var dv = GetOneById(id);
            if(dv == null)
            {
                throw new ArgumentException();
            }
            _context.Remove(dv);
            _context.SaveChangesAsync();
		}

		public  void Edit(DichVu dv)
        {
            var model = _context.DichVus.First(f => f.Id == dv.Id);
            _context.Entry<DichVu>(model).State = EntityState.Detached;
            _context.Update(dv);
            _context.SaveChangesAsync();
        }
		public IQueryable<DichVu> GetAll()
        {
            return _context.DichVus
                .Include(dv => dv.LoaiDv);
        }

        public DichVu GetOneById(int? id)
        {
            return GetAll().FirstOrDefault(dv => dv.Id == id);
        }

        public  void New(DichVu dv)
        {
            _context.Add(dv);
            _context.SaveChangesAsync();
        }
         public IQueryable<DichVu> SearchByName(string dv)
        {
            return _context.DichVus.Where(d=>d.Name.Contains(dv));
           
        }
        public IQueryable<DichVu> getLDatPhongByCMND(string CMND)
        {
            return _context.DichVus;
        }
        public IQueryable<DichVu> getLDatPhongBySDT(string SDT)
        {
            return _context.DichVus;
        }
    }
}
