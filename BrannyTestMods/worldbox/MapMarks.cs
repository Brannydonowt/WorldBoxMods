using System;
using UnityEngine;

// Token: 0x020002D4 RID: 724
public class MapMarks
{
	// Token: 0x06000FA3 RID: 4003 RVA: 0x0008B71C File Offset: 0x0008991C
	public static void drawMarks()
	{
		MapMarks.kings.clear();
		MapMarks.leaders.clear();
		MapMarks.army.clear();
		MapMarks.boats.clear();
		if (!MapBox.instance.qualityChanger.lowRes)
		{
			return;
		}
		if (PlayerConfig.optionBoolEnabled("map_kings_leaders"))
		{
			MapMarks.drawMarksLeaders();
		}
		if (PlayerConfig.optionBoolEnabled("marks_boats"))
		{
			MapMarks.drawMarksBoats();
		}
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x0008B788 File Offset: 0x00089988
	public static void drawMarksBoats()
	{
		Race race = AssetManager.raceLibrary.get("boat");
		ActorContainer actorContainer = (race != null) ? race.units : null;
		if (actorContainer == null || actorContainer.Count == 0)
		{
			return;
		}
		foreach (Actor actor in actorContainer)
		{
			if (!(actor == null) && actor.data.alive && actor.stats.drawBoatMark)
			{
				KingdomColor kingdomColor = actor.kingdom.kingdomColor;
				if (kingdomColor != null)
				{
					MapMarks.drawMark(actor.currentTile, null, ref kingdomColor.colorBorderOut, MapMarks.boats);
				}
				else
				{
					MapMarks.drawMark(actor.currentTile, null, ref Toolbox.color_white, MapMarks.boats);
				}
			}
		}
	}

	// Token: 0x06000FA5 RID: 4005 RVA: 0x0008B854 File Offset: 0x00089A54
	public static void drawMarksLeaders()
	{
		for (int i = 0; i < MapBox.instance.kingdoms.list_civs.Count; i++)
		{
			Kingdom kingdom = MapBox.instance.kingdoms.list_civs[i];
			if (!(kingdom.king == null) && kingdom.king.data.alive)
			{
				MapMarks.drawMark(kingdom.king.currentTile, null, ref kingdom.kingdomColor.colorBorderOut, MapMarks.kings);
			}
		}
		for (int j = 0; j < MapBox.instance.kingdoms.list_civs.Count; j++)
		{
			Kingdom kingdom2 = MapBox.instance.kingdoms.list_civs[j];
			for (int k = 0; k < kingdom2.cities.Count; k++)
			{
				City city = kingdom2.cities[k];
				if (!(city.leader == null) && city.leader.data.alive)
				{
					MapMarks.drawMark(city.leader.currentTile, null, ref kingdom2.kingdomColor.colorBorderOut, MapMarks.leaders);
				}
			}
		}
		for (int l = 0; l < MapBox.instance.unitGroupManager.groups.Count; l++)
		{
			UnitGroup unitGroup = MapBox.instance.unitGroupManager.groups[l];
			if (!(unitGroup.groupLeader == null) && unitGroup.groupLeader.data.alive && unitGroup.groupLeader.kingdom.isCiv())
			{
				MapMarks.drawMark(unitGroup.groupLeader.currentTile, null, ref unitGroup.groupLeader.kingdom.kingdomColor.colorBorderOut, MapMarks.army);
			}
		}
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x0008BA24 File Offset: 0x00089C24
	public static void drawArrows()
	{
		MapMarks.arrows.clear();
		if (!MapBox.instance.qualityChanger.lowRes)
		{
			return;
		}
		if (!PlayerConfig.optionBoolEnabled("map_kings_leaders"))
		{
			return;
		}
		if (DebugConfig.isOn(DebugOption.CivDrawSettleTarget))
		{
			foreach (Kingdom kingdom in MapBox.instance.kingdoms.list_civs)
			{
				foreach (City city in kingdom.cities)
				{
					if (city.settleTarget != null)
					{
						WorldTile tile = city.getTile();
						WorldTile worldTile = city.settleTarget.centerTile;
						if (tile != null && worldTile != null)
						{
							MapMarks.drawMark(tile, worldTile, ref Toolbox.color_yellow, MapMarks.arrows);
						}
					}
				}
			}
		}
		if (DebugConfig.isOn(DebugOption.KingdomDrawAttackTarget))
		{
			foreach (Kingdom kingdom2 in MapBox.instance.kingdoms.list_civs)
			{
				if (!(kingdom2.target_city == null) && !(kingdom2.capital == null))
				{
					WorldTile tile = kingdom2.capital.getTile();
					WorldTile worldTile = kingdom2.target_city.getTile();
					if (tile != null && worldTile != null)
					{
						MapMarks.drawMark(tile, worldTile, ref Toolbox.color_red, MapMarks.arrows);
					}
				}
			}
		}
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x0008BBC0 File Offset: 0x00089DC0
	private static void drawMark(WorldTile pTile, WorldTile pTile2, ref Color pColor, MarkContainer pContainer)
	{
		if (pTile == null)
		{
			return;
		}
		MapMark mapMark = pContainer.get();
		mapMark.gameObject.SetActive(true);
		mapMark.transform.position = pTile.posV3;
		if (pContainer == MapMarks.arrows)
		{
			float z = Toolbox.getAngle(pTile.posV3.x, pTile.posV3.y, pTile2.posV3.x, pTile2.posV3.y) * 57.29578f;
			mapMark.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, z));
			float num = Toolbox.DistTile(pTile, pTile2);
			float num2 = (float)mapMark.spriteRenderer.sprite.texture.width;
			float x = num / num2;
			Vector3 localScale = new Vector3(x, 1f, 1f);
			mapMark.transform.localScale = localScale;
			mapMark.spriteRenderer.color = pColor;
			return;
		}
		if (pContainer == MapMarks.kings)
		{
			mapMark.spriteRenderer.color = pColor;
			mapMark.transform.position = pTile.posV3;
			return;
		}
		if (pContainer == MapMarks.leaders)
		{
			mapMark.spriteRenderer.color = pColor;
			mapMark.transform.position = pTile.posV3;
			return;
		}
		if (pContainer == MapMarks.army)
		{
			mapMark.spriteRenderer.color = pColor;
			mapMark.transform.position = pTile.posV3;
			return;
		}
		if (pContainer == MapMarks.boats)
		{
			mapMark.spriteRendererSail.color = pColor;
			mapMark.transform.position = pTile.posV3;
		}
	}

	// Token: 0x040012D2 RID: 4818
	private static MarkContainer arrows = new MarkContainer("p_mapArrow");

	// Token: 0x040012D3 RID: 4819
	private static MarkContainer leaders = new MarkContainer("p_mapLeader");

	// Token: 0x040012D4 RID: 4820
	private static MarkContainer kings = new MarkContainer("p_mapKing");

	// Token: 0x040012D5 RID: 4821
	private static MarkContainer army = new MarkContainer("p_mapArmy");

	// Token: 0x040012D6 RID: 4822
	private static MarkContainer boats = new MarkContainer("p_mapMarkBoat");

	// Token: 0x040012D7 RID: 4823
	private static float timer = 0f;
}
