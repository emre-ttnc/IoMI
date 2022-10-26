using IoMI.Shared.Models.InstrumentModels;

namespace IoMI.Shared.Models.ApplicationModels;

public class GasMeterInspectionApplicationModel : BaseApplicationModel
{
    public ICollection<GasMeterModel> GasMeters { get; set; } = new HashSet<GasMeterModel>();
}
