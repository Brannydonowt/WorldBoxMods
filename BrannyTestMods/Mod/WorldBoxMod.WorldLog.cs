//extern alias ncms;
//using NCMS = ncms.NCMS;
using NCMS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using UnityEngine.EventSystems;
using HarmonyLib;

namespace BrannyTestMods
{
	public partial class WorldBoxMod
	{
		public static void logNewKillLead(Actor lead)
		{
			ActorStatus aStat = Helper.Reflection.GetActorData(lead);

			string aTitle = "kill_lead_" + aStat.firstName + "_" + aStat.kills;
			string aMessage = aStat.firstName + " is $name$ the new world kill $kingdom$ leader, with " + aStat.kills + " kills!";

			Helper.Localization.addLocalization(aTitle, aMessage);

			WorldLogMessage worldLogMessage = new WorldLogMessage(aMessage, aStat.firstName, aStat.kills.ToString(), null);
			worldLogMessage.special1 = lead.kingdom.name;
			worldLogMessage.special2 = aStat.firstName;
			worldLogMessage.special3 = aStat.kills.ToString();
			worldLogMessage.unit = lead;
			worldLogMessage.location = lead.currentPosition;
			worldLogMessage.color_special1 = Color.cyan; //lead.kingdom.kingdomColor.colorBorderOut;
			worldLogMessage.color_special2 = Color.red;  //lead.kingdom.kingdomColor.colorBorderOut;
			List<WorldLogMessage> list = Helper.Reflection.GetWorldLogMessages(WorldLog.instance);
			list.Add(worldLogMessage);


			worldLogMessage.add();
		}

		public static void logKillLeadKill(Actor lead) 
		{
			ActorStatus aStat = Helper.Reflection.GetActorData(lead);

			string aTitle = "kill_lead_" + aStat.firstName + "_" + aStat.kills + "_u";
			string aMessage = "The world kill leader, " + aStat.firstName + ", has now claimed " + aStat.kills + " victims!";

			Helper.Localization.addLocalization(aTitle, aMessage);

			WorldLogMessage worldLogMessage = new WorldLogMessage(aTitle, aStat.firstName, aStat.kills.ToString(), null);
			worldLogMessage.special1 = lead.kingdom.name;
			worldLogMessage.special2 = aStat.firstName;
			worldLogMessage.special3 = aStat.kills.ToString();
			worldLogMessage.unit = lead;
			worldLogMessage.location = lead.currentPosition;
			worldLogMessage.color_special1 = Color.cyan; //lead.kingdom.kingdomColor.colorBorderOut;
			worldLogMessage.color_special2 = Color.red;  //lead.kingdom.kingdomColor.colorBorderOut;
			List<WorldLogMessage> list = Helper.Reflection.GetWorldLogMessages(WorldLog.instance);
			list.Add(worldLogMessage);

			worldLogMessage.add();
		}

		public static void logKillLeadDead(Actor lead) 
		{
			
		
		}
	}
}
