using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucktive.Core.Abstractions
{
    public record Error(string Code, string Description, int? statusCode)
    {
        public static readonly Error None = new(string.Empty, string.Empty, null);
    }
}
