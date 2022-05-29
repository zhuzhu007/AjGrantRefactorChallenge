namespace AjGrantRefactorChallenge.Receipts
{
    public interface IReceiptTemplate
    {
        public string TemplateType {get;}
        public string LineTemplate {get;}
        public string ReceiptTemplate {get;}
    }
}