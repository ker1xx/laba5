using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5.admin_panel.Models
{
    public class EmployeesModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public int JobTitleId { get; set; }
        public decimal Salary { get; set; }
    }
}
