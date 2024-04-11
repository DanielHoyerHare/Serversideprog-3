using BlazorApp1.Models;

namespace BlazorApp1.Codes;

public class CprSubmitHandler
{
    HashingHandler _hashHandler;
    public bool SubmitCpr(ToDoContext _toDoContext, Cpr Value)
    {
        String cprNr;
        Cpr cprContext = _toDoContext.Cprs.FirstOrDefault(x => x.User == Value.User);
        if (cprContext == null) return true;
        cprNr = cprContext.CprNr;
        if (cprNr == null)
        {
            Value.CprNr = _hashHandler.BCryptHashing(Value.CprNr);
            _toDoContext.Cprs.Add(Value);
            _toDoContext.SaveChanges();
            return false;
        }
        if (_hashHandler.BCryptVerifyHashing(Value.CprNr, cprNr))
        {
            return false;
        }
        return true;
    }
}
