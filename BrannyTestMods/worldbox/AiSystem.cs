using System;
using System.Collections.Generic;
using ai.behaviours;

// Token: 0x02000155 RID: 341
public class AiSystem<T, TJob, TTask, TAction, TCondition> where TJob : JobAsset<TCondition> where TTask : BehaviourTaskBase<TAction> where TAction : BehaviourActionBase<T> where TCondition : BehaviourBaseCondition<T>
{
	// Token: 0x060007CD RID: 1997 RVA: 0x000563D1 File Offset: 0x000545D1
	public AiSystem(T pObject)
	{
		this.ai_object = pObject;
		this.nextJobDelegate = new GetNextJobID(AiSystem<T, TJob, TTask, TAction, TCondition>.nextJobDefault);
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x000563F4 File Offset: 0x000545F4
	private void updateNewBehJob()
	{
		if (this.job == null)
		{
			string text = this.nextJobDelegate();
			this.setJob(text);
		}
		if (this.job == null)
		{
			return;
		}
		if (this.task_index > this.job.tasks.Count - 1)
		{
			this.task_index = 0;
		}
		List<TaskContainer<TCondition>> tasks = this.job.tasks;
		int num = this.task_index;
		this.task_index = num + 1;
		TaskContainer<TCondition> taskContainer = tasks[num];
		bool flag = true;
		if (taskContainer.conditions != null && taskContainer.conditions.Count > 0)
		{
			for (int i = 0; i < taskContainer.conditions.Count; i++)
			{
				flag = taskContainer.conditions[i].check(this.ai_object);
				if (!flag)
				{
					break;
				}
			}
		}
		if (flag)
		{
			this.setTask(taskContainer.id, true, false);
			return;
		}
		this.setTask("nothing", true, false);
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x000564F0 File Offset: 0x000546F0
	public void setTask(string pTaskId, bool pClean = true, bool pCleanJob = false)
	{
		if (pClean)
		{
			this.clearBeh();
		}
		if (pCleanJob)
		{
			this.job = default(TJob);
			this.task_index = 0;
		}
		this.task = this.task_library.get(pTaskId);
		this.action_index = 0;
		JobAction jobAction = this.taskSwitchedAction;
		if (jobAction == null)
		{
			return;
		}
		jobAction();
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x00056545 File Offset: 0x00054745
	public void restartJob()
	{
		this.action_index = 0;
		this.task_index = 0;
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x00056555 File Offset: 0x00054755
	internal void clearBeh()
	{
		if (this.clearAction != null)
		{
			this.clearAction();
		}
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x0005656C File Offset: 0x0005476C
	public void setJob(string pJobID)
	{
		JobAction jobAction = this.jobSetStartAction;
		if (jobAction != null)
		{
			jobAction();
		}
		if (pJobID == null)
		{
			this.job = default(TJob);
		}
		else
		{
			this.job = this.jobs_library.get(pJobID);
		}
		this.task_index = 0;
		if (this.jobSetEndAction != null)
		{
			this.jobSetEndAction();
		}
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x000565C8 File Offset: 0x000547C8
	internal void update()
	{
		if (this.task == null)
		{
			this.updateNewBehJob();
		}
		if (this.task == null)
		{
			return;
		}
		if (this.action_index > this.task.list.Count - 1)
		{
			this.setTaskBehFinished();
			return;
		}
		this.action = this.task.get(this.action_index);
		switch (this.action.startExecute(this.ai_object))
		{
		case BehResult.Continue:
			this.action_index++;
			return;
		case BehResult.Stop:
			this.setTaskBehFinished();
			return;
		case BehResult.RepeatStep:
		case BehResult.Skip:
			break;
		case BehResult.RestartTask:
			this.action_index = 0;
			break;
		default:
			return;
		}
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x00056687 File Offset: 0x00054887
	public void setTaskBehFinished()
	{
		this.task = default(TTask);
		this.action_index = -1;
		JobAction jobAction = this.taskSwitchedAction;
		if (jobAction == null)
		{
			return;
		}
		jobAction();
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x000566AC File Offset: 0x000548AC
	private void debugLogAction()
	{
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x000566AE File Offset: 0x000548AE
	private void debugLogActionResult(BehResult pResult)
	{
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x000566B0 File Offset: 0x000548B0
	public void debug(DebugTool pTool)
	{
		TAction taction = this.action;
		string text = (taction != null) ? taction.GetType().ToString() : null;
		if (text != null)
		{
			text = text.Replace("ai.behaviours.", "");
		}
		string pT = "job:";
		TJob tjob = this.job;
		pTool.setText(pT, (tjob != null) ? tjob.id : null);
		string pT2 = "task next:";
		int num = this.task_index + 1;
		TJob tjob2 = this.job;
		int? num2 = (tjob2 != null) ? new int?(tjob2.tasks.Count) : null;
		object pT3;
		if (!(num < num2.GetValueOrDefault() & num2 != null))
		{
			pT3 = "";
		}
		else
		{
			TJob tjob3 = this.job;
			pT3 = ((tjob3 != null) ? tjob3.tasks[this.task_index + 1].id : null);
		}
		pTool.setText(pT2, pT3);
		pTool.setSeparator();
		string pT4 = ": task:";
		TTask ttask = this.task;
		pTool.setText(pT4, (ttask != null) ? ttask.id : null);
		string pT5 = ": task index:";
		string str = this.task_index.ToString();
		string str2 = "/";
		TJob tjob4 = this.job;
		pTool.setText(pT5, str + str2 + ((tjob4 != null) ? new int?(tjob4.tasks.Count) : null).ToString());
		pTool.setSeparator();
		pTool.setText(":: action:", text);
		string pT6 = ":: action index:";
		string str3 = this.action_index.ToString();
		string str4 = "/";
		TTask ttask2 = this.task;
		pTool.setText(pT6, str3 + str4 + ((ttask2 != null) ? new int?(ttask2.list.Count) : null).ToString());
		pTool.setSeparator();
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x0005687E File Offset: 0x00054A7E
	public static string nextJobDefault()
	{
		return null;
	}

	// Token: 0x04000A3C RID: 2620
	public AssetLibrary<TJob> jobs_library;

	// Token: 0x04000A3D RID: 2621
	public AssetLibrary<TTask> task_library;

	// Token: 0x04000A3E RID: 2622
	internal int action_index;

	// Token: 0x04000A3F RID: 2623
	internal int task_index;

	// Token: 0x04000A40 RID: 2624
	public TJob job;

	// Token: 0x04000A41 RID: 2625
	internal TTask task;

	// Token: 0x04000A42 RID: 2626
	internal TAction action;

	// Token: 0x04000A43 RID: 2627
	private T ai_object;

	// Token: 0x04000A44 RID: 2628
	public GetNextJobID nextJobDelegate;

	// Token: 0x04000A45 RID: 2629
	public JobAction jobSetStartAction;

	// Token: 0x04000A46 RID: 2630
	public JobAction jobSetEndAction;

	// Token: 0x04000A47 RID: 2631
	public JobAction clearAction;

	// Token: 0x04000A48 RID: 2632
	public JobAction taskSwitchedAction;
}
