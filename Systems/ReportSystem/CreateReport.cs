using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DeadRabbitProject.ReportSystem
{
    public class CreateReport
    {
        //string uniqueID = GenerateUniqueID(16)
        public static string Msg;
        static string ReportID(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                char randomChar = chars[random.Next(chars.Length)];
                stringBuilder.Append(randomChar);
            }

            return stringBuilder.ToString();
        }
        //But this method in the NPC Code
        public static bool HasProfile(string UID, string MacAddress)
        {
            Database.MySQL.MySQLConnection dB = new Database.MySQL.MySQLConnection();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand("SELECT * FROM dailyreportscount WHERE UID=@UID AND MacAddress=@Mac", dB.getConnection());
            command.Parameters.Add("@UID", MySqlDbType.VarChar).Value = UID;
            command.Parameters.Add("@Mac", MySqlDbType.VarChar).Value = MacAddress;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            // check if this username already exists in the database
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
         
        public static void CreateProfile(uint UID , string PlayerName, string MacAddress, string PlayerIP)
        {
            new DBFunctionality.MySqlCommand(DBFunctionality.MySqlCommandType.INSERT).Insert("dailyreportscount").Insert("UID", UID).Insert("PlayerName", PlayerName)
                        .Insert("MacAddress", MacAddress).Insert("PlayerIP", PlayerIP).Execute();
        }
        public static bool IsFake(uint PlayerUID ,string MacAddress,int RepoCount)
        {
            //1.check the number of report for this device today
            Database.MySQL.MySQLConnection dB = new Database.MySQL.MySQLConnection();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand("SELECT * FROM dailyreportscount WHERE UID=@UID AND MacAddress=@Mac AND ReportCount=@RepoCount ", dB.getConnection());
            command.Parameters.Add("@UID", MySqlDbType.VarChar).Value = PlayerUID;
            command.Parameters.Add("@Mac", MySqlDbType.VarChar).Value = RepoCount;
            command.Parameters.Add("@RepoCount", MySqlDbType.VarChar).Value = RepoCount;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            // check if this username already exists in the database
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsGuildWarDay(DateTime guildWarday)
        {
            if (guildWarday.DayOfWeek == DayOfWeek.Friday)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsDuplicated(string reportID)
        {
            Database.MySQL.MySQLConnection dB = new Database.MySQL.MySQLConnection();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand("SELECT * FROM reports WHERE ReportID=@repoID", dB.getConnection());
            command.Parameters.Add("@repoID", MySqlDbType.VarChar).Value = reportID;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            // check if this username already exists in the database
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void SubmitReport(uint UID, string From ,string MacAddress ,string playerName, string Details)
        {
            //A1b2C3d4E5f6G7H8
            string uniqueID = ReportID(16);
            DateTime now = DateTime.Today;
            DateTime ReportTime = DateTime.Now;

            if (IsDuplicated(uniqueID))
            {
                return;
            }
            else
            {
                
                if (IsFake(UID,MacAddress,0))
                {
                    Msg = "this report is fake or you have reached the max reports number for today";
                }
                else
                {
                    if (IsGuildWarDay(now))
                    {
                        Msg = "Sorry, you can't report Any Player Toady Cuz of the GuildWar";
                    }
                    else
                    {
                        new DBFunctionality.MySqlCommand(DBFunctionality.MySqlCommandType.INSERT).Insert("reports").Insert("ReportID", uniqueID).Insert("From", From)
                        .Insert("PlayerName", playerName).Insert("Details", Details).Insert("ReportTime", ReportTime.ToString()).Execute();
                    }
                }
               
            }
        }
    }
}
