using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000195 RID: 405
public class ZoneCalculator : MapLayer
{
	// Token: 0x0600093F RID: 2367 RVA: 0x00061BDC File Offset: 0x0005FDDC
	internal override void create()
	{
		base.create();
		this.colorValues = new Color(1f, 0.46f, 0.19f, 1f);
		this.color_selected = new RelationColor("#ffffff");
		this.color_ally = new RelationColor("#00ff00");
		this.color_enemy = new RelationColor("#ff0000");
		Color color = this.sprRnd.color;
		color.a = 0.78f;
		this.sprRnd.color = color;
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x00061C64 File Offset: 0x0005FE64
	internal override void clear()
	{
		base.clear();
		foreach (TileZone tileZone in this.zones)
		{
			tileZone.clear();
		}
		this._currentDrawnZones.Clear();
		this._toCleanUp.Clear();
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x00061CD0 File Offset: 0x0005FED0
	public void generate()
	{
		this.zones.Clear();
		this.zonesDict_id.Clear();
		int num = 8;
		this.totalZonesX = Config.ZONE_AMOUNT_X * num;
		this.totalZonesY = Config.ZONE_AMOUNT_Y * num;
		this.map = new TileZone[this.totalZonesX, this.totalZonesY];
		int num2 = 0;
		for (int i = 0; i < this.totalZonesY; i++)
		{
			for (int j = 0; j < this.totalZonesX; j++)
			{
				TileZone tileZone = new TileZone
				{
					x = j,
					y = i,
					id = num2++
				};
				if ((j + i) % 2 == 0)
				{
					tileZone.debug_zone_color = this.color1;
				}
				else
				{
					tileZone.debug_zone_color = this.color2;
				}
				this.map[j, i] = tileZone;
				this.zones.Add(tileZone);
				this.zonesDict_id.Add(tileZone.id, tileZone);
				this.fillZone(tileZone);
			}
		}
		this.generateNeighbours();
		this.zones.Shuffle<TileZone>();
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x00061DDE File Offset: 0x0005FFDE
	public TileZone getZoneByID(int pID)
	{
		return this.zonesDict_id[pID];
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x00061DEC File Offset: 0x0005FFEC
	public TileZone getZone(int x, int y)
	{
		if (x < 0 || x >= this.totalZonesX || y < 0 || y >= this.totalZonesY)
		{
			return null;
		}
		return this.map[x, y];
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x00061E18 File Offset: 0x00060018
	private void generateNeighbours()
	{
		for (int i = 0; i < this.zones.Count; i++)
		{
			TileZone tileZone = this.zones[i];
			tileZone.neighbours = new List<TileZone>(4);
			tileZone.neighboursAll = new List<TileZone>(8);
			TileZone zone = this.getZone(tileZone.x - 1, tileZone.y);
			tileZone.AddNeighbour(zone, TileDirection.Left, false);
			zone = this.getZone(tileZone.x + 1, tileZone.y);
			tileZone.AddNeighbour(zone, TileDirection.Right, false);
			zone = this.getZone(tileZone.x, tileZone.y - 1);
			tileZone.AddNeighbour(zone, TileDirection.Down, false);
			zone = this.getZone(tileZone.x, tileZone.y + 1);
			tileZone.AddNeighbour(zone, TileDirection.Up, false);
			zone = this.getZone(tileZone.x - 1, tileZone.y - 1);
			tileZone.AddNeighbour(zone, TileDirection.Null, true);
			zone = this.getZone(tileZone.x - 1, tileZone.y + 1);
			tileZone.AddNeighbour(zone, TileDirection.Null, true);
			zone = this.getZone(tileZone.x + 1, tileZone.y - 1);
			tileZone.AddNeighbour(zone, TileDirection.Null, true);
			zone = this.getZone(tileZone.x + 1, tileZone.y + 1);
			tileZone.AddNeighbour(zone, TileDirection.Null, true);
		}
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x00061F60 File Offset: 0x00060160
	private void fillZone(TileZone pZone)
	{
		int num = pZone.x * 8;
		int num2 = pZone.y * 8;
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				WorldTile tile = this.world.GetTile(i + num, j + num2);
				if (tile != null)
				{
					tile.zone = pZone;
					pZone.addTile(tile, i, j);
					if (i == 4 && j == 4)
					{
						pZone.centerTile = tile;
					}
				}
			}
		}
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x00061FD4 File Offset: 0x000601D4
	private void checkDrawnZonesDirty()
	{
		if (!this._drawZones_dirty)
		{
			return;
		}
		this._drawZones_dirty = false;
		this._redraw_timer = 0f;
		if (this._mode == ZoneDisplayMode.Relations)
		{
			foreach (TileZone tileZone in this._currentDrawnZones)
			{
				tileZone.last_drawn_id = 0;
				tileZone.last_drawn_hashcode = 0;
			}
		}
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x00062050 File Offset: 0x00060250
	public override void update(float pElapsed)
	{
		this.checkDrawnZonesDirty();
		if (this.world.isSelectedPower("relations"))
		{
			this.setMode(ZoneDisplayMode.Relations);
			this.sprRnd.enabled = true;
		}
		else if (this.world.showCityZones())
		{
			this.setMode(ZoneDisplayMode.CityBorders);
			this.sprRnd.enabled = true;
		}
		else if (this.world.showKingdomZones())
		{
			this.setMode(ZoneDisplayMode.KingdomBorders);
			this.sprRnd.enabled = true;
		}
		else if (this.world.showCultureZones())
		{
			this.setMode(ZoneDisplayMode.Cultures);
			this.sprRnd.enabled = true;
		}
		else
		{
			this.setMode(ZoneDisplayMode.None);
			this.sprRnd.enabled = false;
		}
		this.redrawZones();
		base.update(pElapsed);
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x00062110 File Offset: 0x00060310
	internal void setDrawnZonesDirty()
	{
		this._drawZones_dirty = true;
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x00062119 File Offset: 0x00060319
	private void setMode(ZoneDisplayMode pMode)
	{
		if (this._mode != pMode)
		{
			this._redraw_timer = 0f;
		}
		this._mode = pMode;
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x00062138 File Offset: 0x00060338
	private void colorModeCultures(TileZone pZone)
	{
		Culture culture = pZone.culture;
		if (culture == null)
		{
			return;
		}
		bool flag = this.isBorderColor_cultures(pZone.zone_up, culture, false);
		bool flag2 = this.isBorderColor_cultures(pZone.zone_down, culture, false);
		bool flag3 = this.isBorderColor_cultures(pZone.zone_left, culture, false);
		bool flag4 = this.isBorderColor_cultures(pZone.zone_right, culture, false);
		int num = (int)(this._mode * (ZoneDisplayMode)10000000);
		int hash = pZone.culture.hash;
		if (flag)
		{
			num += 100000;
		}
		if (flag2)
		{
			num += 10000;
		}
		if (flag3)
		{
			num += 1000;
		}
		if (flag4)
		{
			num += 100;
		}
		bool flag5 = false;
		if (culture == this.highlight_culture)
		{
			num++;
			flag5 = true;
		}
		if (pZone.last_drawn_id == num && pZone.last_drawn_hashcode == hash)
		{
			return;
		}
		pZone.last_drawn_id = num;
		pZone.last_drawn_hashcode = hash;
		this._dirty = true;
		for (int i = 0; i < pZone.tiles.Count; i++)
		{
			WorldTile worldTile = pZone.tiles[i];
			if (flag5)
			{
				this.pixels[worldTile.data.tile_id] = Toolbox.color_white_32;
			}
			else if (!worldTile.worldTileZoneBorder.border)
			{
				this.pixels[worldTile.data.tile_id] = culture.color32;
			}
			else if (worldTile.worldTileZoneBorder.borderUp && flag)
			{
				this.pixels[worldTile.data.tile_id] = culture.color32_border;
			}
			else if (worldTile.worldTileZoneBorder.borderDown && flag2)
			{
				this.pixels[worldTile.data.tile_id] = culture.color32_border;
			}
			else if (worldTile.worldTileZoneBorder.borderLeft && flag3)
			{
				this.pixels[worldTile.data.tile_id] = culture.color32_border;
			}
			else if (worldTile.worldTileZoneBorder.borderRight && flag4)
			{
				this.pixels[worldTile.data.tile_id] = culture.color32_border;
			}
			else
			{
				this.pixels[worldTile.data.tile_id] = culture.color32;
			}
		}
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x0006237C File Offset: 0x0006057C
	private void colorModeRelations(TileZone pZone)
	{
		if (Config.selectedKingdom == null)
		{
			return;
		}
		RelationColor relationColor;
		if (pZone.city.kingdom == Config.selectedKingdom)
		{
			relationColor = this.color_selected;
		}
		else if (pZone.city.kingdom.isEnemy(Config.selectedKingdom))
		{
			relationColor = this.color_enemy;
		}
		else
		{
			relationColor = this.color_ally;
		}
		bool flag = this.isBorderColor_relations(pZone.zone_up, pZone.city, true);
		bool flag2 = this.isBorderColor_relations(pZone.zone_down, pZone.city, false);
		bool flag3 = this.isBorderColor_relations(pZone.zone_left, pZone.city, false);
		bool flag4 = this.isBorderColor_relations(pZone.zone_right, pZone.city, true);
		int num = (int)(this._mode * (ZoneDisplayMode)10000000);
		int num2 = Config.selectedKingdom.hashcode;
		if (flag)
		{
			num += 100000;
		}
		if (flag2)
		{
			num += 10000;
		}
		if (flag3)
		{
			num += 1000;
		}
		if (flag4)
		{
			num += 100;
		}
		num2++;
		if (pZone.last_drawn_id == num && pZone.last_drawn_hashcode == num2)
		{
			return;
		}
		pZone.last_drawn_id = num;
		pZone.last_drawn_hashcode = num2;
		this._dirty = true;
		for (int i = 0; i < pZone.tiles.Count; i++)
		{
			WorldTile worldTile = pZone.tiles[i];
			if (!worldTile.worldTileZoneBorder.border)
			{
				this.pixels[worldTile.data.tile_id] = relationColor.color;
			}
			else if (worldTile.worldTileZoneBorder.borderUp && flag)
			{
				this.pixels[worldTile.data.tile_id] = relationColor.border;
			}
			else if (worldTile.worldTileZoneBorder.borderDown && flag2)
			{
				this.pixels[worldTile.data.tile_id] = relationColor.border;
			}
			else if (worldTile.worldTileZoneBorder.borderLeft && flag3)
			{
				this.pixels[worldTile.data.tile_id] = relationColor.border;
			}
			else if (worldTile.worldTileZoneBorder.borderRight && flag4)
			{
				this.pixels[worldTile.data.tile_id] = relationColor.border;
			}
			else
			{
				this.pixels[worldTile.data.tile_id] = relationColor.color;
			}
		}
		this._debug_redrawn_last_amount++;
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x000625EC File Offset: 0x000607EC
	private void colorModKingdomBorders(TileZone pZone)
	{
		Color32 color = pZone.city.kingdom.kingdomColor.colorBorderInsideAlpha;
		Color32 color2 = pZone.city.kingdom.kingdomColor.colorBorderOut;
		bool flag = this.isBorderColor_kingdoms(pZone.zone_up, pZone.city, true);
		bool flag2 = this.isBorderColor_kingdoms(pZone.zone_down, pZone.city, false);
		bool flag3 = this.isBorderColor_kingdoms(pZone.zone_left, pZone.city, false);
		bool flag4 = this.isBorderColor_kingdoms(pZone.zone_right, pZone.city, true);
		int num = (int)(this._mode * (ZoneDisplayMode)10000000);
		int hashcode = pZone.city.kingdom.hashcode;
		if (flag)
		{
			num += 100000;
		}
		if (flag2)
		{
			num += 10000;
		}
		if (flag3)
		{
			num += 1000;
		}
		if (flag4)
		{
			num += 100;
		}
		bool flag5 = false;
		if (pZone.city.kingdom == this.highlight_kingdom)
		{
			num++;
			flag5 = true;
		}
		if (pZone.last_drawn_id == num && pZone.last_drawn_hashcode == hashcode)
		{
			return;
		}
		pZone.last_drawn_id = num;
		pZone.last_drawn_hashcode = hashcode;
		this._dirty = true;
		for (int i = 0; i < pZone.tiles.Count; i++)
		{
			WorldTile worldTile = pZone.tiles[i];
			if (flag5)
			{
				this.pixels[worldTile.data.tile_id] = Toolbox.color_white_32;
			}
			else if (!worldTile.worldTileZoneBorder.border)
			{
				this.pixels[worldTile.data.tile_id] = color;
			}
			else if (worldTile.worldTileZoneBorder.borderUp && flag)
			{
				this.pixels[worldTile.data.tile_id] = color2;
			}
			else if (worldTile.worldTileZoneBorder.borderDown && flag2)
			{
				this.pixels[worldTile.data.tile_id] = color2;
			}
			else if (worldTile.worldTileZoneBorder.borderLeft && flag3)
			{
				this.pixels[worldTile.data.tile_id] = color2;
			}
			else if (worldTile.worldTileZoneBorder.borderRight && flag4)
			{
				this.pixels[worldTile.data.tile_id] = color2;
			}
			else
			{
				this.pixels[worldTile.data.tile_id] = color;
			}
		}
		this._debug_redrawn_last_amount++;
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x00062874 File Offset: 0x00060A74
	private void colorModCityBorders(TileZone pZone)
	{
		Color32 color = pZone.city.kingdom.kingdomColor.colorBorderInsideAlpha;
		Color32 color2 = pZone.city.kingdom.kingdomColor.colorBorderOut;
		bool flag = this.isBorderColor_cities(pZone.zone_up, pZone.city, true);
		bool flag2 = this.isBorderColor_cities(pZone.zone_down, pZone.city, false);
		bool flag3 = this.isBorderColor_cities(pZone.zone_left, pZone.city, false);
		bool flag4 = this.isBorderColor_cities(pZone.zone_right, pZone.city, true);
		int num = (int)(this._mode * (ZoneDisplayMode)10000000);
		int hashcode = pZone.city.hashcode;
		if (flag)
		{
			num += 100000;
		}
		if (flag2)
		{
			num += 10000;
		}
		if (flag3)
		{
			num += 1000;
		}
		if (flag4)
		{
			num += 100;
		}
		num += pZone.city.kingdom.hashcode;
		bool flag5 = false;
		if (pZone.city == this.highlight_city)
		{
			num++;
			flag5 = true;
		}
		if (pZone.last_drawn_id == num && pZone.last_drawn_hashcode == hashcode)
		{
			return;
		}
		pZone.last_drawn_id = num;
		pZone.last_drawn_hashcode = hashcode;
		this._dirty = true;
		for (int i = 0; i < pZone.tiles.Count; i++)
		{
			WorldTile worldTile = pZone.tiles[i];
			if (flag5)
			{
				this.pixels[worldTile.data.tile_id] = Toolbox.color_white_32;
			}
			else if (!worldTile.worldTileZoneBorder.border)
			{
				this.pixels[worldTile.data.tile_id] = color;
			}
			else if (worldTile.worldTileZoneBorder.borderUp && flag)
			{
				this.pixels[worldTile.data.tile_id] = color2;
			}
			else if (worldTile.worldTileZoneBorder.borderDown && flag2)
			{
				this.pixels[worldTile.data.tile_id] = color2;
			}
			else if (worldTile.worldTileZoneBorder.borderLeft && flag3)
			{
				this.pixels[worldTile.data.tile_id] = color2;
			}
			else if (worldTile.worldTileZoneBorder.borderRight && flag4)
			{
				this.pixels[worldTile.data.tile_id] = color2;
			}
			else
			{
				this.pixels[worldTile.data.tile_id] = color;
			}
		}
		this._debug_redrawn_last_amount++;
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x00062B0C File Offset: 0x00060D0C
	private void colorZone(TileZone pZone)
	{
		this.checkKingdom = true;
		switch (this._mode)
		{
		case ZoneDisplayMode.CityBorders:
			this.colorModCityBorders(pZone);
			break;
		case ZoneDisplayMode.Relations:
			this.colorModeRelations(pZone);
			break;
		case ZoneDisplayMode.Cultures:
			this.colorModeCultures(pZone);
			break;
		case ZoneDisplayMode.KingdomBorders:
			this.colorModKingdomBorders(pZone);
			break;
		}
		this._currentDrawnZones.Add(pZone);
		this._toCleanUp.Remove(pZone);
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x00062B7D File Offset: 0x00060D7D
	private bool isBorderColor_cultures(TileZone pZone, Culture pCulture, bool pCheckFriendly = false)
	{
		return pZone == null || pZone.culture == null || pZone.culture != pCulture;
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x00062B9C File Offset: 0x00060D9C
	private bool isBorderColor_relations(TileZone pZone, City pCity, bool pCheckFriendly = false)
	{
		return (pZone == null || !(pZone.city != pCity) || !(pZone.city != null) || pZone.city.kingdom != pCity.kingdom) && (pZone == null || pZone.city == null || pZone.city.kingdom != pCity.kingdom);
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x00062C08 File Offset: 0x00060E08
	private bool isBorderColor_kingdoms(TileZone pZone, City pCity, bool pCheckFriendly = false)
	{
		if (pCheckFriendly && pZone != null && pZone.city != pCity && pZone.city != null && this.checkKingdom && pZone.city.kingdom == pCity.kingdom)
		{
			return false;
		}
		if (this.checkKingdom)
		{
			return pZone == null || pZone.city == null || pZone.city.kingdom != pCity.kingdom;
		}
		return pZone == null || pZone.city != pCity;
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x00062C98 File Offset: 0x00060E98
	private bool isBorderColor_cities(TileZone pZone, City pCity, bool pCheckFriendly = false)
	{
		if (pCheckFriendly && pZone != null && pZone.city != pCity && pZone.city != null && this.checkKingdom && pZone.city.kingdom == pCity.kingdom)
		{
			return false;
		}
		if (this.checkKingdom)
		{
			return pZone == null || pZone.city == null || pZone.city.kingdom != pCity.kingdom || pZone.city != pCity;
		}
		return pZone == null || pZone.city != pCity;
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x00062D30 File Offset: 0x00060F30
	private void checkHighlight()
	{
		this.highlight_kingdom = null;
		this.highlight_city = null;
		this.highlight_culture = null;
		if (ScrollWindow.isWindowActive())
		{
			return;
		}
		if (this.world.isSelectedPowerAny() && this.world.isPowerForceMapMode(MapMode.None))
		{
			return;
		}
		if (Input.mousePresent && this.world.qualityChanger.lowRes)
		{
			WorldTile mouseTilePos = this.world.getMouseTilePos();
			MapMode mapMode = this.getCurrentMode();
			if (this.world.isPowerForceMapMode(MapMode.Villages))
			{
				mapMode = MapMode.Villages;
			}
			if (mouseTilePos != null)
			{
				if (mapMode == MapMode.Cultures && mouseTilePos.zone.culture != null)
				{
					this.highlight_culture = mouseTilePos.zone.culture;
					this._redraw_timer = 0f;
					return;
				}
				if (mapMode == MapMode.Kingdoms && mouseTilePos.zone.city != null)
				{
					this.highlight_kingdom = mouseTilePos.zone.city.kingdom;
					this._redraw_timer = 0f;
					return;
				}
				if (mapMode == MapMode.Villages && mouseTilePos.zone.city != null)
				{
					this.highlight_city = mouseTilePos.zone.city;
					this._redraw_timer = 0f;
					return;
				}
				this._redraw_timer = 0f;
			}
		}
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x00062E64 File Offset: 0x00061064
	public MapMode getCurrentMode()
	{
		MapMode mapMode = this.world.getForcedMapMode(MapMode.None);
		if (mapMode == MapMode.None)
		{
			if (this.world.showCultureZones())
			{
				mapMode = MapMode.Cultures;
			}
			else if (this.world.showKingdomZones())
			{
				mapMode = MapMode.Kingdoms;
			}
			else if (this.world.showCityZones())
			{
				mapMode = MapMode.Villages;
			}
		}
		return mapMode;
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x00062EB8 File Offset: 0x000610B8
	private void redrawZones()
	{
		if (this._lastSelectedKingdom != Config.selectedKingdom)
		{
			this._lastSelectedKingdom = Config.selectedKingdom;
			this._redraw_timer = 0f;
		}
		this.checkHighlight();
		if (this._redraw_timer > 0f)
		{
			this._redraw_timer -= Time.deltaTime;
			return;
		}
		this._redraw_timer = 0.3f;
		this._dirty = false;
		this._debug_redrawn_last_amount = 0;
		if (this._currentDrawnZones.Any<TileZone>())
		{
			this._toCleanUp.UnionWith(this._currentDrawnZones);
		}
		if (this.sprRnd.enabled)
		{
			switch (this._mode)
			{
			case ZoneDisplayMode.CityBorders:
				for (int i = 0; i < this.world.citiesList.Count; i++)
				{
					City city = this.world.citiesList[i];
					for (int j = 0; j < city.zones.Count; j++)
					{
						TileZone pZone = city.zones[j];
						this.colorZone(pZone);
					}
				}
				break;
			case ZoneDisplayMode.Relations:
				for (int k = 0; k < this.world.citiesList.Count; k++)
				{
					City city2 = this.world.citiesList[k];
					for (int l = 0; l < city2.zones.Count; l++)
					{
						TileZone pZone2 = city2.zones[l];
						this.colorZone(pZone2);
					}
				}
				break;
			case ZoneDisplayMode.Cultures:
				for (int m = 0; m < this.world.cultures.list.Count; m++)
				{
					foreach (TileZone pZone3 in this.world.cultures.list[m].zones)
					{
						this.colorZone(pZone3);
					}
				}
				break;
			case ZoneDisplayMode.KingdomBorders:
				for (int n = 0; n < this.world.kingdoms.list.Count; n++)
				{
					Kingdom kingdom = this.world.kingdoms.list[n];
					for (int num = 0; num < kingdom.cities.Count; num++)
					{
						City city3 = kingdom.cities[num];
						for (int num2 = 0; num2 < city3.zones.Count; num2++)
						{
							TileZone pZone4 = city3.zones[num2];
							this.colorZone(pZone4);
						}
					}
				}
				break;
			}
		}
		if (this._toCleanUp.Any<TileZone>())
		{
			this.clearDrawnZones();
		}
		if (this._dirty)
		{
			base.updatePixels();
		}
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x00063188 File Offset: 0x00061388
	private void clearDrawnZones()
	{
		foreach (TileZone tileZone in this._toCleanUp)
		{
			this.colorZone(tileZone, Toolbox.clear);
			tileZone.last_drawn_id = 0;
			tileZone.last_drawn_hashcode = 0;
			this._currentDrawnZones.Remove(tileZone);
		}
		this._toCleanUp.Clear();
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x00063208 File Offset: 0x00061408
	private void colorZone(TileZone pZone, Color32 pColor)
	{
		this._dirty = true;
		for (int i = 0; i < pZone.tiles.Count; i++)
		{
			WorldTile worldTile = pZone.tiles[i];
			this.pixels[worldTile.data.tile_id] = pColor;
		}
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x00063258 File Offset: 0x00061458
	public void debug(DebugTool pTool)
	{
		if (this._debug_redrawn_last_amount != 0)
		{
			this._debug_redrawn_last = this._debug_redrawn_last_amount;
		}
		pTool.setText("_toCleanUp", this._toCleanUp.Count);
		pTool.setText("_lastDrawnZones", this._currentDrawnZones.Count);
		pTool.setText("redrawn_last", this._debug_redrawn_last);
		pTool.setSeparator();
	}

	// Token: 0x04000BE2 RID: 3042
	public Color color1 = Color.gray;

	// Token: 0x04000BE3 RID: 3043
	public Color color2 = Color.white;

	// Token: 0x04000BE4 RID: 3044
	public RelationColor color_selected;

	// Token: 0x04000BE5 RID: 3045
	public RelationColor color_ally;

	// Token: 0x04000BE6 RID: 3046
	public RelationColor color_enemy;

	// Token: 0x04000BE7 RID: 3047
	public List<TileZone> zones = new List<TileZone>();

	// Token: 0x04000BE8 RID: 3048
	internal Dictionary<int, TileZone> zonesDict_id = new Dictionary<int, TileZone>();

	// Token: 0x04000BE9 RID: 3049
	internal TileZone[,] map;

	// Token: 0x04000BEA RID: 3050
	private bool checkKingdom;

	// Token: 0x04000BEB RID: 3051
	private bool _dirty;

	// Token: 0x04000BEC RID: 3052
	private Kingdom _lastSelectedKingdom;

	// Token: 0x04000BED RID: 3053
	private HashSetTileZone _currentDrawnZones = new HashSetTileZone();

	// Token: 0x04000BEE RID: 3054
	private HashSetTileZone _toCleanUp = new HashSetTileZone();

	// Token: 0x04000BEF RID: 3055
	private int _debug_redrawn_last_amount;

	// Token: 0x04000BF0 RID: 3056
	private int _debug_redrawn_last;

	// Token: 0x04000BF1 RID: 3057
	private float _redraw_timer;

	// Token: 0x04000BF2 RID: 3058
	private bool _drawZones_dirty;

	// Token: 0x04000BF3 RID: 3059
	private ZoneDisplayMode _mode;

	// Token: 0x04000BF4 RID: 3060
	internal int totalZonesX;

	// Token: 0x04000BF5 RID: 3061
	internal int totalZonesY;

	// Token: 0x04000BF6 RID: 3062
	private Kingdom highlight_kingdom;

	// Token: 0x04000BF7 RID: 3063
	private City highlight_city;

	// Token: 0x04000BF8 RID: 3064
	private Culture highlight_culture;
}
