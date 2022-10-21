using System.ComponentModel.DataAnnotations;

namespace IoMI.Shared.Models.InstrumentModels;

public class AddNewScaleModel : BaseAddNewInstrumentModel
{
    [Required]
    [RegularExpression("^[1-5]{1}$", ErrorMessage = "Accuracy class can only be betwen 1-5.")]
    public string? AccuracyClass { get; set; }

    [Required]
    [RegularExpression("^[a-zA-Z0-9,.]{3,16}$", ErrorMessage = "Maximum capacity can only be alphanumeric and between 3-16 characters.")]
    public string? MaximumCapacity { get; set; }
}