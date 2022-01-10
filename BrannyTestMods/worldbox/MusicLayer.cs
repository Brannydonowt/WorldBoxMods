using System;
using sfx;
using UnityEngine;

// Token: 0x0200019E RID: 414
public class MusicLayer : MonoBehaviour
{
	// Token: 0x06000979 RID: 2425 RVA: 0x00063FD1 File Offset: 0x000621D1
	public void create(string pId, AudioClip pClip)
	{
		this.id = pId;
		this.s.clip = pClip;
		this.s.loop = true;
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x00063FFE File Offset: 0x000621FE
	internal void setWorldSize(int pAmountMin, int pAmountMax)
	{
		this.godView = true;
		this.worldSize = true;
		this.worldSize_amount_min = pAmountMin;
		this.worldSize_amount_max = pAmountMax;
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x0006401C File Offset: 0x0006221C
	internal void setRaceSize(string pRace, int pAmountMin, int pAmountMax)
	{
		this.race = true;
		this.raceSize = true;
		this.raceSize_min = pAmountMin;
		this.raceSize_max = pAmountMax;
		if (pRace != null)
		{
			if (pRace == "elf")
			{
				this.elf = true;
				return;
			}
			if (pRace == "orc")
			{
				this.orc = true;
				return;
			}
			if (pRace == "human")
			{
				this.human = true;
				return;
			}
			if (!(pRace == "dwarf"))
			{
				return;
			}
			this.dwarf = true;
		}
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x0006409C File Offset: 0x0006229C
	internal void setAdvancement(string pRace, int pAmountMin, int pAmountMax)
	{
		this.race = true;
		this.advancement = true;
		this.advancement_min = pAmountMin;
		this.advancement_max = pAmountMax;
		if (pRace != null)
		{
			if (pRace == "elf")
			{
				this.elf = true;
				return;
			}
			if (pRace == "orc")
			{
				this.orc = true;
				return;
			}
			if (pRace == "human")
			{
				this.human = true;
				return;
			}
			if (!(pRace == "dwarf"))
			{
				return;
			}
			this.dwarf = true;
		}
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x0006411C File Offset: 0x0006231C
	internal void setRace(string pRace)
	{
		this.race = true;
		if (pRace != null)
		{
			if (pRace == "elf")
			{
				this.elf = true;
				return;
			}
			if (pRace == "orc")
			{
				this.orc = true;
				return;
			}
			if (pRace == "human")
			{
				this.human = true;
				return;
			}
			if (!(pRace == "dwarf"))
			{
				return;
			}
			this.dwarf = true;
		}
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x00064188 File Offset: 0x00062388
	internal void update()
	{
		bool flag = false;
		if (this.godView)
		{
			if (MusicMan.godView)
			{
				if (this.race)
				{
					if (this.orc && MusicMan.races["orc"].kingdom_exists)
					{
						flag = true;
					}
					if (this.elf && MusicMan.races["elf"].kingdom_exists)
					{
						flag = true;
					}
					if (this.dwarf && MusicMan.races["dwarf"].kingdom_exists)
					{
						flag = true;
					}
					if (this.human && MusicMan.races["human"].kingdom_exists)
					{
						flag = true;
					}
				}
				else if (this.worldSize)
				{
					if (this.worldSize_amount_max < MusicMan.world_size && MusicMan.world_size >= this.worldSize_amount_min)
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
			}
		}
		else if (!MusicMan.godView)
		{
			if (this.orc)
			{
				flag = this.checkRace("orc");
			}
			if (this.elf)
			{
				flag = this.checkRace("elf");
			}
			if (this.dwarf)
			{
				flag = this.checkRace("dwarf");
			}
			if (this.human)
			{
				flag = this.checkRace("human");
			}
			if (this.environment && MusicMan.dict_values[this.environmentID].enabled && MusicMan.dict_values[this.environmentID].amount > 0)
			{
				flag = true;
			}
		}
		if (flag)
		{
			if (!base.gameObject.activeSelf)
			{
				MusicMan.play(this.id);
			}
		}
		else
		{
			this.isPlaying = false;
		}
		if (this.isPlaying)
		{
			this.s.volume += Time.deltaTime;
			return;
		}
		if (base.gameObject.activeSelf)
		{
			this.s.volume -= Time.deltaTime;
			if (this.s.volume <= 0f)
			{
				MusicMan.stop(this.id);
			}
		}
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x00064390 File Offset: 0x00062590
	private bool checkRace(string pRace)
	{
		if (MusicMan.races[pRace].buildings > 0)
		{
			if (this.advancement && this.advancement_min < MusicMan.races[pRace].advancements && MusicMan.races[pRace].advancements <= this.advancement_max)
			{
				return true;
			}
			if (this.raceSize && this.raceSize_min < MusicMan.races[pRace].buildings && MusicMan.races[pRace].buildings <= this.raceSize_max)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x00064425 File Offset: 0x00062625
	public void stop()
	{
		this.isPlaying = false;
		this.s.Stop();
		this.s.volume = 0f;
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x00064458 File Offset: 0x00062658
	public bool play(float pTime = 0f)
	{
		this.isPlaying = true;
		if (base.gameObject.activeSelf)
		{
			return false;
		}
		this.s.Play();
		this.s.time = pTime;
		this.s.volume = 0f;
		base.gameObject.SetActive(true);
		return true;
	}

	// Token: 0x04000C17 RID: 3095
	public bool environment;

	// Token: 0x04000C18 RID: 3096
	public string environmentID = "";

	// Token: 0x04000C19 RID: 3097
	public bool size;

	// Token: 0x04000C1A RID: 3098
	public int sizeBound;

	// Token: 0x04000C1B RID: 3099
	public bool godView;

	// Token: 0x04000C1C RID: 3100
	public bool worldSize;

	// Token: 0x04000C1D RID: 3101
	public int worldSize_amount_min;

	// Token: 0x04000C1E RID: 3102
	public int worldSize_amount_max;

	// Token: 0x04000C1F RID: 3103
	public bool race;

	// Token: 0x04000C20 RID: 3104
	public bool orc;

	// Token: 0x04000C21 RID: 3105
	public bool elf;

	// Token: 0x04000C22 RID: 3106
	public bool human;

	// Token: 0x04000C23 RID: 3107
	public bool dwarf;

	// Token: 0x04000C24 RID: 3108
	public bool buildings;

	// Token: 0x04000C25 RID: 3109
	public bool advancement;

	// Token: 0x04000C26 RID: 3110
	public int advancement_min;

	// Token: 0x04000C27 RID: 3111
	public int advancement_max;

	// Token: 0x04000C28 RID: 3112
	public bool raceSize;

	// Token: 0x04000C29 RID: 3113
	public int raceSize_min;

	// Token: 0x04000C2A RID: 3114
	public int raceSize_max;

	// Token: 0x04000C2B RID: 3115
	public string id;

	// Token: 0x04000C2C RID: 3116
	public string path;

	// Token: 0x04000C2D RID: 3117
	private AudioClip clip;

	// Token: 0x04000C2E RID: 3118
	public AudioSource s;

	// Token: 0x04000C2F RID: 3119
	public bool isPlaying;
}
