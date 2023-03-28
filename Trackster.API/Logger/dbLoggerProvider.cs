using Microsoft.Extensions.Options;

namespace Trackster.API.Logger
{
    [ProviderAlias("Database")]
    public class dbLoggerProvider : ILoggerProvider
    {
        public readonly dbLoggerOptions Options;

        public dbLoggerProvider(IOptions<dbLoggerOptions> _options)
        {
            Options = _options.Value;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new dbLogger(this);
        }

        public void Dispose()
        {
        }
    }
}
