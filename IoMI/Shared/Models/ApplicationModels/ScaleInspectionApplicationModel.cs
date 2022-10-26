using IoMI.Shared.Models.InstrumentModels;

namespace IoMI.Shared.Models.ApplicationModels;

public class ScaleInspectionApplicationModel : BaseApplicationModel
{
    public ICollection<ScaleModel> Scales { get; set; } = new List<ScaleModel>();
}
