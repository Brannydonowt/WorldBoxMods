using System;

// Token: 0x0200011F RID: 287
public class UnitSelectionEffect : BaseAnimatedObject
{
	// Token: 0x06000671 RID: 1649 RVA: 0x0004AF9B File Offset: 0x0004919B
	internal override void create()
	{
		base.create();
		base.transform.parent = this.world.transform;
		base.transform.name = "unit_selector_effect";
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x0004AFCC File Offset: 0x000491CC
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		bool flag = true;
		Actor actor = null;
		if (this.world.selectedButtons.isPowerSelected() && !this.world.selectedButtons.selectedButton.godPower.allow_unit_selection)
		{
			flag = false;
		}
		if (this.world.qualityChanger.lowRes)
		{
			flag = false;
		}
		if (flag)
		{
			actor = MapBox.instance.getActorNearCursor();
			flag = !(actor == null);
		}
		if (flag)
		{
			base.gameObject.SetActive(true);
			base.transform.position = actor.currentPosition;
			base.transform.localScale = actor.transform.localScale;
			return;
		}
		base.gameObject.SetActive(false);
		base.transform.position = Globals.POINT_IN_VOID;
	}
}
