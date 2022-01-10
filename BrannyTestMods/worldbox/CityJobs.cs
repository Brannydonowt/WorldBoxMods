using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F6 RID: 246
[Serializable]
public class CityJobs
{
	// Token: 0x06000576 RID: 1398 RVA: 0x00044BDB File Offset: 0x00042DDB
	public void clearTasks()
	{
		this.total = 0;
		this.jobs.Clear();
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x00044BF0 File Offset: 0x00042DF0
	public void addJob(string pJob, int pValue, int pMax = 0)
	{
		if (!this.jobs.ContainsKey(pJob))
		{
			this.jobs.Add(pJob, 0);
		}
		if (pMax != 0)
		{
			this.jobs[pJob] = Mathf.Clamp(pValue, 1, pMax);
			return;
		}
		this.jobs[pJob] = pValue;
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00044C3D File Offset: 0x00042E3D
	public bool continueJob(string pJob)
	{
		return this.jobs.ContainsKey(pJob);
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x00044C50 File Offset: 0x00042E50
	public int countOccupied(string pJob)
	{
		if (this.occupied.ContainsKey(pJob))
		{
			return this.occupied[pJob];
		}
		return 0;
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x00044C70 File Offset: 0x00042E70
	public bool haveJob(string pJob)
	{
		return this.jobs.ContainsKey(pJob) && this.jobs[pJob] != 0 && (!this.occupied.ContainsKey(pJob) || this.occupied[pJob] < this.jobs[pJob]);
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x00044CC8 File Offset: 0x00042EC8
	public void takeJob(string pJob)
	{
		if (!this.occupied.ContainsKey(pJob))
		{
			this.occupied.Add(pJob, 1);
			return;
		}
		Dictionary<string, int> dictionary = this.occupied;
		dictionary[pJob]++;
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x00044D0C File Offset: 0x00042F0C
	public void freeJob(string pJob)
	{
		if (!this.occupied.ContainsKey(pJob))
		{
			this.occupied.Add(pJob, 0);
			return;
		}
		Dictionary<string, int> dictionary = this.occupied;
		dictionary[pJob]--;
		if (this.occupied[pJob] < 0)
		{
			this.occupied[pJob] = 0;
		}
	}

	// Token: 0x04000760 RID: 1888
	public int total;

	// Token: 0x04000761 RID: 1889
	public Dictionary<string, int> jobs = new Dictionary<string, int>();

	// Token: 0x04000762 RID: 1890
	public Dictionary<string, int> occupied = new Dictionary<string, int>();
}
