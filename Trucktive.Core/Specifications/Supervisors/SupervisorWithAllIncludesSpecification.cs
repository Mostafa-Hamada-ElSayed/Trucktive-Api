using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trucktive.Core.Entities;
using System.Linq.Expressions;

namespace Trucktive.Core.Specifications.Supervisors
{
    public class SupervisorWithAllIncludesSpecification : BaseSpecification<Supervisor>
    {
        public SupervisorWithAllIncludesSpecification()
        {
            AddIncludes();
        }

        public SupervisorWithAllIncludesSpecification(int id)
            : base(s => s.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(s => s.Company);
            Includes.Add(s => s.Drivers);
            Includes.Add(s => s.Vehicles);
        }

        public Expression<Func<Supervisor, bool>> CheckForNullRelationships()
        {
            return s => s.Company != null || s.Drivers != null || s.Vehicles != null;
        }
    }
}
