using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Infrastructure
{
    public class ValidationResult
    {
        public bool IsSuccess { get; set; }

        public List<string> Messages { get; set; } = new List<string>();

        public string AllErrors
        {
            get
            {
                var output = string.Empty;

                foreach (var error in Messages)
                    output += error + "\n\r";

                return output;
            }
        }
    }

    public class ValidationResult<T> : ValidationResult
    {
        public T ValidatedResult { get; set; }

    }
}
