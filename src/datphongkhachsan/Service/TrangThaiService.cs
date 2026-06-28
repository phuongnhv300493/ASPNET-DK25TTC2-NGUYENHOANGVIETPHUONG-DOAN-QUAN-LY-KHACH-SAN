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
    public class TrangThaiService : ITrangThai
    {
         private readonly ApplicationDbContext _context;
        public TrangThaiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<string> GetlistNameTrangThai()
        {
            IQueryable<string> listName = from m in _context.TrangThais
                orderby m.Name descending
                select m.Name;
            return listName;
        }

        void ITrangThai.Delete(int id)
        {
            throw new NotImplementedException();
        }

        void ITrangThai.Edit(TrangThai a)
        {
            throw new NotImplementedException();
        }

        IQueryable<TrangThai> ITrangThai.GetAll()
        {
            throw new NotImplementedException();
        }

        TrangThai ITrangThai.GetOneById(int? id)
        {
            throw new NotImplementedException();
        }

        void ITrangThai.New(TrangThai a)
        {
            throw new NotImplementedException();
        }
    }
}