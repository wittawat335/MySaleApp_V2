using System;
using System.Collections.Generic;

namespace MySaleApp.Domain.Entities;

public partial class DocumentNumber
{
    public int DocumentNumberId { get; set; }

    public int LastNumber { get; set; }

    public DateTime? CreateDate { get; set; }
}
