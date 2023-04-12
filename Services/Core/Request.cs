namespace OneB
{
    public enum RequestVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    };
    public record Request
    {
        public string Query { get; set; }
        public string Param { get; set; }
        public string Service { get; set; }
        public RequestVerb RequestVerb { get; set; } = RequestVerb.POST;
        public byte[] Body { get; set; }
    }
}
