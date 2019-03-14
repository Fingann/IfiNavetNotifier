namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class IsClosed : IBusinessRule
    {
        public string RuleName { get; } = "Closed";
        public int Priority { get; } = 10;

        public bool CheckComplience(IfiEvent database, IfiEvent target)
        {
            return database.Open && !target.Open;
        }
    }
}