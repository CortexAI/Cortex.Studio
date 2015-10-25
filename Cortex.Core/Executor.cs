using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cortex.Core.Elements.Logic;
using Cortex.Core.Model;

namespace Cortex.Core
{
    public class Executor : IDisposable
    {
        private readonly Process _process;
        private Thread _thread;

        public Executor(Process process)
        {
            _process = process;
        }

        public bool IsRunning
        {
            get
            {
                if (_thread == null)
                    return false;
                return _thread.IsAlive;
            }
        }

        public void Dispose()
        {
            Stop();
        }

        public void Start()
        {
            var startPoint = _process.Elements.FirstOrDefault(e => e is StartPoint) as StartPoint;
            if (startPoint != null)
            {
                _thread = new Thread(Run) {Priority = ThreadPriority.AboveNormal, IsBackground = true };
                _thread.Start(startPoint);
            }
            else
            {
                throw new InvalidOperationException("No startpoint element");
            }
        }

        public void Stop()
        {
            if (!IsRunning) return;

            _thread.Interrupt();
            Task.Delay(2000).ContinueWith(delegate
            {
                if (IsRunning)
                {
                    _thread.Abort();
                    _thread = null;
                }
            });
        }

        private static void Run(object arg)
        {
            var startPoint = arg as StartPoint;
            if(startPoint == null)
                throw new InvalidOperationException("Invalid startpoint");
            var cts = new CancellationTokenSource();
            try
            {
                var flowObject = new Flow(cts.Token);
                startPoint.Run(flowObject);
            }
            catch (ThreadInterruptedException)
            {
                cts.Cancel();
            }
        }
    }
}