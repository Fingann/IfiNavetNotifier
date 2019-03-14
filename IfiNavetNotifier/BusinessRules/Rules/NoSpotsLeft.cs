namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class NoSpotsLeft : IBusinessRule
    {
        public string RuleName { get; } = "No spots left";
        public int Priority { get; } = 1;

        public bool CheckComplience(IfiEvent database, IfiEvent target)
        {
            return database.PlacesLeft > 0 && target.PlacesLeft == 0;
        }
    }
}