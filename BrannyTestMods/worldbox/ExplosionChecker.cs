using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000175 RID: 373
public class ExplosionChecker
{
	// Token: 0x06000863 RID: 2147 RVA: 0x0005B140 File Offset: 0x00059340
	public ExplosionChecker()
	{
		ExplosionChecker.instance = this;
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x0005B168 File Offset: 0x00059368
	public bool checkNearby(WorldTile pTile, int pRange)
	{
		int num = pRange * 10000000 + pTile.x * 1000 + pTile.y;
		if (this.data.ContainsKey(num) || this.isNearbyOthers(pTile, (float)(pRange / 3)))
		{
			return true;
		}
		this.add(num, pTile, pRange);
		this.updateNearbyTimers(pTile, (float)pRange);
		return false;
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x0005B1C4 File Offset: 0x000593C4
	private void updateNearbyTimers(WorldTile pTile, float pRange)
	{
		float num = 1f;
		num += (float)(this.data.Count / 10);
		float num2 = pRange + (float)(this.data.Count / 5);
		num = Mathf.Clamp(num, 1f, 5f);
		num2 = Mathf.Clamp(num2, pRange, pRange * 5f);
		foreach (int num3 in this.data.Keys)
		{
			ExplosionMemoryData explosionMemoryData = this.data[num3];
			if (Toolbox.Dist((float)pTile.x, (float)pTile.y, (float)explosionMemoryData.x, (float)explosionMemoryData.y) < num2)
			{
				explosionMemoryData.timer = num;
			}
		}
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0005B29C File Offset: 0x0005949C
	private bool isNearbyOthers(WorldTile pTile, float pRange)
	{
		foreach (ExplosionMemoryData explosionMemoryData in this.data.Values)
		{
			if (Toolbox.Dist((float)pTile.x, (float)pTile.y, (float)explosionMemoryData.x, (float)explosionMemoryData.y) < pRange)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x0005B318 File Offset: 0x00059518
	private void add(int pID, WorldTile pTile, int pRange)
	{
		ExplosionMemoryData explosionMemoryData = new ExplosionMemoryData();
		explosionMemoryData.range = pRange;
		explosionMemoryData.x = pTile.x;
		explosionMemoryData.y = pTile.y;
		float num = 1f;
		num += (float)(this.data.Count / 10);
		num = Mathf.Clamp(num, 1f, 5f);
		explosionMemoryData.timer = num;
		this.data.Add(pID, explosionMemoryData);
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x0005B388 File Offset: 0x00059588
	public void update(float pElapsed)
	{
		foreach (int num in this.data.Keys)
		{
			ExplosionMemoryData explosionMemoryData = this.data[num];
			explosionMemoryData.timer -= pElapsed;
			if (explosionMemoryData.timer <= 0f)
			{
				this._to_remove.Add(num);
			}
		}
		if (this._to_remove.Count > 0)
		{
			for (int i = 0; i < this._to_remove.Count; i++)
			{
				this.data.Remove(this._to_remove[i]);
			}
			this._to_remove.Clear();
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0005B454 File Offset: 0x00059654
	public void clear()
	{
		this.data.Clear();
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x0005B461 File Offset: 0x00059661
	public static void debug(DebugTool pTool)
	{
		pTool.setText("explosion_checker", ExplosionChecker.instance.data.Count);
	}

	// Token: 0x04000AE5 RID: 2789
	private const float TIMER = 1f;

	// Token: 0x04000AE6 RID: 2790
	public static ExplosionChecker instance;

	// Token: 0x04000AE7 RID: 2791
	private Dictionary<int, ExplosionMemoryData> data = new Dictionary<int, ExplosionMemoryData>();

	// Token: 0x04000AE8 RID: 2792
	private List<int> _to_remove = new List<int>(16);
}
