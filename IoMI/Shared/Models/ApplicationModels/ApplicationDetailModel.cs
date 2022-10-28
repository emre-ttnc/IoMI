namespace IoMI.Shared.Models.ApplicationModels;

public class ApplicationDetailModel<T>
{
    public string ApplicantFullName { get; set; } = string.Empty;
    public DateTime ApplicationDate { get; set; }
    public bool IsAccepted { get; set; }
    public bool IsCompleted { get; set; }
    public List<T> Instruments { get; set; } = new();
}
