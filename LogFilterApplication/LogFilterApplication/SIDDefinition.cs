using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogFilterApplication
{
    public class SIDDefinition
    {
        public Dictionary<string, string> AvailableSIDs = new Dictionary<string, string>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public SIDDefinition()
        {
            AvailableSIDs.Add("7ec.623206.24", "SOC_HV_Batt");
            AvailableSIDs.Add("42e.44", "T_HV_Bat");
            AvailableSIDs.Add("7ec.623203.24", "U_HV_Batt");
            AvailableSIDs.Add("7ec.623204.24", "I_HV_Batt");
            AvailableSIDs.Add("5d7.0", "V_Veh_CAN");
            AvailableSIDs.Add("186.16", "M_EM");
            AvailableSIDs.Add("1f8.40", "N_EM");
            AvailableSIDs.Add("430.38", "Status_HV_Bat_Kühlung");
            AvailableSIDs.Add("7ec.622005.24", "U_12V_BAT");
        }
    }
}
