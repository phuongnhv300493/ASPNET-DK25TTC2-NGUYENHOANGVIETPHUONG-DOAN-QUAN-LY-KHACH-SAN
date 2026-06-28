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
    public class LoaiDvService : ILoaiDichVu
    {
         private readonly ApplicationDbContext _context;

        public LoaiDvService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(LoaiDichVu LoaiDv)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LoaiDichVu> GetAll()
        {
            throw new NotImplementedException();
        }

        public LoaiDichVu GetOneById(int? id)
        {
            throw new NotImplementedException();
        }

        public void New(LoaiDichVu LoaiDv)
        {
            throw new NotImplementedException();
        }
    }
}
