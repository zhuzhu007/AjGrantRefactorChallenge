using AjGrantRefactorChallenge.Line;

namespace AjGrantRefactorChallenge
{
    public class Order : IOrder
    {
        private const decimal TaxRate = .1m;
        private readonly IList<ILine> _lines;

        public Order(string company)
        {
            Company = company;
            _lines = new List<ILine>();
        }

        public string Company { get; set; }

        public void AddLine(ILine line)
        {
            _lines.Add(line);
        }

        public IList<ILine> GetLines(){
            return _lines;
        }
    }
}