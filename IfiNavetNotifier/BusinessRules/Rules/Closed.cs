using IfiNavetNotifier.Extentions;

namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class Closed : IBusinessRule
    {
        public string RuleName { get; } = nameof(Closed).TitleCaseToRegular();
        public int Priority { get; } = 10;

        public bool CheckComplience(IfiEvent database, IfiEvent target)
        {
            return database.Open && !target.Open;
        }
    }
}