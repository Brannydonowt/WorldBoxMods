using System;
using System.Collections.Generic;

// Token: 0x02000062 RID: 98
public class ActorEquipment
{
	// Token: 0x06000215 RID: 533 RVA: 0x00027044 File Offset: 0x00025244
	public List<ItemData> getDataForSave()
	{
		List<ItemData> list = new List<ItemData>();
		ActorEquipment._list = ActorEquipment.getList(this, false);
		foreach (ActorEquipmentSlot actorEquipmentSlot in ActorEquipment._list)
		{
			list.Add(actorEquipmentSlot.data);
		}
		return list;
	}

	// Token: 0x06000216 RID: 534 RVA: 0x000270B0 File Offset: 0x000252B0
	public void load(List<ItemData> pList)
	{
		if (pList == null || pList.Count == 0)
		{
			return;
		}
		foreach (ItemData itemData in pList)
		{
			this.getSlot(itemData.type).setItem(itemData);
		}
	}

	// Token: 0x06000217 RID: 535 RVA: 0x00027118 File Offset: 0x00025318
	public static List<ActorEquipmentSlot> getList(ActorEquipment pEquipment, bool pEmpty = false)
	{
		ActorEquipment._list.Clear();
		ActorEquipment.addToList(pEquipment.helmet, pEmpty);
		ActorEquipment.addToList(pEquipment.armor, pEmpty);
		ActorEquipment.addToList(pEquipment.weapon, pEmpty);
		ActorEquipment.addToList(pEquipment.boots, pEmpty);
		ActorEquipment.addToList(pEquipment.ring, pEmpty);
		ActorEquipment.addToList(pEquipment.amulet, pEmpty);
		return ActorEquipment._list;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0002717C File Offset: 0x0002537C
	public ActorEquipmentSlot getSlot(EquipmentType pType)
	{
		switch (pType)
		{
		case EquipmentType.Weapon:
			return this.weapon;
		case EquipmentType.Helmet:
			return this.helmet;
		case EquipmentType.Armor:
			return this.armor;
		case EquipmentType.Boots:
			return this.boots;
		case EquipmentType.Ring:
			return this.ring;
		case EquipmentType.Amulet:
			return this.amulet;
		default:
			return null;
		}
	}

	// Token: 0x06000219 RID: 537 RVA: 0x000271D4 File Offset: 0x000253D4
	public static void addToList(ActorEquipmentSlot pSlot, bool pEmpty)
	{
		if (pEmpty)
		{
			if (pSlot.data != null)
			{
				return;
			}
		}
		else if (pSlot.data == null)
		{
			return;
		}
		ActorEquipment._list.Add(pSlot);
	}

	// Token: 0x040002D3 RID: 723
	public const string NONE = "none";

	// Token: 0x040002D4 RID: 724
	public ActorEquipmentSlot helmet = new ActorEquipmentSlot(EquipmentType.Helmet);

	// Token: 0x040002D5 RID: 725
	public ActorEquipmentSlot armor = new ActorEquipmentSlot(EquipmentType.Armor);

	// Token: 0x040002D6 RID: 726
	public ActorEquipmentSlot weapon = new ActorEquipmentSlot(EquipmentType.Weapon);

	// Token: 0x040002D7 RID: 727
	public ActorEquipmentSlot boots = new ActorEquipmentSlot(EquipmentType.Boots);

	// Token: 0x040002D8 RID: 728
	public ActorEquipmentSlot ring = new ActorEquipmentSlot(EquipmentType.Ring);

	// Token: 0x040002D9 RID: 729
	public ActorEquipmentSlot amulet = new ActorEquipmentSlot(EquipmentType.Amulet);

	// Token: 0x040002DA RID: 730
	private static List<ActorEquipmentSlot> _list = new List<ActorEquipmentSlot>();
}
