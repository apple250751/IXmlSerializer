using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace IXml
{
    public class XmlReader
    {
        private XmlElement _current = default(XmlElement);

        public XmlReader(XmlElement root)
        {
            _current = root;
        }

        public string ReadValueAsString(string tag)
        {
            return _current[tag].InnerText;
        }

        public int ReadValueAsInt(string tag)
        {
            return int.Parse(ReadValueAsString(tag));
        }

        public bool ReadValueAsBool(string tag)
        {
            return bool.Parse(_current[tag].InnerText);
        }

        public string ReadAttributeAsString(string tag)
        {
            return _current.GetAttribute(tag);
        }

        public int ReadAttributeAsInt(string tag)
        {
            return int.Parse(ReadAttributeAsString(tag));
        }

        public bool ReadAttributeAsBool(string tag)
        {
            return bool.Parse(ReadAttributeAsString(tag));
        }

        public void ReadValue(string tag, IXmlSerializable obj)
        {
            var current = _current;
            _current = _current[tag];
            obj.ReadXml(this);
            _current = current;
        }

        public void ReadValue<T>(string tag, ICollection<T> collection)
            where T : class, IXmlSerializable, new()
        {
            if (collection == null)
                return;

            var current = _current;
            foreach (XmlElement element in _current[tag])
            {
                _current = element;
                var item = new T();
                item.ReadXml(this);
                collection.Add(item);
            }
            _current = current;
        }

        public void ReadValue(string tag, ICollection<int> collection)
        {
            if (collection == null)
                return;

            collection.Clear();
            foreach (XmlElement element in _current[tag])
                collection.Add(int.Parse(element.InnerText));

        }

        public void ReadValue(string tag, ICollection<string> collection)
        {
            if (collection == null)
                return;

            collection.Clear();

            foreach (XmlElement element in _current[tag])
                collection.Add(element.InnerText);

        }

        public void ReadValue(string tag, ICollection<bool> collection)
        {
            if (collection == null)
                return;

            collection.Clear();

            foreach (XmlElement element in _current[tag])
                collection.Add(bool.Parse(element.InnerText));

        }
    }
}
