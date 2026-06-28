using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace datphongkhachsan.Data.DataModel
{
    public interface IDichVu
    {
        IQueryable<DichVu> GetAll();
        DichVu GetOneById(int? id);
        void New(DichVu dv);
        void Edit(DichVu dv);
        void Delete(int id);
    }
    public partial class DichVu
    {
        public DichVu()
        {
            ChiTietDichVuDatPhong = new HashSet<ChiTietDichVuDatPhong>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
       
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int? InStock { get; set; }
        public int LoaiDvid { get; set; }
        [NotMapped]
        [Display(Name = "Số lượng mua")]
        public  int SoLuongMua { get; set; }
        public virtual LoaiDichVu LoaiDv { get; set; }
        public virtual ICollection<ChiTietDichVuDatPhong> ChiTietDichVuDatPhong { get; set; }
    }
}
