using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace datphongkhachsan.Data.DataModel
{
    public interface IChuongTrinh
    {
        IQueryable<ChuongTrinh> GetAll();
        ChuongTrinh GetOneById(int? id);
        void New(ChuongTrinh obj);
        void Edit(ChuongTrinh obj);
        void Delete(int id);
    }
    public  class ChuongTrinh
    {
        public ChuongTrinh()
        {
            Phongs = new HashSet<Phong>();
        }

        public int Id { get; set; }
        [Display(Name = "Tên chương trình")]
        public string TenChuongTrinh { get; set; }
        [Display(Name = "Tỷ lệ thay đổi giá (%)")]
        public int TiLeThayDoiGia { get; set; }

        [Display(Name = "Là tăng giá")]

        public bool IsTang { get; set; }

        public virtual ICollection<Phong> Phongs { get; set; }
    }
}
