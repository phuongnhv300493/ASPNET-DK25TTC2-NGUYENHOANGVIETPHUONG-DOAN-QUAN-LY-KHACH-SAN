using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace datphongkhachsan.Data.DataModel
{
    public interface IChiTietDichVuDatPhong
    {
        IQueryable<ChiTietDichVuDatPhong> GetAll();
        ChiTietDichVuDatPhong GetOneById(int? id);
        void New(ChiTietDichVuDatPhong obj);
        void Edit(ChiTietDichVuDatPhong obj);
        void Delete(int id);
    }
    public class ChiTietDichVuDatPhong
    {
        [Key]
        public  int Id { get; set; }
        public int DatPhongId { get; set; }
        public int DichVuId { get; set; }
        public int SoLuong { get; set; }

        public virtual DatPhong DatPhong { get; set; }
        public virtual DichVu DichVu { get; set; }
    }
}
