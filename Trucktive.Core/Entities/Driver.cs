using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucktive.Core.Entities
{
    public class Driver : BaseEntity
    {
        public string FName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Rate { get; set; } = 0m;



        public int SupervisorId { get; set; }
        public Supervisor Supervisor { get; set; } = default!;
        public ICollection<Trip> Trips { get; set; } = new HashSet<Trip>();
        public ICollection<DriverBehavior> Behaviors { get; set; } = new HashSet<DriverBehavior>();
    }
}
