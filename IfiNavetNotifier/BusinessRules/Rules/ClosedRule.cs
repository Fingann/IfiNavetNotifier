using IfiNavetNotifier.Extentions;

namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class ClosedRule : IBusinessRule
    {
        public string RuleName { get; } = nameof(ClosedRule).RuleToRegular();
        public int Priority { get; } = 10;

        public bool CheckComplience(IfiEvent database, IfiEvent target)
        {
            return database.Open && !target.Open;
        }
    }
}