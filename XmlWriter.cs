using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace IXml
{
    public class XmlWriter
    {
        private XmlElement _current = default(XmlElement);

        public XmlWriter(XmlElement root)
        {
            _current = root;
        }

        public void WriteValue(string tag, int val)
        {
            WriteValue(tag, val.ToString());
        }

        public void WriteValue(string tag, string val)
        {
            if (val == null || tag == null || val.Length == 0 || tag.Length == 0)
                return;
            var value = _current.OwnerDocument.CreateElement(tag);
            value.InnerText = val;
            _current.AppendChild(value);
        }

        public void WriteValue(string tag, bool val)
        {
            WriteValue(tag, val.ToString());
        }

        public void WriteAttribute(string tag, string val)
        {
            if (val == null || tag == null || val.Length == 0 || tag.Length == 0)
                return;
            _current.SetAttribute(tag, val);
        }

        public void WriteAttribute(string tag, int val)
        {
            WriteAttribute(tag, val.ToString());
        }

        public void WriteAttribute(string tag, bool val)
        {
            WriteAttribute(tag, val.ToString());
        }

        public void WriteValue(string tag, IXmlSerializable val)
        {
            if (val == null)
                return;

            var value = _current.OwnerDocument.CreateElement(tag);
            _current.AppendChild(value);
            var current = _current;
            _current = value;

            val.WriteXml(this);

            _current = current;
        }

        public void WriteValue<T>(string tag, ICollection<T> collection, string itemTag = null)
            where T : class, IXmlSerializable
        {
            if (collection == null)
                return;

            itemTag = itemTag ?? typeof(T).Name;
            var value = _current.OwnerDocument.CreateElement(tag);
            _current.AppendChild(value);
            var current = _current;
            _current = value;

            foreach (var item in collection)
                WriteValue(itemTag, item);

            _current = current;
        }

        public void WriteValue(string tag, ICollection<int> collection, string itemTag = null)
        {
            if (collection == null)
                return;

            itemTag = itemTag ?? typeof(int).Name;
            var value = _current.OwnerDocument.CreateElement(tag);
            _current.AppendChild(value);
            var current = _current;
            _current = value;

            foreach (var item in collection)
                WriteValue(itemTag, item);

            _current = current;
        }

        public void WriteValue(string tag, ICollection<string> collection, string itemTag = null)
        {
            if (collection == null)
                return;

            itemTag = itemTag ?? typeof(int).Name;
            var value = _current.OwnerDocument.CreateElement(tag);
            _current.AppendChild(value);
            var current = _current;
            _current = value;

            foreach (var item in collection)
                WriteValue(itemTag, item);

            _current = current;
        }

        public void WriteValue(string tag, ICollection<bool> collection, string itemTag = null)
        {
            if (collection == null)
                return;

            itemTag = itemTag ?? typeof(bool).Name;
            var value = _current.OwnerDocument.CreateElement(tag);
            _current.AppendChild(value);
            var current = _current;
            _current = value;

            foreach (var item in collection)
                WriteValue(itemTag, item);

            _current = current;
        }
    }
}
