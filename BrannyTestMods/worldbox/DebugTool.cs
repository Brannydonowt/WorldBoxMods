using System;
using System.Collections.Generic;
using System.Linq;
using life.taxi;
using pathfinding;
using sfx;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020D RID: 525
public class DebugTool : MonoBehaviour
{
	// Token: 0x06000BB5 RID: 2997 RVA: 0x000710AC File Offset: 0x0006F2AC
	private void Awake()
	{
		List<string> list = new List<string>();
		list.Add("Benchmark");
		list.Add("ChunkManager");
		list.Add("Actor AI");
		list.Add("Boat AI");
		list.Add("City AI");
		list.Add("Kingdom AI");
		list.Add("Tile Info");
		list.Add("Building Info");
		list.Add("Game Info");
		list.Add("Kingdoms Hidden");
		list.Add("Kingdoms Civ");
		list.Add("Behaviours");
		list.Add("Cities");
		list.Add("City Tasks");
		list.Add("City Jobs");
		list.Add("City Info");
		list.Add("City Storage");
		list.Add("City Buildings");
		list.Add("City Professions");
		list.Add("Races");
		list.Add("Unit Info");
		list.Add("Zone Info");
		list.Add("Map Chunk");
		list.Add("Island Info");
		list.Add("Camera");
		list.Add("Music");
		list.Add("Region");
		list.Add("Boat");
		list.Add("Taxi");
		list.Add("Ads");
		list.Add("Connections");
		list.Add("Capture");
		list.Add("Unit Groups");
		list.Add("Auto Tester");
		list.Add("Effects");
		list.Add("Population");
		list.Add("Sprite Manager");
		list.Add("Building Manager");
		list.Add("Cultures");
		list.Add("Tile Types");
		list.Add("Jobs");
		list.Add("UnitTemp");
		this.dropdown.AddOptions(list);
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x0007129C File Offset: 0x0006F49C
	private void Update()
	{
		if (this.world == null)
		{
			this.world = MapBox.instance;
			return;
		}
		this.clearTexts();
		int num = 0;
		float num2 = 0f;
		Actor actor = null;
		string text = this.dropdown.captionText.text;
		if (text != null)
		{
			uint num3 = <PrivateImplementationDetails>.ComputeStringHash(text);
			WorldTile mouseTilePos;
			City city2;
			if (num3 <= 1730126541U)
			{
				if (num3 <= 854471445U)
				{
					if (num3 <= 635123262U)
					{
						if (num3 <= 423316261U)
						{
							if (num3 != 401795101U)
							{
								if (num3 != 423316261U)
								{
									goto IL_3FF7;
								}
								if (!(text == "Zone Info"))
								{
									goto IL_3FF7;
								}
								this.world.zoneCalculator.debug(this);
								mouseTilePos = this.world.getMouseTilePos();
								if (mouseTilePos != null)
								{
									MapChunk chunk = mouseTilePos.chunk;
									this.setText("visible:", mouseTilePos.zone.visible);
									this.setText("buildigs:", mouseTilePos.zone.buildings.Count);
									this.setSeparator();
									this.setText("id:", mouseTilePos.zone.x.ToString() + " " + mouseTilePos.zone.y.ToString());
									this.setText("city:", mouseTilePos.zone.city != null);
									this.setText("bushes:", mouseTilePos.zone.food.Count);
									this.setText("trees:", mouseTilePos.zone.trees.Count);
									this.setText("plants:", mouseTilePos.zone.plants.Count);
									this.setText("buildings:", mouseTilePos.zone.buildings.Count);
									this.setText("abandoned:", mouseTilePos.zone.abandoned.Count);
									this.setText("ruins:", mouseTilePos.zone.ruins.Count);
									this.setText("tilesWithGround:", mouseTilePos.zone.tilesWithGround);
									this.setText("tilesWithSoil:", mouseTilePos.zone.tilesWithSoil);
									this.setText("canBeFarms:", mouseTilePos.zone.canBeFarms.Count);
									string pT = "count deep ocean:";
									HashSetWorldTile tilesOfType = mouseTilePos.zone.getTilesOfType(TileLibrary.deep_ocean);
									this.setText(pT, (tilesOfType != null) ? new int?(tilesOfType.Count) : null);
									string pT2 = "count soil:";
									HashSetWorldTile tilesOfType2 = mouseTilePos.zone.getTilesOfType(TileLibrary.soil_low);
									this.setText(pT2, (tilesOfType2 != null) ? new int?(tilesOfType2.Count) : null);
									string pT3 = "count fuse:";
									HashSetWorldTile tilesOfType3 = mouseTilePos.zone.getTilesOfType(TopTileLibrary.fuse);
									this.setText(pT3, (tilesOfType3 != null) ? new int?(tilesOfType3.Count) : null);
									goto IL_3FF7;
								}
								goto IL_3FF7;
							}
							else
							{
								if (!(text == "Kingdoms Hidden"))
								{
									goto IL_3FF7;
								}
								this.setText("#hkingdoms:", this.world.kingdoms.list_hidden.Count);
								using (List<Kingdom>.Enumerator enumerator = this.world.kingdoms.list_hidden.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										Kingdom kingdom = enumerator.Current;
										if (kingdom.units.Count != 0 || kingdom.buildings.Count != 0)
										{
											this.setText(kingdom.name, kingdom.units.Count.ToString() + " " + kingdom.buildings.Count.ToString());
										}
									}
									goto IL_3FF7;
								}
								goto IL_3E57;
							}
						}
						else if (num3 != 495674670U)
						{
							if (num3 != 545709711U)
							{
								if (num3 != 635123262U)
								{
									goto IL_3FF7;
								}
								if (!(text == "Connections"))
								{
									goto IL_3FF7;
								}
								RegionLinkHashes.debug(this);
								goto IL_3FF7;
							}
							else
							{
								if (!(text == "Tile Info"))
								{
									goto IL_3FF7;
								}
								mouseTilePos = this.world.getMouseTilePos();
								if (mouseTilePos == null)
								{
									goto IL_3FF7;
								}
								this.setText("x", mouseTilePos.x);
								this.setText("y", mouseTilePos.y);
								this.setText("id", mouseTilePos.data.tile_id);
								this.setText("type", mouseTilePos.Type.id);
								this.setText("layer", mouseTilePos.Type.layerType);
								this.setText("main tile", (mouseTilePos.main_type != null) ? mouseTilePos.main_type.id : "-");
								this.setText("cap tile", (mouseTilePos.top_type != null) ? mouseTilePos.top_type.id : "-");
								this.setText("burned", mouseTilePos.burned_stages);
								this.setText("targetedBy", mouseTilePos.targetedBy != null);
								this.setText("units", mouseTilePos.units.Count);
								this.setText("good_for_boat", mouseTilePos.isGoodForBoat());
								this.setText("heat", mouseTilePos.heat);
								this.setSeparator();
								this.setText("--zone:", "");
								TileZone zone = mouseTilePos.zone;
								if (zone.buildings.Count > 0)
								{
									this.setText("buildings:", zone.buildings.Count);
								}
								if (zone.ruins.Count > 0)
								{
									this.setText("ruins:", zone.ruins.Count);
								}
								if (zone.trees.Count > 0)
								{
									this.setText("trees:", zone.trees.Count);
								}
								if (zone.stone.Count > 0)
								{
									this.setText("stone:", zone.stone.Count);
								}
								if (zone.ore.Count > 0)
								{
									this.setText("ore_deposits:", zone.ore.Count);
								}
								if (zone.gold.Count > 0)
								{
									this.setText("gold:", zone.gold.Count);
								}
								if (zone.food.Count > 0)
								{
									this.setText("fruits:", zone.food.Count);
								}
								if (zone.tilesOnFire > 0)
								{
									this.setText("fire:", zone.tilesOnFire);
								}
								if (zone.tilesWithLiquid > 0)
								{
									this.setText("water tiles:", zone.tilesWithLiquid);
								}
								if (zone.tilesWithGround > 0)
								{
									this.setText("ground tiles:", zone.tilesWithGround);
								}
								if (zone.city != null)
								{
									this.setText("city:", zone.city.name);
								}
								if (zone.city != null && zone.city.kingdom != null)
								{
									this.setText("kingdom:", zone.city.kingdom.name);
								}
								if (mouseTilePos.building != null)
								{
									this.setSeparator();
									this.setText("--building:", "");
									this.setText("name:", mouseTilePos.building.transform.name);
									this.setText("resources:", mouseTilePos.building.haveResources);
									this.setText("alive:", mouseTilePos.building.data.alive);
									this.setText("city:", (mouseTilePos.building.city != null) ? mouseTilePos.building.city.data.cityName : "-");
									string pT4 = "kingdom:";
									City city = mouseTilePos.building.city;
									this.setText(pT4, (((city != null) ? city.kingdom : null) != null) ? mouseTilePos.building.city.kingdom.name : "-");
									goto IL_3FF7;
								}
								goto IL_3FF7;
							}
						}
						else
						{
							if (!(text == "City Jobs"))
							{
								goto IL_3FF7;
							}
							mouseTilePos = this.world.getMouseTilePos();
							if (mouseTilePos == null)
							{
								goto IL_3FF7;
							}
							city2 = mouseTilePos.zone.city;
							if (!(city2 == null))
							{
								int num4 = 0;
								int num5 = 0;
								foreach (string text2 in city2.jobs.jobs.Keys)
								{
									num = city2.jobs.jobs[text2];
									int num6 = 0;
									if (city2.jobs.occupied.ContainsKey(text2))
									{
										num6 = city2.jobs.occupied[text2];
									}
									num4 += num;
									num5 += num6;
									this.setText(text2 + ":", num6.ToString() + "/" + num.ToString());
								}
								foreach (string text3 in city2.jobs.occupied.Keys)
								{
									if (!city2.jobs.jobs.ContainsKey(text3))
									{
										int num7 = city2.jobs.occupied[text3];
										num5 += num7;
										this.setText(text3 + ":", num7.ToString() + "/" + 0.ToString());
									}
								}
								this.setText("total:", num5.ToString() + "/" + num4.ToString());
								goto IL_3FF7;
							}
							goto IL_3FF7;
						}
					}
					else if (num3 <= 799064405U)
					{
						if (num3 != 739559781U)
						{
							if (num3 != 799064405U)
							{
								goto IL_3FF7;
							}
							if (!(text == "Auto Tester"))
							{
								goto IL_3FF7;
							}
							if (this.world.auto_tester != null)
							{
								this.setText("active:", this.world.auto_tester.active);
								this.setText("d_string:", this.world.auto_tester.debugString);
								this.world.auto_tester.ai.debug(this);
								goto IL_3FF7;
							}
							goto IL_3FF7;
						}
						else
						{
							if (!(text == "Unit Info"))
							{
								goto IL_3FF7;
							}
							mouseTilePos = this.world.getMouseTilePos();
							if (mouseTilePos == null)
							{
								goto IL_3FF7;
							}
							foreach (Actor actor2 in this.world.units)
							{
								float num8 = Toolbox.DistTile(actor2.currentTile, mouseTilePos);
								if (actor == null || num8 < num2)
								{
									actor = actor2;
									num2 = num8;
								}
							}
							if (!(actor == null))
							{
								this.setText("profession:", actor.data.profession);
								if (actor.ai.job != null)
								{
									this.setText("current_job:", actor.ai.job.id);
								}
								this.setText("id:", actor.data.actorID);
								this.setSeparator();
								this.setText("name:", actor.data.firstName);
								this.setText("stayingInBuilding:", actor.insideBuilding != null);
								this.setText("bag res:", actor.inventory.resource);
								this.setText("bag amount:", actor.inventory.amount);
								this.setText("ignore:", actor.targetsToIgnore.Count);
								string pT5 = "path global:";
								List<MapRegion> current_path_global = actor.current_path_global;
								this.setText(pT5, (current_path_global != null) ? new int?(current_path_global.Count) : null);
								this.setText("path local:", actor.current_path.Count);
								this.setText("health:", actor.data.health.ToString() + "/" + actor.curStats.health.ToString());
								this.setText("damage:", actor.stats.baseStats.damage.ToString() + "/" + actor.curStats.damage.ToString());
								this.setText("race:", actor.race.id);
								this.setText("city:", (actor.city == null) ? "-" : actor.city.data.cityName);
								this.setText("kingdom:", (actor.kingdom == null) ? "-" : actor.kingdom.name);
								this.setSeparator();
								this.setText("hunger:", actor.data.hunger.ToString() + "/" + actor.stats.maxHunger.ToString());
								this.setSeparator();
								this.setText("texturePath:", actor.current_texture);
								SpriteAnimation spriteAnimation = actor.spriteAnimation;
								if (((spriteAnimation != null) ? spriteAnimation.getCurrentGraphics() : null) != null)
								{
									this.setText("curTexture:", actor.spriteAnimation.getCurrentGraphics().name);
								}
								if (actor.actorAnimationData != null)
								{
									this.setText("actorAnimationData:", actor.actorAnimationData.id);
								}
								this.setText("stats name:", actor.stats.id);
								this.setSeparator();
								this.setText("timer_action:", actor.timer_action);
								this.setText("_timeout_targets:", actor._timeout_targets);
								this.setText("unitAttackTarget:", (actor.attackTarget == null) ? "-" : (actor.attackTarget.base_data.alive.ToString() ?? ""));
								this.setSeparator();
								this.setText("zPosition.y:", actor.zPosition.y);
								this.setText("attackTimer:", actor.attackTimer);
								this.setSeparator();
								this.setSeparator();
								this.setText("moveJumpOffset:", actor.moveJumpOffset.y);
								this.setText("alive:", actor.data.alive);
								this.setText("zPosition.y:", actor.zPosition.y);
								this.setText("sprite_anim_is_on:", actor.spriteAnimation.isOn);
								this.setText("currentFrameIndex:", actor.spriteAnimation.currentFrameIndex);
								goto IL_3FF7;
							}
							goto IL_3FF7;
						}
					}
					else if (num3 != 800808545U)
					{
						if (num3 != 828401016U)
						{
							if (num3 != 854471445U)
							{
								goto IL_3FF7;
							}
							if (!(text == "City Professions"))
							{
								goto IL_3FF7;
							}
							mouseTilePos = this.world.getMouseTilePos();
							if (mouseTilePos == null)
							{
								goto IL_3FF7;
							}
							city2 = mouseTilePos.zone.city;
							if (!(city2 == null))
							{
								this.setSeparator();
								this.setText("total:", city2.units.Count);
								this.setText("king:", city2.countProfession(UnitProfession.King));
								this.setText("leader:", city2.countProfession(UnitProfession.Leader));
								this.setText("units:", city2.countProfession(UnitProfession.Unit));
								this.setText("babies:", city2.countProfession(UnitProfession.Baby));
								this.setText("warriors:", city2.countProfession(UnitProfession.Warrior));
								this.setText("null:", city2.countProfession(UnitProfession.Null));
								goto IL_3FF7;
							}
							goto IL_3FF7;
						}
						else
						{
							if (!(text == "City Tasks"))
							{
								goto IL_3FF7;
							}
							goto IL_1B9E;
						}
					}
					else
					{
						if (!(text == "Capture"))
						{
							goto IL_3FF7;
						}
						mouseTilePos = this.world.getMouseTilePos();
						if (mouseTilePos == null)
						{
							goto IL_3FF7;
						}
						city2 = mouseTilePos.zone.city;
						if (!(city2 == null))
						{
							string pT6 = "capturing by:";
							Kingdom capturingBy = city2.capturingBy;
							this.setText(pT6, (capturingBy != null) ? capturingBy.name : null);
							this.setText("ticks:", city2.captureTicks);
							this.setText("capture units:", city2.capturingUnits.Count);
							this.setText("isGettingCaptured()", city2.isGettingCaptured());
							using (Dictionary<Kingdom, int>.KeyCollection.Enumerator enumerator4 = city2.capturingUnits.Keys.GetEnumerator())
							{
								while (enumerator4.MoveNext())
								{
									Kingdom kingdom2 = enumerator4.Current;
									this.setText("-" + kingdom2.name, city2.capturingUnits[kingdom2]);
								}
								goto IL_3FF7;
							}
							goto IL_1FAB;
						}
						goto IL_3FF7;
					}
				}
				else if (num3 <= 1227782301U)
				{
					if (num3 <= 1133960731U)
					{
						if (num3 != 1025745633U)
						{
							if (num3 != 1133960731U)
							{
								goto IL_3FF7;
							}
							if (!(text == "Boat"))
							{
								goto IL_3FF7;
							}
							mouseTilePos = this.world.getMouseTilePos();
							if (mouseTilePos == null)
							{
								goto IL_3FF7;
							}
							foreach (Actor actor3 in this.world.units)
							{
								float num8 = Toolbox.DistTile(actor3.currentTile, mouseTilePos);
								if (actor3.stats.isBoat && (actor == null || num8 < num2))
								{
									actor = actor3;
									num2 = num8;
								}
							}
							if (!(actor == null))
							{
								Boat component = actor.GetComponent<Boat>();
								this.setSeparator();
								this.setText("curState:", component.curState);
								this.setText("units:", component.unitsInside.Count);
								this.setText("passengerWaitCounter:", component.passengerWaitCounter);
								goto IL_3FF7;
							}
							goto IL_3FF7;
						}
						else
						{
							if (!(text == "UnitTemp"))
							{
								goto IL_3FF7;
							}
							WorldBehaviourUnitTemperatures.debug(this);
							goto IL_3FF7;
						}
					}
					else if (num3 != 1154069137U)
					{
						if (num3 != 1218478315U)
						{
							if (num3 != 1227782301U)
							{
								goto IL_3FF7;
							}
							if (!(text == "Region"))
							{
								goto IL_3FF7;
							}
							mouseTilePos = this.world.getMouseTilePos();
							if (mouseTilePos == null)
							{
								goto IL_3FF7;
							}
							if (mouseTilePos.region == null)
							{
								return;
							}
							WorldTile mouseTilePos2 = this.world.getMouseTilePos();
							if (mouseTilePos2 != null && mouseTilePos2.region != null)
							{
								this.setText("-chunk :", mouseTilePos2.region.chunk.x.ToString() + " " + mouseTilePos2.region.chunk.y.ToString());
								this.setText("-tiles_corners :", mouseTilePos2.region.getTileCorners().Count);
								this.setText("- used in path :", mouseTilePos2.region.usedByPathLock.ToString() + " " + mouseTilePos2.region.regionPathID.ToString());
								this.setText("- region wave:", mouseTilePos2.region.path_wave_id);
								this.setText("- region:", mouseTilePos2.region.id);
								this.setText("- centerRegion:", mouseTilePos2.region.centerRegion);
								this.setText("- region tiles:", mouseTilePos2.region.tiles.Count);
								this.setText("- region neigbours:", mouseTilePos2.region.neighbours.Count);
								string pT7 = "- edge right:";
								MapRegion edge_region_right = mouseTilePos2.edge_region_right;
								this.setText(pT7, (edge_region_right != null) ? new int?(edge_region_right.id) : null);
								string pT8 = "- edge up:";
								MapRegion edge_region_up = mouseTilePos2.edge_region_up;
								this.setText(pT8, (edge_region_up != null) ? new int?(edge_region_up.id) : null);
								mouseTilePos2.region.debugLinks(this);
								goto IL_3FF7;
							}
							goto IL_3FF7;
						}
						else
						{
							if (!(text == "Sprite Manager"))
							{
								goto IL_3FF7;
							}
							mouseTilePos = this.world.getMouseTilePos();
							if (mouseTilePos == null)
							{
								goto IL_3FF7;
							}
							foreach (Actor actor4 in this.world.units)
							{
								float num8 = Toolbox.DistTile(actor4.currentTile, mouseTilePos);
								if (actor == null || num8 < num2)
								{
									actor = actor4;
									num2 = num8;
								}
							}
							if (!(actor == null))
							{
								UnitSpriteConstructor.debug(this, actor);
								this.setSeparator();
								this.setText("gender:", actor.data.gender);
								string pT9 = "s_body:";
								Sprite s_body_sprite = actor.s_body_sprite;
								this.setText(pT9, ((s_body_sprite != null) ? s_body_sprite.name : null) ?? "-");
								this.setText("body:", actor.current_texture);
								string pT10 = "head:";
								Sprite s_head_sprite = actor.s_head_sprite;
								this.setText(pT10, ((s_head_sprite != null) ? s_head_sprite.name : null) ?? "-");
								string pT11 = "item:";
								Sprite s_item_sprite = actor.s_item_sprite;
								this.setText(pT11, ((s_item_sprite != null) ? s_item_sprite.name : null) ?? "-");
								this.setText("skin:", actor.data.skin);
								this.setText("set:", actor.data.skin_set);
								string pT12 = "weapon:";
								Sprite s_item_sprite_weapon = actor.s_item_sprite_weapon;
								this.setText(pT12, ((s_item_sprite_weapon != null) ? s_item_sprite_weapon.name : null) ?? "-");
								this.setText("item_dirty:", actor.item_sprite_dirty);
								goto IL_3FF7;
							}
							goto IL_3FF7;
						}
					}
					else
					{
						if (!(text == "Races"))
						{
							goto IL_3FF7;
						}
						using (List<Race>.Enumerator enumerator5 = AssetManager.raceLibrary.list.GetEnumerator())
						{
							while (enumerator5.MoveNext())
							{
								Race race = enumerator5.Current;
								if (race.units.Count != 0)
								{
									if (this.textCount > 0)
									{
										this.setSeparator();
									}
									this.setText("race:", race.id);
									this.setText("units:", race.units.Count);
								}
							}
							goto IL_3FF7;
						}
						goto IL_39D8;
					}
				}
				else if (num3 <= 1481252212U)
				{
					if (num3 != 1247930252U)
					{
						if (num3 != 1256630173U)
						{
							if (num3 != 1481252212U)
							{
								goto IL_3FF7;
							}
							if (!(text == "Music"))
							{
								goto IL_3FF7;
							}
							MusicMan.debug(this);
							goto IL_3FF7;
						}
						else
						{
							if (!(text == "Jobs"))
							{
								goto IL_3FF7;
							}
							this.world.job_manager_buildings.debug(this);
							goto IL_3FF7;
						}
					}
					else
					{
						if (!(text == "Benchmark"))
						{
							goto IL_3FF7;
						}
						this.setText("buildings_avg:", Toolbox.getBenchResult("bench_buildings", true));
						this.setText("buildings_tick:", Toolbox.getBenchResult("bench_buildings", false));
						this.setSeparator();
						this.setText("actor0:", Toolbox.getBenchResult("actor0", true));
						this.setText("actor1:", Toolbox.getBenchResult("actor1", true));
						this.setText("actor2:", Toolbox.getBenchResult("actor2", true));
						this.setText("actor_total:", Toolbox.getBenchResult("actor_total", true));
						this.setText("test_follow:", Toolbox.getBenchResult("test_follow", true));
						goto IL_3FF7;
					}
				}
				else if (num3 != 1496308997U)
				{
					if (num3 != 1610019083U)
					{
						if (num3 != 1730126541U)
						{
							goto IL_3FF7;
						}
						if (!(text == "Unit Groups"))
						{
							goto IL_3FF7;
						}
						this.setText("groups:", this.world.unitGroupManager.groups.Count);
						foreach (UnitGroup unitGroup in this.world.unitGroupManager.groups)
						{
							this.setText(": " + unitGroup.id.ToString(), unitGroup.getDebug());
						}
						this.setSeparator();
						goto IL_3FF7;
					}
					else
					{
						if (!(text == "Boat AI"))
						{
							goto IL_3FF7;
						}
						mouseTilePos = this.world.getMouseTilePos();
						if (mouseTilePos == null)
						{
							goto IL_3FF7;
						}
						foreach (Actor actor5 in this.world.units)
						{
							if (!actor5.isInsideSomething() && actor5.stats.isBoat)
							{
								float num8 = Toolbox.DistTile(actor5.currentTile, mouseTilePos);
								if (actor == null || num8 < num2)
								{
									actor = actor5;
									num2 = num8;
								}
							}
						}
						if (!(actor == null))
						{
							this.setText("action_timer:", actor.timer_action);
							this.setText("stat id:", actor.stats.id);
							if (actor.GetComponent<Boat>().taxiRequest != null)
							{
								this.setText("taxi state:", actor.GetComponent<Boat>().taxiRequest.state);
								this.setText("taxi actors:", actor.GetComponent<Boat>().taxiRequest.actors.Count);
								this.setText("taxi reqest:", actor.GetComponent<Boat>().taxiRequest.requestTile.pos[0].ToString() + ":" + actor.GetComponent<Boat>().taxiRequest.requestTile.pos[1].ToString());
							}
							actor.ai.debug(this);
							goto IL_3FF7;
						}
						goto IL_3FF7;
					}
				}
				else
				{
					if (!(text == "Effects"))
					{
						goto IL_3FF7;
					}
					ExplosionChecker.debug(this);
					using (List<BaseEffectController>.Enumerator enumerator7 = this.world.stackEffects.list.GetEnumerator())
					{
						while (enumerator7.MoveNext())
						{
							BaseEffectController baseEffectController = enumerator7.Current;
							baseEffectController.debug(this);
						}
						goto IL_3FF7;
					}
				}
			}
			else
			{
				if (num3 <= 2853021368U)
				{
					if (num3 <= 2078068825U)
					{
						if (num3 <= 1846055364U)
						{
							if (num3 != 1838665714U)
							{
								if (num3 != 1846055364U)
								{
									goto IL_3FF7;
								}
								if (!(text == "City AI"))
								{
									goto IL_3FF7;
								}
								mouseTilePos = this.world.getMouseTilePos();
								if (mouseTilePos == null)
								{
									goto IL_3FF7;
								}
								city2 = mouseTilePos.zone.city;
								if (!(city2 == null))
								{
									this.setText("warrior_timer:", city2.timer_warrior);
									this.setText("army:", string.Concat(new string[]
									{
										city2.getArmy().ToString(),
										"/",
										city2.getArmyMaxCity().ToString(),
										"/",
										city2.getArmyMaxLeader().ToString()
									}));
									this.setSeparator();
									if (city2.ai != null)
									{
										city2.ai.debug(this);
									}
									this.setSeparator();
									this.setText("action_timer:", city2.timer_action);
									goto IL_3FF7;
								}
								goto IL_3FF7;
							}
							else
							{
								if (!(text == "Cities"))
								{
									goto IL_3FF7;
								}
								this.world.citiesList.Sort(new Comparison<City>(this.citySorter));
								using (List<City>.Enumerator enumerator8 = this.world.citiesList.GetEnumerator())
								{
									while (enumerator8.MoveNext())
									{
										City city3 = enumerator8.Current;
										if (this.textCount > 0)
										{
											this.setSeparator();
										}
										this.setText("#name:", city3.data.cityName);
										this.setText("race:", city3.race.id);
										this.setText("units:", city3.getPopulationTotal());
										this.setText("zones:", city3.zones.Count);
										this.setText("buildings:", city3.buildings.Count);
										if (this.textCount > 30)
										{
											this.setSeparator();
											this.setText("more...", "...");
											break;
										}
									}
									goto IL_3FF7;
								}
							}
						}
						else if (num3 != 1980335996U)
						{
							if (num3 != 2008601669U)
							{
								if (num3 != 2078068825U)
								{
									goto IL_3FF7;
								}
								if (!(text == "ChunkManager"))
								{
									goto IL_3FF7;
								}
								this.setText("PARALLEL:", MapChunkManager.PARALLEL);
								this.setText("m_dirtyChunks:", Toolbox.getBenchCounter("m_dirtyChunks"));
								this.setText("m_newRegions:", Toolbox.getBenchCounter("m_newRegions"));
								this.setText("m_newLinks:", Toolbox.getBenchCounter("m_newLinks"));
								this.setText("m_newIslands:", Toolbox.getBenchCounter("m_newIslands"));
								this.setSeparator();
								this.setText("clear_regions:", Toolbox.getBenchResult("clear_regions", false));
								this.setText("calc_regions:", Toolbox.getBenchResult("calc_regions", false));
								this.setText("shuffle_region_tiles:", Toolbox.getBenchResult("shuffle_region_tiles", false));
								this.setText("center_regions:", Toolbox.getBenchResult("center_regions", false));
								this.setText("calc_links:", Toolbox.getBenchResult("calc_links", false));
								this.setText("create_links:", Toolbox.getBenchResult("create_links", false));
								this.setText("calc_linked_regions:", Toolbox.getBenchResult("calc_linked_regions", false));
								this.setText("findIslands:", Toolbox.getBenchResult("findIslands", false));
								this.setText("city_place_finder:", Toolbox.getBenchResult("city_place_finder", false));
								this.setSeparator();
								this.setText("chunk_total:", Toolbox.getBenchResult("chunk_total", false));
								goto IL_3FF7;
							}
							else
							{
								if (!(text == "Behaviours"))
								{
									goto IL_3FF7;
								}
								this.world.dropManager.debug(this);
								this.setText("dirty last:", this.world.dirtyTilesLast);
								this.setText("dirty tiles:", this.world.tilesDirty.Count);
								this.setSeparator();
								this.setText("tiles:", this.world.tilesList.Count);
								this.setSeparator();
								string pT13 = "grass:";
								HashSetMapRegion last_used_regions = WorldBehaviourActionGrass.last_used_regions;
								this.setText(pT13, ((last_used_regions != null) ? new int?(last_used_regions.Count<MapRegion>()) : null).ToString() + " | " + WorldBehaviourActionGrass.ticks_to_clear.ToString());
								this.setText("water:", WorldBehaviourOcean.tiles.Count);
								this.setText("burned_tiles:", WorldBehaviourActionBurnedTiles.tiles_to_update.Count);
								this.setSeparator();
								string pT14 = "lava:";
								TileDictionary tileDictionary = this.world.lavaLayer.tileDictionary;
								this.setText(pT14, (tileDictionary != null) ? new int?(tileDictionary.Count()) : null);
								string pT15 = "grey goo:";
								TileDictionary tileDictionary2 = this.world.greyGooLayer.tileDictionary;
								this.setText(pT15, (tileDictionary2 != null) ? new int?(tileDictionary2.Count()) : null);
								string pT16 = "conway";
								TileDictionary tileDictionary3 = this.world.conwayLayer.tileDictionary;
								this.setText(pT16, (tileDictionary3 != null) ? new int?(tileDictionary3.Count()) : null);
								this.setText("flash effect:", this.world.flashEffects.pixels_to_update.Count());
								this.setSeparator();
								string pT17 = "explosion layer:";
								TileDictionary tileDictionary4 = this.world.explosionLayer.tileDictionary;
								this.setText(pT17, (tileDictionary4 != null) ? new int?(tileDictionary4.Count()) : null);
								this.setText("bombDict:", this.world.explosionLayer.bombDict.Count());
								this.setText("nextWave:", this.world.explosionLayer.nextWave.Count<WorldTile>());
								this.setText("delayedBombs:", this.world.explosionLayer.nextWave.Count<WorldTile>());
								this.setText("timedBombs:", this.world.explosionLayer.timedBombs.Count<WorldTile>());
								goto IL_3FF7;
							}
						}
						else
						{
							if (!(text == "Actor AI"))
							{
								goto IL_3FF7;
							}
							mouseTilePos = this.world.getMouseTilePos();
							if (mouseTilePos == null)
							{
								goto IL_3FF7;
							}
							foreach (Actor actor6 in this.world.units)
							{
								if (!actor6.isInsideSomething())
								{
									float num8 = Toolbox.DistTile(actor6.currentTile, mouseTilePos);
									if (actor == null || num8 < num2)
									{
										actor = actor6;
										num2 = num8;
									}
								}
							}
							if (!(actor == null))
							{
								this.setText("action_timer:", actor.timer_action);
								this.setText("stat id:", actor.stats.id);
								actor.ai.debug(this);
								goto IL_3FF7;
							}
							goto IL_3FF7;
						}
					}
					else if (num3 <= 2210814771U)
					{
						if (num3 != 2150434697U)
						{
							if (num3 != 2210814771U)
							{
								goto IL_3FF7;
							}
							if (!(text == "City Buildings"))
							{
								goto IL_3FF7;
							}
							mouseTilePos = this.world.getMouseTilePos();
							if (mouseTilePos == null)
							{
								goto IL_3FF7;
							}
							city2 = mouseTilePos.zone.city;
							if (!(city2 == null))
							{
								this.setSeparator();
								int num9 = 0;
								int num10 = 0;
								this.setText("#type", "");
								foreach (string text4 in city2.buildings_dict_type.Keys)
								{
									this.setText(text4 + ":", city2.buildings_dict_type[text4].Count);
									num9 += city2.buildings_dict_type[text4].Count;
								}
								this.setSeparator();
								this.setText("#name", "");
								foreach (string text5 in city2.buildings_dict_id.Keys)
								{
									this.setText(text5 + ":", city2.buildings_dict_id[text5].Count);
									num10 += city2.buildings_dict_id[text5].Count;
								}
								this.setSeparator();
								this.setText("total:", city2.buildings.Count);
								this.setText("total by type:", num9);
								this.setText("total by name:", num10);
								this.setText("next building:", city2._debug_nextPlannedBuilding);
								goto IL_3FF7;
							}
							goto IL_3FF7;
						}
						else
						{
							if (!(text == "Kingdoms Civ"))
							{
								goto IL_3FF7;
							}
							goto IL_3E57;
						}
					}
					else if (num3 != 2361626449U)
					{
						if (num3 != 2433035465U)
						{
							if (num3 != 2853021368U)
							{
								goto IL_3FF7;
							}
							if (!(text == "Island Info"))
							{
								goto IL_3FF7;
							}
							mouseTilePos = this.world.getMouseTilePos();
							if (mouseTilePos == null)
							{
								goto IL_3FF7;
							}
							if (mouseTilePos.region == null)
							{
								return;
							}
							TileIsland island = mouseTilePos.region.island;
							if (island == null)
							{
								return;
							}
							this.setText("islands:", this.world.islandsCalculator.islands.Count);
							this.setText("regions:", island.regions.Count);
							this.setSeparator();
							this.setText("id:", island.id);
							this.setText("hash:", island.debug_hash_code);
							this.setText("tiles:", island.getTileCount());
							this.setText("unit limit:", island.regions.Count * 4);
							this.setText("created:", island.created);
							this.setText("docks:", island.docks.Count);
							goto IL_3FF7;
						}
						else
						{
							if (!(text == "Taxi"))
							{
								goto IL_3FF7;
							}
							this.setText("requests:", TaxiManager.list.Count);
							this.setSeparator();
							TaxiManager.list.Sort((TaxiRequest a, TaxiRequest b) => b.actors.Count.CompareTo(a.actors.Count));
							TaxiManager.list.ForEach(delegate(TaxiRequest tRequest)
							{
								int num22 = 0;
								if (tRequest.taxi != null)
								{
									num22 = tRequest.taxi.unitsInside.Count;
								}
								this.setText("state", string.Concat(new string[]
								{
									tRequest.state.ToString(),
									" ",
									num22.ToString(),
									"/",
									tRequest.actors.Count.ToString(),
									" | ",
									(tRequest.taxi != null).ToString()
								}));
							});
							goto IL_3FF7;
						}
					}
					else
					{
						if (!(text == "Ads"))
						{
							goto IL_3FF7;
						}
						goto IL_A94;
					}
				}
				else if (num3 <= 3708478502U)
				{
					if (num3 <= 3393244091U)
					{
						if (num3 != 3031702387U)
						{
							if (num3 != 3393244091U)
							{
								goto IL_3FF7;
							}
							if (!(text == "Building Info"))
							{
								goto IL_3FF7;
							}
						}
						else
						{
							if (!(text == "City Storage"))
							{
								goto IL_3FF7;
							}
							mouseTilePos = this.world.getMouseTilePos();
							if (mouseTilePos == null)
							{
								goto IL_3FF7;
							}
							city2 = mouseTilePos.zone.city;
							if (!(city2 == null))
							{
								foreach (string text6 in city2.data.storage.resources.Keys)
								{
									this.setText(text6 + ":", city2.data.storage.get(text6));
								}
								using (List<ActorEquipmentSlot>.Enumerator enumerator11 = ActorEquipment.getList(city2.data.storage.itemStorage, false).GetEnumerator())
								{
									while (enumerator11.MoveNext())
									{
										ActorEquipmentSlot actorEquipmentSlot = enumerator11.Current;
										this.setText(actorEquipmentSlot.type.ToString() ?? "", string.Concat(new string[]
										{
											actorEquipmentSlot.data.prefix,
											" ",
											actorEquipmentSlot.data.material,
											" ",
											actorEquipmentSlot.data.id,
											" ",
											actorEquipmentSlot.data.suffix
										}));
									}
									goto IL_3FF7;
								}
								goto IL_1B9E;
							}
							goto IL_3FF7;
						}
					}
					else if (num3 != 3647548080U)
					{
						if (num3 != 3677402052U)
						{
							if (num3 != 3708478502U)
							{
								goto IL_3FF7;
							}
							if (!(text == "Cultures"))
							{
								goto IL_3FF7;
							}
							this.setText("cultures:", this.world.cultures.list.Count);
							for (int i = 0; i < this.world.cultures.list.Count; i++)
							{
								this.world.cultures.list[i].debug(this);
							}
							goto IL_3FF7;
						}
						else
						{
							if (!(text == "Kingdom AI"))
							{
								goto IL_3FF7;
							}
							mouseTilePos = this.world.getMouseTilePos();
							if (mouseTilePos == null)
							{
								goto IL_3FF7;
							}
							city2 = mouseTilePos.zone.city;
							if (city2 == null)
							{
								goto IL_3FF7;
							}
							Kingdom kingdom3 = city2.kingdom;
							if (kingdom3.king != null)
							{
								this.setText("personality:", kingdom3.king.s_personality.id);
								this.setText("agression:", kingdom3.king.curStats.personality_aggression);
								this.setText("administration:", kingdom3.king.curStats.personality_administration);
								this.setText("diplomatic:", kingdom3.king.curStats.personality_diplomatic);
								this.setSeparator();
							}
							this.setText("timer_action:", kingdom3.timer_action);
							this.setText("timer_attack_target:", kingdom3.timer_attack_target);
							this.setText("timer_event:", kingdom3.timer_event);
							this.setText("timer_loyalty:", kingdom3.timer_loyalty);
							this.setText("timer_new_king:", kingdom3.timer_new_king);
							this.setText("timer_no_city:", kingdom3.timer_no_city);
							this.setText("timer_settle_target:", kingdom3.timer_settle_target);
							this.setSeparator();
							this.setText("action_timer:", kingdom3.timer_action);
							if (kingdom3.ai != null)
							{
								kingdom3.ai.debug(this);
								goto IL_3FF7;
							}
							goto IL_3FF7;
						}
					}
					else
					{
						if (!(text == "Map Chunk"))
						{
							goto IL_3FF7;
						}
						mouseTilePos = this.world.getMouseTilePos();
						if (mouseTilePos != null)
						{
							Toolbox.findSameRaceInChunkAround(mouseTilePos.chunk, "crab");
							this.setText("crabs", Toolbox.temp_list_units.Count);
							this.setText("units_total:", num);
							goto IL_3FF7;
						}
						goto IL_3FF7;
					}
				}
				else if (num3 <= 3880549230U)
				{
					if (num3 != 3789444470U)
					{
						if (num3 != 3877430738U)
						{
							if (num3 != 3880549230U)
							{
								goto IL_3FF7;
							}
							if (!(text == "Camera"))
							{
								goto IL_3FF7;
							}
							this.world.camera.GetComponent<MoveCamera>().debug(this);
							this.setSeparator();
							this.setText("zoom", this.world.camera.orthographicSize);
							this.setText("visible zones", this.world.zone_camera.zones.Count.ToString() + "/" + this.world.zoneCalculator.zones.Count.ToString());
							this.setText("Input.touchCount", Input.touchCount);
							if (Input.touchCount > 0)
							{
								for (int j = 0; j < Input.touchCount; j++)
								{
									this.setText("Touch.fingerId[" + j.ToString() + "]", Input.GetTouch(j).fingerId);
									this.setText("Touch.rawPosition[" + j.ToString() + "]", Input.GetTouch(j).rawPosition);
									this.setText("Touch.pos[" + j.ToString() + "]", Input.GetTouch(j).position);
									this.setText("Touch.dpos[" + j.ToString() + "]", Input.GetTouch(j).deltaPosition);
									this.setText("Touch.delta[" + j.ToString() + "]", Input.GetTouch(j).deltaTime);
									this.setText("Touch.radius[" + j.ToString() + "]", Input.GetTouch(j).radius);
									this.setText("Touch.pressure[" + j.ToString() + "]", Input.GetTouch(j).pressure);
								}
							}
							this.setText("Axis Vertical", Input.GetAxis("Vertical"));
							this.setText("Axis Horizontal", Input.GetAxis("Horizontal"));
							this.setText("MoveCamera.debugString", MoveCamera.debugString);
							this.setText("MoveCamera.distDebug", MoveCamera.distDebug);
							this.setText("MoveCamera.cameraDragRun", MoveCamera.cameraDragRun);
							this.setText("MoveCamera.debugDragDiff", MoveCamera.debugDragDiff);
							this.setText("rightClickTimer", this.world.inspectTimerClick);
							this.setText("cameraDragActivated", MoveCamera.cameraDragActivated);
							this.setText("Input.touchSupported", Input.touchSupported);
							this.setText("Input.touchPressureSupported", Input.touchPressureSupported);
							this.setText("Input.multiTouchEnabled", Input.multiTouchEnabled);
							this.setText("Input.stylusTouchSupported", Input.stylusTouchSupported);
							this.setText("Input.simulateMouseWithTouches", Input.simulateMouseWithTouches);
							this.setText("Input.mousePresent", Input.mousePresent);
							this.setText("Input.mousePosition", Input.mousePosition);
							this.setText("Input.mouseScrollDelta", Input.mouseScrollDelta);
							this.setText("Button 0", Input.GetMouseButton(0));
							this.setText("Button 1", Input.GetMouseButton(1));
							this.setText("Button 2", Input.GetMouseButton(2));
							this.setText("Axis ScrollWheel", Input.GetAxis("Mouse ScrollWheel"));
							this.setText("Axis Mouse X", Input.GetAxis("Mouse X"));
							this.setText("Axis Mouse Y", Input.GetAxis("Mouse Y"));
							this.setText("Raw Mouse X", Input.GetAxisRaw("Mouse X"));
							this.setText("Raw Mouse Y", Input.GetAxisRaw("Mouse Y"));
							goto IL_3FF7;
						}
						else
						{
							if (!(text == "Population"))
							{
								goto IL_3FF7;
							}
							int num11 = 0;
							int num12 = 0;
							foreach (City city4 in this.world.citiesList)
							{
								num11 += city4.getPopulationUnits();
								num12 += city4.getPopulationPopPoints();
							}
							this.setText("city units:", num11);
							this.setText("city pop_points:", num12);
							this.setText("city total:", num11 + num12);
							this.setText("unit list:", this.world.units.debug());
							goto IL_3FF7;
						}
					}
					else
					{
						if (!(text == "City Info"))
						{
							goto IL_3FF7;
						}
						goto IL_1FAB;
					}
				}
				else if (num3 != 3971524102U)
				{
					if (num3 != 4090216549U)
					{
						if (num3 != 4236207072U)
						{
							goto IL_3FF7;
						}
						if (!(text == "Tile Types"))
						{
							goto IL_3FF7;
						}
						this.setText("tumor_low:", TopTileLibrary.tumor_low.hashset.Count);
						this.setText("tumor_high:", TopTileLibrary.tumor_high.hashset.Count);
						this.setText("biomass_low:", TopTileLibrary.biomass_low.hashset.Count);
						this.setText("biomass_high:", TopTileLibrary.biomass_high.hashset.Count);
						this.setText("pumpkin_low:", TopTileLibrary.pumpkin_low.hashset.Count);
						this.setText("pumpkin_high:", TopTileLibrary.pumpkin_high.hashset.Count);
						this.setText("cybertile_low:", TopTileLibrary.cybertile_low.hashset.Count);
						this.setText("cybertile_high:", TopTileLibrary.cybertile_high.hashset.Count);
						this.setText("deep_ocean:", TileLibrary.deep_ocean.hashset.Count);
						this.setText("pit_deep_ocean:", TileLibrary.pit_deep_ocean.hashset.Count);
						goto IL_3FF7;
					}
					else
					{
						if (!(text == "Game Info"))
						{
							goto IL_3FF7;
						}
						goto IL_39D8;
					}
				}
				else
				{
					if (!(text == "Building Manager"))
					{
						goto IL_3FF7;
					}
					this.setText("buildings:", this.world.buildings.Count);
					int num13 = 0;
					int num14 = 0;
					int num15 = 0;
					foreach (Building building in this.world.buildings)
					{
						if (building._is_visible)
						{
							num13++;
						}
						if (building.tween_active)
						{
							num15++;
						}
					}
					this.setText("visible:", num13.ToString() + "/" + this.world.buildings.Count.ToString());
					this.setText("tweens:", num14.ToString() + "/" + this.world.buildings.Count.ToString());
					this.setText("tween_active:", num15.ToString() + "/" + this.world.buildings.Count.ToString());
					this.setSeparator();
					goto IL_3FF7;
				}
				mouseTilePos = this.world.getMouseTilePos();
				if (mouseTilePos == null)
				{
					goto IL_3FF7;
				}
				Building building2 = mouseTilePos.building;
				if (!(building2 == null))
				{
					if (building2.stats.docks)
					{
						this.setText("boats_fishing:", building2.GetComponent<Docks>().boats_fishing.Count);
					}
					this.setText("id:", building2.data.objectID);
					this.setText("animData_index:", building2.animData_index);
					this.setText("kingdom:", building2.kingdom.id);
					this.setText("kingdom civ:", building2.kingdom.asset.civ);
					string pT18 = "kingdom race:";
					Race race2 = building2.kingdom.race;
					this.setText(pT18, (race2 != null) ? race2.id : null);
					this.setText("sprite_dirty:", building2.sprite_dirty);
					this.setText("animationState:", building2.animationState);
					this.setText("state:", building2.data.state);
					this.setText("template:", building2.data.templateID);
					this.setText("health:", building2.data.health);
					this.setText("health cur:", building2.curStats.health);
					if (building2.kingdom != null)
					{
						this.setText("kingdom:", building2.kingdom.name);
					}
					this.setSeparator();
					this.setText("tiles:", building2.tiles.Count);
					this.setText("zones:", building2.zones.Count);
					this.setSeparator();
					this.setText("alive:", building2.data.alive);
					this.setText("under construction:", building2.data.underConstruction);
					this.setText("progress:", building2.data.progress);
					if (building2.city != null)
					{
						this.setText("city:", building2.city.data.cityName);
					}
					this.setSeparator();
					this.setText("tween_active:", building2.tween_active);
					this.setSeparator();
					this.setText("state:", building2.animationState);
					this.setText("is_visible:", building2._is_visible);
					this.setText("scaleVal:", building2.scaleVal);
					this.setText("transform.localScale:", building2.transform.localScale);
					goto IL_3FF7;
				}
				goto IL_3FF7;
			}
			IL_A94:
			this.setText("isShowing:", RewardedAds.isShowing());
			this.setText("isReady:", RewardedAds.isReady());
			this.setText("RewardBasedVideoAd:", RewardedAds.hasAd());
			this.setText("debug:", RewardedAds.debug);
			this.setSeparator();
			goto IL_3FF7;
			IL_1B9E:
			mouseTilePos = this.world.getMouseTilePos();
			if (mouseTilePos == null)
			{
				goto IL_3FF7;
			}
			city2 = mouseTilePos.zone.city;
			if (!(city2 == null))
			{
				this.setText("trees:", city2.tasks.trees);
				this.setText("stone:", city2.tasks.stone);
				this.setText("ore:", city2.tasks.ore);
				this.setText("metal:", city2.tasks.metal);
				this.setText("gold:", city2.tasks.gold);
				this.setText("bushes:", city2.tasks.bushes);
				this.setText("farmFields:", city2.tasks.farmFields);
				this.setText("canBeFarms:", city2.tasks.canBeFarms);
				this.setText("wheats:", city2.tasks.wheats);
				this.setText("ruins:", city2.tasks.ruins);
				this.setText("roads:", city2.tasks.roads);
				this.setText("fire:", city2.tasks.fire);
				goto IL_3FF7;
			}
			goto IL_3FF7;
			IL_1FAB:
			mouseTilePos = this.world.getMouseTilePos();
			if (mouseTilePos == null)
			{
				goto IL_3FF7;
			}
			city2 = mouseTilePos.zone.city;
			if (!(city2 == null))
			{
				this.setText("#name:", city2.data.cityName);
				this.setText("city units:", city2.getPopulationUnits());
				this.setText("city popon:", city2.getPopulationPopPoints());
				this.setText("city total:", city2.getPopulationTotal());
				this.setText("race:", city2.race.id);
				this.setText("units:", city2.getPopulationTotal().ToString() + "/" + city2.status.homesTotal.ToString());
				this.setText("in houses:", city2.countInHouses());
				this.setSeparator();
				if (city2.leader != null)
				{
					this.setText("leader:", city2.leader.data.firstName);
				}
				if (city2.kingdom != null)
				{
					this.setText("kingdom:", city2.kingdom.name);
				}
				if (city2.kingdom != null)
				{
					this.setText("#name:", city2.kingdom.id);
				}
				this.setSeparator();
				this.setText("zones:", city2.zones.Count);
				this.setText("buildings:", city2.buildings.Count);
				this.setText("homes free:", city2.status.homesFree);
				this.setText("homes occupied:", city2.status.homesOccupied);
				this.setSeparator();
				this.setSeparator();
				this.setText("roads to build:", city2.roadTilesToBuild.Count);
				this.setSeparator();
				this.setText("jobs total:", city2.jobs.total);
				this.setSeparator();
				this.setText("next building:", city2._debug_nextPlannedBuilding);
				goto IL_3FF7;
			}
			goto IL_3FF7;
			IL_39D8:
			this.setText("elapsed:", this.world.elapsed);
			this.setText("delta time:", this.world.deltaTime);
			this.setText("actor0:", Toolbox.getBenchResult("actor0", true));
			this.setText("actor1:", Toolbox.getBenchResult("actor1", true));
			this.setText("actor2:", Toolbox.getBenchResult("actor2", true));
			this.setText("actor_total:", Toolbox.getBenchResult("actor_total", true));
			this.setText("test_follow:", Toolbox.getBenchResult("test_follow", true));
			this.setText("rightClickTimer:", this.world.inspectTimerClick);
			this.setText("cache g paths:", this.world.regionPathFinder.debug());
			this.setText("cache raycasts:", CachedRaycastIslands.debug());
			this.setText("units:", this.world.units.debug());
			this.setText("buildings:", this.world.buildings.debug());
			this.setText("cities:", this.world.citiesList.Count);
			this.setText("civ kingdoms:", this.world.kingdoms.list_civs.Count);
			this.setSeparator();
			this.setText("gameTime:", (int)this.world.gameStats.data.gameTime);
			this.setText("gameLaunches:", this.world.gameStats.data.gameLaunches);
			this.setText("worldTime:", this.world.mapStats.worldTime);
			this.setSeparator();
			this.setText("size tiles:", this.world.tilesMap.Length);
			this.setText("chunks:", this.world.mapChunkManager.list.Count);
			this.setText("- dirty last:", this.world.mapChunkManager.m_dirtyChunks);
			this.setText("- regions:", this.world.mapChunkManager.calcRegions());
			this.setText("- hashes:", RegionLinkHashes.getCount());
			this.setText("- islands:", this.world.islandsCalculator.islands.Count);
			this.setSeparator();
			int num16 = 0;
			int num17 = 0;
			int num18 = 0;
			int num19 = 0;
			foreach (Actor actor7 in this.world.units)
			{
				if (actor7.gameObject.activeSelf)
				{
					num16++;
				}
				else
				{
					num17++;
				}
				if (actor7.isInfectedWithAnything())
				{
					num19++;
				}
				if (actor7._is_visible)
				{
					num18++;
				}
			}
			this.setSeparator();
			this.setText("visible actors:", num18.ToString() + "/" + this.world.units.Count.ToString());
			this.setText("active actors:", num16.ToString() + "/" + this.world.units.Count.ToString());
			this.setText("non_active actors:", num17.ToString() + "/" + this.world.units.Count.ToString());
			goto IL_3FF7;
			IL_3E57:
			this.setText("#kingdoms:", this.world.kingdoms.list_civs.Count);
			this.setText("- units total:", this.world.units.Count);
			int num20 = 0;
			using (IEnumerator<Actor> enumerator3 = this.world.units.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					if (enumerator3.Current.kingdom == null)
					{
						num20++;
					}
				}
			}
			this.setText("- units no kingdom:", num20);
			this.world.kingdoms.list_civs.Sort(new Comparison<Kingdom>(this.kingdomSorter));
			foreach (Kingdom kingdom4 in this.world.kingdoms.list_civs)
			{
				if (this.textCount > 0)
				{
					this.setSeparator();
				}
				this.setText("#name", kingdom4.name);
				this.setText("age", kingdom4.age);
				this.setText("units", kingdom4.units.Count);
				this.setText("army", kingdom4.countArmy().ToString() + "/" + kingdom4.countArmyMax().ToString());
				this.setText("buildings", kingdom4.buildings.Count);
			}
		}
		IL_3FF7:
		this.text1.color = Toolbox.makeColor("#FEBC66");
		this.text1.text = this.string1;
		this.text2.text = this.string2;
		float preferredWidth = LayoutUtility.GetPreferredWidth(this.text1.GetComponent<RectTransform>());
		float preferredWidth2 = LayoutUtility.GetPreferredWidth(this.text2.GetComponent<RectTransform>());
		float num21 = preferredWidth + preferredWidth2 + 10f;
		if (num21 < 70f)
		{
		}
		num21 = 100f;
		float preferredHeight = LayoutUtility.GetPreferredHeight(this.text1.GetComponent<RectTransform>());
		this.background.GetComponent<RectTransform>().sizeDelta = new Vector2(num21, preferredHeight + 15f);
		this.text1.GetComponent<RectTransform>().sizeDelta = new Vector2(preferredWidth, preferredHeight);
		this.text2.GetComponent<RectTransform>().sizeDelta = new Vector2(preferredWidth2, preferredHeight);
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x00075598 File Offset: 0x00073798
	private void OnDrawGizmos()
	{
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x000755A8 File Offset: 0x000737A8
	public int kingdomSorter(Kingdom k1, Kingdom k2)
	{
		return k2.units.Count.CompareTo(k1.units.Count);
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x000755D4 File Offset: 0x000737D4
	public int citySorter(City c1, City c2)
	{
		return c2.getPopulationTotal().CompareTo(c1.getPopulationTotal());
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x000755F8 File Offset: 0x000737F8
	internal void setText(string pT1, object pT2)
	{
		this.string1 += pT1;
		this.string2 += ((pT2 != null) ? pT2.ToString() : null);
		this.string1 += "\n";
		this.string2 += "\n";
		this.textCount++;
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x00075670 File Offset: 0x00073870
	internal void setSeparator()
	{
		this.string1 = (this.string1 ?? "");
		this.string2 = (this.string2 ?? "");
		this.string1 += "\n";
		this.string2 += "\n";
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x000756D3 File Offset: 0x000738D3
	private void clearTexts()
	{
		this.textCount = 0;
		this.string1 = "";
		this.string2 = "";
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x000756F2 File Offset: 0x000738F2
	public void clickClose()
	{
		Object.Destroy(base.gameObject, 0.01f);
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x00075704 File Offset: 0x00073904
	public void clickDuplicate()
	{
		Object.Instantiate<GameObject>(base.gameObject, base.transform.parent).transform.name = "DebugTool";
	}

	// Token: 0x04000E35 RID: 3637
	private MapBox world;

	// Token: 0x04000E36 RID: 3638
	public Text text1;

	// Token: 0x04000E37 RID: 3639
	public Text text2;

	// Token: 0x04000E38 RID: 3640
	public Image background;

	// Token: 0x04000E39 RID: 3641
	private string string1;

	// Token: 0x04000E3A RID: 3642
	private string string2;

	// Token: 0x04000E3B RID: 3643
	private int textCount;

	// Token: 0x04000E3C RID: 3644
	public Dropdown dropdown;

	// Token: 0x04000E3D RID: 3645
	private WorldTile last_tile;

	// Token: 0x04000E3E RID: 3646
	private WorldTile last_building;
}
