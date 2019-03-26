using System;
using IfiNavetNotifier.Extentions;
using IfiNavetNotifier.Logger;

namespace IfiNavetNotifier.BusinessRules.Rules
{
    public class LogChangeRule: IBusinessRule
    {
    public string RuleName { get; } = nameof(FullRule).ToRuleName();
    public int Priority { get; } = 99;
    public ConsoleLogger ConsoleLogger { get; set; } = new ConsoleLogger();

    public bool CheckCompliance(IfiEvent database, IfiEvent target)
    {
        if (database.PlacesLeft != target.PlacesLeft)
        {
           ConsoleLogger.Informtion(database.Name +" - db: "+ database.PlacesLeft + ", new: "+target.PlacesLeft);
        }

        return false;
    }
    }
}
    
