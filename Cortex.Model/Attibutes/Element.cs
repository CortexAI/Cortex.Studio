using System;
using System.Linq;

namespace Cortex.Model.Attibutes
{
    [AttributeUsage(AttributeTargets.Class)]
    class ElementAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Category { get; private set; }
        public string Description { get; private set; }
        public Uri IconUri { get; private set; }

        public ElementAttribute(string name, string category, string description, string uriString)
        {
            Name = name;
            Category = category;
            Description = description;

            if(!string.IsNullOrEmpty(uriString))
                IconUri = new Uri(uriString);
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    class FlowInputAttribute : Attribute
    {
        public string Name { get; private set; }

        public FlowInputAttribute(string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    class InputAttribute : Attribute
    {
        public string Name { get; private set; }

        public InputAttribute(string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    class OutputAttribute : Attribute
    {
        public string Name { get; private set; }

        public OutputAttribute(string name)
        {
            Name = name;
        }
    }

    [Serializable]
    [Element("Test element", "Common", "Just a test element", null)]
    class TestElement
    {
        [Input("Input 1")]
        public double Input1 { get; set; }
        
        [FlowInput("Event 1")]
        void OnInput1()
        {
            _output = Input1;
            FlowOut.Call();
        }

        [FlowInput("Event 2")]
        void OnInput2()
        {
            _output = Input1 * Input1;
            FlowOut.Call();
        }

        [Output("Output")] 
        private double _output;

        [Output("Output")]
        public Flow FlowOut { get; set; }
    }
   
}
