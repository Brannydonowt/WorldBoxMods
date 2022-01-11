using System;
using System.Collections.Generic;
using UnityEngine;

public static class WorldLogExtension
{	

	//public WorldLogExtension(MapBox pWorld) : base(world2)
	//{
	//	WorldLog.instance = (WorldLog)this;
	//	this.world = pWorld;
	//	this.list = new List<WorldLogMessage>();
	//	if (Config.isComputer)
	//	{
	//		this.limit = 200;
	//		return;
	//	}
	//	this.limit = 40;
	//}

	//public static void logNewKillLead(Actor lead)
	//{
	//	ActorStatus aStat = Helper.Reflection.GetActorData(lead);
	//	WorldLogMessage worldLogMessage = new WorldLogMessage("kill_lead_new", aStat.firstName, aStat.kills.ToString(), null);
	//	worldLogMessage.unit = lead;
	//	worldLogMessage.location = lead.currentPosition;
	//	worldLogMessage.color_special1 = Color.cyan; //lead.kingdom.kingdomColor.colorBorderOut;
	//	worldLogMessage.color_special2 = Color.red;  //lead.kingdom.kingdomColor.colorBorderOut;
	//	ref worldLogMessage.add();
	//}

	//// Token: 0x040005CC RID: 1484
	//internal List<WorldLogMessage> list;

	//private static MapBox world2;

	//// Token: 0x040005CD RID: 1485
	//private MapBox world;

	//// Token: 0x040005CE RID: 1486
	//public static WorldLog instance;

	//// Token: 0x040005CF RID: 1487
	//private int limit;
}