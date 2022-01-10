using System;
using System.Collections.Generic;

// Token: 0x020000DD RID: 221
public class Batch<T>
{
	// Token: 0x060004A0 RID: 1184 RVA: 0x0003F0DB File Offset: 0x0003D2DB
	public Batch()
	{
		this.createJobs();
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x0003F118 File Offset: 0x0003D318
	public void updateJobsPre(float pElapsed)
	{
		this._elapsed = pElapsed;
		for (int i = 0; i < this.jobs_pre.Count; i++)
		{
			Job<T> job = this.jobs_pre[i];
			this._cur_container = job.container;
			job.job_updater();
		}
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x0003F168 File Offset: 0x0003D368
	public void updateJobsParallel(float pElapsed)
	{
		for (int i = 0; i < this.jobs_parallel.Count; i++)
		{
			Job<T> job = this.jobs_parallel[i];
			this._cur_container = job.container;
			job.job_updater();
		}
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x0003F1B0 File Offset: 0x0003D3B0
	public void updateJobsPost(float pElapsed)
	{
		this._elapsed = pElapsed;
		for (int i = 0; i < this.jobs_post.Count; i++)
		{
			Job<T> job = this.jobs_post[i];
			this._cur_container = job.container;
			job.job_updater();
		}
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x0003F200 File Offset: 0x0003D400
	internal virtual void prepare()
	{
		for (int i = 0; i < this.containers.Count; i++)
		{
			this.containers[i].doChecks();
		}
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x0003F234 File Offset: 0x0003D434
	protected void createJob(out ObjectContainer<T> pContainer, JobUpdater pJobUpdater, JobType pType)
	{
		pContainer = new ObjectContainer<T>();
		this.containers.Add(pContainer);
		if (pJobUpdater != null)
		{
			this.addJob(pContainer, pJobUpdater, pType);
		}
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x0003F258 File Offset: 0x0003D458
	protected void addJob(ObjectContainer<T> pContainer, JobUpdater pJobUpdater, JobType pType)
	{
		switch (pType)
		{
		case JobType.Pre:
			this.putJob(pContainer, pJobUpdater, this.jobs_pre);
			return;
		case JobType.Post:
			this.putJob(pContainer, pJobUpdater, this.jobs_post);
			return;
		case JobType.Parallel:
			this.putJob(pContainer, pJobUpdater, this.jobs_parallel);
			return;
		default:
			return;
		}
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0003F2A4 File Offset: 0x0003D4A4
	private void putJob(ObjectContainer<T> pContainer, JobUpdater pJobUpdater, List<Job<T>> pListJobs)
	{
		pListJobs.Add(new Job<T>
		{
			container = pContainer,
			job_updater = pJobUpdater
		});
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0003F2BF File Offset: 0x0003D4BF
	protected void addUpdaterOnly(ObjectContainer<T> pContainer, JobUpdater pJobUpdater, JobType pType)
	{
		this.containers.Add(pContainer);
		this.addJob(pContainer, pJobUpdater, pType);
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0003F2D8 File Offset: 0x0003D4D8
	internal void clear()
	{
		for (int i = 0; i < this.containers.Count; i++)
		{
			this.containers[i].Clear();
		}
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0003F30C File Offset: 0x0003D50C
	internal virtual void remove(T pObject)
	{
		for (int i = 0; i < this.containers.Count; i++)
		{
			this.containers[i].Remove(pObject);
		}
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0003F341 File Offset: 0x0003D541
	internal virtual void add(T pObject)
	{
		this.main.Add(pObject);
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0003F34F File Offset: 0x0003D54F
	protected bool check(ObjectContainer<T> pContainer)
	{
		this._list = null;
		if (pContainer.Count > 0)
		{
			pContainer.checkAddRemove();
			this._list = pContainer.getSimpleList();
			return true;
		}
		return false;
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x0003F376 File Offset: 0x0003D576
	protected virtual void createJobs()
	{
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x0003F378 File Offset: 0x0003D578
	public void debug(DebugTool pTool)
	{
		pTool.setText("total", this.main.Count);
	}

	// Token: 0x040006CA RID: 1738
	internal ObjectContainer<T> main;

	// Token: 0x040006CB RID: 1739
	internal bool free_slots;

	// Token: 0x040006CC RID: 1740
	private List<ObjectContainer<T>> containers = new List<ObjectContainer<T>>();

	// Token: 0x040006CD RID: 1741
	private List<Job<T>> jobs_pre = new List<Job<T>>();

	// Token: 0x040006CE RID: 1742
	private List<Job<T>> jobs_post = new List<Job<T>>();

	// Token: 0x040006CF RID: 1743
	private List<Job<T>> jobs_parallel = new List<Job<T>>();

	// Token: 0x040006D0 RID: 1744
	internal MapBox world;

	// Token: 0x040006D1 RID: 1745
	protected List<T> _list;

	// Token: 0x040006D2 RID: 1746
	protected float _elapsed;

	// Token: 0x040006D3 RID: 1747
	protected ObjectContainer<T> _cur_container;

	// Token: 0x040006D4 RID: 1748
	internal JobUpdater clearParallelResults;

	// Token: 0x040006D5 RID: 1749
	internal JobUpdater applyParallelResults;
}
