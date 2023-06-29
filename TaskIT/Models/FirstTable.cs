using System;
using System.Collections.Generic;

namespace TaskIT.Models;

public partial class FirstTable
{
    public int Id { get; set; }

    public DateTime? RecordDate { get; set; }

    public int? BranchId { get; set; }

    public int CropYear { get; set; }

    public int CounterpartyId { get; set; }

    public string CounterpartyName { get; set; } = null!;

    public int? ContactId { get; set; }

    public string Product { get; set; } = null!;

    public int Price { get; set; }

    public decimal Amount { get; set; }

    public string Process { get; set; } = null!;

    public decimal? Wetness { get; set; }

    public decimal? Garbage { get; set; }

    public string? Infection { get; set; }
}
