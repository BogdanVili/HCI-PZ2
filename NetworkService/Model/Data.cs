using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NetworkService.Model
{
    public class Data
    {
        public void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception)
            {

            }
        }

        public T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                string attributeXml = string.Empty;

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception)
            {
                //Log exception here
            }

            return objectOut;
        }

        public ObservableCollection<DistributedEnergyResource> LoadData(string filename)
        {
            ObservableCollection<DistributedEnergyResource> DERs = DeSerializeObject<ObservableCollection<DistributedEnergyResource>>(filename);
            if (DERs != null && DERs.Any())
            {
                return DERs;
            }
            else
            {
                return new ObservableCollection<DistributedEnergyResource>();
            }
        }

        public void SaveData(string filename, ObservableCollection<DistributedEnergyResource> DERs)
        {
            SerializeObject<ObservableCollection<DistributedEnergyResource>>(DERs, filename);
        }
    }
}
