using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000087 RID: 135
public class DropManager : BaseMapObject
{
	// Token: 0x060002F5 RID: 757 RVA: 0x00032670 File Offset: 0x00030870
	internal override void create()
	{
		base.create();
		string path = "effects/p_drop";
		this.original_drop = (GameObject)Resources.Load(path, typeof(GameObject));
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x000326A4 File Offset: 0x000308A4
	public Drop spawn(WorldTile pTile, string pDropID, float zHeight = -1f, float pScale = -1f)
	{
		DropAsset pAsset = AssetManager.drops.get(pDropID);
		return this.spawn(pTile, pAsset, zHeight, pScale);
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x000326C8 File Offset: 0x000308C8
	public Drop spawn(WorldTile pTile, DropAsset pAsset, float zHeight = -1f, float pScale = -1f)
	{
		Drop @object = this.GetObject();
		@object.launch(pTile, pAsset, zHeight);
		if (pScale == -1f)
		{
			pScale = pAsset.default_scale;
		}
		@object.setScale(new Vector3(pScale, pScale, @object.transform.localScale.z));
		return @object;
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x00032718 File Offset: 0x00030918
	public void spawnBurstPixel(WorldTile pTile, string pDropID, float pForce, float pForceZ, float pStartZ = 0f, float pScale = -1f)
	{
		Drop drop = this.spawn(pTile, pDropID, pStartZ, pScale);
		drop.burst = true;
		drop.zPosition.z = pStartZ;
		drop.updatePosition();
		float f = Toolbox.randomFloat(-3.1415927f, 3.1415927f);
		float pX = pForce * Mathf.Cos(f);
		float pY = pForce * Mathf.Sin(f);
		float pZ = Toolbox.randomFloat(pForceZ, pForceZ * 2f);
		drop.addForce(pX, pY, pZ);
		drop.updatePosition();
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x0003278C File Offset: 0x0003098C
	public void spawnPixel(WorldTile pTile, WorldTile pCenterTile, bool pLimit = false)
	{
	}

	// Token: 0x060002FA RID: 762 RVA: 0x0003279C File Offset: 0x0003099C
	public void clear()
	{
		for (int i = 0; i < this.drops.Count; i++)
		{
			Drop pObject = this.drops[i];
			this.makeInactive(pObject);
		}
		this.activeIndex = 0;
	}

	// Token: 0x060002FB RID: 763 RVA: 0x000327DA File Offset: 0x000309DA
	private void makeInactive(Drop pObject)
	{
		pObject.active = false;
		pObject.gameObject.SetActive(false);
	}

	// Token: 0x060002FC RID: 764 RVA: 0x000327F0 File Offset: 0x000309F0
	private void killObject(Drop pObject)
	{
		this.makeInactive(pObject);
		int num = pObject.drop_index - 1;
		int num2 = this.activeIndex - 1;
		if (num != num2)
		{
			this.switchDrop = this.drops[num2];
			this.drops[num2] = pObject;
			this.drops[num] = this.switchDrop;
			pObject.drop_index = num2 + 1;
			this.switchDrop.drop_index = num + 1;
		}
		if (this.activeIndex > 0)
		{
			this.activeIndex--;
		}
		this.switchDrop = null;
	}

	// Token: 0x060002FD RID: 765 RVA: 0x00032880 File Offset: 0x00030A80
	public void landPixel(Drop pObject)
	{
		WorldTile currentTile = pObject.currentTile;
		this.killObject(pObject);
		if (currentTile == null)
		{
			return;
		}
		this.world.flashEffects.flashPixel(currentTile, 14, ColorType.White);
	}

	// Token: 0x060002FE RID: 766 RVA: 0x000328B4 File Offset: 0x00030AB4
	private void debugString()
	{
		string str = "";
		for (int i = 0; i < this.drops.Count; i++)
		{
			if (this.drops[i].active)
			{
				str += "O";
			}
			else
			{
				str += "x";
			}
		}
		Debug.Log(str + " ::: " + this.activeIndex.ToString());
	}

	// Token: 0x060002FF RID: 767 RVA: 0x00032928 File Offset: 0x00030B28
	public Drop GetObject()
	{
		if (this.drops.Count > this.activeIndex)
		{
			this.tDrop = this.drops[this.activeIndex];
		}
		else
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.original_drop, base.transform);
			this.tDrop = gameObject.GetComponent<Drop>();
			this.tDrop.gameObject.layer = base.gameObject.layer;
			this.tDrop.controller = this;
			this.tDrop.transform.parent = base.transform;
			this.drops.Add(this.tDrop);
			this.tDrop.drop_index = this.drops.Count;
		}
		this.activeIndex++;
		this.tDrop.gameObject.SetActive(true);
		this.tDrop.burst = false;
		this.tDrop.createGround = true;
		this.tDrop.forcedTileType = null;
		this.tDrop.transform.localScale = Vector3.one;
		this.tDrop.active = true;
		return this.tDrop;
	}

	// Token: 0x06000300 RID: 768 RVA: 0x00032A50 File Offset: 0x00030C50
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this.timeout_timer > 0f)
		{
			this.timeout_timer -= this.world.deltaTime;
		}
		for (int i = this.activeIndex - 1; i >= 0; i--)
		{
			Drop drop = this.drops[i];
			if (drop.created && drop.active)
			{
				drop.update(Time.fixedDeltaTime);
			}
			else if (this.activeIndex == drop.drop_index)
			{
				this.activeIndex--;
				Debug.LogError("do we ever hit this??? " + this.activeIndex.ToString());
			}
		}
	}

	// Token: 0x06000301 RID: 769 RVA: 0x00032B00 File Offset: 0x00030D00
	public void debug(DebugTool pTool)
	{
		pTool.setText("drops total", this.drops.Count.ToString() ?? "");
		pTool.setText("drops active", this.activeIndex.ToString() ?? "");
	}

	// Token: 0x0400049A RID: 1178
	private List<Drop> drops = new List<Drop>();

	// Token: 0x0400049B RID: 1179
	private float timeout_timer;

	// Token: 0x0400049C RID: 1180
	private int limitBurstPixels;

	// Token: 0x0400049D RID: 1181
	internal int activeIndex;

	// Token: 0x0400049E RID: 1182
	private GameObject original_drop;

	// Token: 0x0400049F RID: 1183
	private Drop switchDrop;

	// Token: 0x040004A0 RID: 1184
	private Drop tDrop;
}
