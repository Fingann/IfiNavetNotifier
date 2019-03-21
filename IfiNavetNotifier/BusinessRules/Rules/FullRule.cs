using IfiNavetNotifier.Extentions;

namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class FullRule : IBusinessRule
    {
        public string RuleName { get; } = nameof(FullRule).ToRuleName();
        public int Priority { get; } = 99;

        public bool CheckCompliance(IfiEvent database, IfiEvent target)
        {
            return database.PlacesLeft > 0 && target.PlacesLeft == 0;
        }
    }
}