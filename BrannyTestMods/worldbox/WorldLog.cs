using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AC RID: 172
public class WorldLog
{
	// Token: 0x06000375 RID: 885 RVA: 0x00037280 File Offset: 0x00035480
	public WorldLog(MapBox pWorld)
	{
		WorldLog.instance = this;
		this.world = pWorld;
		this.list = new List<WorldLogMessage>();
		if (Config.isComputer)
		{
			this.limit = 200;
			return;
		}
		this.limit = 40;
	}

	// Token: 0x06000376 RID: 886 RVA: 0x000372BB File Offset: 0x000354BB
	public void truncateList()
	{
		while (this.list.Count >= this.limit)
		{
			this.list.RemoveAt(this.list.Count - 1);
		}
	}

	// Token: 0x06000377 RID: 887 RVA: 0x000372EA File Offset: 0x000354EA
	public void clear()
	{
		this.list.Clear();
	}

	// Token: 0x06000378 RID: 888 RVA: 0x000372F8 File Offset: 0x000354F8
	public static void logNewKing(Kingdom pKingdom)
	{
		WorldLogMessage worldLogMessage = new WorldLogMessage("king_new", pKingdom.name, pKingdom.king.data.firstName, null);
		worldLogMessage.unit = pKingdom.king;
		worldLogMessage.location = pKingdom.king.currentPosition;
		worldLogMessage.color_special1 = pKingdom.kingdomColor.colorBorderOut;
		worldLogMessage.color_special2 = pKingdom.kingdomColor.colorBorderOut;
		ref worldLogMessage.add();
	}

	// Token: 0x06000379 RID: 889 RVA: 0x00037378 File Offset: 0x00035578
	public static void logKingLeft(Kingdom pKingdom, Actor pActor)
	{
		WorldLogMessage worldLogMessage = new WorldLogMessage("king_left", pKingdom.name, pActor.data.firstName, null);
		worldLogMessage.unit = pActor;
		worldLogMessage.location = pActor.currentPosition;
		worldLogMessage.kingdom = pKingdom;
		worldLogMessage.color_special1 = pKingdom.kingdomColor.colorBorderOut;
		ref worldLogMessage.add();
	}

	// Token: 0x0600037A RID: 890 RVA: 0x000373E0 File Offset: 0x000355E0
	public static void logKingDead(Kingdom pKingdom, Actor pActor)
	{
		WorldLogMessage worldLogMessage = new WorldLogMessage("king_dead", pKingdom.name, pActor.data.firstName, null);
		worldLogMessage.location = pActor.currentPosition;
		worldLogMessage.kingdom = pKingdom;
		worldLogMessage.color_special1 = pKingdom.kingdomColor.colorBorderOut;
		ref worldLogMessage.add();
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00037440 File Offset: 0x00035640
	public static void logKingMurder(Kingdom pKingdom, Actor pActor, Actor pAttacker)
	{
		string pText = "king_killed_" + Toolbox.randomInt(1, 3).ToString();
		string name = pKingdom.name;
		string firstName = pActor.data.firstName;
		string pSpecial;
		if (pAttacker == null)
		{
			pSpecial = null;
		}
		else
		{
			ActorStatus data = pAttacker.data;
			pSpecial = ((data != null) ? data.firstName : null);
		}
		WorldLogMessage worldLogMessage = new WorldLogMessage(pText, name, firstName, pSpecial);
		worldLogMessage.color_special1 = pKingdom.kingdomColor.colorBorderOut;
		worldLogMessage.color_special2 = pKingdom.kingdomColor.colorBorderOut;
		bool flag;
		if (pAttacker == null)
		{
			flag = (null != null);
		}
		else
		{
			Kingdom kingdom = pAttacker.kingdom;
			flag = (((kingdom != null) ? kingdom.kingdomColor : null) != null);
		}
		if (flag)
		{
			worldLogMessage.color_special3 = pAttacker.kingdom.kingdomColor.colorBorderOut;
		}
		worldLogMessage.unit = pAttacker;
		worldLogMessage.location = pActor.currentPosition;
		worldLogMessage.kingdom = pKingdom;
		ref worldLogMessage.add();
	}

	// Token: 0x0600037C RID: 892 RVA: 0x00037514 File Offset: 0x00035714
	public static void logFavDead(Actor pActor)
	{
		WorldLogMessage worldLogMessage = new WorldLogMessage("favorite_dead_" + Toolbox.randomInt(1, 3).ToString(), pActor.data.firstName, null, null);
		worldLogMessage.location = pActor.currentPosition;
		bool flag;
		if (pActor == null)
		{
			flag = (null != null);
		}
		else
		{
			Kingdom kingdom = pActor.kingdom;
			flag = (((kingdom != null) ? kingdom.kingdomColor : null) != null);
		}
		if (flag)
		{
			worldLogMessage.color_special1 = pActor.kingdom.kingdomColor.colorBorderOut;
		}
		ref worldLogMessage.add();
	}

	// Token: 0x0600037D RID: 893 RVA: 0x00037598 File Offset: 0x00035798
	public static void logFavMurder(Actor pActor, Actor pAttacker)
	{
		string pText = "favorite_killed_" + Toolbox.randomInt(1, 3).ToString();
		string firstName = pActor.data.firstName;
		string pSpecial;
		if (pAttacker == null)
		{
			pSpecial = null;
		}
		else
		{
			ActorStatus data = pAttacker.data;
			pSpecial = ((data != null) ? data.firstName : null);
		}
		WorldLogMessage worldLogMessage = new WorldLogMessage(pText, firstName, pSpecial, null);
		bool flag;
		if (pActor == null)
		{
			flag = (null != null);
		}
		else
		{
			Kingdom kingdom = pActor.kingdom;
			flag = (((kingdom != null) ? kingdom.kingdomColor : null) != null);
		}
		if (flag)
		{
			worldLogMessage.color_special1 = pActor.kingdom.kingdomColor.colorBorderOut;
		}
		bool flag2;
		if (pAttacker == null)
		{
			flag2 = (null != null);
		}
		else
		{
			Kingdom kingdom2 = pAttacker.kingdom;
			flag2 = (((kingdom2 != null) ? kingdom2.kingdomColor : null) != null);
		}
		if (flag2)
		{
			worldLogMessage.color_special2 = pAttacker.kingdom.kingdomColor.colorBorderOut;
		}
		worldLogMessage.unit = pAttacker;
		worldLogMessage.location = pActor.currentPosition;
		ref worldLogMessage.add();
	}

	// Token: 0x0600037E RID: 894 RVA: 0x0003766C File Offset: 0x0003586C
	public static void logNewCity(City pCity)
	{
		WorldLogMessage worldLogMessage = new WorldLogMessage("city_new", pCity.data.cityName, null, null);
		worldLogMessage.city = pCity;
		worldLogMessage.location = pCity.lastCityCenter;
		worldLogMessage.color_special1 = pCity.kingdom.kingdomColor.colorBorderOut;
		ref worldLogMessage.add();
	}

	// Token: 0x0600037F RID: 895 RVA: 0x000376C8 File Offset: 0x000358C8
	public static void logCityRevolt(City pCity)
	{
		WorldLogMessage worldLogMessage = new WorldLogMessage("log_city_revolted", pCity.data.cityName, pCity.kingdom.name, null);
		worldLogMessage.city = pCity;
		worldLogMessage.location = pCity.lastCityCenter;
		ref worldLogMessage.add();
	}

	// Token: 0x06000380 RID: 896 RVA: 0x00037714 File Offset: 0x00035914
	public static void logNewWar(Kingdom pKingdom1, Kingdom pKingdom2)
	{
		WorldLogMessage worldLogMessage = new WorldLogMessage("diplomacy_war_started", pKingdom1.name, pKingdom2.name, null);
		worldLogMessage.kingdom = pKingdom2;
		worldLogMessage.location = pKingdom2.location;
		worldLogMessage.color_special1 = pKingdom1.kingdomColor.colorBorderOut;
		worldLogMessage.color_special2 = pKingdom2.kingdomColor.colorBorderOut;
		ref worldLogMessage.add();
	}

	// Token: 0x06000381 RID: 897 RVA: 0x0003777C File Offset: 0x0003597C
	public static void logNewPeace(Kingdom pKingdom1, Kingdom pKingdom2)
	{
		WorldLogMessage worldLogMessage = new WorldLogMessage("diplomacy_peace", pKingdom1.name, pKingdom2.name, null);
		worldLogMessage.kingdom = pKingdom2;
		worldLogMessage.location = pKingdom2.location;
		worldLogMessage.color_special1 = pKingdom1.kingdomColor.colorBorderOut;
		worldLogMessage.color_special2 = pKingdom2.kingdomColor.colorBorderOut;
		ref worldLogMessage.add();
	}

	// Token: 0x06000382 RID: 898 RVA: 0x000377E4 File Offset: 0x000359E4
	public static void logNewKingdom(Kingdom pKingdom)
	{
		WorldLogMessage worldLogMessage = new WorldLogMessage("kingdom_new", pKingdom.name, null, null);
		worldLogMessage.kingdom = pKingdom;
		worldLogMessage.location = pKingdom.location;
		worldLogMessage.color_special1 = pKingdom.kingdomColor.colorBorderOut;
		ref worldLogMessage.add();
	}

	// Token: 0x06000383 RID: 899 RVA: 0x00037834 File Offset: 0x00035A34
	public static void logKingdomDestroyed(Kingdom pKingdom)
	{
		WorldLogMessage worldLogMessage = new WorldLogMessage("kingdom_destroyed", pKingdom.name, null, null);
		worldLogMessage.kingdom = pKingdom;
		worldLogMessage.location = pKingdom.location;
		worldLogMessage.color_special1 = pKingdom.kingdomColor.colorBorderOut;
		ref worldLogMessage.add();
	}

	// Token: 0x06000384 RID: 900 RVA: 0x00037884 File Offset: 0x00035A84
	public static void logCityDestroyed(City pCity)
	{
		WorldLogMessage worldLogMessage = new WorldLogMessage("city_destroyed", pCity.data.cityName, null, null);
		worldLogMessage.color_special1 = pCity.kingdom.kingdomColor.colorBorderOut;
		worldLogMessage.location = pCity.lastCityCenter;
		worldLogMessage.city = pCity;
		ref worldLogMessage.add();
	}

	// Token: 0x06000385 RID: 901 RVA: 0x000378E0 File Offset: 0x00035AE0
	public static void logDisaster(DisasterAsset pAsset, WorldTile pTile, string pName = null, City pCity = null, Actor pUnit = null)
	{
		if (string.IsNullOrEmpty(pAsset.world_log))
		{
			return;
		}
		WorldLogMessage worldLogMessage = new WorldLogMessage(pAsset.world_log, null, null, null);
		worldLogMessage.icon = pAsset.world_log_icon;
		worldLogMessage.location = pTile.posV3;
		worldLogMessage.special1 = pName;
		if (pCity != null)
		{
			worldLogMessage.special2 = pCity.data.cityName;
			worldLogMessage.city = pCity;
		}
		worldLogMessage.unit = pUnit;
		ref worldLogMessage.add();
	}

	// Token: 0x06000386 RID: 902 RVA: 0x00037960 File Offset: 0x00035B60
	public static void locationJump(Vector3 pVector)
	{
		HistoryHud.disableRaycasts();
		MapBox.instance.locatePosition(pVector, new Action(HistoryHud.enableRaycasts), new Action(HistoryHud.enableRaycasts));
	}

	// Token: 0x06000387 RID: 903 RVA: 0x0003798A File Offset: 0x00035B8A
	public static void locationFollow(Actor pActor)
	{
		HistoryHud.disableRaycasts();
		MapBox.instance.locateAndFollow(pActor, new Action(HistoryHud.enableRaycasts), new Action(HistoryHud.enableRaycasts));
	}

	// Token: 0x040005CC RID: 1484
	internal List<WorldLogMessage> list;

	// Token: 0x040005CD RID: 1485
	private MapBox world;

	// Token: 0x040005CE RID: 1486
	public static WorldLog instance;

	// Token: 0x040005CF RID: 1487
	private int limit;
}
