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
    public class LoaiPhongService : ILoaiPhong
    {
         private readonly ApplicationDbContext _context;

        public LoaiPhongService(ApplicationDbContext context)
        {
            _context = context;
        }

        void ILoaiPhong.Delete(int id)
        {
            throw new NotImplementedException();
        }

        void ILoaiPhong.Edit(LoaiPhong LoaiPhong)
        {
            throw new NotImplementedException();
        }

        IQueryable<LoaiPhong> ILoaiPhong.GetAll()
        {
            throw new NotImplementedException();
        }

        LoaiPhong ILoaiPhong.GetOneById(int? id)
        {
            throw new NotImplementedException();
        }

        void ILoaiPhong.New(LoaiPhong LoaiPhong)
        {
            throw new NotImplementedException();
        }
    }
}
