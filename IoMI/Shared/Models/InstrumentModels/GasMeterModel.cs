using IoMI.Shared.Models.InspectionModels;

namespace IoMI.Shared.Models.InstrumentModels;

public class GasMeterModel : BaseInstrumentModel
{
    public string? MaxFlowRate { get; set; }

    public ICollection<GasMeterInspectionModel> Inspections { get; set; } = new HashSet<GasMeterInspectionModel>();
}