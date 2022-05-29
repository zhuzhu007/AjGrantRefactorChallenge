using AjGrantRefactorChallenge.Line;

namespace AjGrantRefactorChallenge
{
    public interface IOrder
    {
        public string Company { get; set; }
        void AddLine(ILine line);
        IList<ILine> GetLines();
    }
}