﻿using BlazorApp1.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Codes;

public class CprSubmitHandler
{
    public bool SubmitCpr(ToDoContext _toDoContext, Cpr Value, NavigationManager nm)
    {
        if (Value.CprNr == null) return true;
        string? cprNr = getCprNumber(_toDoContext, Value.User);
        if (cprNr == null)
        {
            Value.CprNr = HashingHandler.BCryptHashing(Value.CprNr);
            _toDoContext.Cprs.Add(Value);
            _toDoContext.SaveChanges();
            nm.NavigateTo("ToDo");
            return false;
        }
        if (HashingHandler.BCryptVerifyHashing(Value.CprNr, cprNr))
        {
            nm.NavigateTo("ToDo");
            return false;
        }
        return true;
    }

    public string? getCprNumber(ToDoContext _toDoContext, string name)
    {
        Cpr? cprContext = _toDoContext.Cprs.FirstOrDefault(x => x.User == name);
        if (cprContext != null) return cprContext.CprNr;
        return null;   
    }
}
