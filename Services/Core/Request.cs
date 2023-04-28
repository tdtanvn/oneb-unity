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
        public RequestVerb RequestVerb { get; set; } = RequestVerb.POST;
        public byte[] Body { get; set; }
    }
}
