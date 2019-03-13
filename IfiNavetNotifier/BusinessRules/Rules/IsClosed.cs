namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class IsClosed : IBusinessRule
    {
        public string RuleName { get; } = "Closed";

        public bool CheckComplience(IfiEvent database, IfiEvent target)
        {
            return database.Open && !target.Open;
        }
    }
}