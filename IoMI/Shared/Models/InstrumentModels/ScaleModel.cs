using IoMI.Shared.Models.InspectionModels;

namespace IoMI.Shared.Models.InstrumentModels;

public class ScaleModel : BaseInstrumentModel
{
    public string? MaximumCapacity { get; set; }
    public string? AccuracyClass { get; set; }

    public ICollection<ScaleInspectionModel> Inspections { get; set; } = new HashSet<ScaleInspectionModel>();
}