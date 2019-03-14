using IfiNavetNotifier.Extentions;

namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class Opened: IBusinessRule
    {
        public string RuleName { get; } = nameof(AwailableSpots).TitleCaseToRegular();
        public int Priority { get; } = 10;

        public bool CheckComplience(IfiEvent database, IfiEvent target)
        {
            return !database.Open && target.Open;
        }
    }
}