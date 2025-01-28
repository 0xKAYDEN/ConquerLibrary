using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using DeadRabbitProject.Game.MsgTournaments;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DeadRabbitProject.Hunters
{
    internal class HuntersRank
    {
        //1. Register Hunter Into Database

        public static void RegisterHunters(uint UID, string PlayerName ,int BattelPower ,string GuildName)
        {
            int defSocre = 1000;
            string defRank = "E";
            new DBFunctionality.MySqlCommand(DBFunctionality.MySqlCommandType.INSERT).Insert("huntersrank").Insert("UID", UID).Insert("PlayerName", PlayerName)
            .Insert("BattlePower", BattelPower).Insert("Score", defSocre).Insert("Rank", defRank).Insert("GuildName", GuildName).Execute();
        }
        //2. Set Rank Score And Rank Litter 

        static string HunterRankLitter {  get; set; }
        public enum Rank
        {
            E = 1000,
            D = 1500,
            C = 2000,
            B = 2500,
            A = 3000,
            AA = 3500,
            S = 4000,
            SS = 4500,
            SSS = 5000
        }

        public static string CalculateRank(int _Score)
        {
            if (_Score >= (int)Rank.SSS)
            {
                return "SSS";
            }
            if (_Score >= (int)Rank.SS && _Score < (int)Rank.SSS)
            {
                return "SS";
            }
            if (_Score >= (int)Rank.S && _Score < (int)Rank.SS)
            {
                return "S";
            }
            if (_Score >= (int)Rank.AA && _Score < (int)Rank.S)
            {
                return "AA";
            }
            if (_Score >= (int)Rank.A && _Score < (int)Rank.AA)
            {
                return "A";
            }
            if (_Score >= (int)Rank.B && _Score < (int)Rank.A)
            {
                return "B";
            }
            if (_Score >= (int)Rank.C && _Score < (int)Rank.B)
            {
                return "C";
            }
            if (_Score >= (int)Rank.D && _Score < (int)Rank.C)
            {
                return "D";
            }
            if (_Score >= (int)Rank.E && _Score < (int)Rank.D)
            {
                return "E";
            }
            // Default return value if the score is below the lowest rank
            return "E";
        }

        //3.Updgrade Player Rank
        public static void UpgradePlayerRank(uint UID, int Score, string Rank)
        {
            new DBFunctionality.MySqlCommand(DBFunctionality.MySqlCommandType.UPDATE).Update("huntersrank").Set("Score", Score).Set("Rank", Rank).Where("UID", UID).Execute();
        }
    }
    
}
