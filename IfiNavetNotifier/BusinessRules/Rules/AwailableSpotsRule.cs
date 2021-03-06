using IfiNavetNotifier.Extentions;

namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class AwailableSpotsRule : IBusinessRule
    {
        public string RuleName { get; } = nameof(AwailableSpotsRule).ToRuleName();
        public int Priority { get; } = 1;

        public bool CheckCompliance(IfiEvent database, IfiEvent target)
        {
            return database.PlacesLeft == 0 && target.PlacesLeft > 0;
        }
    }
}