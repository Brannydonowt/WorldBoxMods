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
	// Extension for WorldBox WorldLogs

	// Example WorldLog
	//public static void logNewKing(Kingdom pKingdom)
	//{
	//	WorldLogMessage worldLogMessage = new WorldLogMessage("king_new", pKingdom.name, pKingdom.king.data.firstName, null);
	//	worldLogMessage.unit = pKingdom.king;
	//	worldLogMessage.location = pKingdom.king.currentPosition;
	//	worldLogMessage.color_special1 = pKingdom.kingdomColor.colorBorderOut;
	//	worldLogMessage.color_special2 = pKingdom.kingdomColor.colorBorderOut;
	//	ref worldLogMessage.add();
	//}
	//	WorldLog.logNewKingdom(kingdom);


	public partial class WorldBoxMod
	{


		public static void logNewKillLead(Actor lead)
		{
			Debug.Log("Announcing new kill lead");
			ActorStatus aStat = Helper.Reflection.GetActorData(lead);

			WorldLogMessage worldLogMessage = new WorldLogMessage("kill_lead_new", aStat.firstName, aStat.kills.ToString(), null);
			worldLogMessage.unit = lead;
			worldLogMessage.location = lead.currentPosition;
			worldLogMessage.color_special1 = Color.cyan; //lead.kingdom.kingdomColor.colorBorderOut;
			worldLogMessage.color_special2 = Color.red;  //lead.kingdom.kingdomColor.colorBorderOut;
			List<WorldLogMessage> list = Helper.Reflection.GetWorldLogMessages(WorldLog.instance);
			list.Add(worldLogMessage);
			//Helper.Reflection.SetField(Helper.Reflection.GetWorldLogMessages(WorldLog.instance), "list", list);
			Debug.Log("Added Message to list *shrug*");
			
			worldLogMessage.add();
			//ref worldLogMessage.add();
		}
	}
}
