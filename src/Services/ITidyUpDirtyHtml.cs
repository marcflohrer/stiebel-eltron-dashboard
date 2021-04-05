namespace StiebelEltronApiServer.Services
{
    public interface ITidyUpDirtyHtml
    {
        public string GetTidyHtml(string dirtyHtml);
    }
}