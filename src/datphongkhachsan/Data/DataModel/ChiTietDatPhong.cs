using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace datphongkhachsan.Data.DataModel
{
    public interface IChiTietDatPhong
    {
        IQueryable<ChiTietDatPhong> GetAll();
        ChiTietDatPhong GetOneById(int? id);
        void New(ChiTietDatPhong obj);
        void Edit(ChiTietDatPhong obj);
        void Delete(int id);
    }
    public  class ChiTietDatPhong
    {
        public int DatPhongId { get; set; }
        public int PhongId { get; set; }
        public int TrangThaiId { get; set; }
        [Display(Name = "Thời gian")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm tt}")]
        public DateTime ThoiGian { get; set; }

        public virtual DatPhong DatPhong { get; set; }
        public virtual Phong Phong { get; set; }
        public virtual TrangThai TrangThai { get; set; }
    }
}
