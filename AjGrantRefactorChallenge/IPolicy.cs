namespace AjGrantRefactorChallenge
{
    public interface IPolicy
    {
        public string PolicyType { get; set; }
        public string PolicyHolderName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}