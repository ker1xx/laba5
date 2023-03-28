using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5.admin_panel.Models
{
    public class GoodsModel
    {
        public decimal Price { get; set; }
        public string Sex { get; set; }
        public int NameId { get; set; }
        public int Size { get; set; }
    }
}
