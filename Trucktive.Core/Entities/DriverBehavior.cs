using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucktive.Core.Entities
{
    public class DriverBehavior : BaseEntity
    {
        public string Type { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}
