using System;

// Token: 0x02000190 RID: 400
public class WorldBehaviour
{
	// Token: 0x06000937 RID: 2359 RVA: 0x00061798 File Offset: 0x0005F998
	internal void clear()
	{
		this.timer = this.interval;
		if (this._clearAction != null)
		{
			this._clearAction();
		}
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x000617B9 File Offset: 0x0005F9B9
	public WorldBehaviour(float pInterval, float pRandomInterval, string pID, WorldBehaviourAction pUpdateAction, WorldBehaviourAction pClearAction)
	{
		this.id = pID;
		this.interval = pInterval;
		this.interval_random = pRandomInterval;
		this.timer = pInterval;
		this._updateAction = pUpdateAction;
		this._clearAction = pClearAction;
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x000617F4 File Offset: 0x0005F9F4
	public void update(float pElapsed)
	{
		if (!DebugConfig.isOn(DebugOption.SystemWorldBehaviours))
		{
			return;
		}
		if (MapBox.instance.qualityChanger.lowRes && !this.enabled_on_minimap)
		{
			return;
		}
		if (MapBox.instance.isPaused())
		{
			return;
		}
		if (this.timer > 0f)
		{
			this.timer -= pElapsed;
			return;
		}
		this.timer = this.interval + Toolbox.randomFloat(0f, this.interval_random);
		this._updateAction();
	}

	// Token: 0x04000BC9 RID: 3017
	private float timer;

	// Token: 0x04000BCA RID: 3018
	private float interval;

	// Token: 0x04000BCB RID: 3019
	private float interval_random;

	// Token: 0x04000BCC RID: 3020
	private WorldBehaviourAction _updateAction;

	// Token: 0x04000BCD RID: 3021
	private WorldBehaviourAction _clearAction;

	// Token: 0x04000BCE RID: 3022
	private string id;

	// Token: 0x04000BCF RID: 3023
	public bool enabled_on_minimap = true;
}
