using IfiNavetNotifier.Extentions;

namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class TimeChangedRule: IBusinessRule
    {
        public string RuleName { get; } = nameof(TimeChangedRule).ToRuleName();
        public int Priority { get; } = 5;

        public bool CheckCompliance(IfiEvent database, IfiEvent target)
        {
            return database.Date.TimeOfDay != target.Date.TimeOfDay;
        }
    }
}