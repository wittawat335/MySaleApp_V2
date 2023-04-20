using System;
using System.Collections.Generic;

namespace Sale.Domain.Model;

public partial class Role
{
    public int RoleId { get; set; }

    public string? Name { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
