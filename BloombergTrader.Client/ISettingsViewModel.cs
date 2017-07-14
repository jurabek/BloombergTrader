namespace BloombergTrader.Client
{
    public interface ISettingsViewModel
    {
        string ServerHost { get; set; }
        int ServerPort { get; set; }
    }
}