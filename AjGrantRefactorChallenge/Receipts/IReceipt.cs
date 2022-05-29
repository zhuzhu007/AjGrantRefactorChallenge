namespace AjGrantRefactorChallenge.Receipts
{
    public interface IReceipt
    {
         public decimal TaxRate {get;}
         string GenerateReceipt(DateTime generateTime);
    }
}