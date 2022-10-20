using IoMI.Shared.Models.InstrumentModels;

namespace IoMI.Shared.Models.InspectionModels;

public class GasMeterInspectionModel : BaseInspectionModel
{
    public ICollection<GasMeterModel> GasMeter { get; set; } = new HashSet<GasMeterModel>();
}