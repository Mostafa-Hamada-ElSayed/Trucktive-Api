using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucktive.Core.Entities
{
    public class Vehicle : BaseEntity
    {
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }


        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        public int? SupervisorId { get; set; }
        public Supervisor? Supervisor { get; set; }

        public ICollection<Trip> Trips { get; set; } = new HashSet<Trip>();
        public ICollection<Fault> Faults { get; set; } = new HashSet<Fault>();
    }
}
