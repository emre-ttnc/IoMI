using System.ComponentModel.DataAnnotations;

namespace IoMI.Shared.Models.InstrumentModels;

public class AddNewGasMeterModel : BaseAddNewInstrumentModel
{
    [Required]
    [RegularExpression("^[a-zA-Z0-9,.\\/\\s]{3,16}$", ErrorMessage = "Maximum flow rate can only be alphanumeric and between 3-16 characters.")]
    public string? MaxFlowRate { get; set; }
}