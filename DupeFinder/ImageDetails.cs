namespace DupeFinder
{
    public class ImageDetails
    {
        public ulong Hash { get; set; }
        public long Size { get; set; }
        public string Path { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Hash},{Size},{Path}";
        }
    }
}
