using IfiNavetNotifier.Extentions;

namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class DateChangedRule: IBusinessRule
    {
        public string RuleName { get; } = nameof(DateChangedRule).ToRuleName();
        public int Priority { get; } = 10;

        public bool CheckCompliance(IfiEvent database, IfiEvent target)
        {
            return database.Date.DayOfYear != target.Date.DayOfYear;
        }
    }
}