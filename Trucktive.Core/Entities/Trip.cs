using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucktive.Core.Entities
{
    public class Trip : BaseEntity
    {
        public DateTime DateTime { get; set; }
        public decimal Rate { get; set; }
        public int HashBraking { get; set; }
        public int HashAcceleration { get; set; }
        public decimal IdPrime { get; set; }
        public int OverspeedEvents { get; set; }


        public int VehicleId { get; set; } 
        public Vehicle Vehicle { get; set; } 

        public int DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}
