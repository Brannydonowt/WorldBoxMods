using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public class BuildingSpritesContainer
{
	// Token: 0x0600042A RID: 1066 RVA: 0x0003C0E3 File Offset: 0x0003A2E3
	public BuildingAnimationData getAnim(string pID)
	{
		if (!this.dict.ContainsKey(pID))
		{
			Debug.Log("NO BUILDING ANIM " + pID);
		}
		return this.dict[pID];
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x0003C110 File Offset: 0x0003A310
	public void load(string pPath)
	{
		foreach (Sprite sprite in Resources.LoadAll<Sprite>(pPath))
		{
			this.dict_sprites.Add(sprite.name, sprite);
			this.addBuildingData(sprite.name, sprite);
		}
		foreach (BuildingAnimationData buildingAnimationData in this.dict.Values)
		{
			buildingAnimationData.idle.createSpriteArray();
		}
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x0003C1A4 File Offset: 0x0003A3A4
	private void addBuildingData(string pId, Sprite pSprite)
	{
		string[] array = pId.Split(new char[]
		{
			'#'
		});
		string text = array[0];
		bool animated = array[1].Contains("@");
		BuildingAnimationData buildingAnimationData;
		if (this.dict.ContainsKey(text))
		{
			buildingAnimationData = this.dict[text];
		}
		else
		{
			buildingAnimationData = new BuildingAnimationData();
			buildingAnimationData.animated = animated;
			buildingAnimationData.idle = new BuildingAnimation
			{
				timeBetweenFrames = 0.1f
			};
			this.dict.Add(text, buildingAnimationData);
		}
		buildingAnimationData.idle.list.Add(pSprite);
	}

	// Token: 0x04000651 RID: 1617
	internal Dictionary<string, BuildingAnimationData> dict = new Dictionary<string, BuildingAnimationData>();

	// Token: 0x04000652 RID: 1618
	internal Dictionary<string, Sprite> dict_sprites = new Dictionary<string, Sprite>();
}
