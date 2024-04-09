using System;
using System.Collections.Generic;

namespace BlazorApp1.Models;

public partial class ToDoList
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Item { get; set; } = null!;

    public virtual Cpr User { get; set; } = null!;
}
