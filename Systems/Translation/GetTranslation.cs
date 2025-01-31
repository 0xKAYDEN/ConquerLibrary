using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace DeadRabbitProject.Translation
{
    internal class GetTranslation
    {
        public static string _Line1;
        public static string _Line2;
        public static string _Line3;
        public static string _Answer1;
        public static string _Answer2;
        public static void Translate(uint NPC_ID)
        {
            string filePath = File.ReadAllText("" + Program.ServerConfig.DbLocation + "\\Translation\\" + NPC_ID + ".json");
            Conversation jsonObject = JsonConvert.DeserializeObject<Conversation>(filePath);
            Console.WriteLine(filePath);
            _Line1 = jsonObject.Line1;
            _Line2 = jsonObject.Line2;
            _Line3 = jsonObject.Line3;
            _Answer1 = jsonObject.Answer1;
            _Answer2 = jsonObject.Answer2;
        }
        public class Conversation
        {
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public string Line3 { get; set; }
            public string Answer1 { get; set; }
            public string Answer2 { get; set; }
        }

    }
}
