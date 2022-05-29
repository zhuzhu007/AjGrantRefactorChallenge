using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjGrantRefactorChallenge
{
    public class Policy : IPolicy
    {
        public Policy(string policyType, string policyHolderName, string description, decimal price)
        {
            PolicyType= policyType;
            PolicyHolderName = policyHolderName;
            Description = description;
            Price = price;
        }
        public string PolicyType { get; set; }
        public string PolicyHolderName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }
}
