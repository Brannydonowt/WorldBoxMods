using System;
using System.Collections.Generic;
using life;
using UnityEngine;

// Token: 0x02000125 RID: 293
public class ActorAnimationLoader
{
	// Token: 0x060006D6 RID: 1750 RVA: 0x0004E62C File Offset: 0x0004C82C
	public static void init()
	{
		new Dictionary<string, Sprite>();
		Sprite[] array = Resources.LoadAll<Sprite>("actors/races/items");
		ActorAnimationLoader.banner = Resources.Load<Sprite>("actors/races/civ_army_banner");
		foreach (Sprite sprite in array)
		{
			ActorAnimationLoader.dictItems.Add(sprite.name, sprite);
		}
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x0004E67C File Offset: 0x0004C87C
	public static Sprite getItem(string pID)
	{
		if (string.IsNullOrEmpty(pID))
		{
			return null;
		}
		Sprite sprite = null;
		ActorAnimationLoader.dictItems.TryGetValue(pID, ref sprite);
		if (sprite == null)
		{
			Debug.LogError("Texture is Missing: " + pID);
		}
		return sprite;
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x0004E6C0 File Offset: 0x0004C8C0
	public static Sprite getHeadSpecial(string pPath, string pName)
	{
		string text = pPath + "_" + pName;
		Sprite result = null;
		ActorAnimationLoader.dictCivHeads.TryGetValue(text, ref result);
		if (!ActorAnimationLoader.dictCivHeads.ContainsKey(text))
		{
			foreach (Sprite sprite in Resources.LoadAll<Sprite>(pPath))
			{
				string text2 = pPath + "_" + sprite.name;
				ActorAnimationLoader.dictCivHeads.Add(text2, sprite);
			}
			result = ActorAnimationLoader.dictCivHeads[text];
		}
		return result;
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x0004E744 File Offset: 0x0004C944
	public static Sprite getHead(string pPath, int pHeadIndex)
	{
		string text = pPath + "_head_" + pHeadIndex.ToString();
		Sprite sprite = null;
		ActorAnimationLoader.dictCivHeads.TryGetValue(text, ref sprite);
		if (sprite == null)
		{
			foreach (Sprite sprite2 in Resources.LoadAll<Sprite>(pPath))
			{
				ActorAnimationLoader.dictCivHeads.Add(pPath + "_" + sprite2.name, sprite2);
			}
			sprite = ActorAnimationLoader.dictCivHeads[text];
		}
		return sprite;
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x0004E7C4 File Offset: 0x0004C9C4
	public static Sprite getHeadFromFullPath(string pPath)
	{
		Sprite sprite = null;
		ActorAnimationLoader.dictCivHeads.TryGetValue(pPath, ref sprite);
		if (sprite == null)
		{
			foreach (Sprite sprite2 in Resources.LoadAll<Sprite>(pPath))
			{
				ActorAnimationLoader.dictCivHeads.Add(pPath, sprite2);
			}
			sprite = ActorAnimationLoader.dictCivHeads[pPath];
		}
		return sprite;
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x0004E81C File Offset: 0x0004CA1C
	public static AnimationDataBoat loadAnimationBoat(string pTexturePath)
	{
		if (!ActorAnimationLoader.dict_boats.ContainsKey(pTexturePath))
		{
			Dictionary<string, Sprite> dictionary = new Dictionary<string, Sprite>();
			Sprite[] array = Resources.LoadAll<Sprite>("actors/boats/" + pTexturePath);
			foreach (Sprite sprite in array)
			{
				dictionary.Add(sprite.name, sprite);
			}
			AnimationDataBoat animationDataBoat = new AnimationDataBoat();
			animationDataBoat.broken = new ActorAnimation();
			animationDataBoat.broken.frames = new Sprite[]
			{
				dictionary["broken"]
			};
			animationDataBoat.normal = new ActorAnimation();
			animationDataBoat.normal.frames = new Sprite[]
			{
				dictionary["normal"]
			};
			foreach (Sprite sprite2 in array)
			{
				if (!sprite2.name.Contains("@1") && sprite2.name.Contains("@"))
				{
					ActorAnimationLoader.createBoatAnimationArray(animationDataBoat, dictionary, sprite2.name, 0.2f);
				}
			}
			ActorAnimationLoader.dict_boats[pTexturePath] = animationDataBoat;
		}
		return ActorAnimationLoader.dict_boats[pTexturePath];
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x0004E940 File Offset: 0x0004CB40
	private static void createBoatAnimationArray(AnimationDataBoat pAnimationData, Dictionary<string, Sprite> pDict, string pID, float pTimeBetween = 0.2f)
	{
		int num = int.Parse(pID.Split(new char[]
		{
			'@'
		})[0]);
		ActorAnimation actorAnimation = new ActorAnimation();
		actorAnimation.frames = new Sprite[2];
		actorAnimation.frames[0] = pDict[num.ToString() + "@" + 0.ToString()];
		actorAnimation.frames[1] = pDict[num.ToString() + "@" + 1.ToString()];
		actorAnimation.timeBetweenFrames = pTimeBetween;
		pAnimationData.dict.Add(num, actorAnimation);
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x0004E9DC File Offset: 0x0004CBDC
	public static AnimationDataUnit loadAnimationUnit(string pTexturePath, ActorStats pStats)
	{
		if (!ActorAnimationLoader.dict_units.ContainsKey(pTexturePath))
		{
			AnimationDataUnit animationDataUnit = ActorAnimationLoader.generateAnimation(pTexturePath, pStats);
			if (pStats.texture_heads != string.Empty)
			{
				animationDataUnit.heads = Resources.LoadAll<Sprite>("actors/" + pStats.texture_heads);
			}
		}
		return ActorAnimationLoader.dict_units[pTexturePath];
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x0004EA38 File Offset: 0x0004CC38
	private static AnimationDataUnit generateAnimation(string pSheetPath, ActorStats pStats)
	{
		AnimationDataUnit animationDataUnit = new AnimationDataUnit();
		animationDataUnit.sprites = new Dictionary<string, Sprite>();
		foreach (Sprite sprite in Resources.LoadAll<Sprite>(pSheetPath))
		{
			animationDataUnit.sprites.Add(sprite.name, sprite);
		}
		animationDataUnit.frameData = new Dictionary<string, AnimationFrameData>();
		animationDataUnit.id = pSheetPath;
		ActorAnimationLoader.generateFrameData(animationDataUnit, animationDataUnit.sprites, "walk_0,walk_1,walk_2,walk_3,swim_0,swim_1,swim_2,swim_3");
		ActorAnimationLoader.dict_units.Add(pSheetPath, animationDataUnit);
		if (pStats.animation_swim != string.Empty)
		{
			animationDataUnit.swimming = ActorAnimationLoader.createAnim(0, animationDataUnit.sprites, pStats.animation_swim, pStats.animation_swim_speed);
		}
		if (pStats.animation_walk != string.Empty)
		{
			animationDataUnit.walking = ActorAnimationLoader.createAnim(1, animationDataUnit.sprites, pStats.animation_walk, pStats.animation_walk_speed);
		}
		if (pStats.animation_idle != string.Empty)
		{
			animationDataUnit.idle = ActorAnimationLoader.createAnim(2, animationDataUnit.sprites, pStats.animation_idle, pStats.animation_idle_speed);
		}
		return animationDataUnit;
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x0004EB44 File Offset: 0x0004CD44
	private static void generateFrameData(AnimationDataUnit pAnimData, Dictionary<string, Sprite> pFrames, string pFramesIDs)
	{
		foreach (string text in pFramesIDs.Split(new char[]
		{
			','
		}))
		{
			if (pFrames.ContainsKey(text))
			{
				AnimationFrameData animationFrameData = new AnimationFrameData();
				animationFrameData.id = text;
				Sprite sprite = pFrames[text];
				if (pFrames.ContainsKey(text + "_head"))
				{
					Sprite sprite2 = pFrames[text + "_head"];
					float num = sprite2.rect.x - sprite.rect.x;
					num = num - sprite.pivot.x + sprite2.pivot.x;
					float num2 = sprite2.rect.y - sprite.rect.y;
					num2 = num2 - sprite.pivot.y + sprite2.pivot.y;
					animationFrameData.posHead = new Vector2(num, num2);
					float x = sprite2.rect.x - sprite.rect.x;
					float y = sprite2.rect.y - sprite.rect.y;
					animationFrameData.posHead_new = new Vector2(x, y);
					animationFrameData.showHead = true;
				}
				if (pFrames.ContainsKey(text + "_item"))
				{
					Sprite sprite2 = pFrames[text + "_item"];
					float num3 = sprite2.rect.x - sprite.rect.x;
					num3 = num3 - sprite.pivot.x + sprite2.pivot.x;
					float num4 = sprite2.rect.y - sprite.rect.y;
					num4 = num4 - sprite.pivot.y + sprite2.pivot.y;
					animationFrameData.posItem = new Vector2(num3, num4);
					animationFrameData.posItem_new = new Vector2(sprite2.rect.x - sprite.rect.x, sprite2.rect.y - sprite.rect.y);
					animationFrameData.showItem = true;
				}
				pAnimData.frameData.Add(text, animationFrameData);
			}
		}
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x0004EDC5 File Offset: 0x0004CFC5
	private static ActorAnimation createAnim(int pID, Dictionary<string, Sprite> dict, string pString, float pTimeBetweenFrame = 0.01f)
	{
		return new ActorAnimation
		{
			id = pID,
			frames = ActorAnimationLoader.createArray(dict, pString),
			timeBetweenFrames = pTimeBetweenFrame
		};
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x0004EDE8 File Offset: 0x0004CFE8
	private static Sprite[] createArray(Dictionary<string, Sprite> dict, string pString)
	{
		string[] array = pString.Split(new char[]
		{
			','
		});
		List<Sprite> list = new List<Sprite>();
		int i = 0;
		foreach (string text in array)
		{
			if (dict.ContainsKey(array[i]))
			{
				Sprite sprite = dict[array[i]];
				list.Add(sprite);
				i++;
			}
		}
		Sprite[] array3 = new Sprite[list.Count];
		for (i = 0; i < list.Count; i++)
		{
			array3[i] = list[i];
		}
		return array3;
	}

	// Token: 0x0400087A RID: 2170
	public static Dictionary<Sprite, int> int_ids_heads = new Dictionary<Sprite, int>();

	// Token: 0x0400087B RID: 2171
	public static Dictionary<Sprite, int> int_ids_body = new Dictionary<Sprite, int>();

	// Token: 0x0400087C RID: 2172
	private static Dictionary<string, AnimationDataUnit> dict_units = new Dictionary<string, AnimationDataUnit>();

	// Token: 0x0400087D RID: 2173
	private static Dictionary<string, AnimationDataBoat> dict_boats = new Dictionary<string, AnimationDataBoat>();

	// Token: 0x0400087E RID: 2174
	private static Dictionary<string, Sprite> dictItems = new Dictionary<string, Sprite>();

	// Token: 0x0400087F RID: 2175
	private static Dictionary<string, Sprite> dictCivHeads = new Dictionary<string, Sprite>();

	// Token: 0x04000880 RID: 2176
	public static Sprite banner;
}
