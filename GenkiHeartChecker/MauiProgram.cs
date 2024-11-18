using GenkiHeartChecker.Components;
using Microsoft.Extensions.Logging;
using SQLite;


namespace GenkiHeartChecker
{
    public static class MauiProgram
    {
        public const string DbFileName = @"GenkiHeartHistory.db";
        public const string VersionNumber = "0.0.1";
        public const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
        public static string DbPath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DbFileName);
            }
        }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            IServiceCollection services = builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<DBService>(s));
            
            return builder.Build();
        }
    }
}
