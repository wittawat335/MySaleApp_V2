using System;
using System.Collections.Generic;

namespace Sale.Domain.Model;

public partial class DocumentNumber
{
    public int DocumentNumberId { get; set; }

    public int LastNumber { get; set; }

    public DateTime? CreateDate { get; set; }
}
