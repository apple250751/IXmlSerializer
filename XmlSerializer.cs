using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace IXml
{
    public class XmlSerializer
    {
        /// <summary>
        /// 序列化
        /// </summary>
        public void Serialize(string path, string tag, IXmlSerializable data)
        {
            var document = new XmlDocument();
            var root = document.CreateElement(tag);
            document.AppendChild(root);
            var writer = new XmlWriter(root);
            data.WriteXml(writer);

            var split = path.Split('/');
            var rootsize = path.Length - split.Last().Length;
            var rootpath = path.Substring(0, rootsize);
            if (rootpath.Length != 0 && !System.IO.File.Exists(rootpath))
                System.IO.Directory.CreateDirectory(rootpath);

            document.Save(path);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        public void Serialize<T>(string path, string tag, ICollection<T> collection)
            where T : class, IXmlSerializable
        {
            var document = new XmlDocument();
            var root = document.CreateElement(tag);
            var writer = new XmlWriter(root);
            //document.AppendChild(root);
            writer.WriteValue(tag, collection);
            //foreach (var item in collection)
            //    writer.WriteValue(item.GetType().Name, item);

            document.AppendChild(root.FirstChild);

            var split = path.Split('/');
            var rootsize = path.Length - split.Last().Length;
            var rootpath = path.Substring(0, rootsize);
            if (rootpath.Length != 0 && !System.IO.File.Exists(rootpath))
                System.IO.Directory.CreateDirectory(rootpath);

            document.Save(path);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public void Deserialize(string path, string tag, IXmlSerializable data)
        {
            if (!System.IO.File.Exists(path))
                return;

            var document = new XmlDocument();
            document.Load(path);
            var reader = new XmlReader(document.DocumentElement);
            data.ReadXml(reader);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public void Deserialize<T>(string path, string tag, ICollection<T> collection)
            where T : class, IXmlSerializable, new()
        {
            if (!System.IO.File.Exists(path))
                return;

            var document = new XmlDocument();
            document.Load(path);
            var root = document.CreateElement(tag);
            root.AppendChild(document.DocumentElement);

            var reader = new XmlReader(root);

            reader.ReadValue(tag, collection);
        }
    }
}
