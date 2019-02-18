using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace IXml
{
    public interface IXmlSerializable
    {
        void WriteXml(XmlWriter writer);

        void ReadXml(XmlReader reader);
    }
}
