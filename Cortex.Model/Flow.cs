using System;

namespace Cortex.Model
{
    [Serializable]
    public class Flow
    {
        public void Raise()
        {
        }

        public void Call()
        {
            throw new NotImplementedException();
        }
    }
}