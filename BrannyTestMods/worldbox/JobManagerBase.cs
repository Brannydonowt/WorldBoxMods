using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Token: 0x020000E2 RID: 226
public class JobManagerBase<TBatch, T> where TBatch : Batch<T>, new()
{
	// Token: 0x060004C5 RID: 1221 RVA: 0x0003FAD5 File Offset: 0x0003DCD5
	public void updateBase(float pElapsed)
	{
		Toolbox.bench("manager base");
		this.updateBaseJobsPre(pElapsed);
		this.updateBaseJobsParallel(pElapsed);
		this.updateBaseJobsPost(pElapsed);
		Toolbox.benchEnd("manager base");
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x0003FB04 File Offset: 0x0003DD04
	internal void checkCleanup()
	{
		if (!this.batches_all_dirty)
		{
			return;
		}
		this.batches_all_dirty = false;
		this.temp_list_keeper.Clear();
		for (int i = 0; i < this.batches_all.Count; i++)
		{
			TBatch tbatch = this.batches_all[i];
			if (tbatch.main.Count > 0)
			{
				this.temp_list_keeper.Add(tbatch);
			}
			else
			{
				tbatch.clear();
			}
		}
		this.batches_all.Clear();
		this.batches_all.AddRange(this.temp_list_keeper);
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0003FB98 File Offset: 0x0003DD98
	internal void updateBaseJobsPre(float pElapsed)
	{
		for (int i = 0; i < this.batches_all.Count; i++)
		{
			TBatch tbatch = this.batches_all[i];
			if (tbatch.main.Count != 0)
			{
				tbatch.updateJobsPre(pElapsed);
			}
		}
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0003FBE8 File Offset: 0x0003DDE8
	internal void updateBaseJobsPost(float pElapsed)
	{
		for (int i = 0; i < this.batches_all.Count; i++)
		{
			TBatch tbatch = this.batches_all[i];
			if (tbatch.main.Count != 0)
			{
				tbatch.updateJobsPost(pElapsed);
			}
		}
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x0003FC38 File Offset: 0x0003DE38
	internal void clearParallelResults()
	{
		for (int i = 0; i < this.batches_all.Count; i++)
		{
			TBatch tbatch = this.batches_all[i];
			if (tbatch.main.Count != 0 && tbatch.clearParallelResults != null)
			{
				tbatch.clearParallelResults();
			}
		}
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x0003FC98 File Offset: 0x0003DE98
	internal void applyParallelResults()
	{
		for (int i = 0; i < this.batches_all.Count; i++)
		{
			TBatch tbatch = this.batches_all[i];
			if (tbatch.applyParallelResults != null)
			{
				tbatch.applyParallelResults();
			}
		}
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x0003FCE8 File Offset: 0x0003DEE8
	internal void updateBaseJobsParallel(float pElapsed)
	{
		this.clearParallelResults();
		Toolbox.bench("test_parallel");
		if (Config.jobs_updater_parallel)
		{
			Parallel.ForEach<TBatch>(this.batches_all, delegate(TBatch tBatch)
			{
				tBatch.updateJobsParallel(pElapsed);
			});
		}
		else
		{
			for (int i = 0; i < this.batches_all.Count; i++)
			{
				this.batches_all[i].updateJobsParallel(pElapsed);
			}
		}
		Toolbox.benchEnd("test_parallel");
		this.applyParallelResults();
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x0003FD77 File Offset: 0x0003DF77
	internal virtual void removeObject(T pObject)
	{
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x0003FD7C File Offset: 0x0003DF7C
	protected TBatch newBatch()
	{
		TBatch tbatch = Activator.CreateInstance<TBatch>();
		tbatch.world = MapBox.instance;
		this.batches_all.Add(tbatch);
		return tbatch;
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x0003FDAC File Offset: 0x0003DFAC
	internal virtual void addNewObject(T pObject)
	{
		TBatch batch = this.getBatch();
		batch.add(pObject);
		batch.main.checkAddRemove();
		if (batch.main.Count >= JobManagerBase<TBatch, T>.MAX_ELEMENTS)
		{
			batch.free_slots = false;
			this.batches_free.RemoveAt(this.batches_free.Count - 1);
		}
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x0003FE18 File Offset: 0x0003E018
	internal TBatch getBatch()
	{
		if (this.batches_free.Count == 0)
		{
			TBatch pBatch = this.newBatch();
			this.makeFree(pBatch);
		}
		return this.batches_free[this.batches_free.Count - 1];
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x0003FE58 File Offset: 0x0003E058
	protected void checkFree(TBatch pBatch)
	{
		pBatch.main.checkAddRemove();
		if (pBatch.main.Count < JobManagerBase<TBatch, T>.MAX_ELEMENTS)
		{
			this.makeFree(pBatch);
		}
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x0003FE88 File Offset: 0x0003E088
	protected virtual void makeFree(TBatch pBatch)
	{
		if (pBatch.free_slots)
		{
			return;
		}
		pBatch.free_slots = true;
		this.batches_free.Add(pBatch);
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x0003FEB0 File Offset: 0x0003E0B0
	internal void clear()
	{
		this.batches_free.Clear();
		for (int i = 0; i < this.batches_all.Count; i++)
		{
			TBatch tbatch = this.batches_all[i];
			tbatch.clear();
			this.makeFree(tbatch);
		}
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x0003FF00 File Offset: 0x0003E100
	public void debug(DebugTool pTool)
	{
		int num = 0;
		for (int i = 0; i < this.batches_all.Count; i++)
		{
			TBatch tbatch = this.batches_all[i];
			num += tbatch.main.Count;
		}
		pTool.setText("batches all", this.batches_all.Count);
		pTool.setText("objects", num);
		pTool.setSeparator();
		pTool.setText("parallel_test", Config.jobs_updater_parallel);
		pTool.setText("parallel_test_test", Toolbox.getBenchResult("test_parallel", true));
		pTool.setText("manager base", Toolbox.getBenchResult("manager base", true));
	}

	// Token: 0x040006EA RID: 1770
	internal List<TBatch> batches_all = new List<TBatch>();

	// Token: 0x040006EB RID: 1771
	public static int MAX_ELEMENTS = 256;

	// Token: 0x040006EC RID: 1772
	protected List<TBatch> batches_free = new List<TBatch>();

	// Token: 0x040006ED RID: 1773
	private List<TBatch> temp_list_keeper = new List<TBatch>();

	// Token: 0x040006EE RID: 1774
	internal bool batches_all_dirty;
}
