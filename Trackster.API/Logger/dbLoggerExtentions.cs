using Microsoft.Extensions.Logging;

namespace Trackster.API.Logger
{
    public static class dbLoggerExtensions
    {
        public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder, Action<dbLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, dbLoggerProvider>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
