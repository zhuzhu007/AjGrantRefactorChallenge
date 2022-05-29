namespace AjGrantRefactorChallenge.Line
{
    public interface ILine
    {
        public IPolicy Policy { get; set; }
        public int Quantity { get; set; }
        public decimal Amount {get;}
    }
}