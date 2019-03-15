using IfiNavetNotifier.Extentions;

namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class FiveLeftRule : IBusinessRule
    {
        public string RuleName { get; } = nameof(FiveLeftRule).ToRuleName();
        public int Priority { get; } = 1;

        public bool CheckCompliance(IfiEvent database, IfiEvent target)
        {
            return database.PlacesLeft != 5 && target.PlacesLeft == 5;
        }
    }
}