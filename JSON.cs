using System;
using System.Collections.Generic;
using System.Globalization;

namespace HGSDK
{
    public static class JSON
    {
        private static void Add(JSONData current, string key, string value, bool isString)
        {
            if (current == null) return;
            var item = JSONValue.Parse(value, isString);
            if (current is JSONArray)
            {
                if (value != "")
                {
                    current.Add(item);
                }
            }
            else if (key != "")
            {
                current.Add(key, item);
            }
        }

        public static JSONData Parse(string json)
        {
            var stack = new Stack<JSONData>();
            JSONData current = null;
            var i = 0;
            var value = "";
            var key = "";
            var quoteMode = false;
            var valueIsString = false;
            while (i < json.Length)
            {
                switch (json[i])
                {
                    case '{':
                        if (quoteMode)
                        {
                            value += json[i];
                            break;
                        }
                        stack.Push(new JSONObject());
                        if (current != null)
                        {
                            key = key.Trim();
                            if (current is JSONArray)
                                current.Add(stack.Peek());
                            else if (key != "")
                                current.Add(key, stack.Peek());
                        }
                        key = "";
                        value = "";
                        current = stack.Peek();
                        break;

                    case '[':
                        if (quoteMode)
                        {
                            value += json[i];
                            break;
                        }

                        stack.Push(new JSONArray());
                        if (current != null)
                        {
                            key = key.Trim();
                            if (current is JSONArray)
                            {
                                current.Add(stack.Peek());
                            }
                            else if (key != "")
                            {
                                current.Add(key, stack.Peek());
                            }
                        }
                        key = "";
                        value = "";
                        current = stack.Peek();
                        break;

                    case '}':
                    case ']':
                        if (quoteMode)
                        {
                            value += json[i];
                            break;
                        }
                        if (stack.Count == 0)
                            throw new Exception("JSON Parse: Too many closing brackets");

                        stack.Pop();
                        Add(current, key.Trim(), value, valueIsString);
                        valueIsString = false;
                        key = "";
                        value = "";
                        if (stack.Count > 0)
                        {
                            current = stack.Peek();
                        }
                        break;

                    case ':':
                        if (quoteMode)
                        {
                            value += json[i];
                            break;
                        }
                        key = value;
                        value = "";
                        valueIsString = false;
                        break;

                    case '"':
                        quoteMode ^= true;
                        valueIsString = quoteMode == true ? true : valueIsString;
                        break;

                    case ',':
                        if (quoteMode)
                        {
                            value += json[i];
                            break;
                        }
                        Add(current, key.Trim(), value, valueIsString);
                        valueIsString = false;
                        key = "";
                        value = "";
                        break;

                    case '\r':
                    case '\n':
                        break;

                    case ' ':
                    case '\t':
                        if (quoteMode)
                            value += json[i];
                        break;

                    case '\\':
                        ++i;
                        if (quoteMode)
                        {
                            var C = json[i];
                            switch (C)
                            {
                                case 't':
                                    value += '\t';
                                    break;
                                case 'r':
                                    value += '\r';
                                    break;
                                case 'n':
                                    value += '\n';
                                    break;
                                case 'b':
                                    value += '\b';
                                    break;
                                case 'f':
                                    value += '\f';
                                    break;
                                case 'u':
                                {
                                    var s = json.Substring(i + 1, 4);
                                    value += (char) int.Parse(s, NumberStyles.AllowHexSpecifier);
                                    i += 4;
                                    break;
                                }
                                default:
                                    value += C;
                                    break;
                            }
                        }
                        break;

                    default:
                        value += json[i];
                        break;
                }
                ++i;
            }
            if (quoteMode)
            {
                throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
            }
            return current;
        }
    }
}
