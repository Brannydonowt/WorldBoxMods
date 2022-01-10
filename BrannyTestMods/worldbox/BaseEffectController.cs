using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FE RID: 254
public class BaseEffectController : BaseMapObject
{
	// Token: 0x060005A6 RID: 1446 RVA: 0x00045763 File Offset: 0x00043963
	internal override void create()
	{
		base.create();
		this.list = new List<BaseEffect>();
		this.timer_interval = 0.9f;
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x00045784 File Offset: 0x00043984
	public BaseEffect GetObject()
	{
		BaseEffect baseEffect;
		if (this.list.Count > this.activeIndex)
		{
			baseEffect = this.list[this.activeIndex];
		}
		else
		{
			baseEffect = Object.Instantiate<Transform>(this.prefab).gameObject.GetComponent<BaseEffect>();
			this.addNewObject(baseEffect);
			if (!baseEffect.created)
			{
				baseEffect.create();
			}
			this.list.Add(baseEffect);
			baseEffect.effectIndex = this.list.Count;
		}
		this.activeIndex++;
		baseEffect.active = true;
		baseEffect.gameObject.SetActive(true);
		baseEffect.state = 1;
		baseEffect.clear();
		return baseEffect;
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x00045831 File Offset: 0x00043A31
	internal void addNewObject(BaseEffect pEffect)
	{
		pEffect.setWorld();
		pEffect.controller = this;
		pEffect.transform.parent = base.transform;
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x00045854 File Offset: 0x00043A54
	public void killObject(BaseEffect pObject)
	{
		if (!pObject.active)
		{
			return;
		}
		this.makeInactive(pObject);
		int num = pObject.effectIndex - 1;
		int num2 = this.activeIndex - 1;
		if (num != num2)
		{
			this.switchObject = this.list[num2];
			this.list[num2] = pObject;
			this.list[num] = this.switchObject;
			pObject.effectIndex = num2 + 1;
			this.switchObject.effectIndex = num + 1;
		}
		if (this.activeIndex > 0)
		{
			this.activeIndex--;
		}
		this.switchObject = null;
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x000458ED File Offset: 0x00043AED
	private void makeInactive(BaseEffect pObject)
	{
		pObject.active = false;
		pObject.transform.SetParent(base.transform);
		pObject.gameObject.SetActive(false);
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x00045914 File Offset: 0x00043B14
	private void debugString()
	{
		string str = "";
		for (int i = 0; i < this.list.Count; i++)
		{
			if (this.list[i].active)
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

	// Token: 0x060005AC RID: 1452 RVA: 0x00045985 File Offset: 0x00043B85
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		this.updateChildren(pElapsed);
		this.updateSpawn(pElapsed);
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x0004599C File Offset: 0x00043B9C
	private void updateSpawn(float pElapsed)
	{
		if (this.world.isPaused())
		{
			return;
		}
		if (this.useInterval)
		{
			if (this.timer > 0f)
			{
				this.timer -= pElapsed;
				return;
			}
			this.timer = this.timer_interval;
			this.spawn();
		}
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x000459F0 File Offset: 0x00043BF0
	private void updateChildren(float pElapsed)
	{
		for (int i = this.activeIndex - 1; i >= 0; i--)
		{
			BaseEffect baseEffect = this.list[i];
			if (baseEffect.created && baseEffect.active)
			{
				baseEffect.update(pElapsed);
			}
		}
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x00045A36 File Offset: 0x00043C36
	public virtual void spawn()
	{
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x00045A38 File Offset: 0x00043C38
	public BaseEffect spawnNew()
	{
		if (this.isInLimit())
		{
			return null;
		}
		BaseEffect @object = this.GetObject();
		if (@object.spriteAnimation != null)
		{
			@object.spriteAnimation.resetAnim(0);
		}
		return @object;
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00045A71 File Offset: 0x00043C71
	internal virtual BaseEffect spawnAt(WorldTile pTile, float pScale = 0.5f)
	{
		if (this.isInLimit())
		{
			return null;
		}
		BaseEffect @object = this.GetObject();
		@object.prepare(pTile, pScale);
		return @object;
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x00045A8B File Offset: 0x00043C8B
	internal virtual BaseEffect spawnAt(Vector3 pVector, float pScale = 0.5f)
	{
		if (this.isInLimit())
		{
			return null;
		}
		BaseEffect @object = this.GetObject();
		@object.prepare(pVector, pScale);
		return @object;
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x00045AA8 File Offset: 0x00043CA8
	public BaseEffect spawnAtRandomScale(WorldTile pTile, float pScaleMin = 1f, float pScaleMax = 1f)
	{
		if (this.isInLimit())
		{
			return null;
		}
		BaseEffect @object = this.GetObject();
		float pScale = Toolbox.randomFloat(pScaleMin, pScaleMax);
		@object.prepare(pTile, pScale);
		return @object;
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x00045AD5 File Offset: 0x00043CD5
	private bool isInLimit()
	{
		return this.objectLimit != 0 && this.activeIndex > this.objectLimit;
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x00045AF0 File Offset: 0x00043CF0
	internal void clear()
	{
		for (int i = 0; i < this.list.Count; i++)
		{
			BaseEffect pObject = this.list[i];
			this.makeInactive(pObject);
		}
		this.activeIndex = 0;
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x00045B30 File Offset: 0x00043D30
	internal void debug(DebugTool pTool)
	{
		pTool.setText(base.name, this.activeIndex.ToString() + "/" + this.list.Count.ToString());
	}

	// Token: 0x04000796 RID: 1942
	public Transform prefab;

	// Token: 0x04000797 RID: 1943
	internal int activeIndex;

	// Token: 0x04000798 RID: 1944
	internal List<BaseEffect> list;

	// Token: 0x04000799 RID: 1945
	internal float timer;

	// Token: 0x0400079A RID: 1946
	public float timer_interval = 1f;

	// Token: 0x0400079B RID: 1947
	internal int objectLimit;

	// Token: 0x0400079C RID: 1948
	public bool useInterval = true;

	// Token: 0x0400079D RID: 1949
	private BaseEffect switchObject;
}
