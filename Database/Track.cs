namespace Database
{
    public class Track
    {
        public string SourceId { get; set; }
        public StreamingSource Source { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
    }
}