﻿using System;
using System.ComponentModel.Composition;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements.Logic
{
    [Serializable]
    public class IfElement : IElement
    {
        public string Name { get { return "If"; } }
        public string Category { get { return "Common"; } }
        public Uri IconUri { get { return null; } }
        public string Description { get { return "If statement"; } }
        public IInputPin[] Inputs { get; private set; }
        public IOutputPin[] Outputs { get; private set; }

        public IfElement()
        {
            Inputs = new IInputPin[]
            {
                new FlowInputPin(OnCall),
                new InputPin("Condition", typeof (bool), false),
            };

            Outputs = new IOutputPin[]
            {
                new FlowOutputPin("True"),
                new FlowOutputPin("False"),
            };
        }

        public void OnCall()
        {
            var flow = Inputs[0].Value as Flow;
            if((bool)Inputs[1].Value)
                ((FlowOutputPin)Outputs[0]).Call(flow);
            else
                ((FlowOutputPin)Outputs[1]).Call(flow);
        }
    }
}