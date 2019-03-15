using IfiNavetNotifier.Extentions;

namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class ClosedRule : IBusinessRule
    {
        public string RuleName { get; } = nameof(ClosedRule).ToRuleName();
        public int Priority { get; } = 10;

        public bool CheckCompliance(IfiEvent database, IfiEvent target)
        {
            return database.Open && !target.Open;
        }
    }
}