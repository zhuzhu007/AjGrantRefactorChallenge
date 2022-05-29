namespace AjGrantRefactorChallenge.Line
{
    public class MotorCycleLine : ILine
    {
        public MotorCycleLine(IPolicy policy, int quantity)
        {
            Policy = policy;
            Quantity = quantity;
        }

        public IPolicy Policy { get; set; }
        public int Quantity { get; set; }
        public decimal Amount {
            get {
                if (Quantity >= 2)
                    return Quantity * Policy.Price * .8m;
                else
                    return Quantity * Policy.Price;
            }
        }
    }
}