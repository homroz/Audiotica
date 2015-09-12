﻿using Audiotica.Windows.AppEngine;
using Audiotica.Windows.AppEngine.Bootstrppers;
using Audiotica.Windows.AppEngine.Modules;
using Autofac;

namespace Audiotica.Windows.Factories
{
    internal static class AppKernelFactory
    {
        public static Module[] GetModules() =>
            new Module[]
            {
                new UtilityModule(),
                new MatchEngineValidatorModule(),
                new MatchEngineProviderModule(),
                new MetadataProviderModule(),
                new ConverterModule(),
                new ServiceModule(),
                new ViewModelModule()
            };

        public static IBootStrapper[] GetBootStrappers() =>
            new IBootStrapper[]
            {
                new BackgroundAudioBootstrapper(),
                new LibraryBootstrapper(),
                new DownloadServiceBootstrapper()
            };

        public static AppKernel Create() => new AppKernel(GetBootStrappers(), GetModules());
    }
}