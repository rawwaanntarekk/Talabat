namespace LinkDev.Talabat.APIs.Controllers.Errors
{
    internal class ModelError
    {
        public required string Field { get; set; }
        public required IEnumerable<string> Errors { get; set; }
    }
}
