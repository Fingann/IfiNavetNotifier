namespace IfiNavetNotifier.BusinessRules
{
    public interface IBusinessRule
    {
        string RuleName { get; }
        int Priority { get; }
        bool CheckCompliance(IfiEvent database, IfiEvent target);
    }
}