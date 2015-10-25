using System;
using System.IO;

namespace Cortex.Core.Model.Serialization
{
    public class PersisterWriter : IPersisterWriter, IDisposable
    {
        private readonly ReferenceManager _referenceManager;
        private readonly StreamWriter _writer;
        private int _section;

        public PersisterWriter(string fileName)
        {
            _writer = new StreamWriter(fileName);
            _section = 0;
            _referenceManager = new ReferenceManager();
        }

        public void Dispose()
        {
            _writer.Close();
            _writer.Dispose();
        }

        public void Set(string key, IPersistable value)
        {
            if (_referenceManager.Exist(value))
            {
                WriteVal(key, "ref", _referenceManager.GetId(value).ToString());
            }
            else
            {
                string typeStr = value.GetType().ToString();
                if (!value.GetType().Assembly.Equals(GetType().Assembly))
                    typeStr = value.GetType().AssemblyQualifiedName;

                int id = _referenceManager.Add(value);
                WriteVal(key, "object:" + id, typeStr);
                _section += 1;
                value.Save(this);
                _section -= 1;
            }
        }

        public void Set(string key, int value)
        {
            WriteVal(key, "int", value.ToString());
        }

        public void Set(string key, string value)
        {
            WriteVal(key, "string", value);
        }

        public void Set(string key, bool value)
        {
            WriteVal(key, "bool", value.ToString());
        }

        public void Set(string key, double value)
        {
            WriteVal(key, "double", value.ToString());
        }

        public void Set(string key, Type value)
        {
            WriteVal(key, "type", value.AssemblyQualifiedName);
        }

        public void Set(string key, object value)
        {
            if (value is IPersistable)
                Set(key, (IPersistable) value);
            else if (value is int)
                Set(key, (int) value);
            else if (value is string)
                Set(key, (string) value);
            else if (value is Type)
                Set(key, (Type) value);
            else if (value is double)
                Set(key, (double) value);
            else if (value is bool)
                Set(key, (bool) value);
            else
            {
                throw new ArgumentException("Value has unsopported type");
            }
        }

        private void Write(string val)
        {
            for (int i = 0; i < _section; i++)
                _writer.Write("\t");
            _writer.WriteLine(val);
        }

        private void WriteVal(string key, string type, string val)
        {
            Write(string.Format("{0}<{1}>: {2}", key, type, val));
        }
    }
}