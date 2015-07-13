using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using Cortex.Modules.Output.ViewModels;
using Gemini.Framework;
using Gemini.Framework.Results;
using NLog.Config;

namespace Cortex.Modules.Output
{
    [Export(typeof(IModule))]
    class Module : ModuleBase
    {
        [Import]
        private OutputViewModel _outputViewModel;

        public override IEnumerable<Type> DefaultTools
        {
            get
            {
                yield return typeof(OutputViewModel);
            }
        }

        public Module()
        {
            ConfigurationItemFactory.Default.Targets.RegisterDefinition("ShoutingTarget", typeof(ShoutingTarget));
            LogManager.GetLog = GetLog;
        }

        private static ILog GetLog(Type type)
        {
            return new NLogLogger(type);
        }

        public override void Initialize()
        {
            var log = LogManager.GetLog(typeof(Module));
            log.Info("Logger module initialized");
        }

        private IEnumerable<IResult> ShowLog()
        {
            yield return Show.Tool<OutputViewModel>();
        }
    }
}
