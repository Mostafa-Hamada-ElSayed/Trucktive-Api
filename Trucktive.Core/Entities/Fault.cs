using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucktive.Core.Entities
{
    public class Fault : BaseEntity
    {
        public int SecurityLevel { get; set; } // ??? 
        public string ProblemName { get; set; } = string.Empty;
        public DateTime Time { get; set; }


        public int VehicleId { get; set; } 
        public Vehicle Vehicle { get; set; } 
    }
}
