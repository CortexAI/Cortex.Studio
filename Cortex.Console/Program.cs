using System.Linq;
using Cortex.Model;
using Cortex.Model.Elements.Debug;
using Cortex.Model.Elements.Logic;
using Cortex.Model.Elements.Types;

namespace Cortex.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var process = new Process();
            var start = new StartPoint();
            var stringElement = new NetTypeElement<string>(){ Value = "Hello world!"};
            var logger = new DebugLog();

            process.AddElement(start);
            process.AddElement(stringElement);
            process.AddElement(logger);

            process.AddConnection(new Connection(start, start.Outputs.First(), logger, logger.Inputs.First()));
            process.AddConnection(new Connection(stringElement, stringElement.Outputs.First(), logger, logger.Inputs.Last()));

            using (var executor = new Executor(process))
            {
                executor.Start();
            }

            System.Console.ReadKey();
        }
    }
}
