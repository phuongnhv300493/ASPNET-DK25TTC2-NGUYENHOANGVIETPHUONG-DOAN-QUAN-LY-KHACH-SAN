using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace datphongkhachsan.Data.DataModel
{
    public interface IPhong
    {
        IQueryable<LoaiPhong> GetAll();
        LoaiPhong GetOneById(int? id);
        void New(LoaiPhong LoaiPhong);
        void Edit(LoaiPhong LoaiPhong);
        void Delete(int id);
    }
    public class Phong
    {
        public Phong()
        {
            ChiTietDatPhong = new HashSet<ChiTietDatPhong>();
            HinhPhong = new HashSet<HinhPhong>();
        }

        public int Id { get; set; }
        [Display(Name = "Tên phòng")]
        public string Name { get; set; }

        [Display(Name = "Mô tả ngắn")]
        public string ShortDescription { get; set; }

       
        [Column(TypeName = "Money")] 
        [DisplayFormat(DataFormatString ="{0:C2}")]
        
        public decimal Price { get; set; }

        [Display(Name="Loại phòng")]
        public int? LoaiPhongId { get; set; }
        public int? ChuongTrinhId { get; set; }
        public string HinhUrl { get; set; }


        public virtual ChuongTrinh ChuongTrinh { get; set; }
        public virtual LoaiPhong LoaiPhong { get; set; }
        public virtual ICollection<ChiTietDatPhong> ChiTietDatPhong { get; set; }
        public virtual ICollection<HinhPhong> HinhPhong { get; set; }

        
    }
}
