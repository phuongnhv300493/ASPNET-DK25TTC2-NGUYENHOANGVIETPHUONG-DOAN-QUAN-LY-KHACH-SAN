using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace datphongkhachsan.Data.DataModel.ViewModel
{
    public class PhongViewModel
    {
        public Phong phongs { get; set; }
        public IEnumerable<LoaiPhong> loaiphongs {get;set;}
        public IEnumerable<ChuongTrinh> chuongtrinhs { get; set; }

    }
}
