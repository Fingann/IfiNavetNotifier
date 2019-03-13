namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class AwailableSpots : IBusinessRule
    {
        public string RuleName { get; } = "Available spots";

        public bool CheckComplience(IfiEvent database, IfiEvent target)
        {
            return database.PlacesLeft == 0 && target.PlacesLeft > 0;
        }
    }
}