using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x020000BF RID: 191
public class Building : BaseSimObject
{
	// Token: 0x060003CA RID: 970 RVA: 0x00039626 File Offset: 0x00037826
	internal bool isBurnable()
	{
		return this.data.health > 0 && this.stats.burnable;
	}

	// Token: 0x060003CB RID: 971 RVA: 0x00039644 File Offset: 0x00037844
	public void setAnimData(int pIndex)
	{
		if (pIndex >= this.stats.sprites.animationData.Count || pIndex < 0)
		{
			pIndex = 0;
		}
		this.animData = this.stats.sprites.animationData[pIndex];
		this.animData_index = pIndex;
	}

	// Token: 0x060003CC RID: 972 RVA: 0x00039694 File Offset: 0x00037894
	public void applyAnimDataToAnimation()
	{
		if (this.data.state == BuildingState.Ruins || this.data.state == BuildingState.CivAbandoned)
		{
			this.spriteAnimation.isOn = false;
		}
		else
		{
			this.spriteAnimation.isOn = this.animData.animated;
		}
		if (this.data.state == BuildingState.Ruins)
		{
			this.spriteAnimation.frames = this.animData.ruins;
			return;
		}
		this.spriteAnimation.frames = this.animData.main;
	}

	// Token: 0x060003CD RID: 973 RVA: 0x0003971C File Offset: 0x0003791C
	internal void stopFire()
	{
		this.removeStatusEffect("burning", null, -1);
	}

	// Token: 0x060003CE RID: 974 RVA: 0x0003972B File Offset: 0x0003792B
	internal override void create()
	{
		base.create();
		base.setObjectType(MapObjectType.Building);
		this.tiles = new List<WorldTile>();
		this.startShake(0.3f);
	}

	// Token: 0x060003CF RID: 975 RVA: 0x00039750 File Offset: 0x00037950
	private void addComponent(BaseBuildingComponent pObject)
	{
		if (this.components == null)
		{
			this.components = new List<BaseBuildingComponent>();
		}
		this.components.Add(pObject);
		pObject.create();
		this.batch.c_components.Add(this);
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x00039788 File Offset: 0x00037988
	internal void setBuilding(WorldTile pTile, BuildingAsset pTemplate, BuildingData pData)
	{
		this.world.job_manager_buildings.addNewObject(this);
		if (pData == null)
		{
			this.setData(new BuildingData());
			this.setTemplate(pTemplate);
			this.data.objectID = this.world.mapStats.getNextId("building");
			this.data.mainX = pTile.pos.x;
			this.data.mainY = pTile.pos.y;
			this.setState(BuildingState.Normal);
			this.updateStats();
			this.data.health = this.curStats.health;
			if (pTemplate.buildingType != BuildingType.None)
			{
				this.setHaveResources(true);
			}
			this.data.spawnPixelActive = this.stats.spawnPixel;
		}
		else
		{
			this.setData(pData);
			this.setTemplate(pTemplate);
			if (!this.stats.cityBuilding && this.data.state == BuildingState.Null)
			{
				this.setState(BuildingState.Normal);
			}
			if (pTemplate.buildingType != BuildingType.None)
			{
				this.setHaveResources(true);
			}
			if (this.stats.cityBuilding && this.data.cityID == "" && !this.data.alive && this.stats.canBeAbandoned)
			{
				this.makeAbandoned();
			}
		}
		this.setStatsDirty();
		this.currentTile = pTile;
		this.currentPosition = this.currentTile.posV3;
		if (this.currentTile.tile_down != null)
		{
			this.doorTile = this.currentTile.tile_down;
		}
		else
		{
			this.doorTile = this.currentTile;
		}
		this.m_transform.localScale = new Vector3(Building.defaultScale.x, 0f, Building.defaultScale.z);
		this.currentScale.x = Building.defaultScale.x;
		this.currentScale.y = Building.defaultScale.y;
		this.fillFrontTiles();
		this.fillTiles();
		if (!this.stats.cityBuilding)
		{
			this.data.underConstruction = false;
		}
		if (this.data.underConstruction)
		{
			this.setSpriteUnderConstruction();
		}
		else
		{
			int num = -1;
			if (pData != null)
			{
				num = pData.frameID;
			}
			this.setSpriteMain(true);
			if (num != -1)
			{
				this.finishScaleTween();
				this.setAnimData(num);
			}
		}
		this.setSpriteDirty();
		this.setPositionDirty();
		this.updatePosition();
		if (pTemplate.smoke)
		{
			this.addComponent(this.m_gameObject.AddComponent<BuildingSmokeEffect>());
		}
		if (pTemplate.spawnPixel)
		{
			this.addComponent(this.m_gameObject.AddComponent<BuildingEffectSpawnPixel>());
		}
		if (pTemplate.grow_creep)
		{
			this.addComponent(this.m_gameObject.AddComponent<BuildingCreepHUB>());
		}
		if (pTemplate.buildingType == BuildingType.Wheat)
		{
			this.addComponent(this.m_gameObject.AddComponent<Wheat>());
		}
		if (pTemplate.buildingType == BuildingType.Fruits)
		{
			this.addComponent(this.m_gameObject.AddComponent<BuildingFruitGrowth>());
		}
		if (pTemplate.iceTower)
		{
			this.addComponent(this.m_gameObject.AddComponent<IceTower>());
		}
		if (pTemplate.spawnUnits)
		{
			this.addComponent(this.m_gameObject.AddComponent<UnitSpawner>());
		}
		if (pTemplate.beehive)
		{
			this.addComponent(this.m_gameObject.AddComponent<Beehive>());
		}
		if (pTemplate.docks)
		{
			this.addComponent(this.m_gameObject.AddComponent<Docks>());
		}
		if (pTemplate.tower)
		{
			this.addComponent(this.m_gameObject.AddComponent<BuildingTower>());
		}
		if (pData == null && !pTemplate.cityBuilding)
		{
			this.setAnimationState(BuildingAnimationState.Normal);
			this.setScaleTween(0f, 0.2f);
		}
		if (this.data.state == BuildingState.Ruins)
		{
			this.setSpriteRuin();
		}
		if (this.stats.cityBuilding && this.kingdom == null)
		{
			this.setKingdom(this.world.kingdoms.getKingdomByID("abandoned"), true);
			this.setState(BuildingState.CivAbandoned);
			this.setSpriteDirty();
		}
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00039B61 File Offset: 0x00037D61
	public override void setStatsDirty()
	{
		this.statsDirty = true;
		this.batch.c_stats_dirty.Add(this);
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00039B7B File Offset: 0x00037D7B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void setSpriteDirty()
	{
		this.sprite_dirty = true;
		this.batch.c_sprite_dirty.Add(this);
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x00039B95 File Offset: 0x00037D95
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void setPositionDirty()
	{
		this.positionDirty = true;
		this.batch.c_position_dirty.Remove(this);
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x00039BAF File Offset: 0x00037DAF
	public override void setData(BaseObjectData pData)
	{
		this.data = (BuildingData)pData;
		base.setData(pData);
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00039BC4 File Offset: 0x00037DC4
	internal void setHaveResources(bool pValue)
	{
		this.haveResources = pValue;
		switch (this.stats.buildingType)
		{
		case BuildingType.None:
		case BuildingType.Tree:
		case BuildingType.Stone:
		case BuildingType.Ore:
		case BuildingType.Gold:
			break;
		case BuildingType.Fruits:
			this.setSpriteMain(true);
			if (this.haveResources)
			{
				this.spriteAnimation.frames = this.animData.main;
			}
			else
			{
				this.spriteAnimation.frames = this.animData.special;
			}
			this.spriteAnimation.forceUpdateFrame();
			break;
		default:
			return;
		}
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00039C4C File Offset: 0x00037E4C
	public void setSpriteUnderConstruction()
	{
		if (this.stats.sprites.construction == null)
		{
			return;
		}
		this.setAnimData(0);
		this.setMainSprite(this.stats.sprites.construction);
		this.spriteAnimation.isOn = false;
		this.spriteRenderer.color = Color.white;
		this.spriteChanged();
		this.setScaleTween(0f, 0.2f);
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x00039CC4 File Offset: 0x00037EC4
	internal void setScaleTween(float pFrom = 0f, float pDuration = 0.2f)
	{
		if (Config.worldLoading)
		{
			this.finishScaleTween();
			return;
		}
		if (this.animationState != BuildingAnimationState.Normal)
		{
			return;
		}
		this.tween_scale_start = pFrom;
		this.tween_scale_target = 1f;
		this.tween_scale_time = 0f;
		this.tween_scale_duration = pDuration;
		this.tween_active = true;
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x00039D14 File Offset: 0x00037F14
	internal void finishScaleTween()
	{
		this.setAnimationState(BuildingAnimationState.Normal);
		this.tween_scale_start = 0f;
		this.tween_scale_target = 1f;
		this.tween_scale_duration = 1f;
		this.tween_scale_time = this.tween_scale_duration;
		this.tween_value = 1f;
		this.tween_active = false;
		this.currentScale.y = -100f;
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x00039D77 File Offset: 0x00037F77
	private void OnBecameVisible()
	{
		this._is_visible = true;
	}

	// Token: 0x060003DA RID: 986 RVA: 0x00039D80 File Offset: 0x00037F80
	private void OnBecameInvisible()
	{
		this._is_visible = false;
	}

	// Token: 0x060003DB RID: 987 RVA: 0x00039D8C File Offset: 0x00037F8C
	internal bool canBeUpgraded()
	{
		if (this.data.underConstruction || !this.data.alive)
		{
			return false;
		}
		bool flag = this.stats.canBeUpgraded;
		if (flag)
		{
			BuildingAsset buildingAsset = AssetManager.buildings.get(this.stats.upgradeTo);
			if (!string.IsNullOrEmpty(buildingAsset.tech))
			{
				Culture culture = this.city.getCulture();
				if (culture != null && !culture.haveTech(buildingAsset.tech))
				{
					flag = false;
				}
			}
		}
		return flag;
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00039E0C File Offset: 0x0003800C
	internal void upgradeBulding()
	{
		if (!this.canBeUpgraded())
		{
			return;
		}
		BuildingAsset buildingAsset = AssetManager.buildings.get(this.stats.upgradeTo);
		if ((buildingAsset.fundament.left != this.stats.fundament.left || buildingAsset.fundament.right != this.stats.fundament.right || buildingAsset.fundament.top != this.stats.fundament.top || buildingAsset.fundament.bottom != this.stats.fundament.bottom) && !this.checkTilesForUpgrade(this.currentTile, buildingAsset))
		{
			return;
		}
		if (this.city != null)
		{
			this.city.setBuildingDictID(this, false);
		}
		this.setTemplate(buildingAsset);
		if (this.city != null)
		{
			this.city.setBuildingDictID(this, true);
		}
		this.setSpriteMain(true);
		this.updateStats();
		this.data.health = this.curStats.health;
		this.fillTiles();
		if (this.world.qualityChanger.isFullLowRes())
		{
			this._is_visible = false;
		}
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00039F3C File Offset: 0x0003813C
	private void setTemplate(BuildingAsset pTemplate)
	{
		this.stats = pTemplate;
		this.data.templateID = this.stats.id;
		if (!string.IsNullOrEmpty(this.stats.kingdom))
		{
			this.setKingdom(this.world.kingdoms.dict_hidden[this.stats.kingdom], true);
		}
		this.m_transform.name = this.stats.id;
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00039FB8 File Offset: 0x000381B8
	internal void setKingdom(Kingdom pKingdom, bool pCheckHiddenKingdoms = true)
	{
		if (pKingdom == null)
		{
			if (this.kingdom != null)
			{
				this.kingdom.buildings.Remove(this);
			}
			this.kingdom = null;
			this.setSpriteDirty();
		}
		else
		{
			if (this.kingdom == null)
			{
				this.kingdom = pKingdom;
				this.kingdom.buildings.Add(this);
			}
			else if (this.kingdom != pKingdom)
			{
				this.kingdom.buildings.Remove(this);
				this.kingdom = pKingdom;
				this.kingdom.buildings.Add(this);
			}
			if (this.stats.docks && this.kingdom.isCiv())
			{
				base.GetComponent<Docks>().setKingdom(pKingdom);
			}
		}
		if (this.stats.cityBuilding && this.data.alive)
		{
			if (this.kingdom != null && this.kingdom.isCiv() && !this.isRuin())
			{
				this.setState(BuildingState.CivKingdom);
				this.setSpriteDirty();
			}
			else
			{
				this.setState(BuildingState.CivAbandoned);
			}
		}
		if (pCheckHiddenKingdoms && this.stats.cityBuilding && this.kingdom == null)
		{
			if (this.data.state == BuildingState.Ruins)
			{
				this.kingdom = this.world.kingdoms.getKingdomByID("ruins");
				this.kingdom.buildings.Add(this);
			}
			else if (this.data.state == BuildingState.CivAbandoned)
			{
				this.kingdom = this.world.kingdoms.getKingdomByID("abandoned");
				this.kingdom.buildings.Add(this);
			}
		}
		this.setTilesDirty();
	}

	// Token: 0x060003DF RID: 991 RVA: 0x0003A150 File Offset: 0x00038350
	private void setState(BuildingState pState)
	{
		this.checkAutoRemove();
		if (this.data.state == BuildingState.Removed)
		{
			return;
		}
		if (this.data.state == BuildingState.Ruins)
		{
			return;
		}
		if (pState == BuildingState.Ruins && this.data.state != BuildingState.Ruins)
		{
			this.data.health = this.curStats.health * 5;
			this.curStats.health = this.data.health;
			this.setSpriteDirty();
		}
		this.data.state = pState;
		this.updateColors();
		this.checkAnimated();
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0003A1E0 File Offset: 0x000383E0
	private void checkAnimated()
	{
		if (this.spriteAnimation == null)
		{
			return;
		}
		if (this.data.state == BuildingState.CivAbandoned)
		{
			this.spriteAnimation.isOn = false;
			return;
		}
		if (this.animData != null && this.animData.animated)
		{
			this.spriteAnimation.isOn = true;
		}
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0003A238 File Offset: 0x00038438
	internal void updateColors()
	{
		if (this.data.state == BuildingState.CivAbandoned || (this.stats.cityBuilding && this.data.state == BuildingState.Ruins))
		{
			this.spriteRenderer.color = Toolbox.color_abandoned_building;
			return;
		}
		this.spriteRenderer.color = Toolbox.color_white;
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x0003A290 File Offset: 0x00038490
	internal bool checkTilesForUpgrade(WorldTile pTile, BuildingAsset pTemplate)
	{
		int num = pTile.pos.x - pTemplate.fundament.left;
		int num2 = pTile.pos.y - pTemplate.fundament.bottom;
		int num3 = pTemplate.fundament.right + pTemplate.fundament.left + 1;
		int num4 = pTemplate.fundament.top + pTemplate.fundament.bottom + 1;
		for (int i = 0; i < num3; i++)
		{
			for (int j = 0; j < num4; j++)
			{
				WorldTile tile = this.world.GetTile(num + i, num2 + j);
				if (tile == null)
				{
					return false;
				}
				if (!tile.Type.canBuildOn)
				{
					return false;
				}
				if (tile.zone.city != this.city)
				{
					return false;
				}
				Building building = tile.building;
				if (building != null && building != this)
				{
					if (building.stats.priority >= this.stats.priority)
					{
						return false;
					}
					if (building.stats.upgradeLevel >= this.stats.upgradeLevel)
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x0003A3D0 File Offset: 0x000385D0
	internal void setRandomTemplate()
	{
		List<string> list = new List<string>();
		if (this.stats.type == "house")
		{
			if (this.stats.id.Contains("human"))
			{
				list.Add("tent_human");
				list.Add("house_human");
				list.Add("1house_human");
				list.Add("2house_human");
				list.Add("3house_human");
				list.Add("4house_human");
				list.Add("5house_human");
			}
			else if (this.stats.id.Contains("elf"))
			{
				list.Add("tent_elf");
				list.Add("house_elf");
				list.Add("1house_elf");
				list.Add("2house_elf");
				list.Add("3house_elf");
				list.Add("4house_elf");
				list.Add("5house_elf");
			}
			else if (this.stats.id.Contains("orc"))
			{
				list.Add("tent_orc");
				list.Add("house_orc");
				list.Add("1house_orc");
				list.Add("2house_orc");
				list.Add("3house_orc");
				list.Add("4house_orc");
				list.Add("5house_orc");
			}
			else if (this.stats.id.Contains("dwarf"))
			{
				list.Add("tent_dwarf");
				list.Add("house_dwarf");
				list.Add("1house_dwarf");
				list.Add("2house_dwarf");
				list.Add("3house_dwarf");
				list.Add("4house_dwarf");
				list.Add("5house_dwarf");
			}
		}
		if (this.stats.type == "hall")
		{
			if (this.stats.id.Contains("human"))
			{
				list.Add("hall_human");
				list.Add("1hall_human");
				list.Add("2hall_human");
			}
			else if (this.stats.id.Contains("elf"))
			{
				list.Add("hall_elf");
				list.Add("1hall_elf");
				list.Add("2hall_elf");
			}
			else if (this.stats.id.Contains("orc"))
			{
				list.Add("hall_orc");
				list.Add("1hall_orc");
				list.Add("2hall_orc");
			}
			else if (this.stats.id.Contains("dwarf"))
			{
				list.Add("hall_dwarf");
				list.Add("1hall_dwarf");
				list.Add("2hall_dwarf");
			}
		}
		if (list.Count == 0)
		{
			return;
		}
		this.setTemplate(AssetManager.buildings.get(list.GetRandom<string>()));
		this.setSpriteMain(true);
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x0003A6C2 File Offset: 0x000388C2
	internal void debugConstructions()
	{
		if (this.stats.sprites.construction == null)
		{
			return;
		}
		this.data.underConstruction = true;
		this.setSpriteUnderConstruction();
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x0003A6EF File Offset: 0x000388EF
	internal void debugRuins()
	{
		this.data.underConstruction = false;
		this.setSpriteRuin();
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x0003A704 File Offset: 0x00038904
	internal void debugNextFrame()
	{
		this.data.underConstruction = false;
		int num = this.animData_index + 1;
		if (num > this.stats.sprites.animationData.Count)
		{
			num = 0;
		}
		this.setAnimData(num);
		this.spriteAnimation.isOn = this.animData.animated;
		this.spriteAnimation.frames = this.animData.main;
		if (this.animData.animated)
		{
			this.spriteAnimation.timeBetweenFrames = 0.1f;
		}
		this.prepareForSave();
		this.spriteAnimation.forceUpdateFrame();
		this.spriteChanged();
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x0003A7A8 File Offset: 0x000389A8
	internal void setSpriteMain(bool pTween = true)
	{
		string id = this.stats.id;
		int num = Toolbox.randomInt(0, this.stats.sprites.animationData.Count);
		this.setAnimData(num);
		this.spriteAnimation.isOn = this.animData.animated;
		this.spriteAnimation.frames = this.animData.main;
		if (this.animData.animated)
		{
			this.spriteAnimation.timeBetweenFrames = 0.1f;
		}
		this.spriteAnimation.forceUpdateFrame();
		this.spriteChanged();
		if (this.stats.randomFlip)
		{
			this.spriteRenderer.flipX = Toolbox.randomBool();
		}
		this.setSpriteDirty();
		if (!Config.worldLoading && pTween)
		{
			this.setScaleTween(0f, 0.2f);
		}
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x0003A880 File Offset: 0x00038A80
	internal void checkSpriteConstructor()
	{
		if (!this.sprite_dirty)
		{
			return;
		}
		if (!this._is_visible || this.world.qualityChanger.lowRes)
		{
			return;
		}
		long buildingSpriteID = UnitSpriteConstructor.getBuildingSpriteID(this.id_sprite_building, this.kingdom.kingdomColor);
		this.sprite_dirty = false;
		this.batch.c_sprite_dirty.Remove(this);
		if (buildingSpriteID == this._last_contructed_sprite_id)
		{
			return;
		}
		this.batch.l_parallel_update_sprites.Add(this);
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x0003A8FC File Offset: 0x00038AFC
	internal void applyConstructedSprite()
	{
		Sprite spriteBuilding = UnitSpriteConstructor.getSpriteBuilding(this, this.kingdom.kingdomColor);
		long buildingSpriteID = UnitSpriteConstructor.getBuildingSpriteID(this.id_sprite_building, this.kingdom.kingdomColor);
		if (spriteBuilding == null)
		{
			return;
		}
		this._last_contructed_sprite_id = buildingSpriteID;
		this.spriteRenderer.sprite = spriteBuilding;
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x0003A94F File Offset: 0x00038B4F
	internal void setMainSprite(Sprite pSprite)
	{
		this.s_main_sprite = pSprite;
		this.id_sprite_building = pSprite.GetHashCode();
		this.setSpriteDirty();
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x0003A96C File Offset: 0x00038B6C
	private void fillFrontTiles()
	{
		this.frontTiles = new List<WorldTile>();
		int num = this.currentTile.pos.x - this.stats.fundament.left;
		int y = this.currentTile.pos.y;
		int bottom = this.stats.fundament.bottom;
		int num2 = this.stats.fundament.right + this.stats.fundament.left + 1;
		int top = this.stats.fundament.top;
		int bottom2 = this.stats.fundament.bottom;
		for (int i = 0; i < num2; i++)
		{
			WorldTile tile = this.world.GetTile(num + i, this.doorTile.pos.y);
			this.frontTiles.Add(tile);
		}
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x0003AA50 File Offset: 0x00038C50
	private void fillTiles()
	{
		if (this.tiles.Count != 0)
		{
			this.clearTiles();
		}
		int num = this.currentTile.pos.x - this.stats.fundament.left;
		int num2 = this.currentTile.pos.y - this.stats.fundament.bottom;
		int num3 = this.stats.fundament.right + this.stats.fundament.left + 1;
		int num4 = this.stats.fundament.top + this.stats.fundament.bottom + 1;
		int num5 = 0;
		int num6 = 0;
		for (int i = num5; i < num3; i++)
		{
			for (int j = num6; j < num4; j++)
			{
				WorldTile tile = this.world.GetTile(num + i, num2 + j);
				if (tile != null)
				{
					this.setBuildingTile(tile, i, j);
				}
			}
		}
		this.setTilesDirty();
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x0003AB54 File Offset: 0x00038D54
	internal void checkDirtyTiles()
	{
		if (!this.tiles_dirty)
		{
			return;
		}
		this.tiles_dirty = false;
		for (int i = 0; i < this.tiles.Count; i++)
		{
			WorldTile pTile = this.tiles[i];
			this.world.setTileDirty(pTile, false);
		}
		BatchBuildings batchBuildings = this.batch;
		if (batchBuildings == null)
		{
			return;
		}
		batchBuildings.c_tiles_dirty.Remove(this);
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x0003ABB7 File Offset: 0x00038DB7
	private void setTilesDirty()
	{
		this.tiles_dirty = true;
		BatchBuildings batchBuildings = this.batch;
		if (batchBuildings == null)
		{
			return;
		}
		batchBuildings.c_tiles_dirty.Add(this);
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x0003ABD6 File Offset: 0x00038DD6
	private void forceUpdateTilesDirty()
	{
		this.setTilesDirty();
		this.checkDirtyTiles();
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x0003ABE4 File Offset: 0x00038DE4
	private void setBuildingTile(WorldTile pTile, int pX, int pY)
	{
		if (pTile.building != null && pTile.building != this)
		{
			pTile.building.startRemove(false);
		}
		pTile.building = this;
		pTile.buildingX = pX;
		pTile.buildingY = pY;
		if (!this.tiles.Contains(pTile))
		{
			this.tiles.Add(pTile);
			if (!this.zones.Contains(pTile.zone))
			{
				this.zones.Add(pTile.zone);
				pTile.zone.addBuilding(this);
			}
		}
		TileType tileType = null;
		TopTileType topTileType = null;
		if (!string.IsNullOrEmpty(this.stats.transformTilesToTileType))
		{
			tileType = AssetManager.tiles.get(this.stats.transformTilesToTileType);
		}
		if (!string.IsNullOrEmpty(this.stats.transformTilesToTopTiles))
		{
			topTileType = AssetManager.topTiles.get(this.stats.transformTilesToTopTiles);
		}
		if (tileType != null || topTileType != null)
		{
			if (tileType == null)
			{
				tileType = pTile.main_type;
			}
			MapAction.terraformTile(pTile, tileType, topTileType, TerraformLibrary.nothing);
		}
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x0003ACEC File Offset: 0x00038EEC
	internal void retake()
	{
		this.setState(BuildingState.CivKingdom);
		this.data.alive = true;
		this.setSpriteDirty();
		for (int i = 0; i < this.zones.Count; i++)
		{
			TileZone tileZone = this.zones[i];
			tileZone.removeBuilding(this);
			tileZone.addBuilding(this);
		}
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x0003AD44 File Offset: 0x00038F44
	internal void makeAbandoned()
	{
		this.data.cityID = "";
		if (this.zones.Count != 0)
		{
			for (int i = 0; i < this.zones.Count; i++)
			{
				TileZone tileZone = this.zones[i];
				tileZone.buildings.Remove(this);
				tileZone.addBuilding(this);
			}
		}
		this.setTilesDirty();
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x0003ADA9 File Offset: 0x00038FA9
	internal bool isRuin()
	{
		return this.stats.isRuin || !this.data.alive || this.data.state == BuildingState.Ruins;
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x0003ADD7 File Offset: 0x00038FD7
	public bool isAbandoned()
	{
		return this.data.state == BuildingState.CivAbandoned;
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x0003ADE7 File Offset: 0x00038FE7
	public void prepareForSave()
	{
		this.data.frameID = this.animData_index;
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x0003ADFA File Offset: 0x00038FFA
	public bool isNonUsable()
	{
		return this.isRuin() || this.isAbandoned();
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x0003AE10 File Offset: 0x00039010
	internal void startDestroyBuilding(bool pRemove = false)
	{
		this.clearCity();
		this.setSpriteDirty();
		this.setOnMakeRuin();
		this.setState(BuildingState.Ruins);
		if (string.IsNullOrEmpty(this.stats.ruins) || this.data.underConstruction)
		{
			pRemove = true;
		}
		if (pRemove)
		{
			this.setSpriteRuin();
			this.startRemove(true);
			return;
		}
		this.setKingdom(this.world.kingdoms.getKingdomByID("ruins"), true);
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x0003AE88 File Offset: 0x00039088
	private void clearZoneBuilding()
	{
		for (int i = 0; i < this.zones.Count; i++)
		{
			this.zones[i].removeBuilding(this);
		}
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x0003AEC0 File Offset: 0x000390C0
	private void clearCity()
	{
		if (this.city == null)
		{
			return;
		}
		this.city.removeBuilding(this);
		this.clearCityZones();
		this.setCity(null, false);
		this.setKingdom(this.world.kingdoms.getKingdomByID("ruins"), true);
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x0003AF14 File Offset: 0x00039114
	internal void kill()
	{
		if (!this.data.alive)
		{
			return;
		}
		this.clearZoneBuilding();
		this.data.alive = false;
		if (this.stats.cityBuilding && !this.stats.isRuin)
		{
			this.world.mapStats.housesDestroyed++;
		}
		this.clearCity();
		for (int i = 0; i < this.zones.Count; i++)
		{
			this.zones[i].addBuilding(this);
		}
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x0003AFA4 File Offset: 0x000391A4
	private void clearCityZones()
	{
		this.clearZoneBuilding();
		if (this.city != null)
		{
			for (int i = 0; i < this.zones.Count; i++)
			{
				TileZone tileZone = this.zones[i];
				if (tileZone.buildings.Count <= 0)
				{
					this.city.removeZone(tileZone, false);
				}
			}
		}
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x0003B003 File Offset: 0x00039203
	private void clearZones()
	{
		this.clearZoneBuilding();
		this.zones.Clear();
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x0003B018 File Offset: 0x00039218
	internal void setCity(City pCity, bool pSetKingdom = true)
	{
		this.city = pCity;
		if (pCity != null)
		{
			this.data.cityID = this.city.data.cityID;
			if (pSetKingdom)
			{
				this.setKingdom(pCity.kingdom, true);
				return;
			}
		}
		else
		{
			this.data.cityID = "";
			if (pSetKingdom)
			{
				this.setKingdom(null, true);
			}
		}
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x0003B07C File Offset: 0x0003927C
	internal void setSpriteRuin()
	{
		if (string.IsNullOrEmpty(this.stats.ruins))
		{
			return;
		}
		this.clearZoneBuilding();
		this.zones.Clear();
		this.clearTiles();
		if (!this.data.underConstruction)
		{
			string ruins = this.stats.ruins;
			if (this.animData.animated)
			{
				this.spriteAnimation.currentFrameIndex = 0;
				this.spriteAnimation.forceUpdateFrame();
			}
			if (this.stats.ruins == "same_id")
			{
				this.spriteAnimation.frames = this.animData.ruins;
				this.setMainSprite(this.animData.ruins[0]);
				if (!Config.worldLoading)
				{
					this.setScaleTween(0f, 0.2f);
				}
			}
			else
			{
				this.setTemplate(AssetManager.buildings.get(ruins));
				this.setSpriteMain(false);
			}
			this.spriteAnimation.isOn = false;
		}
		this.fillTiles();
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x0003B176 File Offset: 0x00039376
	private void spriteChanged()
	{
		if (this.spriteAnimation.isOn && base.GetComponent<BuildingAnimationUpdated>() == null)
		{
			this.addComponent(this.m_gameObject.AddComponent<BuildingAnimationUpdated>());
		}
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x0003B1A4 File Offset: 0x000393A4
	internal override void updateStats()
	{
		base.updateStats();
		this.curStats.clear();
		this.curStats.addStats(this.stats.baseStats);
		this.batch.c_stats_dirty.Remove(this);
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x0003B1E0 File Offset: 0x000393E0
	internal void chopTree()
	{
		if (this.chopped)
		{
			return;
		}
		base.removeAllStatusEffects();
		Sfx.play("treeFall", true, this.m_transform.localPosition.x, this.m_transform.localPosition.y);
		this.chopped = true;
		this.haveResources = false;
		float z = (float)(Toolbox.randomBool() ? 90 : -90);
		this.curTween = TweenSettingsExtensions.OnComplete<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(ShortcutExtensions.DORotate(this.m_transform, new Vector3(0f, 0f, z), 1f, 0), 17), new TweenCallback(this.finishChop));
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x0003B283 File Offset: 0x00039483
	private void finishChop()
	{
		this.cancelTween();
		this.startRemove(false);
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x0003B292 File Offset: 0x00039492
	internal void startRemove(bool pSetRuinSprite = true)
	{
		if (this.isAnimationState(BuildingAnimationState.OnRemove))
		{
			return;
		}
		this.cancelTween();
		this.setAnimationState(BuildingAnimationState.OnRemove);
		this.clearTiles();
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x0003B2B1 File Offset: 0x000394B1
	private bool isAnimationState(BuildingAnimationState pState)
	{
		return this.animationState == pState;
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x0003B2BC File Offset: 0x000394BC
	private void setOnMakeRuin()
	{
		if (this.isAnimationState(BuildingAnimationState.OnRuin))
		{
			return;
		}
		if (this.data.state == BuildingState.Ruins)
		{
			return;
		}
		this.setAnimationState(BuildingAnimationState.OnRuin);
		if (!this.data.underConstruction && this.data.alive)
		{
			Sfx.play(this.stats.destroyedSound, true, (float)this.currentTile.pos.x, (float)this.currentTile.pos.y);
		}
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x0003B33C File Offset: 0x0003953C
	internal void destroyBuilding()
	{
		this.setState(BuildingState.Removed);
		this.cancelTween();
		this.setAnimationState(BuildingAnimationState.Normal);
		this.clearZones();
		this.kill();
		this.setKingdom(null, false);
		this.world.removeBuildingFully(this);
		this.clearTiles();
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x0003B378 File Offset: 0x00039578
	internal void clearTiles()
	{
		this.forceUpdateTilesDirty();
		for (int i = 0; i < this.tiles.Count; i++)
		{
			this.tiles[i].building = null;
		}
		this.tiles.Clear();
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x0003B3BE File Offset: 0x000395BE
	private void cancelTween()
	{
		if (this.curTween != null && this.curTween.active)
		{
			TweenExtensions.Kill(this.curTween, false);
		}
		this.tween_scale_start = 1f;
		this.tween_scale_target = 1f;
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0003B3F8 File Offset: 0x000395F8
	private void checkTweens()
	{
		if (this.curTween != null && this.curTween.active)
		{
			return;
		}
		BuildingAnimationState buildingAnimationState = this.animationState;
		if (buildingAnimationState != BuildingAnimationState.OnRuin)
		{
			if (buildingAnimationState != BuildingAnimationState.OnRemove)
			{
				return;
			}
			this.cancelTween();
			Vector3 vector = new Vector3(Building.defaultScale.x, 0f, Building.defaultScale.z);
			Ease ease = 26;
			if (this.chopped)
			{
				vector.x = 0f;
				vector.y = Building.defaultScale.y;
				ease = 8;
			}
			this.curTween = TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.m_transform, vector, 0.5f), ease), new TweenCallback(this.destroyBuilding));
			if (this.stats.cityBuilding)
			{
				this.startShake(0.5f);
			}
			return;
		}
		else
		{
			this.cancelTween();
			if (Config.timeScale > 10f)
			{
				this.completeMakingRuin();
				return;
			}
			Vector3 vector2 = new Vector3(Building.defaultScale.x, Building.defaultScale.y * 0.7f, Building.defaultScale.z);
			this.curTween = TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.m_transform, vector2, 0.1f), 8), new TweenCallback(this.completeMakingRuin));
			return;
		}
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x0003B534 File Offset: 0x00039734
	private void setAnimationState(BuildingAnimationState pState)
	{
		this.animationState = pState;
		this.checkTweens();
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x0003B544 File Offset: 0x00039744
	private void completeMakingRuin()
	{
		this.kill();
		this.setSpriteDirty();
		this.setAnimationState(BuildingAnimationState.Normal);
		this.setSpriteRuin();
		Vector3 vector = new Vector3(Building.defaultScale.x, Building.defaultScale.y, Building.defaultScale.z);
		this.curTween = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.m_transform, vector, 0.1f), 26);
		this.setScaleTween(0f, 0.2f);
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x0003B5C0 File Offset: 0x000397C0
	public void forceScale(float pScale)
	{
		this.scaleVal = 0f;
		if (this.currentScale.y != this.scaleVal)
		{
			this.currentScale.x = Building.defaultScale.x;
			this.currentScale.y = this.scaleVal;
			this.currentScale.z = Building.defaultScale.z;
			this.m_transform.localScale = this.currentScale;
		}
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x0003B638 File Offset: 0x00039838
	internal void updateScale(float pElapsed)
	{
		if (!this._is_visible)
		{
			return;
		}
		if (this.tween_active || this.scaleVal != this.world.qualityChanger.tweenBuildings)
		{
			if (this.tween_active && this.tween_scale_time < this.tween_scale_duration)
			{
				this.tween_scale_time += pElapsed;
				if (this.tween_scale_time >= this.tween_scale_duration)
				{
					this.tween_scale_time = this.tween_scale_duration;
					this.tween_active = false;
				}
				if (this.tween_scale_time == this.tween_scale_duration)
				{
					this.tween_value = 1f;
				}
				else
				{
					this.tween_value = iTween.easeOutBack(this.tween_scale_start, this.tween_scale_target, this.tween_scale_time / this.tween_scale_duration);
				}
			}
			else
			{
				this.tween_value = 1f;
			}
			this.scaleVal = this.world.qualityChanger.tweenBuildings * this.tween_value;
			if (this.currentScale.y != this.scaleVal)
			{
				this.currentScale.y = this.scaleVal;
				this.currentScale.z = Building.defaultScale.z;
				this.batch.l_parallel_scale.Add(this);
			}
		}
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x0003B769 File Offset: 0x00039969
	internal void applyScale()
	{
		this.m_transform.localScale = this.currentScale;
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x0003B77C File Offset: 0x0003997C
	private void angleEffect(float pElapsed)
	{
		if (this.stats.fauna)
		{
			if (this.interval == 0f)
			{
				this.interval = 0.1f + Toolbox.randomFloat(0f, 0.1f);
			}
			this.angle_timer += pElapsed * this.interval;
			if (this.angle_timer >= 1f)
			{
				this.goingLeft = !this.goingLeft;
				this.angle_timer = 0f;
			}
			if (this.goingLeft)
			{
				this.angle = iTween.easeOutBack(-5f, 5f, this.angle_timer);
			}
			else
			{
				this.angle = iTween.easeOutBack(5f, -5f, this.angle_timer);
			}
			base.transform.eulerAngles = new Vector3(0f, 0f, this.angle);
		}
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x0003B860 File Offset: 0x00039A60
	private void checkAutoRemove()
	{
		if (this.stats.auto_remove_ruin && (this.data.state == BuildingState.Ruins || this.data.state == BuildingState.CivAbandoned))
		{
			this.batch.c_auto_remove.Add(this);
			return;
		}
		this.batch.c_auto_remove.Remove(this);
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x0003B8BC File Offset: 0x00039ABC
	internal void updateAutoRemove(float pElapsed)
	{
		if (this._autoRemoveTimer < 300f)
		{
			this._autoRemoveTimer += pElapsed;
			return;
		}
		this._autoRemoveTimer = 0f;
		this.batch.c_auto_remove.Remove(this);
		this.startRemove(true);
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x0003B908 File Offset: 0x00039B08
	public override void update(float pElapsed)
	{
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x0003B90A File Offset: 0x00039B0A
	internal override void addStatusEffect(string pID, float pOverrideTimer = -1f)
	{
		base.addStatusEffect(pID, pOverrideTimer);
		if (base.hasAnyStatusEffect())
		{
			this.batch.c_status_effects.Add(this);
		}
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x0003B92D File Offset: 0x00039B2D
	internal override void removeStatusEffect(string pID, ActiveStatusEffect pEffect = null, int pIndex = -1)
	{
		base.removeStatusEffect(pID, pEffect, pIndex);
		if (!base.hasAnyStatusEffect())
		{
			this.batch.c_status_effects.Remove(this);
		}
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x0003B951 File Offset: 0x00039B51
	internal void updateTimerShakeResources(float pElapsed)
	{
		if (this.timerShakeResource > 0f)
		{
			this.timerShakeResource -= pElapsed;
			if (this.timerShakeResource <= 0f)
			{
				this.batch.c_resource_shaker.Remove(this);
			}
		}
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x0003B98C File Offset: 0x00039B8C
	internal void updateComponents(float pElapsed)
	{
		for (int i = 0; i < this.components.Count; i++)
		{
			this.components[i].update(pElapsed);
		}
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x0003B9C4 File Offset: 0x00039BC4
	public void updatePosition()
	{
		if (!this.positionDirty)
		{
			return;
		}
		this.positionDirty = false;
		this.batch.c_position_dirty.Remove(this);
		Vector3 posV = this.currentTile.posV3;
		posV.z = posV.y + 0.15f;
		if (posV.z < 0f)
		{
			posV.z = 0f;
		}
		posV.x += this.shakeOffset.x;
		posV.y += this.shakeOffset.y;
		this.m_transform.localPosition = posV;
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x0003BA64 File Offset: 0x00039C64
	internal void spawnBurstSpecial(int pAmount = 1, float pModeDist = 1f, float pModeZ = 1f)
	{
		for (int i = 0; i < pAmount; i++)
		{
			this.world.dropManager.spawnBurstPixel(this.currentTile, this.stats.spawnDropID, Toolbox.randomFloat(0.2f, 0.5f * pModeDist), Toolbox.randomFloat(1.3f, 1f * pModeZ), 0f, -1f);
		}
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x0003BACC File Offset: 0x00039CCC
	internal void updateBuild(int pProgress = 1)
	{
		this.data.progress += pProgress;
		this.startShake(0.3f);
		if (this.data.progress > 10)
		{
			this.data.progress = 10;
			this.data.underConstruction = false;
			this.setSpriteMain(true);
			this.setScaleTween(0.25f, 0.2f);
			return;
		}
		this.setScaleTween(0.75f, 0.2f);
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x0003BB47 File Offset: 0x00039D47
	public void startShake(float pVal)
	{
		this.shakeTimer = pVal;
		BatchBuildings batchBuildings = this.batch;
		if (batchBuildings == null)
		{
			return;
		}
		batchBuildings.c_shake.Add(this);
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x0003BB68 File Offset: 0x00039D68
	internal void resourceGathering(float pElapsed)
	{
		if (this.timerShakeResource > 0f)
		{
			return;
		}
		this.batch.c_resource_shaker.Add(this);
		this.startShake(0.3f);
		this.timerShakeResource = 1f;
		switch (this.stats.buildingType)
		{
		case BuildingType.None:
		case BuildingType.Fruits:
			break;
		case BuildingType.Tree:
			Sfx.play("chopping", true, this.currentPosition.x, this.currentPosition.y);
			return;
		case BuildingType.Stone:
		case BuildingType.Ore:
		case BuildingType.Gold:
			Sfx.play("mining", true, this.currentPosition.x, this.currentPosition.y);
			break;
		default:
			return;
		}
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x0003BC18 File Offset: 0x00039E18
	public void updateShake(float pElapsed)
	{
		if (this.shakeTimer > 0f)
		{
			this.shakeOffset.Set(Toolbox.randomFloat(-0.1f, 0.1f), Toolbox.randomFloat(-0.1f, 0.1f), 0f);
			this.setPositionDirty();
			this.shakeTimer -= pElapsed;
			if (this.shakeTimer < 0f)
			{
				this.setPositionDirty();
				this.shakeOffset.Set(0f, 0f, 0f);
				this.batch.c_shake.Remove(this);
			}
		}
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x0003BCB8 File Offset: 0x00039EB8
	internal override void getHit(float pDamage, bool pFlash = true, AttackType pType = AttackType.Other, BaseSimObject pAttacker = null, bool pSkipIfShake = true)
	{
		if (this.isAnimationState(BuildingAnimationState.OnRuin) && this.curTween != null && this.curTween.active)
		{
			return;
		}
		this.data.health -= (int)pDamage;
		this.setScaleTween(0.75f, 0.2f);
		this.startShake(0.3f);
		if (this.data.health <= 0)
		{
			if (this.data.state == BuildingState.Ruins)
			{
				this.startDestroyBuilding(true);
				return;
			}
			this.startDestroyBuilding(false);
		}
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x0003BD40 File Offset: 0x00039F40
	internal int extractResources(Actor pBy, int pAmount = 1)
	{
		this.setScaleTween(0.75f, 0.2f);
		Culture culture = pBy.getCulture();
		int result = pAmount;
		switch (this.stats.buildingType)
		{
		case BuildingType.Tree:
			if (culture != null && culture.haveTech("sharp_axes") && Toolbox.randomChance(culture.stats.bonus_res_chance_wood.value))
			{
				result = (int)culture.stats.bonus_res_wood_amount.value;
			}
			this.chopTree();
			break;
		case BuildingType.Stone:
		case BuildingType.Ore:
		case BuildingType.Gold:
			if (culture != null && culture.haveTech("mining_efficiency") && Toolbox.randomChance(culture.stats.bonus_res_chance_ores.value))
			{
				result = (int)culture.stats.bonus_res_ore_amount.value;
			}
			break;
		case BuildingType.Fruits:
			base.GetComponent<BuildingFruitGrowth>().resourceResetTime = 90f;
			this.setHaveResources(false);
			break;
		case BuildingType.Wheat:
			this.startDestroyBuilding(true);
			break;
		}
		return result;
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x0003BE37 File Offset: 0x0003A037
	internal Color32 getColor(WorldTile pTile)
	{
		if (Config.EVERYTHING_MAGIC_COLOR)
		{
			return Toolbox.EVERYTHING_MAGIC_COLOR32;
		}
		return this.stats.sprites.mapIcon.getColor(pTile.buildingX, pTile.buildingY, this);
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x0003BE68 File Offset: 0x0003A068
	private void checkResourceAmount(int pAmount)
	{
		if (pAmount <= 0)
		{
			this.startRemove(true);
			return;
		}
		this.stats.id.Contains("_s");
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x0003BE8C File Offset: 0x0003A08C
	private void changeResourceSprite(string pType)
	{
		string text = this.stats.id;
		text = text.Replace("_s", "");
		text = text.Replace("_m", "");
		text += pType;
		this.setTemplate(AssetManager.buildings.get(text));
		this.setSpriteMain(true);
		this.setScaleTween(0.75f, 0.2f);
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x0003BEF8 File Offset: 0x0003A0F8
	public WorldTile getConstructionTile()
	{
		WorldTile result = null;
		Toolbox.temp_list_tiles.Clear();
		if (this.stats.docks)
		{
			this.checkZoneForDockConstruction(this.currentTile.zone);
			for (int i = 0; i < this.currentTile.zone.neighboursAll.Count; i++)
			{
				TileZone pZone = this.currentTile.zone.neighboursAll[i];
				this.checkZoneForDockConstruction(pZone);
			}
			if (Toolbox.temp_list_tiles.Count > 0)
			{
				result = Toolbox.temp_list_tiles.GetRandom<WorldTile>();
			}
		}
		else
		{
			result = Toolbox.getRandom<WorldTile>(this.tiles);
		}
		return result;
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x0003BF94 File Offset: 0x0003A194
	private void checkZoneForDockConstruction(TileZone pZone)
	{
		if (pZone.city == null)
		{
			return;
		}
		if (pZone.city != this.city)
		{
			return;
		}
		for (int i = 0; i < pZone.tiles.Count; i++)
		{
			WorldTile worldTile = pZone.tiles[i];
			if (worldTile.Type.ground && Toolbox.DistTile(this.currentTile, worldTile) <= 7f)
			{
				Toolbox.temp_list_tiles.Add(worldTile);
			}
		}
	}

	// Token: 0x04000627 RID: 1575
	public BatchBuildings batch;

	// Token: 0x04000628 RID: 1576
	internal bool positionDirty;

	// Token: 0x04000629 RID: 1577
	internal bool sprite_dirty;

	// Token: 0x0400062A RID: 1578
	internal bool tiles_dirty;

	// Token: 0x0400062B RID: 1579
	internal Sprite s_main_sprite;

	// Token: 0x0400062C RID: 1580
	internal int id_sprite_building;

	// Token: 0x0400062D RID: 1581
	internal BuildingData data;

	// Token: 0x0400062E RID: 1582
	internal BuildingAsset stats;

	// Token: 0x0400062F RID: 1583
	internal bool haveResources;

	// Token: 0x04000630 RID: 1584
	internal List<WorldTile> tiles;

	// Token: 0x04000631 RID: 1585
	internal List<WorldTile> frontTiles;

	// Token: 0x04000632 RID: 1586
	internal WorldTile doorTile;

	// Token: 0x04000633 RID: 1587
	public BuildingAnimationDataNew animData;

	// Token: 0x04000634 RID: 1588
	public int animData_index;

	// Token: 0x04000635 RID: 1589
	internal Tweener curTween;

	// Token: 0x04000636 RID: 1590
	private float shakeTimer;

	// Token: 0x04000637 RID: 1591
	protected Vector3 shakeOffset = new Vector3(0f, 0f, 0f);

	// Token: 0x04000638 RID: 1592
	internal static Vector3 defaultScale = new Vector3(0.25f, 0.25f, 0.25f);

	// Token: 0x04000639 RID: 1593
	internal List<TileZone> zones = new List<TileZone>();

	// Token: 0x0400063A RID: 1594
	internal BuildingAnimationState animationState;

	// Token: 0x0400063B RID: 1595
	internal List<BaseBuildingComponent> components;

	// Token: 0x0400063C RID: 1596
	internal float scaleVal;

	// Token: 0x0400063D RID: 1597
	private float tween_scale_start;

	// Token: 0x0400063E RID: 1598
	private float tween_scale_target = 1f;

	// Token: 0x0400063F RID: 1599
	private float tween_scale_time;

	// Token: 0x04000640 RID: 1600
	private float tween_scale_duration = 1f;

	// Token: 0x04000641 RID: 1601
	private float tween_value = 1f;

	// Token: 0x04000642 RID: 1602
	internal bool tween_active;

	// Token: 0x04000643 RID: 1603
	internal bool chopped;

	// Token: 0x04000644 RID: 1604
	internal bool _is_visible;

	// Token: 0x04000645 RID: 1605
	private long _last_contructed_sprite_id = -1L;

	// Token: 0x04000646 RID: 1606
	private float _autoRemoveTimer;

	// Token: 0x04000647 RID: 1607
	private float angle;

	// Token: 0x04000648 RID: 1608
	private bool goingLeft = true;

	// Token: 0x04000649 RID: 1609
	private float angle_timer;

	// Token: 0x0400064A RID: 1610
	private float interval;

	// Token: 0x0400064B RID: 1611
	internal float timerShakeResource;
}
