namespace AjGrantRefactorChallenge.Receipts
{
    public class HtmlReceiptTemplate : IReceiptTemplate
    {
        public string TemplateType => "HTML";
        public string LineTemplate => $"<li>{{0}} x {{1}} {{2}} = {{3}}</li>";
        public string ReceiptTemplate => $"<html><body><h1>Order Receipt for {{COMPANY_NAME}}</h1><ul>{{LINES}}</ul><h3>Sub-Total: {{SUBTOTAL}}</h3><h3>Tax: {{TAX}}</h3><h2>Total: {{TOTAL}}</h2><h3>Date: {{DATETIME}}</h3></body></html>";
    }
}