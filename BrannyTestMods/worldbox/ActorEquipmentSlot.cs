using System;

// Token: 0x02000061 RID: 97
public class ActorEquipmentSlot
{
	// Token: 0x06000210 RID: 528 RVA: 0x00027002 File Offset: 0x00025202
	public ActorEquipmentSlot(EquipmentType pType = EquipmentType.Armor)
	{
		this.type = pType;
		this.emptySlot();
	}

	// Token: 0x06000211 RID: 529 RVA: 0x00027017 File Offset: 0x00025217
	public void copyTo(ActorEquipmentSlot pSlot)
	{
		this.data = pSlot.data;
	}

	// Token: 0x06000212 RID: 530 RVA: 0x00027025 File Offset: 0x00025225
	public bool isEmpty()
	{
		return this.data == null;
	}

	// Token: 0x06000213 RID: 531 RVA: 0x00027030 File Offset: 0x00025230
	public void emptySlot()
	{
		this.data = null;
	}

	// Token: 0x06000214 RID: 532 RVA: 0x00027039 File Offset: 0x00025239
	internal void setItem(ItemData pData)
	{
		this.data = pData;
	}

	// Token: 0x040002D1 RID: 721
	public ItemData data;

	// Token: 0x040002D2 RID: 722
	public EquipmentType type;
}
