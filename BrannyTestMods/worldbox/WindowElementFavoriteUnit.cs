using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000284 RID: 644
public class WindowElementFavoriteUnit : MonoBehaviour
{
	// Token: 0x06000E2E RID: 3630 RVA: 0x00084FA0 File Offset: 0x000831A0
	public void show(Actor pActor)
	{
		this.actor = pActor;
		this.unitName.text = pActor.data.firstName;
		this.avatarLoader.load(pActor);
		this.text_health.text = pActor.data.health.ToString() + "/" + pActor.curStats.health.ToString();
		this.text_damage.text = (pActor.curStats.damage.ToString() ?? "");
		this.text_level.text = (pActor.data.level.ToString() ?? "");
		this.text_kills.text = (pActor.data.kills.ToString() ?? "");
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x00085077 File Offset: 0x00083277
	public void clickLocate()
	{
		WorldLog.locationFollow(this.actor);
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x00085084 File Offset: 0x00083284
	public void clickInspect()
	{
		Config.selectedUnit = this.actor;
		ScrollWindow.moveAllToLeftAndRemove(true);
		ScrollWindow.showWindow("inspect_unit");
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x000850A1 File Offset: 0x000832A1
	private void OnEnable()
	{
	}

	// Token: 0x04001106 RID: 4358
	private Actor actor;

	// Token: 0x04001107 RID: 4359
	public Text unitName;

	// Token: 0x04001108 RID: 4360
	public UnitAvatarLoader avatarLoader;

	// Token: 0x04001109 RID: 4361
	public Text text_health;

	// Token: 0x0400110A RID: 4362
	public Text text_damage;

	// Token: 0x0400110B RID: 4363
	public Text text_level;

	// Token: 0x0400110C RID: 4364
	public Text text_kills;
}
