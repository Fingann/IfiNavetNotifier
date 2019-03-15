using IfiNavetNotifier.Extentions;

namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class OpenedRule: IBusinessRule
    {
        public string RuleName { get; } = nameof(OpenedRule).RuleToRegular();
        public int Priority { get; } = 10;

        public bool CheckComplience(IfiEvent database, IfiEvent target)
        {
            return !database.Open && target.Open;
        }
    }
}