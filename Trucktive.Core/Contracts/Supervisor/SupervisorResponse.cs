using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trucktive.Core.Entities;

namespace Trucktive.Core.Contracts.Supervisor
{
    public class SupervisorResponse
    {
        public string FName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;


        public int CompanyId { get; set; }
        public Company ?Company { get; set; }

        public ICollection<Driver>? DriverNames { get; set; } = new HashSet<Driver>();
        public ICollection<Vehicle>? Vehicles { get; set; } = new HashSet<Vehicle>(); // One-to-Many with Vehicle
        public string ?CompanyName { get; set; }
        public int Id { get; set; }
    }
}
