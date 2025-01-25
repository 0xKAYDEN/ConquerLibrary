using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeadRabbitProject.ShadowBan
{
    internal class ShadowBanPlayer
    { 
        public static int _Bantime  = 0;
        public enum BanTime
        {
            //Ban For Hours 
            LowReports = 5,
            MidReports = 10,
            HighReports = 24,
            MidHighReports = 48,
            VeryHighReports = 72
        }

        public static void HourCalculation(int ReportsNumber)
        {
           if (ReportsNumber <= 3 || ReportsNumber >= 6)
            {
                //low reports count 
                _Bantime = (int)BanTime.LowReports;
            }
            if (ReportsNumber <= 6 || ReportsNumber >= 9)
            {
                //Mid reports count 
                _Bantime = (int)BanTime.MidReports;
            }
            if (ReportsNumber <= 9 || ReportsNumber >= 12)
            {
                //High reports count 
                _Bantime = (int)BanTime.HighReports;
            }
            if (ReportsNumber <= 12 || ReportsNumber >= 15)
            {
                //Mid High reports count 
                _Bantime = (int)BanTime.MidHighReports;
            }
            if (ReportsNumber <= 15 || ReportsNumber >= 25)
            {
                //Very High reports count 
                _Bantime = (int)BanTime.VeryHighReports;
            }

        }

        public static void ExcuteBan(uint UID,string PlayerName)
        {

        }
    }
}
