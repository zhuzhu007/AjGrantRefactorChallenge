namespace AjGrantRefactorChallenge.Line
{
    public class CarLine : ILine
    {
        public CarLine(IPolicy policy, int quantity)
        {
            Policy = policy;
            Quantity = quantity;
        }

        public IPolicy Policy { get; set; }
        public int Quantity { get; set; }
        public decimal Amount {
            get {
                if (Quantity >= 2)
                    return Quantity * Policy.Price * .9m;
                else
                    return Quantity * Policy.Price;
            }
        }
    }
}