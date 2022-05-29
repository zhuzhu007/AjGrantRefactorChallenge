namespace AjGrantRefactorChallenge.Receipts
{
    public class TextReceiptTemplate : IReceiptTemplate
    {
        public string TemplateType => "text";
        public string LineTemplate => $"\t{{0}} x {{1}} {{2}} = {{3}}{Environment.NewLine}";
        public string ReceiptTemplate => $"Order Receipt for {{COMPANY_NAME}}{Environment.NewLine}{{LINES}}Sub-Total: {{SUBTOTAL}}{Environment.NewLine}Tax: {{TAX}}{Environment.NewLine}Total: {{TOTAL}}{Environment.NewLine}Date: {{DATETIME}}";
    }
}