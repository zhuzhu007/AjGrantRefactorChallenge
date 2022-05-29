using System.Text;

namespace AjGrantRefactorChallenge.Receipts
{
    public class Receipt : IReceipt
    {
        public decimal TaxRate => .1m;
        private readonly IOrder _order;
        private readonly IReceiptTemplate _template;

        public Receipt(IOrder order, IReceiptTemplate template)
        {
            _order = order;
            _template = template;
        }
        public string GenerateReceipt(DateTime generateTime)
        {
            Logger.Instance.LogInformation($"Printing receipt ({_template.TemplateType} version) - Start");

            var totalAmount = 0m;
            var result = new StringBuilder(_template.ReceiptTemplate);
            var lines = new StringBuilder();
            var orderLines = _order.GetLines();

            for (var index = 0; index < orderLines.Count; index++)
            {
                var line = orderLines[index];
                var thisAmount = line.Amount;
                lines.Append(string.Format(_template.LineTemplate, line.Quantity, line.Policy.PolicyHolderName, line.Policy.Description, thisAmount.ToString("C")));
                totalAmount += thisAmount;
            }
            var tax = totalAmount * TaxRate;

            result = result.Replace("{COMPANY_NAME}", _order.Company)
            .Replace("{LINES}", lines.ToString())
            .Replace("{SUBTOTAL}", totalAmount.ToString("C"))
            .Replace("{TAX}", tax.ToString("C"))
            .Replace("{TOTAL}", (totalAmount + tax).ToString("C"))
            .Replace("{DATETIME}", generateTime.ToString("F"));

            Logger.Instance.LogInformation($"Printing receipt ({_template.TemplateType} version) - Finish");

            return result.ToString();
        }
    }
}