using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cortex.Modules.ProcessDesigner.ViewModels;
using Gemini.Modules.Toolbox;

namespace Cortex.Modules.BasicElements
{
    public class Pin
    {
        public string Name { get; private set; }
        public Type Type { get; private set; }
        public object Value { get; set; }

        public Pin(string name, object value)
        {
            Name = name;
            Type = value.GetType();
            Value = value;
        }

        public Pin(string name, Type type, object value)
        {
            Name = name;
            Value = value;
            Type = type;
        }
    }
    
    public interface IElement
    {
        string Name { get; }
        Uri IconUri { get; }
        string Description { get; }
        IList<Pin> Inputs { get; }
        IList<Pin> Outputs { get; }
        void OnInitialize();
        void OnCall();
    }

    [ToolboxItem(typeof(GraphViewModel), "Add", "Maths", "pack://application:,,,/Modules/ProcessDesigner/Resources/active_x_16xLG.png")]
    public class AddElement : IElement
    {
        public string Name { get { return "Add"; } }
        public Uri IconUri { get { return new Uri("pack://application:,,,/Modules/ProcessDesigner/Resources/color_swatch.png"); } }
        public string Description { get { return "Simple addition element"; }}
        public IList<Pin> Inputs { get; private set; }
        public IList<Pin> Outputs { get; private set; }

        public AddElement()
        {
            Inputs = new List<Pin>
            {
                new Pin("In1", 0.0),
                new Pin("In2", 0.0)
            };

            Outputs = new List<Pin>
            {
                new Pin("Output", 0.0)
            };
        }

        public void OnInitialize()
        {
            
        }
        public void OnCall()
        {
            Outputs[0].Value = (double)Inputs[0].Value + (double)Inputs[0].Value;
        }
    }

    [ToolboxItem(typeof(GraphViewModel), "Debug Log", "Debug", "pack://application:,,,/Modules/ProcessDesigner/Resources/color_swatch.png")]
    public class DebugLog : IElement
    {
        public string Name { get { return "Debug Log"; } }
        public Uri IconUri { get { return new Uri("pack://application:,,,/Modules/ProcessDesigner/Resources/color_swatch.png"); } }
        public string Description { get { return "Logs to debug log"; } }
        public IList<Pin> Inputs { get; private set; }
        public IList<Pin> Outputs { get; private set; }

        public DebugLog()
        {
            Inputs = new List<Pin>
            {
                new Pin("Object", typeof (object), null)
            };
            Outputs = new List<Pin>();
        }

        public void OnInitialize()
        {
        }

        public void OnCall()
        {
            Debug.WriteLine(Inputs[0].Value);
        }
    }

    [ToolboxItem(typeof(GraphViewModel), "Start", "Common", "pack://application:,,,/Modules/ProcessDesigner/Resources/color_swatch.png")]
    public class StartPoint : IElement
    {
        public string Name { get { return "Start Point"; } }
        public Uri IconUri { get { return new Uri("pack://application:,,,/Modules/ProcessDesigner/Resources/color_swatch.png"); } }
        public string Description { get { return "Logs to debug log"; } }
        public IList<Pin> Inputs { get; private set; }
        public IList<Pin> Outputs { get; private set; }

        public StartPoint()
        {
            Inputs = new List<Pin>();
            Outputs = new List<Pin>()
            {
                new Pin("Started", typeof (Flow), null)
            };
        }

        public void OnInitialize()
        {
        }

        public void OnCall()
        {
            Debug.WriteLine(Inputs[0].Value);
        }
    }

    public class Flow
    {}
}
