using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HGSDK
{
    public class JSONObject : JSONData, IEnumerable
    {
        private readonly Dictionary<string, JSONData> _dict = new Dictionary<string, JSONData>();

        public override JSONData this[string key]
        {
            get { return _dict.ContainsKey(key) ? _dict[key] : null; }
            set
            {
                if (_dict.ContainsKey(key))
                    _dict[key] = value;
                else
                    _dict.Add(key, value);
            }
        }

        public override JSONData this[int index]
        {
            get
            {
                if (index < 0 || index >= _dict.Count)
                    return null;
                return _dict.ElementAt(index).Value;
            }
            set
            {
                if (index < 0 || index >= _dict.Count)
                    return;
                var key = _dict.ElementAt(index).Key;
                _dict[key] = value;
            }
        }

        public override int Count
        {
            get { return _dict.Count; }
        }

        public override void Add(string key, JSONData item)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (_dict.ContainsKey(key))
                    _dict[key] = item;
                else
                    _dict.Add(key, item);
            }
            else
                _dict.Add(Guid.NewGuid().ToString(), item);
        }

        public override JSONData Remove(string key)
        {
            if (!_dict.ContainsKey(key))
                return null;
            var tmp = _dict[key];
            _dict.Remove(key);
            return tmp;
        }

        public override JSONData Remove(int index)
        {
            if (index < 0 || index >= _dict.Count)
                return null;
            var item = _dict.ElementAt(index);
            _dict.Remove(item.Key);
            return item.Value;
        }

        public override JSONData Remove(JSONData data)
        {
            try
            {
                var item = _dict.First(pair => pair.Value == data);
                _dict.Remove(item.Key);
                return data;
            }
            catch
            {
                return null;
            }
        }

        public override IEnumerable<JSONData> Children
        {
            get {
                return _dict.Select(child => child.Value);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        public override string ToString()
        {
            var result = "{";
            foreach (var pair in _dict)
            {
                if (result.Length > 2)
                    result += ",";
                result += "\"" + Escape(pair.Key) + "\":" + pair.Value.ToString();
            }
            result += "}";
            return result;
        }

        public override string ToString(string prefix)
        {
            var result = "{";
            foreach (var pair in _dict)
            {
                if (result.Length > 3)
                    result += ", ";
                result += "\n" + prefix + "    ";
                result += "\"" + Escape(pair.Key) + "\": " + pair.Value.ToString(prefix + "    ");
            }
            result += "\n" + prefix + "}";
            return result;
        }
    }
}
