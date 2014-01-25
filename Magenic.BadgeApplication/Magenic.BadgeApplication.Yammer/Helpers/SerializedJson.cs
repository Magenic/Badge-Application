using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;


namespace Magenic.BadgeApplication.Yammer
{
    [Serializable]
    public abstract class SerializedJson<T>where T: new()
    {
        public static T GetInstanceFromJson(string data)
        {
            T returnDataType = default(T);

            try
            {
                MemoryStream ms = new MemoryStream();
                byte[] buf = System.Text.UTF8Encoding.UTF8.GetBytes(data);
                ms.Write(buf, 0, Convert.ToInt32(buf.Length));
                ms.Position = 0;
                DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(T));
                returnDataType = (T)s.ReadObject(ms);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return returnDataType;
        }
    }
}
