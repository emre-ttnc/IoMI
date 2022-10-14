namespace IoMI.Shared.Models.ServerResponseModels;

public class ServerResponse<T>
{
    public T? Value { get; set; }
    public bool Success { get; set; } = false;
    public string? ErrorMessage { get; set; }
}