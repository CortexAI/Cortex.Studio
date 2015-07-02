using System;
using System.ComponentModel.Composition;

namespace Cortex.Model.Elements
{
    [Serializable]
    [Export(typeof(IElement))]
    public class StringElement : NetTypeElement<string> { }
}