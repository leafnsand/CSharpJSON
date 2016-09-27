using System.Collections;
using System.Collections.Generic;

namespace HGSDK
{
    public class JSONArray : JSONData, IEnumerable
    {
        private readonly List<JSONData> _list = new List<JSONData>();

        public override JSONData this[int index]
        {
            get
            {
                if (index < 0 || index >= _list.Count)
                    return null;
                return _list[index];
            }
            set
            {
                if (index < 0 || index >= _list.Count)
                    _list.Add(value);
                else
                    _list[index] = value;
            }
        }

        public override int Count
        {
            get { return _list.Count; }
        }

        public override void Add(string key, JSONData item)
        {
            _list.Add(item);
        }

        public override JSONData Remove(int index)
        {
            if (index < 0 || index >= _list.Count)
                return null;
            var tmp = _list[index];
            _list.RemoveAt(index);
            return tmp;
        }

        public override JSONData Remove(JSONData data)
        {
            _list.Remove(data);
            return data;
        }

        public override IEnumerable<JSONData> Children
        {
            get
            {
                foreach (var data in _list)
                    yield return data;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public override string ToString()
        {
            var result = "[ ";
            foreach (var N in _list)
            {
                if (result.Length > 2)
                    result += ", ";
                result += N.ToString();
            }
            result += " ]";
            return result;
        }

        public override string ToString(string prefix)
        {
            var result = "[ ";
            foreach (var N in _list)
            {
                if (result.Length > 3)
                    result += ", ";
                result += "\n" + prefix + "   ";
                result += N.ToString(prefix + "   ");
            }
            result += "\n" + prefix + "]";
            return result;
        }
    }
}
