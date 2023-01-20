using Global.ZennoLab.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountRegisterApplication.RegisterServices.General
{
    internal class JsonReader<JsonT>
    {
        private readonly string _filePath;

        public JsonReader(string filePath)
        {
            _filePath = filePath;
        }

        public JsonT Read()
        {
            string content = File.ReadAllText(_filePath);
            JsonT obj = JToken.Parse(content).ToObject<JsonT>();

            return obj;
        }
    }
}
