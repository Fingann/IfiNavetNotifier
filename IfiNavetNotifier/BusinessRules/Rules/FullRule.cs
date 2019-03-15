using IfiNavetNotifier.Extentions;

namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class FullRule : IBusinessRule
    {
        public string RuleName { get; } = nameof(FullRule).RuleToRegular();
        public int Priority { get; } = 1;

        public bool CheckComplience(IfiEvent database, IfiEvent target)
        {
            return database.PlacesLeft > 0 && target.PlacesLeft == 0;
        }
    }
}