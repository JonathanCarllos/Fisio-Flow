public class SessionDTO
{
    public int SessionId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public int Duration { get; set; }

    public bool Status { get; set; }


    public string? Notes { get; set; }

    public string? Evolution { get; set; }



    public int PatientId { get; set; }

    public string? PatientName { get; set; }



    public int PhysiotherapistId { get; set; }

    public string? PhysiotherapistName { get; set; }


    public string? PhysiotherapistColor { get; set; }
}