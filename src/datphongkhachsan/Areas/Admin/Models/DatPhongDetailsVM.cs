using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using datphongkhachsan.Data.DataModel;

namespace datphongkhachsan.Areas.Admin.Models
{
    public class DatPhongDetailsMD
    {
        public DatPhong DatPhong { get; set; }

        
        public List<ChiTietDatPhong> ChiTietDatPhongs { get; set; }
        public List<ChiTietDichVuDatPhong> ChiTietDichVuDatPhongs { get; set; }
    }
}
