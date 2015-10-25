using System;
using System.Threading;

namespace Cortex.Core.Model
{
    [Serializable]
    public class Flow
    {
        [NonSerialized] private readonly CancellationToken _token;

        public Flow(CancellationToken token)
        {
            _token = token;
        }

        public CancellationToken Token
        {
            get { return _token; }
        }
    }
}