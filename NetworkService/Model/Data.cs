using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

        public ObservableCollection<LoggedData> LoadLogs(string filename)
        {
            ObservableCollection<LoggedData> loggedDatas = new ObservableCollection<LoggedData>();

            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    string[] split1 = line.Split(']');
                    string timeString = split1[0].Trim('[');
                    DateTime time = DateTime.ParseExact(timeString, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);

                    string[] split2 = split1[1].Split(':');
                    int id = Int32.Parse(split2[0].Trim());
                    double valueMeasured = Double.Parse(split2[1].Trim());

                    loggedDatas.Add(new LoggedData(id, time, valueMeasured));
                }

                sr.Close();
            }
            

            if(loggedDatas != null && loggedDatas.Any())
            {
                return loggedDatas;
            }
            else
            {
                return new ObservableCollection<LoggedData>();
            }
        }

        public void SaveLog(string filename, LoggedData loggedData)
        {
            StreamWriter sw = new StreamWriter(filename, append: true);
            sw.WriteLine("[" + loggedData.Time.ToString("dd.MM.yyyy hh:mm") + "] " + loggedData.Id.ToString() + " : " + loggedData.ValueMeasure.ToString());
            sw.Close();
        }

        public void SaveLogs(string filename, ObservableCollection<LoggedData> loggedDatas)
        {
            foreach(LoggedData l in loggedDatas)
            {
                SaveLog(filename, l);
            }
        }
    }
}
