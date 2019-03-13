namespace IfiNavetNotifier.BusinessRules
{
    public interface IBusinessRule
    {
        string RuleName { get; }
        bool CheckComplience(IfiEvent database, IfiEvent target);
    }
}