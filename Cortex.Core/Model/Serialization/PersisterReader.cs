using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cortex.Core.Model.Serialization
{
    public class PersisterReader : IPersisterReader, IDisposable
    {
        private readonly Dictionary<int, IPersistable> _cache = new Dictionary<int, IPersistable>();
        private PersistedRecord _context;

        public PersisterReader(string fileName)
        {
            int lastSection = -1;
            var regex = new Regex("^(\t*)(.+)<(.+)>: (.*)$");
            var stack = new Stack<PersistedRecord>();
            stack.Push(new PersistedRecord(null, null, null));
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Match match = regex.Match(line);
                    int section = match.Groups[1].Length;

                    var obj = new PersistedRecord(match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value);

                    if (section < lastSection)
                    {
                        for (int i = 0; i <= (lastSection - section); i++)
                            stack.Pop();
                    }

                    if (section == lastSection)
                    {
                        stack.Pop();
                    }

                    stack.Peek().Child.Add(obj);
                    stack.Push(obj);

                    lastSection = section;
                }
            }

            _context = stack.Last();
        }

        public void Dispose()
        {
        }

        public T Get<T>(string key)
        {
            if (typeof (IPersistable).IsAssignableFrom(typeof (T)))
                return (T) ((IReadPersist<IPersistable>) this).Get(key);
            return ((IReadPersist<T>) this).Get(key);
        }

        public object GetUnknown(string key)
        {
            PersistedRecord obj = GetObject(key);
            switch (obj.Type)
            {
                case "int":
                    return Get<int>(key);
                case "string":
                    return Get<string>(key);
                case "bool":
                    return Get<bool>(key);
                case "double":
                    return Get<double>(key);
                case "type":
                    return Get<Type>(key);
                default:
                    return Get<IPersistable>(key);
            }
        }

        IPersistable IReadPersist<IPersistable>.Get(string key)
        {
            PersistedRecord oldContext = _context;
            PersistedRecord obj = GetObject(key);
            if (obj.Type.Contains("ref"))
            {
                _context = oldContext;
                int id = Convert.ToInt32(obj.Value);
                if (_cache.ContainsKey(id))
                    return _cache[id];
                throw new Exception("Loop reference");
            }

            Type eType = Type.GetType(obj.Value);
            _context = obj;
            var res = Activator.CreateInstance(eType) as IPersistable;
            res.Load(this);
            _cache.Add(Convert.ToInt32(obj.Type.Split(':')[1]), res);
            _context = oldContext;
            return res;
        }

        object IReadPersist<object>.Get(string key)
        {
            return GetUnknown(key);
        }

        int IReadPersist<int>.Get(string key)
        {
            string raw = GetObject(key).Value;
            int res;
            Int32.TryParse(raw, out res);
            return res;
        }

        string IReadPersist<string>.Get(string key)
        {
            return GetObject(key).Value;
        }

        bool IReadPersist<bool>.Get(string key)
        {
            string raw = GetObject(key).Value;
            bool res;
            Boolean.TryParse(raw, out res);
            return res;
        }

        double IReadPersist<double>.Get(string key)
        {
            string raw = GetObject(key).Value;
            double res;
            Double.TryParse(raw, out res);
            return res;
        }

        Type IReadPersist<Type>.Get(string key)
        {
            string raw = GetObject(key).Value;
            return Type.GetType(raw);
        }

        private PersistedRecord GetObject(string key)
        {
            return _context.Child.FirstOrDefault(o => o.Key.Equals(key));
        }

        private class PersistedRecord
        {
            public readonly List<PersistedRecord> Child = new List<PersistedRecord>();

            public PersistedRecord(string key, string type, string value)
            {
                Key = key;
                Type = type;
                Value = value;
            }

            public string Key { get; private set; }
            public string Type { get; private set; }
            public string Value { get; private set; }

            public override string ToString()
            {
                return string.Format("{0}<{1}>: {2}", Key, Type, Value);
            }
        }
    }
}