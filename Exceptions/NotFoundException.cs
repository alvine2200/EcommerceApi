using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Exceptions
{
    public class NotFoundException : Exception
    {
        readonly string _entityName;
        public NotFoundException(string entityName) : base($"{entityName} not found.")
        {
            _entityName = entityName;
        }
    }
}