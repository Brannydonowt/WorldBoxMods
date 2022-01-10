using System;
using System.Collections.Generic;

// Token: 0x0200015B RID: 347
public abstract class JobAsset<T> : Asset
{
	// Token: 0x060007E7 RID: 2023 RVA: 0x0005692F File Offset: 0x00054B2F
	public void addCondition(T pCondition)
	{
		this.tasks[this.tasks.Count - 1].addCondition(pCondition);
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x00056950 File Offset: 0x00054B50
	public void addTask(string pTask)
	{
		TaskContainer<T> taskContainer = new TaskContainer<T>();
		taskContainer.id = pTask;
		this.tasks.Add(taskContainer);
	}

	// Token: 0x04000A4F RID: 2639
	public List<TaskContainer<T>> tasks = new List<TaskContainer<T>>();
}
