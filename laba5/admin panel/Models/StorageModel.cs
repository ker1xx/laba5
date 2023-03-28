using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5.admin_panel.Models
{
    public class StorageModel
    {
        public int ModelId { get; set; }
        public int Amount { get; set; }
        public int IdDealer { get; set; }
        public decimal first_price { get; set; }
    }
}
