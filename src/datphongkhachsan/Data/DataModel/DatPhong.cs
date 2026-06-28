using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace datphongkhachsan.Data.DataModel
{
    public interface  IDatPhong
    {
        IQueryable<DatPhong> GetAll();
        DatPhong GetOneById(int? id);
        int New(DatPhong DatPhong);
        void Edit(DatPhong DatPhong);
        void Delete(int id);
    }

    public  class DatPhong
    {
        public DatPhong()
        {
            ChiTietDatPhong = new HashSet<ChiTietDatPhong>();
            ChiTietDichVuDatPhong = new HashSet<ChiTietDichVuDatPhong>();
        }

        public int Id { get; set; }
        [Display(Name = "Họ và tên")]
        public string TenNguoiDat { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "CMND/CCCD")]
        public string Cmnd { get; set; }
        [Display(Name = "Số điện thoại")]
        public string Sdt { get; set; }

        [Column(TypeName = "Money")]
        [Display(Name = "Tiền đặt cọc")]
        public decimal TienDatCoc { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Thời gian nhận phòng")]
        public DateTime ThoiGianNhanPhongDuKien { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Thời gian trả phòng")]
        public DateTime ThoiGianTraPhongDuKien { get; set; }
        [Column(TypeName = "Money")]
        [Display(Name = "Tổng tiền")]
        public decimal TongTien { get; set; }

        public int? AccoutId { get; set; }
        public virtual AccountSys Account { get; set; }

        public virtual ICollection<ChiTietDatPhong> ChiTietDatPhong { get; set; }
        public virtual ICollection<ChiTietDichVuDatPhong> ChiTietDichVuDatPhong { get; set; }
    }
}
