namespace IfiNavetNotifier.BusinessRules
{
    public interface IBusinessRule
    {
        string RuleName { get; }
        int Priority { get; }
        bool CheckComplience(IfiEvent database, IfiEvent target);
    }
}