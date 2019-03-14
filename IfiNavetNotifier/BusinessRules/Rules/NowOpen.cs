namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class NowOpen: IBusinessRule
    {
        public string RuleName { get; } = "Opened";
        public int Priority { get; } = 10;

        public bool CheckComplience(IfiEvent database, IfiEvent target)
        {
            return !database.Open && target.Open;
        }
    }
}