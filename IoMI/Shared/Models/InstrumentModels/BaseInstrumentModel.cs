using IoMI.Shared.Models.UserModels;

namespace IoMI.Shared.Models.InstrumentModels;

public class BaseInstrumentModel
{
    public Guid Id { get; set; }
    public string? Brand { get; set; }
    public string? TypeOrModel { get; set; }
    public string? SerialNumber { get; set; }
    public int LastInspectionYear { get; set; }
    public bool IsActive { get; set; }

    public UserModel? UserOfInstrument { get; set; }
}