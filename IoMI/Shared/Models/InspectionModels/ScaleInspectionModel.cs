using IoMI.Shared.Models.InstrumentModels;

namespace IoMI.Shared.Models.InspectionModels;

public class ScaleInspectionModel : BaseInspectionModel
{
    public ICollection<ScaleModel> Scales { get; set; } = new HashSet<ScaleModel>();
}