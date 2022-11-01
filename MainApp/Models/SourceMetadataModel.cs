namespace MetaMaui.Models
{
    public class SourceMetadataModel
    {
        public string Key { get; set; }
        public IList<string> Origins { get; init; } = new List<string>();
        public bool IsMapped { get; set; }
        public IList<string> TopValues { get; set; } = new List<string>();
    }

    internal record MetaForOrigin()
    {
        public string Origin { get; set; }
        public Dictionary<string, List<Object>> Values { get;set;}
    }
}
