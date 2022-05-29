using AjGrantRefactorChallenge.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public string Receipt(DateTime orderTime)
        {
            Logger.Instance.LogInformation("Printing receipt (text version) - Start");

            var totalAmount = 0d;
            var result = new StringBuilder(ReceiptTemplateConstants.CONSOLE_RECEIPT_TEMPLATE);
            var lines = new StringBuilder();
            for (var index = 0; index < _lines.Count; index++)
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

                lines.Append(string.Format(ReceiptTemplateConstants.CONSOLE_LINE_TEMPLATE, line.Quantity, line.Policy.PolicyHolderName, line.Policy.Description, thisAmount.ToString("C")));
                totalAmount += thisAmount;
            }

            var tax = totalAmount * TaxRate;

            result = result.Replace("{COMPANY_NAME}", Company)
            .Replace("{LINES}", lines.ToString())
            .Replace("{SUBTOTAL}", totalAmount.ToString("C"))
            .Replace("{TAX}", tax.ToString("C"))
            .Replace("{TOTAL}", (totalAmount + tax).ToString("C"))
            .Replace("{DATETIME}", orderTime.ToString("F"));
            Logger.Instance.LogInformation("Printing receipt (text version) - Finish");

            return result.ToString();
        }

        public string HtmlReceipt(DateTime orderTime)
        {
            Logger.Instance.LogInformation("Printing receipt (HTML version) - Start");

            var totalAmount = 0d;
            var result = new StringBuilder(ReceiptTemplateConstants.HTML_RECEIPT_TEMPLATE);
            var lines = new StringBuilder();
            if (_lines.Any())
            {
                for (var index = 0; index < _lines.Count; index++)
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

                    lines.Append(string.Format(ReceiptTemplateConstants.HTML_LINE_TEMPLATE, line.Quantity, line.Policy.PolicyHolderName, line.Policy.Description, thisAmount.ToString("C")));
                    totalAmount += thisAmount;
                }
            }
            var tax = totalAmount * TaxRate;
            result = result.Replace("{COMPANY_NAME}", Company)
            .Replace("{LINES}", lines.ToString())
            .Replace("{SUBTOTAL}", totalAmount.ToString("C"))
            .Replace("{TAX}", tax.ToString("C"))
            .Replace("{TOTAL}", (totalAmount + tax).ToString("C"))
            .Replace("{DATETIME}", orderTime.ToString("F"));
            Logger.Instance.LogInformation("Printing receipt (HTML version) - Finish");

            return result.ToString();
        }

    }
}
