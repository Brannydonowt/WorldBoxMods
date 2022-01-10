using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;
using UnityEngine;

// Token: 0x0200003D RID: 61
[ObfuscateLiterals]
public class PowerLibrary : AssetLibrary<GodPower>
{
	// Token: 0x06000179 RID: 377 RVA: 0x0001B7D4 File Offset: 0x000199D4
	public override void init()
	{
		base.init();
		this.add(new GodPower
		{
			id = "citySelect",
			name = "Select City",
			force_map_text = MapMode.Villages,
			icon = "iconCityInspect"
		});
		this.t.tester_enabled = false;
		GodPower t = this.t;
		t.click_action = (PowerActionWithID)Delegate.Combine(t.click_action, new PowerActionWithID(ActionLibrary.inspectCity));
		this.add(new GodPower
		{
			id = "relations",
			name = "Relations",
			force_map_text = MapMode.Villages,
			icon = "iconDiplomacy"
		});
		this.t.tester_enabled = false;
		this.add(new GodPower
		{
			id = "inspect",
			name = "Inspect"
		});
		this.t.tester_enabled = false;
		GodPower t2 = this.t;
		t2.click_action = (PowerActionWithID)Delegate.Combine(t2.click_action, new PowerActionWithID(ActionLibrary.inspectUnit));
		this.t.allow_unit_selection = true;
		this.add(new GodPower
		{
			id = "infinityCoin",
			name = "Infinity Coin"
		});
		GodPower t3 = this.t;
		t3.click_action = (PowerActionWithID)Delegate.Combine(t3.click_action, new PowerActionWithID(this.spawnInfinityCoin));
		this.add(new GodPower
		{
			id = "mapNames",
			name = "Map Names",
			unselectWhenWindow = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "map_names";
		GodPower t4 = this.t;
		t4.toggle_action = (PowerToggleAction)Delegate.Combine(t4.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "cityZones",
			name = "Village Layer",
			unselectWhenWindow = true
		});
		this.t.tester_enabled = false;
		this.t.map_modes_switch = true;
		this.t.toggle_name = "map_city_zones";
		GodPower t5 = this.t;
		t5.toggle_action = (PowerToggleAction)Delegate.Combine(t5.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "culture_zones",
			name = "Culture Layer",
			unselectWhenWindow = true
		});
		this.t.tester_enabled = false;
		this.t.map_modes_switch = true;
		this.t.toggle_name = "map_culture_zones";
		GodPower t6 = this.t;
		t6.toggle_action = (PowerToggleAction)Delegate.Combine(t6.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "kingdom_zones",
			name = "Kingdom Layer",
			unselectWhenWindow = true
		});
		this.t.tester_enabled = false;
		this.t.map_modes_switch = true;
		this.t.toggle_name = "map_kingdom_zones";
		GodPower t7 = this.t;
		t7.toggle_action = (PowerToggleAction)Delegate.Combine(t7.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "kingsAndLeaders",
			name = "Kings and Leaders",
			unselectWhenWindow = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "map_kings_leaders";
		GodPower t8 = this.t;
		t8.toggle_action = (PowerToggleAction)Delegate.Combine(t8.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "marks_boats",
			name = "Boats",
			unselectWhenWindow = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "marks_boats";
		GodPower t9 = this.t;
		t9.toggle_action = (PowerToggleAction)Delegate.Combine(t9.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "historyLog",
			name = "History Log",
			unselectWhenWindow = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "history_log";
		GodPower t10 = this.t;
		t10.toggle_action = (PowerToggleAction)Delegate.Combine(t10.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "pause",
			name = "Pause",
			unselectWhenWindow = true
		});
		this.t.tester_enabled = false;
		this.add(new GodPower
		{
			id = "clock",
			name = "Clock",
			unselectWhenWindow = true,
			requiresPremium = true,
			rank = PowerRank.Rank0_free
		});
		this.t.tester_enabled = false;
		this.t.allow_unit_selection = true;
		this.add(new GodPower
		{
			id = "follow_unit",
			name = "Follow Unit",
			unselectWhenWindow = true
		});
		this.t.tester_enabled = false;
		this.add(new GodPower
		{
			id = "_terraformTiles",
			drawLines = true,
			type = PowerActionType.Tile,
			rank = PowerRank.Rank0_free,
			showToolSizes = true,
			unselectWhenWindow = true,
			holdAction = true,
			clickInterval = 0f
		});
		this.clone("fuse", "_terraformTiles");
		this.t.name = "Fuse";
		this.t.topTileType = "fuse";
		GodPower t11 = this.t;
		t11.click_action = (PowerActionWithID)Delegate.Combine(t11.click_action, new PowerActionWithID(this.drawTiles));
		GodPower t12 = this.t;
		t12.click_action = (PowerActionWithID)Delegate.Combine(t12.click_action, new PowerActionWithID(this.cleanBurnedTile));
		GodPower t13 = this.t;
		t13.click_action = (PowerActionWithID)Delegate.Combine(t13.click_action, new PowerActionWithID(this.stopFire));
		GodPower t14 = this.t;
		t14.click_action = (PowerActionWithID)Delegate.Combine(t14.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t15 = this.t;
		t15.click_action = (PowerActionWithID)Delegate.Combine(t15.click_action, new PowerActionWithID(this.flashPixel));
		GodPower t16 = this.t;
		t16.click_brush_action = (PowerActionWithID)Delegate.Combine(t16.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		this.clone("tileDeepOcean", "_terraformTiles");
		this.t.name = "Deep Ocean";
		this.t.tileType = "pit_deep_ocean";
		this.t.icon = "iconTileDeepOcean";
		GodPower t17 = this.t;
		t17.click_action = (PowerActionWithID)Delegate.Combine(t17.click_action, new PowerActionWithID(this.drawTiles));
		GodPower t18 = this.t;
		t18.click_action = (PowerActionWithID)Delegate.Combine(t18.click_action, new PowerActionWithID(this.cleanBurnedTile));
		GodPower t19 = this.t;
		t19.click_action = (PowerActionWithID)Delegate.Combine(t19.click_action, new PowerActionWithID(this.stopFire));
		GodPower t20 = this.t;
		t20.click_action = (PowerActionWithID)Delegate.Combine(t20.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t21 = this.t;
		t21.click_brush_action = (PowerActionWithID)Delegate.Combine(t21.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		this.clone("tileCloseOcean", "_terraformTiles");
		this.t.name = "Close Ocean";
		this.t.tileType = "pit_close_ocean";
		this.t.icon = "iconTileCloseOcean";
		GodPower t22 = this.t;
		t22.click_action = (PowerActionWithID)Delegate.Combine(t22.click_action, new PowerActionWithID(this.drawTiles));
		GodPower t23 = this.t;
		t23.click_action = (PowerActionWithID)Delegate.Combine(t23.click_action, new PowerActionWithID(this.cleanBurnedTile));
		GodPower t24 = this.t;
		t24.click_action = (PowerActionWithID)Delegate.Combine(t24.click_action, new PowerActionWithID(this.stopFire));
		GodPower t25 = this.t;
		t25.click_action = (PowerActionWithID)Delegate.Combine(t25.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t26 = this.t;
		t26.click_brush_action = (PowerActionWithID)Delegate.Combine(t26.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		this.clone("tileShallowWaters", "_terraformTiles");
		this.t.name = "Shallow Waters";
		this.t.tileType = "pit_shallow_waters";
		this.t.icon = "iconTileShallowWater";
		GodPower t27 = this.t;
		t27.click_action = (PowerActionWithID)Delegate.Combine(t27.click_action, new PowerActionWithID(this.drawTiles));
		GodPower t28 = this.t;
		t28.click_action = (PowerActionWithID)Delegate.Combine(t28.click_action, new PowerActionWithID(this.cleanBurnedTile));
		GodPower t29 = this.t;
		t29.click_action = (PowerActionWithID)Delegate.Combine(t29.click_action, new PowerActionWithID(this.stopFire));
		GodPower t30 = this.t;
		t30.click_action = (PowerActionWithID)Delegate.Combine(t30.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t31 = this.t;
		t31.click_brush_action = (PowerActionWithID)Delegate.Combine(t31.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		this.clone("tileSand", "_terraformTiles");
		this.t.name = "Sand";
		this.t.tileType = "sand";
		this.t.icon = "iconTileSand";
		GodPower t32 = this.t;
		t32.click_action = (PowerActionWithID)Delegate.Combine(t32.click_action, new PowerActionWithID(this.drawTiles));
		GodPower t33 = this.t;
		t33.click_action = (PowerActionWithID)Delegate.Combine(t33.click_action, new PowerActionWithID(this.cleanBurnedTile));
		GodPower t34 = this.t;
		t34.click_action = (PowerActionWithID)Delegate.Combine(t34.click_action, new PowerActionWithID(this.stopFire));
		GodPower t35 = this.t;
		t35.click_action = (PowerActionWithID)Delegate.Combine(t35.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t36 = this.t;
		t36.click_brush_action = (PowerActionWithID)Delegate.Combine(t36.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		this.clone("tileSoil", "_terraformTiles");
		this.t.name = "Soil";
		this.t.tileType = "soil_low";
		this.t.icon = "iconTileSoil";
		GodPower t37 = this.t;
		t37.click_action = (PowerActionWithID)Delegate.Combine(t37.click_action, new PowerActionWithID(this.drawTiles));
		GodPower t38 = this.t;
		t38.click_action = (PowerActionWithID)Delegate.Combine(t38.click_action, new PowerActionWithID(this.cleanBurnedTile));
		GodPower t39 = this.t;
		t39.click_action = (PowerActionWithID)Delegate.Combine(t39.click_action, new PowerActionWithID(this.stopFire));
		GodPower t40 = this.t;
		t40.click_action = (PowerActionWithID)Delegate.Combine(t40.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t41 = this.t;
		t41.click_brush_action = (PowerActionWithID)Delegate.Combine(t41.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		this.clone("tileHighSoil", "_terraformTiles");
		this.t.name = "Soil High";
		this.t.tileType = "soil_high";
		this.t.icon = "iconTileHighSoil";
		GodPower t42 = this.t;
		t42.click_action = (PowerActionWithID)Delegate.Combine(t42.click_action, new PowerActionWithID(this.drawTiles));
		GodPower t43 = this.t;
		t43.click_action = (PowerActionWithID)Delegate.Combine(t43.click_action, new PowerActionWithID(this.cleanBurnedTile));
		GodPower t44 = this.t;
		t44.click_action = (PowerActionWithID)Delegate.Combine(t44.click_action, new PowerActionWithID(this.stopFire));
		GodPower t45 = this.t;
		t45.click_action = (PowerActionWithID)Delegate.Combine(t45.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t46 = this.t;
		t46.click_brush_action = (PowerActionWithID)Delegate.Combine(t46.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		this.clone("tileHills", "_terraformTiles");
		this.t.name = "Hills";
		this.t.tileType = "hills";
		this.t.icon = "iconTileHills";
		GodPower t47 = this.t;
		t47.click_action = (PowerActionWithID)Delegate.Combine(t47.click_action, new PowerActionWithID(this.drawTiles));
		GodPower t48 = this.t;
		t48.click_action = (PowerActionWithID)Delegate.Combine(t48.click_action, new PowerActionWithID(this.cleanBurnedTile));
		GodPower t49 = this.t;
		t49.click_action = (PowerActionWithID)Delegate.Combine(t49.click_action, new PowerActionWithID(this.stopFire));
		GodPower t50 = this.t;
		t50.click_action = (PowerActionWithID)Delegate.Combine(t50.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t51 = this.t;
		t51.click_brush_action = (PowerActionWithID)Delegate.Combine(t51.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		this.clone("tileMountains", "_terraformTiles");
		this.t.name = "Mountains";
		this.t.tileType = "mountains";
		this.t.icon = "iconTileMountains";
		GodPower t52 = this.t;
		t52.click_action = (PowerActionWithID)Delegate.Combine(t52.click_action, new PowerActionWithID(this.drawTiles));
		GodPower t53 = this.t;
		t53.click_action = (PowerActionWithID)Delegate.Combine(t53.click_action, new PowerActionWithID(this.cleanBurnedTile));
		GodPower t54 = this.t;
		t54.click_action = (PowerActionWithID)Delegate.Combine(t54.click_action, new PowerActionWithID(this.stopFire));
		GodPower t55 = this.t;
		t55.click_action = (PowerActionWithID)Delegate.Combine(t55.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t56 = this.t;
		t56.click_brush_action = (PowerActionWithID)Delegate.Combine(t56.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		this.clone("showelPlus", "_terraformTiles");
		this.t.name = "Shovel Plus";
		this.t.icon = "iconShowelPlus";
		this.t.clickInterval = 0.1f;
		GodPower t57 = this.t;
		t57.click_action = (PowerActionWithID)Delegate.Combine(t57.click_action, new PowerActionWithID(this.drawShowelPlus));
		GodPower t58 = this.t;
		t58.click_action = (PowerActionWithID)Delegate.Combine(t58.click_action, new PowerActionWithID(this.cleanBurnedTile));
		GodPower t59 = this.t;
		t59.click_action = (PowerActionWithID)Delegate.Combine(t59.click_action, new PowerActionWithID(this.stopFire));
		GodPower t60 = this.t;
		t60.click_action = (PowerActionWithID)Delegate.Combine(t60.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t61 = this.t;
		t61.click_action = (PowerActionWithID)Delegate.Combine(t61.click_action, new PowerActionWithID(this.flashPixel));
		GodPower t62 = this.t;
		t62.click_brush_action = (PowerActionWithID)Delegate.Combine(t62.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		this.clone("showelMinus", "showelPlus");
		this.t.name = "Shovel Minus";
		this.t.icon = "iconShowelMinuss";
		GodPower t63 = this.t;
		t63.click_action = (PowerActionWithID)Delegate.Combine(t63.click_action, new PowerActionWithID(this.drawShowelMinus));
		GodPower t64 = this.t;
		t64.click_action = (PowerActionWithID)Delegate.Combine(t64.click_action, new PowerActionWithID(this.cleanBurnedTile));
		GodPower t65 = this.t;
		t65.click_action = (PowerActionWithID)Delegate.Combine(t65.click_action, new PowerActionWithID(this.stopFire));
		GodPower t66 = this.t;
		t66.click_action = (PowerActionWithID)Delegate.Combine(t66.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t67 = this.t;
		t67.click_action = (PowerActionWithID)Delegate.Combine(t67.click_action, new PowerActionWithID(this.flashPixel));
		GodPower t68 = this.t;
		t68.click_brush_action = (PowerActionWithID)Delegate.Combine(t68.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		this.clone("vortex", "_terraformTiles");
		this.t.name = "Vortex";
		this.t.icon = "iconVertex2";
		GodPower t69 = this.t;
		t69.click_action = (PowerActionWithID)Delegate.Combine(t69.click_action, new PowerActionWithID(this.stopFire));
		GodPower t70 = this.t;
		t70.click_action = (PowerActionWithID)Delegate.Combine(t70.click_action, new PowerActionWithID(this.useVortex));
		GodPower t71 = this.t;
		t71.click_action = (PowerActionWithID)Delegate.Combine(t71.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t72 = this.t;
		t72.click_action = (PowerActionWithID)Delegate.Combine(t72.click_action, new PowerActionWithID(this.flashPixel));
		this.clone("_drawOnTile", "_terraformTiles");
		this.clone("greyGoo", "_drawOnTile");
		this.t.name = "Grey Goo";
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank3_good;
		GodPower t73 = this.t;
		t73.click_action = (PowerActionWithID)Delegate.Combine(t73.click_action, new PowerActionWithID(this.drawGreyGoo));
		GodPower t74 = this.t;
		t74.click_action = (PowerActionWithID)Delegate.Combine(t74.click_action, new PowerActionWithID(this.stopFire));
		GodPower t75 = this.t;
		t75.click_action = (PowerActionWithID)Delegate.Combine(t75.click_action, new PowerActionWithID(this.flashPixel));
		GodPower t76 = this.t;
		t76.click_brush_action = (PowerActionWithID)Delegate.Combine(t76.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		this.clone("conway", "_drawOnTile");
		this.t.name = "Conway game of Life1";
		GodPower t77 = this.t;
		t77.click_action = (PowerActionWithID)Delegate.Combine(t77.click_action, new PowerActionWithID(this.drawConway));
		GodPower t78 = this.t;
		t78.click_action = (PowerActionWithID)Delegate.Combine(t78.click_action, new PowerActionWithID(this.stopFire));
		GodPower t79 = this.t;
		t79.click_action = (PowerActionWithID)Delegate.Combine(t79.click_action, new PowerActionWithID(this.flashPixel));
		GodPower t80 = this.t;
		t80.click_brush_action = (PowerActionWithID)Delegate.Combine(t80.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		this.clone("conwayInverse", "conway");
		this.t.name = "Conway game of Life2";
		GodPower t81 = this.t;
		t81.click_action = (PowerActionWithID)Delegate.Combine(t81.click_action, new PowerActionWithID(this.drawConwayInverse));
		GodPower t82 = this.t;
		t82.click_action = (PowerActionWithID)Delegate.Combine(t82.click_action, new PowerActionWithID(this.stopFire));
		GodPower t83 = this.t;
		t83.click_action = (PowerActionWithID)Delegate.Combine(t83.click_action, new PowerActionWithID(this.flashPixel));
		GodPower t84 = this.t;
		t84.click_brush_action = (PowerActionWithID)Delegate.Combine(t84.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		this.clone("finger", "_drawOnTile");
		this.t.name = "Finger";
		this.t.icon = "iconTileFinger";
		GodPower t85 = this.t;
		t85.click_action = (PowerActionWithID)Delegate.Combine(t85.click_action, new PowerActionWithID(this.drawFinger));
		GodPower t86 = this.t;
		t86.click_action = (PowerActionWithID)Delegate.Combine(t86.click_action, new PowerActionWithID(this.cleanBurnedTile));
		GodPower t87 = this.t;
		t87.click_action = (PowerActionWithID)Delegate.Combine(t87.click_action, new PowerActionWithID(this.stopFire));
		GodPower t88 = this.t;
		t88.click_action = (PowerActionWithID)Delegate.Combine(t88.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t89 = this.t;
		t89.click_action = (PowerActionWithID)Delegate.Combine(t89.click_action, new PowerActionWithID(this.flashPixel));
		GodPower t90 = this.t;
		t90.click_brush_action = (PowerActionWithID)Delegate.Combine(t90.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		this.clone("lifeEraser", "_drawOnTile");
		GodPower t91 = this.t;
		t91.click_brush_action = (PowerActionWithID)Delegate.Combine(t91.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		GodPower t92 = this.t;
		t92.click_action = (PowerActionWithID)Delegate.Combine(t92.click_action, new PowerActionWithID(this.drawLifeEraser));
		GodPower t93 = this.t;
		t93.click_action = (PowerActionWithID)Delegate.Combine(t93.click_action, new PowerActionWithID(this.flashPixel));
		this.t.name = "Life Eraser";
		this.clone("demolish", "_drawOnTile");
		GodPower t94 = this.t;
		t94.click_brush_action = (PowerActionWithID)Delegate.Combine(t94.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		GodPower t95 = this.t;
		t95.click_action = (PowerActionWithID)Delegate.Combine(t95.click_action, new PowerActionWithID(this.drawDemolish));
		GodPower t96 = this.t;
		t96.click_action = (PowerActionWithID)Delegate.Combine(t96.click_action, new PowerActionWithID(this.flashPixel));
		this.t.name = "Demolish";
		this.clone("sponge", "_drawOnTile");
		this.t.icon = "iconSponge";
		GodPower t97 = this.t;
		t97.click_brush_action = (PowerActionWithID)Delegate.Combine(t97.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		GodPower t98 = this.t;
		t98.click_action = (PowerActionWithID)Delegate.Combine(t98.click_action, new PowerActionWithID(this.drawSponge));
		GodPower t99 = this.t;
		t99.click_action = (PowerActionWithID)Delegate.Combine(t99.click_action, new PowerActionWithID(this.removeRuins));
		GodPower t100 = this.t;
		t100.click_action = (PowerActionWithID)Delegate.Combine(t100.click_action, new PowerActionWithID(this.cleanBurnedTile));
		GodPower t101 = this.t;
		t101.click_action = (PowerActionWithID)Delegate.Combine(t101.click_action, new PowerActionWithID(this.stopFire));
		GodPower t102 = this.t;
		t102.click_action = (PowerActionWithID)Delegate.Combine(t102.click_action, new PowerActionWithID(this.flashPixel));
		this.t.name = "Sponge";
		this.clone("sickle", "sponge");
		this.t.icon = "iconSickle";
		GodPower t103 = this.t;
		t103.click_brush_action = (PowerActionWithID)Delegate.Combine(t103.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		GodPower t104 = this.t;
		t104.click_action = (PowerActionWithID)Delegate.Combine(t104.click_action, new PowerActionWithID(this.drawSickle));
		GodPower t105 = this.t;
		t105.click_action = (PowerActionWithID)Delegate.Combine(t105.click_action, new PowerActionWithID(this.flashPixel));
		this.t.name = "Sickle";
		this.clone("axe", "sponge");
		GodPower t106 = this.t;
		t106.click_brush_action = (PowerActionWithID)Delegate.Combine(t106.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		GodPower t107 = this.t;
		t107.click_action = (PowerActionWithID)Delegate.Combine(t107.click_action, new PowerActionWithID(this.drawAxe));
		GodPower t108 = this.t;
		t108.click_action = (PowerActionWithID)Delegate.Combine(t108.click_action, new PowerActionWithID(this.flashPixel));
		this.t.name = "Axe";
		this.clone("bucket", "sponge");
		GodPower t109 = this.t;
		t109.click_brush_action = (PowerActionWithID)Delegate.Combine(t109.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		GodPower t110 = this.t;
		t110.click_action = (PowerActionWithID)Delegate.Combine(t110.click_action, new PowerActionWithID(this.drawBucket));
		GodPower t111 = this.t;
		t111.click_action = (PowerActionWithID)Delegate.Combine(t111.click_action, new PowerActionWithID(this.flashPixel));
		this.t.name = "Bucket";
		this.clone("pickaxe", "sponge");
		GodPower t112 = this.t;
		t112.click_brush_action = (PowerActionWithID)Delegate.Combine(t112.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		GodPower t113 = this.t;
		t113.click_action = (PowerActionWithID)Delegate.Combine(t113.click_action, new PowerActionWithID(this.drawPickaxe));
		GodPower t114 = this.t;
		t114.click_action = (PowerActionWithID)Delegate.Combine(t114.click_action, new PowerActionWithID(this.flashPixel));
		this.t.name = "Pickaxe";
		this.clone("divineLight", "sponge");
		GodPower t115 = this.t;
		t115.click_brush_action = (PowerActionWithID)Delegate.Combine(t115.click_brush_action, new PowerActionWithID(this.divineLightFX));
		GodPower t116 = this.t;
		t116.click_brush_action = (PowerActionWithID)Delegate.Combine(t116.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		GodPower t117 = this.t;
		t117.click_action = (PowerActionWithID)Delegate.Combine(t117.click_action, new PowerActionWithID(this.drawDivineLight));
		this.t.name = "Divine Light";
		this.t.showToolSizes = true;
		this.add(new GodPower
		{
			id = "_spawnActor",
			type = PowerActionType.SpawnActor,
			rank = PowerRank.Rank0_free,
			unselectWhenWindow = true,
			showSpawnEffect = "spawn",
			actorSpawnHeight = 3f
		});
		this.clone("humans", "_spawnActor");
		this.t.name = "Humans";
		this.t.spawnSound = "spawnHuman";
		this.t.actorStatsId = "unit_human";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("orcs", "_spawnActor");
		this.t.rank = PowerRank.Rank4_awesome;
		this.t.requiresPremium = true;
		this.t.name = "Orcs";
		this.t.spawnSound = "spawnOrc";
		this.t.actorStatsId = "unit_orc";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("elves", "_spawnActor");
		this.t.rank = PowerRank.Rank4_awesome;
		this.t.requiresPremium = true;
		this.t.name = "Elves";
		this.t.spawnSound = "spawnElf";
		this.t.actorStatsId = "unit_elf";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("dwarfs", "_spawnActor");
		this.t.rank = PowerRank.Rank4_awesome;
		this.t.requiresPremium = true;
		this.t.name = "Dwarves";
		this.t.spawnSound = "spawnDwarf";
		this.t.actorStatsId = "unit_dwarf";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("coldOnes", "_spawnActor");
		this.t.name = "Cold Ones";
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.spawnSound = "spawnColdOne";
		this.t.actorStatsId = "walker";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("demons", "_spawnActor");
		this.t.name = "Demon";
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.spawnSound = "spawnDemon";
		this.t.actorStatsId = "demon";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("necromancer", "_spawnActor");
		this.t.name = "Necromancer";
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.spawnSound = "spawnNecromancer";
		this.t.actorStatsId = "necromancer";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("druid", "_spawnActor");
		this.t.name = "Druid";
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.spawnSound = "spawnDruid";
		this.t.actorStatsId = "druid";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("plagueDoctor", "_spawnActor");
		this.t.name = "Plague Doctor";
		this.t.spawnSound = "spawnPlagueDoctor";
		this.t.actorStatsId = "plagueDoctor";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("evilMage", "_spawnActor");
		this.t.name = "Evil Mage";
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.spawnSound = "spawnEvilMage";
		this.t.actorStatsId = "evilMage";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("whiteMage", "_spawnActor");
		this.t.name = "White Mage";
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.spawnSound = "spawnWhiteMage";
		this.t.actorStatsId = "whiteMage";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("bandits", "_spawnActor");
		this.t.name = "Bandits";
		this.t.spawnSound = "spawnBandit";
		this.t.actorStatsId = "bandit";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("snowman", "_spawnActor");
		this.t.name = "Snowman";
		this.t.actorStatsId = "snowman";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("zombies", "_spawnActor");
		this.t.name = "Zombie";
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.spawnSound = "spawnZombie";
		this.t.actorStatsId = "zombie,zombie_orc,zombie_dwarf,zombie_elf";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("skeleton", "_spawnActor");
		this.t.name = "Skeleton";
		this.t.rank = PowerRank.Rank0_free;
		this.t.actorStatsId = "skeleton";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("sheep", "_spawnActor");
		this.t.name = "Sheeps";
		this.t.spawnSound = "sheep spawn";
		this.t.actorStatsId = "sheep";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("rhino", "_spawnActor");
		this.t.name = "Rhino";
		this.t.spawnSound = "rhino spawn";
		this.t.actorStatsId = "rhino";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("monkey", "_spawnActor");
		this.t.name = "Monkey";
		this.t.spawnSound = "monkey spawn";
		this.t.actorStatsId = "monkey";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("buffalo", "_spawnActor");
		this.t.name = "Buffalo";
		this.t.spawnSound = "buffalo spawn";
		this.t.actorStatsId = "buffalo";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("fox", "_spawnActor");
		this.t.name = "Fox";
		this.t.spawnSound = "fox spawn";
		this.t.actorStatsId = "fox";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("hyena", "_spawnActor");
		this.t.name = "Hyena";
		this.t.spawnSound = "hyena spawn";
		this.t.actorStatsId = "hyena";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("dog", "_spawnActor");
		this.t.name = "Dog";
		this.t.spawnSound = "dog spawn";
		this.t.actorStatsId = "dog";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("cow", "_spawnActor");
		this.t.name = "Cow";
		this.t.spawnSound = "cow spawn";
		this.t.actorStatsId = "cow";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("frog", "_spawnActor");
		this.t.name = "Frog";
		this.t.spawnSound = "frog spawn";
		this.t.actorStatsId = "frog";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("crocodile", "_spawnActor");
		this.t.name = "Crocodile";
		this.t.spawnSound = "frog spawn";
		this.t.actorStatsId = "crocodile";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("snake", "_spawnActor");
		this.t.name = "Snake";
		this.t.spawnSound = "snake spawn";
		this.t.actorStatsId = "snake";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("turtle", "_spawnActor");
		this.t.name = "Turtle";
		this.t.spawnSound = "turtle spawn";
		this.t.actorStatsId = "turtle";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("penguin", "_spawnActor");
		this.t.name = "Penguin";
		this.t.spawnSound = "penguin spawn";
		this.t.actorStatsId = "penguin";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("crab", "_spawnActor");
		this.t.name = "Crab";
		this.t.spawnSound = "crab spawn";
		this.t.actorStatsId = "crab";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("ratKing", "_spawnActor");
		this.t.name = "Rat King";
		this.t.actorStatsId = "ratKing";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("rabbit", "_spawnActor");
		this.t.name = "Rabbit";
		this.t.spawnSound = "rabbit spawn";
		this.t.actorStatsId = "rabbit";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("cat", "_spawnActor");
		this.t.name = "Cat";
		this.t.spawnSound = "cat spawn";
		this.t.actorStatsId = "cat";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("chicken", "_spawnActor");
		this.t.name = "Chicken";
		this.t.spawnSound = "chicken spawn";
		this.t.actorStatsId = "chicken";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("wolf", "_spawnActor");
		this.t.name = "Wolfs";
		this.t.spawnSound = "spawnWolf";
		this.t.actorStatsId = "wolf";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("bear", "_spawnActor");
		this.t.name = "Bear";
		this.t.spawnSound = "spawnBear";
		this.t.actorStatsId = "bear";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("piranha", "_spawnActor");
		this.t.name = "Piranha";
		this.t.spawnSound = "spawnPiranha";
		this.t.actorStatsId = "piranha";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("worm", "_spawnActor");
		this.t.name = "Worm";
		this.t.spawnSound = "spawnWorm";
		this.t.actorStatsId = "worm";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("sandSpider", "_spawnActor");
		this.t.name = "Sand Spider";
		this.t.spawnSound = "spawnAnt";
		this.t.actorStatsId = "sandSpider";
		this.t.holdAction = true;
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("robotSanta", "_spawnActor");
		this.t.name = "Robot Santa";
		this.t.spawnSound = "spawnSanta";
		this.t.actorStatsId = "santa";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("godFinger", "_spawnActor");
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.t.name = "God Finger";
		this.t.spawnSound = "spawnGodFinger";
		this.t.actorStatsId = "godFinger";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("ufo", "_spawnActor");
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.t.name = "UFO";
		this.t.spawnSound = "spawnUFO";
		this.t.actorStatsId = "UFO";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("dragon", "_spawnActor");
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.t.name = "Dragon";
		this.t.spawnSound = "spawnDragon";
		this.t.actorStatsId = "dragon";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("fairy", "_spawnActor");
		this.t.rank = PowerRank.Rank2_normal;
		this.t.name = "Fairy";
		this.t.actorStatsId = "fairy";
		this.t.requiresPremium = true;
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("butterfly", "_spawnActor");
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Butterfly";
		this.t.actorStatsId = "butterfly";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("grasshopper", "_spawnActor");
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Grasshopper";
		this.t.actorStatsId = "grasshopper";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("fly", "_spawnActor");
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Fly";
		this.t.actorStatsId = "fly";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("beetle", "_spawnActor");
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Beetle";
		this.t.actorStatsId = "beetle";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("rat", "_spawnActor");
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Rat";
		this.t.actorStatsId = "rat";
		this.t.requiresPremium = true;
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("antBlue", "_spawnActor");
		this.t.name = "Blue Ant";
		this.t.spawnSound = "spawnAnt";
		this.t.actorStatsId = "antBlue";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("antGreen", "_spawnActor");
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Green Ant";
		this.t.spawnSound = "spawnAnt";
		this.t.actorStatsId = "antGreen";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("antBlack", "_spawnActor");
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Black Ant";
		this.t.spawnSound = "spawnAnt";
		this.t.actorStatsId = "antBlack";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("antRed", "_spawnActor");
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Red Ant";
		this.t.spawnSound = "spawnAnt";
		this.t.actorStatsId = "antRed";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("bowlingBall", "_spawnActor");
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.t.name = "Bowling Ball";
		this.t.actorStatsId = "boulder";
		this.t.actorSpawnHeight = 30f;
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("tornado", "_spawnActor");
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.name = "Tornado";
		this.t.actorStatsId = "tornado";
		this.t.actorSpawnHeight = 0f;
		this.t.spawnSound = "tornado";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("crabzilla", "_spawnActor");
		this.t.name = "Crabzilla";
		this.t.rank = PowerRank.Rank4_awesome;
		this.t.requiresPremium = true;
		this.t.actorStatsId = "crabzilla";
		this.t.actorSpawnHeight = 0f;
		this.t.ignoreFastSpawn = true;
		this.t.tester_enabled = false;
		this.t.click_action = new PowerActionWithID(this.spawnCrabzilla);
		this.add(new GodPower
		{
			id = "heatray",
			name = "Heatray",
			requiresPremium = true,
			rank = PowerRank.Rank2_normal,
			unselectWhenWindow = true,
			holdAction = true
		});
		GodPower t118 = this.t;
		t118.click_brush_action = (PowerActionWithID)Delegate.Combine(t118.click_brush_action, new PowerActionWithID(this.heatrayFX));
		GodPower t119 = this.t;
		t119.click_action = (PowerActionWithID)Delegate.Combine(t119.click_action, new PowerActionWithID(this.drawHeatray));
		GodPower t120 = this.t;
		t120.click_action = (PowerActionWithID)Delegate.Combine(t120.click_action, new PowerActionWithID(this.flashPixel));
		this.add(new GodPower
		{
			id = "meteorite",
			name = "Meteorite",
			requiresPremium = true,
			rank = PowerRank.Rank3_good,
			unselectWhenWindow = true,
			showSpawnEffect = "spawn"
		});
		GodPower t121 = this.t;
		t121.click_action = (PowerActionWithID)Delegate.Combine(t121.click_action, new PowerActionWithID(this.spawnMeteorite));
		this.add(new GodPower
		{
			id = "lightning",
			name = "Lightning",
			unselectWhenWindow = true
		});
		GodPower t122 = this.t;
		t122.click_action = (PowerActionWithID)Delegate.Combine(t122.click_action, new PowerActionWithID(this.spawnLightning));
		this.add(new GodPower
		{
			id = "earthquake",
			name = "Earthquake",
			unselectWhenWindow = true
		});
		GodPower t123 = this.t;
		t123.click_action = (PowerActionWithID)Delegate.Combine(t123.click_action, new PowerActionWithID(this.spawnEarthquake));
		this.add(new GodPower
		{
			id = "magnet",
			name = "Magnet",
			showToolSizes = true,
			holdAction = true,
			highlight = true,
			unselectWhenWindow = true
		});
		this.t.click_action = new PowerActionWithID(this.useMagnet);
		this.add(new GodPower
		{
			id = "_spawnSpecial",
			name = "_spawnSpecial",
			unselectWhenWindow = true
		});
		this.clone("force", "_spawnSpecial");
		this.t.name = "Force";
		GodPower t124 = this.t;
		t124.click_action = (PowerActionWithID)Delegate.Combine(t124.click_action, new PowerActionWithID(this.spawnForce));
		this.clone("cloudRain", "_spawnSpecial");
		this.t.name = "Rain Cloud";
		this.t.click_action = new PowerActionWithID(this.spawnCloudRain);
		this.clone("cloudAcid", "_spawnSpecial");
		this.t.name = "Acid Cloud";
		this.t.click_action = new PowerActionWithID(this.spawnCloudAcid);
		this.clone("cloudLava", "_spawnSpecial");
		this.t.name = "Lava Cloud";
		this.t.click_action = new PowerActionWithID(this.spawnCloudLava);
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.clone("cloudSnow", "_spawnSpecial");
		this.t.name = "Snow Cloud";
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.t.click_action = new PowerActionWithID(this.spawnCloudSnow);
		this.add(new GodPower
		{
			id = "_printer",
			unselectWhenWindow = true,
			actorSpawnHeight = 3f,
			showSpawnEffect = "spawn"
		});
		this.clone("printer_hexagon", "_printer");
		this.t.name = "Printer - Hexagon";
		this.t.printersPrint = "hexagon";
		this.t.click_action = new PowerActionWithID(this.spawnPrinter);
		this.clone("printer_skull", "_printer");
		this.t.name = "Printer - Skull";
		this.t.printersPrint = "skull";
		this.t.click_action = new PowerActionWithID(this.spawnPrinter);
		this.clone("printer_squares", "_printer");
		this.t.name = "Printer - Squares";
		this.t.printersPrint = "squares";
		this.t.click_action = new PowerActionWithID(this.spawnPrinter);
		this.clone("printer_yinyang", "_printer");
		this.t.name = "Printer - Yin Yang";
		this.t.printersPrint = "yinyang";
		this.t.click_action = new PowerActionWithID(this.spawnPrinter);
		this.clone("printer_island1", "_printer");
		this.t.name = "Printer - Island";
		this.t.printersPrint = "island1";
		this.t.click_action = new PowerActionWithID(this.spawnPrinter);
		this.clone("printer_star", "_printer");
		this.t.name = "Printer - Star";
		this.t.printersPrint = "star";
		this.t.click_action = new PowerActionWithID(this.spawnPrinter);
		this.clone("printer_heart", "_printer");
		this.t.name = "Printer - Heart";
		this.t.printersPrint = "heart";
		this.t.click_action = new PowerActionWithID(this.spawnPrinter);
		this.clone("printer_diamond", "_printer");
		this.t.name = "Printer - Diamond";
		this.t.printersPrint = "diamond";
		this.t.click_action = new PowerActionWithID(this.spawnPrinter);
		this.add(new GodPower
		{
			id = "_drops",
			holdAction = true,
			showToolSizes = true,
			unselectWhenWindow = true,
			fallingChance = 0.05f
		});
		this.clone("tnt", "_drops");
		this.t.name = "TNT";
		this.t.dropID = "tnt";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t125 = this.t;
		t125.click_power_action = (PowerAction)Delegate.Combine(t125.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("tnt_timed", "_drops");
		this.t.name = "Timed TNT";
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.t.dropID = "tnt_timed";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t126 = this.t;
		t126.click_power_action = (PowerAction)Delegate.Combine(t126.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("water_bomb", "_drops");
		this.t.name = "Water Bomb";
		this.t.dropID = "water_bomb";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t127 = this.t;
		t127.click_power_action = (PowerAction)Delegate.Combine(t127.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("landmine", "_drops");
		this.t.name = "Landmine";
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.t.dropID = "landmine";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t128 = this.t;
		t128.click_power_action = (PowerAction)Delegate.Combine(t128.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("fireworks", "_drops");
		this.t.name = "Fireworks";
		this.t.dropID = "fireworks";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t129 = this.t;
		t129.click_power_action = (PowerAction)Delegate.Combine(t129.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("inspiration", "_drops");
		this.t.name = "Inspiration";
		this.t.dropID = "inspiration";
		this.t.icon = "iconInspiration";
		this.t.fallingChance = 0.01f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t130 = this.t;
		t130.click_power_action = (PowerAction)Delegate.Combine(t130.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("friendship", "_drops");
		this.t.name = "Friendship";
		this.t.icon = "iconFriendship";
		this.t.dropID = "friendship";
		this.t.fallingChance = 0.01f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t131 = this.t;
		t131.click_power_action = (PowerAction)Delegate.Combine(t131.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("spite", "_drops");
		this.t.name = "Spite";
		this.t.icon = "iconSprite";
		this.t.dropID = "spite";
		this.t.fallingChance = 0.01f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t132 = this.t;
		t132.click_power_action = (PowerAction)Delegate.Combine(t132.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("madness", "_drops");
		this.t.name = "Madness";
		this.t.fallingChance = 0.01f;
		this.t.dropID = "madness";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t133 = this.t;
		t133.click_power_action = (PowerAction)Delegate.Combine(t133.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("blessing", "_drops");
		this.t.name = "Blessing";
		this.t.dropID = "blessing";
		this.t.fallingChance = 0.01f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t134 = this.t;
		t134.click_power_action = (PowerAction)Delegate.Combine(t134.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("shield", "_drops");
		this.t.name = "Shield";
		this.t.dropID = "shield";
		this.t.fallingChance = 0.01f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t135 = this.t;
		t135.click_power_action = (PowerAction)Delegate.Combine(t135.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("curse", "_drops");
		this.t.name = "Curse";
		this.t.rank = PowerRank.Rank0_free;
		this.t.dropID = "curse";
		this.t.fallingChance = 0.01f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t136 = this.t;
		t136.click_power_action = (PowerAction)Delegate.Combine(t136.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("zombieInfection", "_drops");
		this.t.name = "Zombie Infection";
		this.t.fallingChance = 0.01f;
		this.t.rank = PowerRank.Rank3_good;
		this.t.dropID = "zombieInfection";
		this.t.requiresPremium = true;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t137 = this.t;
		t137.click_power_action = (PowerAction)Delegate.Combine(t137.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("mushSpores", "_drops");
		this.t.name = "Mush Spores";
		this.t.fallingChance = 0.01f;
		this.t.rank = PowerRank.Rank2_normal;
		this.t.dropID = "mushSpores";
		this.t.requiresPremium = true;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t138 = this.t;
		t138.click_power_action = (PowerAction)Delegate.Combine(t138.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("coffee", "_drops");
		this.t.name = "Coffee";
		this.t.fallingChance = 0.01f;
		this.t.rank = PowerRank.Rank1_common;
		this.t.dropID = "coffee";
		this.t.requiresPremium = true;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t139 = this.t;
		t139.click_power_action = (PowerAction)Delegate.Combine(t139.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("powerup", "_drops");
		this.t.name = "Powerup";
		this.t.fallingChance = 0.01f;
		this.t.rank = PowerRank.Rank1_common;
		this.t.dropID = "powerup";
		this.t.requiresPremium = true;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t140 = this.t;
		t140.click_power_action = (PowerAction)Delegate.Combine(t140.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("plague", "_drops");
		this.t.name = "Plague";
		this.t.dropID = "plague";
		this.t.fallingChance = 0.01f;
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t141 = this.t;
		t141.click_power_action = (PowerAction)Delegate.Combine(t141.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("livingPlants", "_drops");
		this.t.name = "Living Plants";
		this.t.dropID = "livingPlants";
		this.t.fallingChance = 0.01f;
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t142 = this.t;
		t142.click_power_action = (PowerAction)Delegate.Combine(t142.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("livingHouse", "_drops");
		this.t.name = "Living Houses";
		this.t.dropID = "livingHouse";
		this.t.fallingChance = 0.01f;
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t143 = this.t;
		t143.click_power_action = (PowerAction)Delegate.Combine(t143.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("bomb", "_drops");
		this.t.name = "Bomb";
		this.t.dropID = "bomb";
		this.t.fallingChance = 0.01f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t144 = this.t;
		t144.click_power_action = (PowerAction)Delegate.Combine(t144.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("grenade", "bomb");
		this.t.name = "Grenade";
		this.t.dropID = "grenade";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t145 = this.t;
		t145.click_power_action = (PowerAction)Delegate.Combine(t145.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("napalmBomb", "bomb");
		this.t.name = "Napalm Bomb";
		this.t.dropID = "napalmBomb";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t146 = this.t;
		t146.click_power_action = (PowerAction)Delegate.Combine(t146.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("atomicBomb", "_drops");
		this.t.name = "Atomic Bomb";
		this.t.dropID = "atomicBomb";
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t147 = this.t;
		t147.click_power_action = (PowerAction)Delegate.Combine(t147.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("antimatterBomb", "_drops");
		this.t.name = "Antimatter Bomb";
		this.t.dropID = "antimatterBomb";
		this.t.fallingChance = 0.01f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t148 = this.t;
		t148.click_power_action = (PowerAction)Delegate.Combine(t148.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("czarBomba", "_drops");
		this.t.name = "Tsar Bomba";
		this.t.dropID = "czarBomba";
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.t.fallingChance = 0.01f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t149 = this.t;
		t149.click_power_action = (PowerAction)Delegate.Combine(t149.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("rain", "_drops");
		this.t.dropID = "rain";
		this.t.name = "Rain";
		this.t.fallingChance = 0.02f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t150 = this.t;
		t150.click_power_action = (PowerAction)Delegate.Combine(t150.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("bloodRain", "_drops");
		this.t.dropID = "bloodRain";
		this.t.name = "Blood Rain";
		this.t.fallingChance = 0.02f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t151 = this.t;
		t151.click_power_action = (PowerAction)Delegate.Combine(t151.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("fire", "_drops");
		this.t.dropID = "fire";
		this.t.name = "Fire";
		this.t.fallingChance = 0.01f;
		this.t.particleInterval = 0.3f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t152 = this.t;
		t152.click_power_action = (PowerAction)Delegate.Combine(t152.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("snow", "_drops");
		this.t.dropID = "snow";
		this.t.name = "Snow";
		this.t.fallingChance = 0.03f;
		this.t.particleInterval = 0.15f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t153 = this.t;
		t153.click_power_action = (PowerAction)Delegate.Combine(t153.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("acid", "_drops");
		this.t.dropID = "acid";
		this.t.name = "Acid";
		this.t.fallingChance = 0.02f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t154 = this.t;
		t154.click_power_action = (PowerAction)Delegate.Combine(t154.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("lava", "_drops");
		this.t.dropID = "lava";
		this.t.name = "Lava";
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.t.fallingChance = 0.03f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t155 = this.t;
		t155.click_power_action = (PowerAction)Delegate.Combine(t155.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("seedsGrass", "_drops");
		this.t.dropID = "seedsGrass";
		this.t.name = "Grass Seeds";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t156 = this.t;
		t156.click_power_action = (PowerAction)Delegate.Combine(t156.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("seedsSavanna", "_drops");
		this.t.dropID = "seedsSavanna";
		this.t.name = "Savanna Seeds";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t157 = this.t;
		t157.click_power_action = (PowerAction)Delegate.Combine(t157.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("seedsEnchanted", "_drops");
		this.t.dropID = "seedsEnchanted";
		this.t.name = "Enchanted Seeds";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t158 = this.t;
		t158.click_power_action = (PowerAction)Delegate.Combine(t158.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("seedsCorrupted", "_drops");
		this.t.dropID = "seedsCorrupted";
		this.t.name = "Corrupted Seeds";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t159 = this.t;
		t159.click_power_action = (PowerAction)Delegate.Combine(t159.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("seedsMushroom", "_drops");
		this.t.dropID = "seedsMushroom";
		this.t.name = "Mushroom Seeds";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t160 = this.t;
		t160.click_power_action = (PowerAction)Delegate.Combine(t160.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("seedsSwamp", "_drops");
		this.t.dropID = "seedsSwamp";
		this.t.name = "Swamp Seeds";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t161 = this.t;
		t161.click_power_action = (PowerAction)Delegate.Combine(t161.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("seedsInfernal", "_drops");
		this.t.dropID = "seedsInfernal";
		this.t.name = "Infernal Seeds";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t162 = this.t;
		t162.click_power_action = (PowerAction)Delegate.Combine(t162.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("seedsJungle", "_drops");
		this.t.dropID = "seedsJungle";
		this.t.name = "Jungle Seeds";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t163 = this.t;
		t163.click_power_action = (PowerAction)Delegate.Combine(t163.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("fruitBush", "seedsGrass");
		this.t.dropID = "fruitBush";
		this.t.name = "Fruit Bush";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t164 = this.t;
		t164.click_power_action = (PowerAction)Delegate.Combine(t164.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("fertilizerPlants", "seedsGrass");
		this.t.dropID = "fertilizerPlants";
		this.t.name = "Plants Fertilizer";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t165 = this.t;
		t165.click_power_action = (PowerAction)Delegate.Combine(t165.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("fertilizerTrees", "seedsGrass");
		this.t.dropID = "fertilizerTrees";
		this.t.name = "Trees Fertilizer";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t166 = this.t;
		t166.click_power_action = (PowerAction)Delegate.Combine(t166.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("_dropBuilding", "_drops");
		this.t.showToolSizes = false;
		this.t.forceBrush = "circ_1";
		this.t.fallingChance = 0.03f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t167 = this.t;
		t167.click_power_action = (PowerAction)Delegate.Combine(t167.click_power_action, new PowerAction(this.flashPixel));
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPower);
		this.clone("stone", "_dropBuilding");
		this.t.dropID = this.t.id;
		this.t.name = "Stone";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t168 = this.t;
		t168.click_power_action = (PowerAction)Delegate.Combine(t168.click_power_action, new PowerAction(this.flashPixel));
		this.t.forceBrush = "circ_0";
		this.clone("ore_deposit", "_dropBuilding");
		this.t.dropID = this.t.id;
		this.t.name = "Ore Deposit";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t169 = this.t;
		t169.click_power_action = (PowerAction)Delegate.Combine(t169.click_power_action, new PowerAction(this.flashPixel));
		this.t.forceBrush = "circ_0";
		this.clone("gold", "_dropBuilding");
		this.t.dropID = this.t.id;
		this.t.name = "Gold";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t170 = this.t;
		t170.click_power_action = (PowerAction)Delegate.Combine(t170.click_power_action, new PowerAction(this.flashPixel));
		this.t.forceBrush = "circ_0";
		this.clone("tumor", "_dropBuilding");
		this.t.name = "Tumor";
		this.t.dropID = this.t.id;
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t171 = this.t;
		t171.click_power_action = (PowerAction)Delegate.Combine(t171.click_power_action, new PowerAction(this.flashPixel));
		this.clone("biomass", "_dropBuilding");
		this.t.name = "Biomass";
		this.t.dropID = this.t.id;
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t172 = this.t;
		t172.click_power_action = (PowerAction)Delegate.Combine(t172.click_power_action, new PowerAction(this.flashPixel));
		this.clone("superPumpkin", "_dropBuilding");
		this.t.name = "Super Pumpkin";
		this.t.dropID = this.t.id;
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t173 = this.t;
		t173.click_power_action = (PowerAction)Delegate.Combine(t173.click_power_action, new PowerAction(this.flashPixel));
		this.clone("cybercore", "_dropBuilding");
		this.t.name = "Cybercore";
		this.t.dropID = this.t.id;
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t174 = this.t;
		t174.click_power_action = (PowerAction)Delegate.Combine(t174.click_power_action, new PowerAction(this.flashPixel));
		this.clone("geyser", "_dropBuilding");
		this.t.name = "Geyser";
		this.t.dropID = this.t.id;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t175 = this.t;
		t175.click_power_action = (PowerAction)Delegate.Combine(t175.click_power_action, new PowerAction(this.flashPixel));
		this.clone("geyserAcid", "_dropBuilding");
		this.t.name = "Acid Geyser";
		this.t.dropID = this.t.id;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t176 = this.t;
		t176.click_power_action = (PowerAction)Delegate.Combine(t176.click_power_action, new PowerAction(this.flashPixel));
		this.clone("volcano", "_dropBuilding");
		this.t.name = "Volcano";
		this.t.dropID = this.t.id;
		this.t.requiresPremium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t177 = this.t;
		t177.click_power_action = (PowerAction)Delegate.Combine(t177.click_power_action, new PowerAction(this.flashPixel));
		this.clone("goldenBrain", "_dropBuilding");
		this.t.name = "Golden Brain";
		this.t.requiresPremium = true;
		this.t.dropID = this.t.id;
		this.t.rank = PowerRank.Rank1_common;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t178 = this.t;
		t178.click_power_action = (PowerAction)Delegate.Combine(t178.click_power_action, new PowerAction(this.flashPixel));
		this.clone("corruptedBrain", "_dropBuilding");
		this.t.name = "Corrupted Brain";
		this.t.requiresPremium = true;
		this.t.dropID = this.t.id;
		this.t.rank = PowerRank.Rank1_common;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t179 = this.t;
		t179.click_power_action = (PowerAction)Delegate.Combine(t179.click_power_action, new PowerAction(this.flashPixel));
		this.clone("iceTower", "_dropBuilding");
		this.t.name = "Ice Tower";
		this.t.requiresPremium = true;
		this.t.dropID = this.t.id;
		this.t.rank = PowerRank.Rank3_good;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t180 = this.t;
		t180.click_power_action = (PowerAction)Delegate.Combine(t180.click_power_action, new PowerAction(this.flashPixel));
		this.clone("beehive", "_dropBuilding");
		this.t.name = "Beehive";
		this.t.dropID = this.t.id;
		this.t.rank = PowerRank.Rank1_common;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t181 = this.t;
		t181.click_power_action = (PowerAction)Delegate.Combine(t181.click_power_action, new PowerAction(this.flashPixel));
		this.clone("flameTower", "_dropBuilding");
		this.t.name = "Flame Tower";
		this.t.requiresPremium = true;
		this.t.dropID = this.t.id;
		this.t.rank = PowerRank.Rank3_good;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t182 = this.t;
		t182.click_power_action = (PowerAction)Delegate.Combine(t182.click_power_action, new PowerAction(this.flashPixel));
		this.add(new GodPower
		{
			id = "temperaturePlus",
			name = "Temperature",
			holdAction = true,
			showToolSizes = true,
			unselectWhenWindow = true
		});
		this.t.click_brush_action = new PowerActionWithID(this.loopWithCurrentBrush);
		GodPower t183 = this.t;
		t183.click_action = (PowerActionWithID)Delegate.Combine(t183.click_action, new PowerActionWithID(this.drawTemperaturePlus));
		GodPower t184 = this.t;
		t184.click_action = (PowerActionWithID)Delegate.Combine(t184.click_action, new PowerActionWithID(this.temperaturePlusFX));
		GodPower t185 = this.t;
		t185.click_action = (PowerActionWithID)Delegate.Combine(t185.click_action, new PowerActionWithID(this.flashPixel));
		this.clone("temperatureMinus", "temperaturePlus");
		this.t.click_brush_action = new PowerActionWithID(this.loopWithCurrentBrush);
		GodPower t186 = this.t;
		t186.click_action = (PowerActionWithID)Delegate.Combine(t186.click_action, new PowerActionWithID(PowerLibrary.drawTemperatureMinus));
		GodPower t187 = this.t;
		t187.click_action = (PowerActionWithID)Delegate.Combine(t187.click_action, new PowerActionWithID(this.temperatureMinusFX));
		GodPower t188 = this.t;
		t188.click_action = (PowerActionWithID)Delegate.Combine(t188.click_action, new PowerActionWithID(this.flashPixel));
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00021078 File Offset: 0x0001F278
	public override void checkCache()
	{
		foreach (GodPower godPower in this.list)
		{
			if (!string.IsNullOrEmpty(godPower.dropID))
			{
				godPower.cached_drop_asset = AssetManager.drops.get(godPower.dropID);
			}
			else if (!string.IsNullOrEmpty(godPower.tileType))
			{
				godPower.cached_tile_type_asset = AssetManager.tiles.get(godPower.tileType);
			}
			else if (!string.IsNullOrEmpty(godPower.topTileType))
			{
				godPower.cached_top_tile_type_asset = AssetManager.topTiles.get(godPower.topTileType);
			}
		}
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00021134 File Offset: 0x0001F334
	private void traceRanks(PowerButton pTarget)
	{
		string text = "";
		string text2 = "";
		string text3 = "";
		string text4 = "";
		string text5 = "";
		for (int i = 0; i < this.list.Count; i++)
		{
			GodPower godPower = this.list[i];
			switch (godPower.rank)
			{
			case PowerRank.Rank0_free:
				text = text + godPower.name + ", ";
				break;
			case PowerRank.Rank1_common:
				text2 = text2 + godPower.name + ", ";
				break;
			case PowerRank.Rank2_normal:
				text3 = text3 + godPower.name + ", ";
				break;
			case PowerRank.Rank3_good:
				text4 = text4 + godPower.name + ", ";
				break;
			case PowerRank.Rank4_awesome:
				text5 = text5 + godPower.name + ", ";
				break;
			}
		}
		Debug.Log("rank 0: " + text);
		Debug.Log("rank 1: " + text2);
		Debug.Log("rank 2: " + text3);
		Debug.Log("rank 3: " + text4);
		Debug.Log("rank 4: " + text5);
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00021270 File Offset: 0x0001F470
	private bool spawnDrops(WorldTile tTile, GodPower pPower)
	{
		BrushData currentBrushData = Config.currentBrushData;
		bool flag = false;
		if (currentBrushData.size == 0)
		{
			if (MapBox.instance.timerSpawnPixels <= 0f)
			{
				MapBox.instance.timerSpawnPixels = 0.5f;
				flag = true;
			}
		}
		else if (currentBrushData.size == 2 && Toolbox.randomBool())
		{
			if (MapBox.instance.timerSpawnPixels > 0f)
			{
				MapBox.instance.timerSpawnPixels = 0.3f;
				flag = true;
			}
		}
		else
		{
			flag = Toolbox.randomChance(pPower.fallingChance);
		}
		if (MapBox.instance.firstClick)
		{
			MapBox.instance.firstClick = false;
			flag = true;
		}
		if (flag)
		{
			MapBox.instance.dropManager.spawn(tTile, pPower.dropID, -1f, -1f).soundOn = true;
		}
		return true;
	}

	// Token: 0x0600017D RID: 381 RVA: 0x00021334 File Offset: 0x0001F534
	private bool spawnPrinter(WorldTile pTile, string pPower)
	{
		GodPower godPower = this.get(pPower);
		MapBox.instance.stackEffects.startSpawnEffect(pTile, "spawn");
		MapBox.instance.spawnNewUnit("printer", pTile, "print", 6f).GetComponent<Ant>().setPrintTemplate(MapBox.instance.printLibrary.dict[godPower.printersPrint]);
		AchievementLibrary.achievementPrintHeart.check_godPower(godPower);
		return true;
	}

	// Token: 0x0600017E RID: 382 RVA: 0x000213AE File Offset: 0x0001F5AE
	private bool spawnCloudSnow(WorldTile pTile, string pPower)
	{
		MapBox.instance.cloudController.spawnCloud(pTile.posV3, pPower);
		return true;
	}

	// Token: 0x0600017F RID: 383 RVA: 0x000213C7 File Offset: 0x0001F5C7
	private bool spawnCloudLava(WorldTile pTile, string pPower)
	{
		MapBox.instance.cloudController.spawnCloud(pTile.posV3, pPower);
		return true;
	}

	// Token: 0x06000180 RID: 384 RVA: 0x000213E0 File Offset: 0x0001F5E0
	private bool spawnCloudAcid(WorldTile pTile, string pPower)
	{
		MapBox.instance.cloudController.spawnCloud(pTile.posV3, pPower);
		return true;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x000213F9 File Offset: 0x0001F5F9
	private bool useMagnet(WorldTile pTile, string pPower)
	{
		MapBox.instance.actions.magnetAction(false, pTile);
		return true;
	}

	// Token: 0x06000182 RID: 386 RVA: 0x0002140D File Offset: 0x0001F60D
	private bool spawnCloudRain(WorldTile pTile, string pPower)
	{
		MapBox.instance.cloudController.spawnCloud(pTile.posV3, pPower);
		return true;
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00021426 File Offset: 0x0001F626
	private bool spawnCrabzilla(WorldTile pTile, string pPower)
	{
		MapBox.instance.alreadyUsedPower = false;
		MapBox.instance.stackEffects.startBigSpawn(pTile).setEvent("crabzilla", pTile);
		return true;
	}

	// Token: 0x06000184 RID: 388 RVA: 0x0002144F File Offset: 0x0001F64F
	private bool spawnLightning(WorldTile pTile, string pPower)
	{
		MapBox.spawnLightning(pTile, 0.25f);
		return true;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00021460 File Offset: 0x0001F660
	private bool spawnForce(WorldTile pTile, string pPower)
	{
		Sfx.play("force", true, -1f, -1f);
		MapBox.instance.applyForce(pTile, 10, 1.5f, true, true, 0, null, null, null);
		MapBox.instance.spawnFlash(pTile, 10);
		return true;
	}

	// Token: 0x06000186 RID: 390 RVA: 0x000214A8 File Offset: 0x0001F6A8
	private bool spawnInfinityCoin(WorldTile pTile, string pPower)
	{
		MapBox.instance.stackEffects.get("infinityCoin").spawnAt(new Vector3(pTile.posV3.x, pTile.posV3.y - 1f), 0.25f);
		return true;
	}

	// Token: 0x06000187 RID: 391 RVA: 0x000214F6 File Offset: 0x0001F6F6
	private bool spawnEarthquake(WorldTile pTile, string pPower)
	{
		MapBox.instance.earthquakeManager.startQuake(pTile, EarthquakeType.RandomPower);
		return true;
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0002150A File Offset: 0x0001F70A
	private bool spawnMeteorite(WorldTile pTile, string pPower)
	{
		MapBox.instance.spawnMeteorite(pTile);
		return true;
	}

	// Token: 0x06000189 RID: 393 RVA: 0x00021518 File Offset: 0x0001F718
	private void toggleOption(string pPower)
	{
		GodPower godPower = AssetManager.powers.get(pPower);
		WorldTip.instance.showToolbarText(godPower);
		PlayerOptionData playerOptionData = PlayerConfig.dict[godPower.toggle_name];
		playerOptionData.boolVal = !playerOptionData.boolVal;
		if (playerOptionData.boolVal && godPower.map_modes_switch)
		{
			this.disableAllOtherMapModes(pPower);
		}
		PlayerConfig.saveData();
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00021578 File Offset: 0x0001F778
	private void disableAllOtherMapModes(string pMainPower)
	{
		for (int i = 0; i < AssetManager.powers.list.Count; i++)
		{
			GodPower godPower = AssetManager.powers.list[i];
			if (godPower.map_modes_switch && !(godPower.id == pMainPower))
			{
				PlayerOptionData playerOptionData = PlayerConfig.dict[godPower.toggle_name];
				if (playerOptionData.boolVal)
				{
					playerOptionData.boolVal = false;
				}
			}
		}
	}

	// Token: 0x0600018B RID: 395 RVA: 0x000215E6 File Offset: 0x0001F7E6
	private bool useVortex(WorldTile pTile, string pPower)
	{
		Sfx.play("vortex", true, -1f, -1f);
		VortexAction.moveTiles(pTile, Config.currentBrushData);
		return true;
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0002160C File Offset: 0x0001F80C
	private bool drawTiles(WorldTile pTile, string pPowerID)
	{
		GodPower godPower = this.get(pPowerID);
		TileType cached_tile_type_asset = godPower.cached_tile_type_asset;
		TopTileType cached_top_tile_type_asset = godPower.cached_top_tile_type_asset;
		MapBox.instance.flashEffects.flashPixel(pTile, 25, ColorType.White);
		MapAction.terraformTile(pTile, cached_tile_type_asset, cached_top_tile_type_asset, TerraformLibrary.draw);
		return true;
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0002164E File Offset: 0x0001F84E
	private bool flashPixel(WorldTile pTile, string pPowerID = null)
	{
		MapBox.instance.flashEffects.flashPixel(pTile, 10, ColorType.White);
		return true;
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00021664 File Offset: 0x0001F864
	private bool flashPixel(WorldTile pTile, GodPower pPower)
	{
		MapBox.instance.flashEffects.flashPixel(pTile, 10, ColorType.White);
		return true;
	}

	// Token: 0x0600018F RID: 399 RVA: 0x0002167C File Offset: 0x0001F87C
	private bool drawTemperaturePlus(WorldTile pTile, string pPower)
	{
		if (pTile.Type.frozen && Toolbox.randomBool())
		{
			if (pTile.health > 0)
			{
				pTile.health--;
				pTile.grassTicksBeforeGrowth = 5;
			}
			else
			{
				MapAction.unfreezeTile(pTile);
			}
		}
		WorldBehaviourUnitTemperatures.checkTile(pTile, 5);
		if (pTile.Type.lava)
		{
			MapBox.instance.lavaLayer.increaseLava(pTile);
		}
		if (pTile.building && pTile.building.stats.spawnPixel)
		{
			pTile.building.data.spawnPixelActive = true;
		}
		return true;
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00021718 File Offset: 0x0001F918
	public bool temperatureMinusFX(WorldTile pTile, string pPower)
	{
		Sfx.play("temperatureD", true, -1f, -1f);
		return true;
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00021730 File Offset: 0x0001F930
	public bool temperaturePlusFX(WorldTile pTile, string pPower)
	{
		Sfx.play("temperatureU", true, -1f, -1f);
		return true;
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00021748 File Offset: 0x0001F948
	public static bool drawTemperatureMinus(WorldTile pTile, string pPower)
	{
		if (pTile.Type.lava)
		{
			MapBox.instance.lavaLayer.decreaseLava(pTile);
		}
		if (pTile.data.fire)
		{
			pTile.stopFire(false);
		}
		if (!pTile.Type.frozen && Toolbox.randomBool())
		{
			if (pTile.health > 0)
			{
				pTile.health--;
				pTile.grassTicksBeforeGrowth = 5;
			}
			else
			{
				MapAction.freezeTile(pTile);
			}
		}
		WorldBehaviourUnitTemperatures.checkTile(pTile, -5);
		if (pTile.building != null)
		{
			ActionLibrary.addFrozenEffectOnTarget(pTile.building, null);
		}
		return true;
	}

	// Token: 0x06000193 RID: 403 RVA: 0x000217E4 File Offset: 0x0001F9E4
	private bool drawShowelPlus(WorldTile pTile, string pPower)
	{
		if (pTile.health > 0)
		{
			pTile.health--;
		}
		else
		{
			MapAction.increaseTile(pTile, "destroy");
		}
		return false;
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0002180B File Offset: 0x0001FA0B
	private bool drawShowelMinus(WorldTile pTile, string pPower)
	{
		if (pTile.health > 0)
		{
			pTile.health--;
		}
		else
		{
			MapAction.decreaseTile(pTile, "destroy");
		}
		return false;
	}

	// Token: 0x06000195 RID: 405 RVA: 0x00021832 File Offset: 0x0001FA32
	private bool drawGreyGoo(WorldTile pTile, string pPower)
	{
		Sfx.play("goo1", true, -1f, -1f);
		MapBox.instance.greyGooLayer.add(pTile);
		return false;
	}

	// Token: 0x06000196 RID: 406 RVA: 0x0002185A File Offset: 0x0001FA5A
	private bool drawConway(WorldTile pTile, string pPower)
	{
		Sfx.play("conway", true, -1f, -1f);
		if (Random.value > 0.5f)
		{
			MapBox.instance.conwayLayer.add(pTile, "conway");
		}
		return false;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x00021893 File Offset: 0x0001FA93
	private bool drawConwayInverse(WorldTile pTile, string pPower)
	{
		Sfx.play("conway", true, -1f, -1f);
		if (Random.value > 0.5f)
		{
			MapBox.instance.conwayLayer.add(pTile, "conwayInverse");
		}
		return false;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x000218CC File Offset: 0x0001FACC
	private bool drawFinger(WorldTile pTile, string pPower)
	{
		TileType firstPressedType = MapBox.instance.firstPressedType;
		TopTileType firstPressedTopType = MapBox.instance.firstPressedTopType;
		MapAction.terraformTile(pTile, firstPressedType, firstPressedTopType, TerraformLibrary.destroy_no_flash);
		if (pTile.Type.lava)
		{
			MapBox.instance.lavaLayer.loadLavaTile(pTile);
		}
		if (pTile.Type.greyGoo)
		{
			MapBox.instance.greyGooLayer.add(pTile);
		}
		return false;
	}

	// Token: 0x06000199 RID: 409 RVA: 0x00021938 File Offset: 0x0001FB38
	private bool spawnUnit(WorldTile pTile, string pPowerID)
	{
		GodPower godPower = this.get(pPowerID);
		Sfx.play("spawn", true, -1f, -1f);
		if (godPower.spawnSound != "")
		{
			Sfx.play(godPower.spawnSound, true, -1f, -1f);
		}
		if (godPower.id == "sheep")
		{
			Sfx.timeout("sheep");
			if (pTile.Type.lava)
			{
				AchievementLibrary.achievementSacrifice.check();
			}
		}
		if (godPower.showSpawnEffect != string.Empty)
		{
			MapBox.instance.stackEffects.startSpawnEffect(pTile, godPower.showSpawnEffect);
		}
		string pStatsID;
		if (godPower.actorStatsId.Contains(","))
		{
			pStatsID = Toolbox.getRandom<string>(godPower.actorStatsId.Split(new char[]
			{
				','
			}));
		}
		else
		{
			pStatsID = godPower.actorStatsId;
		}
		MapBox.instance.spawnNewUnit(pStatsID, pTile, "", godPower.actorSpawnHeight);
		return true;
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00021A3B File Offset: 0x0001FC3B
	private bool divineLightFX(WorldTile pCenterTile, string pPowerID)
	{
		MapBox.instance.fxDivineLight.playOn(pCenterTile);
		return true;
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00021A50 File Offset: 0x0001FC50
	private bool drawDivineLight(WorldTile pCenterTile, string pPowerID)
	{
		for (int i = 0; i < pCenterTile.units.Count; i++)
		{
			Actor actor = pCenterTile.units[i];
			if (actor.data.alive && actor.gameObject.activeSelf && Toolbox.DistTile(pCenterTile, actor.currentTile) <= 4f)
			{
				actor.removeTrait("infected");
				actor.removeTrait("plague");
				actor.removeTrait("mushSpores");
				actor.removeTrait("tumorInfection");
				if (actor.race.id == "undead" || actor.race.id == "demon")
				{
					actor.getHit((float)actor.curStats.health * 0.4f, true, AttackType.Other, null, true);
				}
				else
				{
					actor.startColorEffect("white");
				}
				if (actor.haveTrait("crippled"))
				{
					actor.removeTrait("crippled");
				}
				if (actor.haveTrait("eyepatch"))
				{
					actor.removeTrait("eyepatch");
				}
				if (actor.haveTrait("skin_burns"))
				{
					actor.removeTrait("skin_burns");
				}
				if (actor.haveTrait("madness"))
				{
					actor.removeTrait("madness");
					if (actor.stats.unit)
					{
						actor.setKingdom(MapBox.instance.kingdoms.dict_hidden["nomads_" + actor.stats.race]);
					}
					else if (actor.stats.kingdom != "")
					{
						actor.setKingdom(MapBox.instance.kingdoms.dict_hidden[actor.stats.kingdom]);
					}
				}
				if (!actor.isInLiquid())
				{
					actor.cancelAllBeh(null);
				}
			}
		}
		return true;
	}

	// Token: 0x0600019C RID: 412 RVA: 0x00021C2C File Offset: 0x0001FE2C
	private bool cleanBurnedTile(WorldTile pTile, string pPowerID)
	{
		pTile.removeBurn();
		return true;
	}

	// Token: 0x0600019D RID: 413 RVA: 0x00021C35 File Offset: 0x0001FE35
	private bool drawSponge(WorldTile pTile, string pPowerID)
	{
		MapAction.tryRemoveTornadoFromTile(pTile);
		if (pTile.Type.wasteland)
		{
			MapAction.decreaseTile(pTile, "flash");
		}
		return true;
	}

	// Token: 0x0600019E RID: 414 RVA: 0x00021C58 File Offset: 0x0001FE58
	private bool drawPickaxe(WorldTile pTile, string pPowerID)
	{
		if (pTile.building != null && (pTile.building.stats.buildingType == BuildingType.Ore || pTile.building.stats.buildingType == BuildingType.Stone || pTile.building.stats.buildingType == BuildingType.Gold))
		{
			pTile.building.startDestroyBuilding(true);
		}
		return true;
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00021CB9 File Offset: 0x0001FEB9
	private bool drawBucket(WorldTile pTile, string pPowerID)
	{
		MapAction.removeLiquid(pTile);
		if (pTile.Type.lava)
		{
			MapAction.decreaseTile(pTile, "flash");
		}
		if (pTile.Type.canBeRemovedWithBucket)
		{
			MapAction.decreaseTile(pTile, "flash");
		}
		return true;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x00021CF4 File Offset: 0x0001FEF4
	private bool drawAxe(WorldTile pTile, string pPowerID)
	{
		if (pTile.building != null)
		{
			if (pTile.building.stats.buildingType == BuildingType.Tree && pTile.building.haveResources)
			{
				pTile.building.chopTree();
			}
			if (pTile.building.stats.id == "tree_dead")
			{
				pTile.building.chopTree();
			}
		}
		for (int i = 0; i < pTile.chunk.k_list_objects.Count; i++)
		{
			Kingdom kingdom = pTile.chunk.k_list_objects[i];
			if (!(kingdom.name != "living_plants"))
			{
				List<BaseSimObject> list = pTile.chunk.k_dict_objects[kingdom];
				for (int j = 0; j < list.Count; j++)
				{
					BaseSimObject baseSimObject = list[j];
					if (baseSimObject.base_data.alive && baseSimObject.objectType == MapObjectType.Actor)
					{
						baseSimObject.a.killHimself(false, AttackType.Other, true, true);
					}
				}
			}
		}
		return true;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x00021DF8 File Offset: 0x0001FFF8
	private bool drawSickle(WorldTile pTile, string pPowerID)
	{
		if (pTile.Type.canBeRemovedWithSickle)
		{
			MapAction.removeGreens(pTile);
		}
		if (pTile.building != null)
		{
			BuildingType buildingType = pTile.building.stats.buildingType;
			if (buildingType - BuildingType.Fruits <= 2)
			{
				pTile.building.startRemove(false);
			}
		}
		return true;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00021E4C File Offset: 0x0002004C
	private bool drawDemolish(WorldTile pTile, string pPowerID)
	{
		if (pTile.building != null && !pTile.building.stats.ignoreDemolish && pTile.building.stats.buildingType == BuildingType.None)
		{
			pTile.building.startDestroyBuilding(true);
		}
		if (pTile.zone.city != null)
		{
			pTile.zone.city.removeZone(pTile.zone, true);
		}
		if (pTile.Type.canBeRemovedWithDemolish)
		{
			MapAction.decreaseTile(pTile, "flash");
		}
		for (int i = 0; i < pTile.chunk.k_list_objects.Count; i++)
		{
			Kingdom kingdom = pTile.chunk.k_list_objects[i];
			if (!(kingdom.name != "living_houses"))
			{
				List<BaseSimObject> list = pTile.chunk.k_dict_objects[kingdom];
				for (int j = 0; j < list.Count; j++)
				{
					Actor actor = (Actor)list[j];
					if (actor.data.alive)
					{
						actor.killHimself(false, AttackType.Other, true, true);
					}
				}
			}
		}
		return true;
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x00021F66 File Offset: 0x00020166
	private bool drawLifeEraser(WorldTile pTile, string pPowerID)
	{
		MapAction.removeLifeFromTile(pTile);
		MapAction.tryRemoveTornadoFromTile(pTile);
		return true;
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x00021F75 File Offset: 0x00020175
	private bool drawHeatray(WorldTile pTile, string pPowerID)
	{
		if (MapBox.instance.heatRayFx.isReady() && Toolbox.randomBool())
		{
			MapBox.instance.heat.addTile(pTile, Toolbox.randomInt(1, 3));
		}
		return true;
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x00021FA8 File Offset: 0x000201A8
	private bool heatrayFX(WorldTile pTile, string pPowerID)
	{
		Sfx.play("heatray", true, -1f, -1f);
		MapBox.instance.heatRayFx.play(pTile.pos, 10);
		this.loopWithBrush(pTile, pPowerID, Brush.get(10, "circ_"));
		return true;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00021FFC File Offset: 0x000201FC
	private bool loopWithCurrentBrush(WorldTile pCenterTile, string pPowerID)
	{
		this.loopWithBrush(pCenterTile, pPowerID, Config.currentBrushData);
		return true;
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0002200D File Offset: 0x0002020D
	private bool loopWithCurrentBrushPower(WorldTile pCenterTile, GodPower pPower)
	{
		this.loopWithBrushPower(pCenterTile, pPower, Config.currentBrushData);
		return true;
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00022020 File Offset: 0x00020220
	private bool loopWithBrushPower(WorldTile pCenterTile, GodPower pPower, BrushData pBrush)
	{
		MapBox.instance.waveController.checkTile(pCenterTile, pBrush.size);
		MapBox.instance.cloudController.checkTile(pCenterTile, pBrush.size);
		MapBox.instance.loopWithBrushPower(pCenterTile, pBrush, pPower.click_power_action, pPower);
		return true;
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x00022070 File Offset: 0x00020270
	private bool loopWithBrush(WorldTile pCenterTile, string pPowerID, BrushData pBrush)
	{
		GodPower godPower = this.get(pPowerID);
		MapBox.instance.waveController.checkTile(pCenterTile, pBrush.size);
		MapBox.instance.cloudController.checkTile(pCenterTile, pBrush.size);
		MapBox.instance.loopWithBrush(pCenterTile, pBrush, godPower.click_action, pPowerID);
		return true;
	}

	// Token: 0x060001AA RID: 426 RVA: 0x000220C5 File Offset: 0x000202C5
	private bool stopFire(WorldTile pTile, string pPowerID)
	{
		pTile.stopFire(false);
		return true;
	}

	// Token: 0x060001AB RID: 427 RVA: 0x000220CF File Offset: 0x000202CF
	private bool destroyBuildings(WorldTile pTile, string pPowerID)
	{
		if (pTile.building == null)
		{
			return false;
		}
		pTile.building.startDestroyBuilding(true);
		return true;
	}

	// Token: 0x060001AC RID: 428 RVA: 0x000220EE File Offset: 0x000202EE
	private bool removeRuins(WorldTile pTile, string pPowerID)
	{
		if (pTile.building != null && pTile.building.isNonUsable())
		{
			pTile.building.startDestroyBuilding(true);
		}
		return true;
	}
}
