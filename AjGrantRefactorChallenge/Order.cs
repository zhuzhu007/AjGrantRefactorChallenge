using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjGrantRefactorChallenge
{
    public class Order
    {
        private const double TaxRate = .1d;
        private readonly IList<Line> _lines = new List<Line>();

        public Order(string company)
        {
            Company = company;
        }

        public string Company { get; set; }

        public void AddLine(Line line)
        {
            _lines.Add(line);
        }

        public string Receipt()
        {
            Logger.Instance.LogInformation("Printing receipt (text version) - Start");

            var totalAmount = 0d;
            var result = new StringBuilder(string.Format("Order Receipt for {0}{1}", Company, Environment.NewLine));
            for (var index = 0; index < _lines.Count; index++)
            {
                var line = _lines[index];
                var thisAmount = 0d;
                if (line.Policy.Price == Policy.Car)
                {
                    if (line.Quantity >= 1)
                        thisAmount += line.Quantity * line.Policy.Price * .9d;
                    else
                        thisAmount += line.Quantity * line.Policy.Price;
                }
                else if (line.Policy.Price == Policy.Motorcycle)
                {
                    if (line.Quantity >= 2)
                        thisAmount += line.Quantity * line.Policy.Price * .8d;
                    else
                        thisAmount += line.Quantity * line.Policy.Price;
                }
                else if (line.Policy.Price == Policy.Home)
                {
                    if (line.Quantity >= 1)
                        thisAmount += line.Quantity * line.Policy.Price * .8d;
                    else
                        thisAmount += line.Quantity * line.Policy.Price;
                }

                result.AppendLine(string.Format("\t{0} x {1} {2} = {3}", line.Quantity, line.Policy.PolicyHolderName, line.Policy.Description, thisAmount.ToString("C")));
                totalAmount += thisAmount;
            }

            result.AppendLine(string.Format("Sub-Total: {0}", totalAmount.ToString("C")));
            var tax = totalAmount * TaxRate;
            result.AppendLine(string.Format("Tax: {0}", tax.ToString("C")));
            result.AppendLine(string.Format("Total: {0}", (totalAmount + tax).ToString("C")));
            result.Append(string.Format("Date: {0}", DateTime.Now.ToString("F")));

            Logger.Instance.LogInformation("Printing receipt (text version) - Finish");

            return result.ToString();
        }

        public string HtmlReceipt()
        {
            Logger.Instance.LogInformation("Printing receipt (HTML version) - Start");

            var totalAmount = 0d;
            var result = new StringBuilder(string.Format("<html><body><h1>Order Receipt for {0}</h1>", Company));
            if (_lines.Any())
            {
                result.Append("<ul>");
                for (var index = 0; index <= _lines.Count; index++)
                {
                    var line = _lines[index];
                    var thisAmount = 0d;
                    if (line.Policy.Price == Policy.Car)
                    {
                        if (line.Quantity >= 2)
                            thisAmount += line.Quantity * line.Policy.Price * .9d;
                        else
                            thisAmount += line.Quantity * line.Policy.Price;
                    }
                    else if (line.Policy.Price == Policy.Motorcycle)
                    {
                        if (line.Quantity >= 2)
                            thisAmount += line.Quantity * line.Policy.Price * .8d;
                        else
                            thisAmount += line.Quantity * line.Policy.Price;
                    }
                    else if (line.Policy.Price == Policy.Home)
                    {
                        if (line.Quantity >= 2)
                            thisAmount += line.Quantity * line.Policy.Price * .8d;
                        else
                            thisAmount += line.Quantity * line.Policy.Price;
                    }

                    result.Append(string.Format("<li>{0} x {1} {2} = {3}</li>", line.Quantity, line.Policy.PolicyHolderName, line.Policy.Description, thisAmount.ToString("C")));
                    totalAmount += thisAmount;
                }

                result.Append("</ul>");
            }
            result.Append(string.Format("<h3>Sub-Total: {0}</h3>", totalAmount.ToString("C")));
            var tax = totalAmount * TaxRate;
            result.Append(string.Format("<h3>Tax: {0}</h3>", tax.ToString("C")));
            result.Append(string.Format("<h2>Total: {0}</h2>", (totalAmount + tax).ToString("C")));
            result.Append(string.Format("<h3>Date: {0}</h3>", DateTime.Now.ToString("F")));
            result.Append("</body></html>");

            Logger.Instance.LogInformation("Printing receipt (HTML version) - Finish");

            return result.ToString();
        }

    }
}
