namespace Trackster.API.Logger
{
    public class dbLoggerOptions
    {
        public string ConnectionString { get; init; }
        public string[] LogFields { get; init; }
        public string LogTable { get; init; }

        public dbLoggerOptions()
        {
        }
    }
}
