namespace AjGrantRefactorChallenge.Constants
{
    public static class ReceiptTemplateConstants
    {
        public static string CONSOLE_LINE_TEMPLATE = $"\t{{0}} x {{1}} {{2}} = {{3}}{Environment.NewLine}";
        public static readonly string HTML_LINE_TEMPLATE = $"<li>{{0}} x {{1}} {{2}} = {{3}}</li>";
        public static readonly string CONSOLE_RECEIPT_TEMPLATE = $"Order Receipt for {{COMPANY_NAME}}{Environment.NewLine}{{LINES}}Sub-Total: {{SUBTOTAL}}{Environment.NewLine}Tax: {{TAX}}{Environment.NewLine}Total: {{TOTAL}}{Environment.NewLine}Date: {{DATETIME}}";
        public static readonly string HTML_RECEIPT_TEMPLATE = $"<html><body><h1>Order Receipt for {{COMPANY_NAME}}</h1><ul>{{LINES}}</ul><h3>Sub-Total: {{SUBTOTAL}}</h3><h3>Tax: {{TAX}}</h3><h2>Total: {{TOTAL}}</h2><h3>Date: {{DATETIME}}</h3></body></html>";
    }
}