namespace SmartTechnologiesM.Activation
{
    public interface IHardwareInfoProvider
    {
        string ProcessorId { get; }
        string MotherboardSerial { get; }
        string MemorySerial { get; }
        string DriveSerial { get; }
    }
}
