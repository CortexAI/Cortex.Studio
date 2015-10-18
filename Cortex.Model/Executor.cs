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
                _thread = new Thread(startPoint.Run) {Priority = ThreadPriority.AboveNormal, IsBackground = true };
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
    }
}