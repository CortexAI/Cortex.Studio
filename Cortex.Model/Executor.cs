using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cortex.Model.Elements.Logic;

namespace Cortex.Model
{
    public class Executor : IDisposable
    {
        private readonly Process _process;
        private Thread _thread;

        public bool IsRunning { get { return _thread.IsAlive; } }

        public Executor(Process process)
        {
            _process = process;
        }

        public void Start()
        {
            var startPoint = _process.Elements.FirstOrDefault(e => e is StartPoint) as StartPoint;
            if (startPoint != null)
            {
                _thread = new Thread(startPoint.Run);
                _thread.Start();
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

        public void Dispose()
        {
            Stop();
        }
    }
}
