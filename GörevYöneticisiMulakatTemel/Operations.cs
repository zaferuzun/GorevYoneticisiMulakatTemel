using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GörevYöneticisiMulakatTemel
{
    public class Operations
    {
        public static string DateCount(DateTime G_date,string G_dateType)
        {
            string P_jobDefinition = "";
            String P_dateDiff = (System.DateTime.Now - G_date).TotalDays.ToString();
            System.TimeSpan diff = System.DateTime.Now.Subtract(G_date);
            double P_dateDifInt = Convert.ToDouble(P_dateDiff);
            if (G_dateType== "Günlük" && (P_dateDifInt > 0 && P_dateDifInt < 1))
            {
                P_jobDefinition = diff.Days + "gün " + diff.Hours + "saat " + diff.Minutes + " dakika zaman geçmiştir.";
            }
            else if(G_dateType == "Haftalık" && (P_dateDifInt > 0 && P_dateDifInt < 7))
            {
                P_jobDefinition = diff.Days + "gün " + diff.Hours + "saat " + diff.Minutes + " dakika zaman geçmiştir.";

            }
            else if(G_dateType == "Aylık" && (P_dateDifInt > 0 && P_dateDifInt < 31))
            {
                P_jobDefinition = diff.Days+"gün " + diff.Hours+"saat "+ diff.Minutes+" dakika zaman geçmiştir.";

            }
            else
            {
                if(P_dateDifInt<0)
                {
                    P_jobDefinition = "Planlama süresi başlamamıştır.";
                }
                else
                {
                    P_jobDefinition = "Planlanan süre bitmiştir.";
                }
            }

            return P_jobDefinition;
        }
    }
}