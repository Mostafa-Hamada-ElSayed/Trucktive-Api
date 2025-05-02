using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trucktive.Core.Entities;

namespace Trucktive.Core.Specifications.Drivers
{
    public class DriverWithSupervisorSpecification : BaseSpecification<Driver>
    {
        public DriverWithSupervisorSpecification()
        {
            AddIncludes();
        }

        public DriverWithSupervisorSpecification(int id)
            : base(d => d.Id == id)
        {
            AddIncludes();
        }
       
        private void AddIncludes()
        {
           Includes.Add(d => d.Supervisor);
        }
    }
}
