using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5.admin_panel.Models
{
    public class OrderInfoModel
    {
        public int OrderInfoId { get;set; }
        public int GoodsInOrderId { get; set; }
        public decimal Profit { get; set; }
    }
}
