﻿using System;
using System.Collections.Generic;

namespace Cortex.Model
{
    public interface IElement
    {
        string Name { get; }
        Uri IconUri { get; }
        string Description { get; }
        string Category { get; }
        IList<InputPin> Inputs { get; }
        IList<OutputPin> Outputs { get; }
    }
}