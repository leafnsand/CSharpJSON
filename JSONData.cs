using System.Collections.Generic;
using System.Linq;

namespace HGSDK
{
    public class JSONData
    {
        public virtual void Add(string key, JSONData item)
        {
        }

        public virtual JSONData this[int index]
        {
            get { return null; }
            set { }
        }

        public virtual JSONData this[string key]
        {
            get { return null; }
            set { }
        }

        public virtual int Count
        {
            get { return 0; }
        }

        public virtual void Add(JSONData item)
        {
            Add("", item);
        }

        public virtual JSONData Remove(string key)
        {
            return null;
        }

        public virtual JSONData Remove(int index)
        {
            return null;
        }

        public virtual JSONData Remove(JSONData data)
        {
            return data;
        }

        public virtual IEnumerable<JSONData> Children
        {
            get { yield break; }
        }

        public override string ToString()
        {
            return "JSONData";
        }

        public virtual string ToString(string prefix)
        {
            return "JSONData";
        }

        public virtual bool IsNull
        {
            get { return true; }
        }

        public virtual bool IsString
        {
            get { return false; }
        }

        public virtual string String
        {
            get { return null; }
            set { }
        }

        public virtual bool IsInt
        {
            get { return false; }
        }

        public virtual int Int
        {
            get { return 0; }
            set { }
        }

        public virtual bool IsFloat
        {
            get { return false; }
        }

        public virtual float Float
        {
            get { return 0.0f; }
            set { }
        }

        public virtual bool IsDouble
        {
            get { return false; }
        }

        public virtual double Double
        {
            get { return 0.0; }
            set { }
        }

        public virtual bool IsBoolean
        {
            get { return false; }
        }

        public virtual bool Boolean
        {
            get { return false; }
            set { }
        }

        public virtual JSONArray AsArray
        {
            get { return this as JSONArray; }
        }

        public virtual JSONObject AsObject
        {
            get { return this as JSONObject; }
        }

        internal static string Escape(string text)
        {
            var result = "";
            foreach (var c in text)
            {
                switch (c)
                {
                    case '\\':
                        result += "\\\\";
                        break;
                    case '\"':
                        result += "\\\"";
                        break;
                    case '\n':
                        result += "\\n";
                        break;
                    case '\r':
                        result += "\\r";
                        break;
                    case '\t':
                        result += "\\t";
                        break;
                    case '\b':
                        result += "\\b";
                        break;
                    case '\f':
                        result += "\\f";
                        break;
                    default:
                        result += c;
                        break;
                }
            }
            return result;
        }
    }
}
