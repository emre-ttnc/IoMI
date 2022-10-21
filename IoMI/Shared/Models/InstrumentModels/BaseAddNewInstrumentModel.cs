using System.ComponentModel.DataAnnotations;

namespace IoMI.Shared.Models.InstrumentModels;

public class BaseAddNewInstrumentModel
{

    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(16, ErrorMessage = "Brand can be up to 16 and at least 3 characters.", MinimumLength = 3)]
    public string? Brand { get; set; }

    [Required]
    [StringLength(16, ErrorMessage = "Type or model can be up to 16 and at least 2 characters.", MinimumLength = 2)]
    public string? TypeOrModel { get; set; }

    [Required]
    [RegularExpression("^[a-zA-Z0-9-]{3,16}$", ErrorMessage = "The serial number can only be alphanumeric and between 3-16 characters.")]
    public string? SerialNumber { get; set; }

    [Required]
    [RegularExpression("^[0-9]{4}$", ErrorMessage = "Please enter only four numbers.")]
    public string? LastInspectionYear { get; set; }

    public bool IsActive { get; set; }
}