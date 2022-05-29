namespace AjGrantRefactorChallenge
{
    public class Line
    {
        public Line(Policy policy, int quantity)
        {
            Policy = policy;
            Quantity = quantity;
        }

        public Policy Policy { get; set; }
        public int Quantity { get; set; }
    }
}