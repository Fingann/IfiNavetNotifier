using IfiNavetNotifier.Extentions;

namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class OpenedRule: IBusinessRule
    {
        public string RuleName { get; } = nameof(OpenedRule).ToRuleName();
        public int Priority { get; } = 100;

        public bool CheckCompliance(IfiEvent database, IfiEvent target)
        {
            return !database.Open && target.Open;
        }
    }
}