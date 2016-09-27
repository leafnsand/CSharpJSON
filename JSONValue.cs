using System;

namespace HGSDK
{
    public enum JSONValueType
    {
        String,
        Double,
        Float,
        Int,
        Boolean,
        Null
    }

    public class JSONValue : JSONData
    {
        private JSONValueType _type = JSONValueType.Null;

        private string _string;
        private double _double;
        private float _float;
        private int _int;
        private bool _boolean;

        internal static JSONValue Parse(string value, bool isString)
        {
            var ret = new JSONValue();
            if (isString)
            {
                ret._string = value;
                ret._type = JSONValueType.String;
            }
            else
            {
                if (double.TryParse(value, out ret._double)) ret._type = JSONValueType.Double;
                if (float.TryParse(value, out ret._float)) ret._type = JSONValueType.Float;
                if (int.TryParse(value, out ret._int)) ret._type = JSONValueType.Int;
                if (bool.TryParse(value, out ret._boolean)) ret._type = JSONValueType.Boolean;
            }
            return ret;
        }

        public JSONValue()
        {
            
        }

        public JSONValue(string value)
        {
            _type = JSONValueType.String;
            _string = value;
        }

        public JSONValue(float value)
        {
            _type = JSONValueType.Float;
            _float = value;
        }

        public JSONValue(double value)
        {
            _type = JSONValueType.Double;
            _double = value;
        }

        public JSONValue(int value)
        {
            _type = JSONValueType.Int;
            _int = value;
        }

        public JSONValue(bool value)
        {
            _type = JSONValueType.Boolean;
            _boolean = value;
        }

        public override string ToString()
        {
            switch (_type)
            {
                case JSONValueType.String:
                    return "\"" + Escape(_string) + "\"";
                case JSONValueType.Double:
                    return _double.ToString();
                case JSONValueType.Float:
                    return _float.ToString();
                case JSONValueType.Int:
                    return _int.ToString();
                case JSONValueType.Boolean:
                    return _boolean.ToString().ToLower();
                case JSONValueType.Null:
                    return "null";
                default:
                    return "JSONValue";
            }
        }

        public override string ToString(string prefix)
        {
            return ToString();
        }

        public override bool IsNull
        {
            get { return _type == JSONValueType.Null; }
        }

        public override bool IsString
        {
            get { return _type == JSONValueType.String; }
        }

        public override string String
        {
            get { return IsString ? _string : null; }
            set
            {
                _type = JSONValueType.String;
                _string = value;
            }
        }

        public override bool IsInt { get { return _type == JSONValueType.Int; } }

        public override int Int
        {
            get { return IsInt ? _int : 0; }
            set
            {
                _type = JSONValueType.Int;
                _int = value;
            }
        }

        public override bool IsFloat { get { return _type == JSONValueType.Float; } }

        public override float Float
        {
            get { return IsFloat ? _float : 0.0f; }
            set
            {
                _type = JSONValueType.Float;
                _float = value;
            }
        }

        public override bool IsDouble { get { return _type == JSONValueType.Double; } }

        public override double Double
        {
            get { return IsDouble ? _double : 0.0; }
            set
            {
                _type = JSONValueType.Double;
                _double = value;
            }
        }

        public override bool IsBoolean { get { return _type == JSONValueType.Boolean; } }

        public override bool Boolean
        {
            get { return IsBoolean ? _boolean : false; }
            set
            {
                _type = JSONValueType.Boolean;
                _boolean = value;
            }
        }
    }
}