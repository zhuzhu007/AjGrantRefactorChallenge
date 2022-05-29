using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjGrantRefactorChallenge
{
    public class Policy
    {
        public const int Car = 105;
        public const int Motorcycle = 56;
        public const int Home = 235;

        public Policy(string policyHolderName, string description, int price)
        {
            PolicyHolderName = policyHolderName;
            Description = description;
            Price = price;
        }

        public string PolicyHolderName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

    }
}
