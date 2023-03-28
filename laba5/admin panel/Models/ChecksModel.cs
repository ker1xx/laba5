using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace laba5
{
    public class ChecksModel
    {
        public int employee_id { get; set; }
        public int market_id { get; set; }
        public decimal total_money { get; set; }
        public DateTime date { get; set; }

    }
}
