using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x0200003A RID: 58
public class NameGeneratorLibrary : AssetLibrary<NameGeneratorAsset>
{
	// Token: 0x0600016C RID: 364 RVA: 0x00017FAF File Offset: 0x000161AF
	private void tests()
	{
		NameGeneratorLibrary.testAllNamesOutput();
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00017FB8 File Offset: 0x000161B8
	public override void init()
	{
		base.init();
		this.add(new NameGeneratorAsset
		{
			id = "kingdom_mottos",
			motto_generator = true
		});
		this.addMottoPart("all_hail", "we follow ,all hail ,we believe in ,one and only ,our life for ,swords for ,all for ,blood for ,we fight for ");
		this.addMottoPart("names", "Mastef,Hugo,Maxim,Jupe,Xiphos,Orange,Glonk,Nikon,Quicklast,Andrey,Sonja,Julia,Poncho,Faris,Cody,Jim,Steeam,Chicky,Chad,Luk,Midnight Blade,Luk,Half Chest,mountain,Airod,Astral,Little Glonk");
		this.addMottoPart("concept", "life,death,sorrow,hope,awesome,tears,eternal,one,best");
		this.addMottoPart("word_something", "nothing,love,spirits,death,life,truth,glory,wealth,roots");
		this.addMottoPart("event", "victory,death,forgiveness,salvation,redemption,happiness,life");
		this.addMottoPart("words_good", "updog,hope,power,pride,honor,strength,wisdom,skill,health,virtue,unity,valor,motherland,truth,peace,progress,prosperity,passion,compassion,duty,destiny,freedom,balance,pride,nature,justice,dignity,safety,vigilance,honesty,integrity,prosperity");
		this.addMottoPart("words_bad", "fear,doubt,anger,frustration,failure,depression,contempt,pain,regrets,sadness,remorse");
		this.addMottoPart("body_parts", "arm,leg,head,finger,body,heart,blood,tail,hell,neck,wings");
		this.addMottoPart("weapon", "sword,axe,spear,bow");
		this.addMottoPart("guided_by", "guided by ");
		this.addMottoPart("forged_by", "forged by ");
		this.addMottoPart("armor", "boots,shield,helmet,armor");
		this.addMottoPart("items", "gold,coins,rock,stone");
		this.addMottoPart("addition", "amazing,beatiful");
		this.addMottoPart("foods", "food,fish,tea,beer,bread");
		this.addMottoPart("elements", "fire,water,earth,air,magic,crystal,steel,poison,lightning,curse");
		this.addMottoPart("we", "We");
		this.addMottoPart("brings", " brings ");
		this.addMottoPart("are", " are ");
		this.addMottoPart("somewhere", " in Hell, in the Darkness, in the Shadows, in the Light, in the Sea, in the Land, in the Dreams, in the History, in the Heavens");
		this.addMottoPart("ending_concept", "life,death,war,battle,sea,kingdom");
		this.addMottoPart("bound", "bound,determined");
		this.addMottoPart("this_is_ending", "fine,good");
		this.addMottoPart("no", "no ,forget ,abandon ,destroy ,exterminate ,burn ");
		this.addMottoPart("life", "love,life,death");
		this.addMottoPart("we_ending", " we trust!, we believe!, we shine!");
		this.addMottoPart("we_desire", " we desire, we believe, we want");
		this.addMottoPart("is_the", " is the way, is the truth, is the destiny");
		this.addMottoPart("we_like", "we like ,we love ");
		this.addMottoPart("is", " is ");
		this.addMottoPart("and", " and ");
		this.addMottoPart("with", "With ");
		this.addMottoPart("in", "In ");
		this.addMottoPart("by", " by ");
		this.addMottoPart("or", " or ");
		this.addMottoPart("hold_our", "hold our ,hold my ");
		this.addMottoPart("we_use_our", "we use our ");
		this.addMottoPart("in_end", " in ");
		this.addMottoPart("we_something", " we live, we go, we attack");
		this.addMottoPart("through", " through ");
		this.addMottoPart("only", "only ,always ,forever ");
		this.addMottoPart("the", " the ");
		string text = "foods;weapon;armor;items;words_good;body_parts;elements";
		this.t.templates.Add("forged_by,names;foods;event;elements;words_good;words_bad");
		this.t.templates.Add("all_hail,names;foods;event");
		this.t.templates.Add("we_like,foods");
		this.t.templates.Add("event,through," + text);
		this.t.templates.Add("hold_our,foods;weapon;armor;items;words_good");
		this.t.templates.Add("words_good;foods;names,is,this_is_ending");
		this.t.templates.Add("in,words_good,we_ending");
		this.t.templates.Add("life,is,words_good");
		this.t.templates.Add("hold_our,weapon;armor;items");
		this.t.templates.Add("foods;weapon;armor;items;words_good,is_the");
		this.t.templates.Add("with," + text + ",somewhere");
		this.t.templates.Add("with," + text + ",we_something");
		this.t.templates.Add("with," + text + ",and," + text);
		this.t.templates.Add(string.Concat(new string[]
		{
			"with,",
			text,
			",and,",
			text,
			",we_something"
		}));
		this.t.templates.Add("we,are,concept");
		this.t.templates.Add("we,are,concept,somewhere");
		this.t.templates.Add("we,are,concept,somewhere");
		this.t.templates.Add("bound,by,word_something");
		this.t.templates.Add("no,words_bad,comma,no,words_bad,comma,only,words_good");
		this.t.templates.Add("words_good,comma,words_good,comma,words_good");
		this.t.templates.Add("words_good,and,words_good");
		this.t.templates.Add("in,word_something,we_ending");
		this.t.templates.Add("with,words_good,and,words_good");
		this.t.templates.Add("words_good;body_parts,somewhere");
		this.t.templates.Add("with,words_good;body_parts,and,words_good;body_parts");
		this.t.templates.Add("guided_by,words_good");
		this.t.templates.Add("words_good,brings,words_good");
		this.t.templates.Add("we_use_our,weapon;body_parts,in_end,ending_concept");
		this.t.templates.Add(text + ",or,words_bad");
		this.add(new NameGeneratorAsset
		{
			id = "human_culture"
		});
		this.t.part_groups.Add("ne,wa,gi,ca,fo,two,ado,ja,ste,sho,klo,e,a,ka,ri,wu,to");
		this.t.part_groups.Add("ru,ni,too,vo,phu,te,ku,ve,su,me,du,pe,to,ste,o,e,a,u,oo");
		this.t.part_groups.Add("rab,dab,pab,ian");
		this.t.templates.Add("Part_group");
		this.add(new NameGeneratorAsset
		{
			id = "elf_culture"
		});
		this.t.part_groups.Add("A',E',O',Yda,Ifa,Yaa,Ia,Oya,Yra,Na,Ma,Ya");
		this.t.part_groups.Add("dre,dra,de,du,te,tre,tra,tro,ti,to,tri,na,ma,sa,da,pa");
		this.t.part_groups.Add("o,e,y,a,yo,oi,,");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "dwarf_culture"
		});
		this.t.part_groups.Add("X,Th,Tr,Gl,H,Dh,Dum,Dor,Gor,Ther,Thor,Gah,Geh,Ger,Dur,Der,Dar,Dig,Deg,Dag,Ger,Dhan,Don,Dan");
		this.t.part_groups.Add("i,o,a,e,odi,adi,edi,ada,idi,uru,udu,igi,egi,oki,oko");
		this.t.part_groups.Add("d,ded,dad,dum,gah,geh,h,d,b,c,gh,th,gh,gagh,digh,dig,dag,dog,,,");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "orc_culture"
		});
		this.t.part_groups.Add("Dek,Kek,Kak,Dak,Zeg,Zog,Zag,Bak,Dak,Rak,Dek,Mek,Mak");
		this.t.part_groups.Add(" ");
		this.t.part_groups.Add("Dek,Kek,Kak,Dak,Zeg,Zog,Zag,Bak,Dak,Rak,Dek,Mek,Mak,Bah,Wah,Waah");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "human_name",
			vowels = new string[]
			{
				"a",
				"e",
				"i",
				"o",
				"u",
				"y"
			},
			consonants = new string[]
			{
				"b",
				"c",
				"d",
				"g",
				"h",
				"ph",
				"ch",
				"k",
				"l",
				"m",
				"n",
				"p",
				"r",
				"s",
				"t",
				"v",
				"w",
				"sh"
			}
		});
		this.t.templates.Add("Letters#3-8");
		this.clone("human_city", "human_name");
		this.t.parts = new string[]
		{
			"ork",
			"ah",
			"ois",
			"nsin",
			"ona",
			"ina",
			"insk",
			"ovo",
			"va",
			"od",
			"irsk",
			"wa",
			"owo",
			"irk",
			"ow",
			"es",
			"go",
			"on",
			"owa",
			"dran",
			"drun",
			"be",
			"ka",
			"ama",
			"kyo",
			"ro",
			"poro",
			"to"
		};
		this.t.templates.Add("Letters#2-5,part");
		this.clone("human_kingdom", "human_name");
		this.t.add_addition_chance = 0.5f;
		this.t.addition_start = new string[]
		{
			"Great",
			"Holy",
			"Realm of the",
			"The"
		};
		this.t.addition_ending = new string[]
		{
			"Empire",
			"Hegemony",
			"Kingdom",
			"Imperium",
			"of Sun",
			"of Moon",
			"Dynasty"
		};
		this.t.templates.Add("addition_start,Letters#2-7,addition_ending");
		this.add(new NameGeneratorAsset
		{
			id = "orc_name",
			vowels = new string[]
			{
				"a",
				"e",
				"o"
			},
			consonants = new string[]
			{
				"d",
				"g",
				"k",
				"n",
				"p",
				"r",
				"t",
				"z"
			},
			parts = new string[]
			{
				"igh",
				"ord",
				"uz",
				"duz",
				"rid",
				"ogh",
				"agh",
				"okh",
				"uh",
				"dzz",
				"ez",
				"az",
				"oz",
				"top",
				"urg"
			}
		});
		this.t.templates.Add("Letters#3-7");
		this.t.templates.Add("Part");
		this.t.templates.Add("Part,part");
		this.t.templates.Add("Part,space,Part");
		this.clone("orc_city", "orc_name");
		this.t.templates.Add("Part,space,Part");
		this.t.templates.Add("consonant,letters#3-5");
		this.t.templates.Add("consonant,letters#3-5,part");
		this.t.templates.Add("consonant,letters#1-3,space,CONSONANT,letters#2-3");
		this.clone("orc_kingdom", "orc_name");
		this.t.add_addition_chance = 0.5f;
		this.t.addition_start = new string[]
		{
			"Bloody",
			"Clan of",
			"Axe of",
			"The",
			"Blood of",
			"Bad",
			"Strong",
			"Tall",
			"Red",
			"Green"
		};
		this.t.addition_ending = new string[]
		{
			"Fighters",
			"Gang",
			"Band",
			"of Death",
			"Axes",
			"Clan",
			"Brothers",
			"Warriors",
			"Boyz",
			"Horde"
		};
		this.t.templates.Add("addition_start,CONSONANT,letters#3-5,part,addition_ending");
		this.t.templates.Add("addition_start,Part,addition_ending");
		this.t.templates.Add("addition_start,Part,part,addition_ending");
		this.t.templates.Add("addition_start,Part,space,Part,addition_ending");
		this.t.templates.Add("addition_start,CONSONANT,letters#1-3,space,CONSONANT,letters#2-3,addition_ending");
		this.add(new NameGeneratorAsset
		{
			id = "dwarf_name",
			vowels = new string[]
			{
				"a",
				"e",
				"o"
			},
			special1 = new string[]
			{
				"Dhun",
				"Dum",
				"Gor",
				"Ber",
				"Ger",
				"Kog",
				"Gul",
				"Ther",
				"Thor",
				"Von",
				"Gil",
				"Ver",
				"Vagh",
				"Gigh",
				"Deg",
				"Dig",
				"Bam",
				"Von",
				"Han",
				"Dhir",
				"Mugh",
				"Mul",
				"Gorn",
				"Gan",
				"Bol",
				"Gal"
			},
			consonants = new string[]
			{
				"b",
				"d",
				"g",
				"h",
				"k",
				"m",
				"n",
				"p",
				"r",
				"s",
				"t"
			},
			parts = new string[]
			{
				"ahl",
				"uhm",
				"ril",
				"til",
				"rim",
				"dum",
				"al",
				"or",
				"uhr",
				"ihr",
				"or"
			},
			max_consonanats_in_row = 1,
			max_vowels_in_row = 1
		});
		this.t.templates.Add("special1,space,Letters#2-4,part");
		this.t.templates.Add("special1,space,Letters#2-4,part");
		this.t.templates.Add("Letters#1-4,part");
		this.t.templates.Add("Letters#1-4,part");
		this.clone("dwarf_city", "dwarf_name");
		this.t.templates.Add("special1,space,Letters#2-4,part");
		this.t.templates.Add("letters#2-4,part");
		this.t.templates.Add("letters#2-4,part");
		this.clone("dwarf_kingdom", "dwarf_name");
		this.t.add_addition_chance = 0.5f;
		this.t.addition_start = new string[]
		{
			"Miners of",
			"Great",
			"Rocky",
			"Spears of",
			"Ancient"
		};
		this.t.addition_ending = new string[]
		{
			"Stones",
			"Rocks",
			"Boulders",
			"Mountain",
			"Mountains",
			"Miners",
			"Kingdom",
			"Shields",
			"Picks",
			"Drunks"
		};
		this.t.templates.Add("addition_start,special1,space,Letters#1-4,part,addition_ending");
		this.t.templates.Add("addition_start,Letters#1-4,part,addition_ending");
		this.t.templates.Add("addition_start,Letters#1-4,part,addition_ending");
		this.t.templates.Add("addition_start,Letters#1-4,part,addition_ending");
		this.add(new NameGeneratorAsset
		{
			id = "elf_name",
			vowels = new string[]
			{
				"a",
				"e",
				"o"
			},
			consonants = new string[]
			{
				"c",
				"d",
				"f",
				"h",
				"l",
				"m",
				"n",
				"r",
				"s",
				"t"
			},
			special1 = new string[]
			{
				"Ylh",
				"Emy",
				"Ny",
				"Ly",
				"If",
				"Se",
				"Am",
				"Yaa",
				"Omy",
				"Na",
				"Yg",
				"Yd",
				"Eo",
				"Yo",
				"O",
				"A"
			},
			special2 = new string[]
			{
				"A'",
				"E'",
				"O'"
			},
			parts = new string[]
			{
				"ua",
				"ra",
				"on",
				"ad",
				"nor",
				"ne",
				"hel",
				"on",
				"hil",
				"ore",
				"ora",
				"ona",
				"era",
				"eas",
				"ari",
				"adi",
				"ona"
			},
			max_consonanats_in_row = 1,
			max_vowels_in_row = 1
		});
		this.t.templates.Add("special1,letters#1-4,part");
		this.t.templates.Add("special2,Letters#1-4,part");
		this.t.templates.Add("letters#1-4,part");
		this.t.templates.Add("letters#1-4,part");
		this.clone("elf_city", "elf_name");
		this.t.templates.Add("special1,letters#1-4,part");
		this.t.templates.Add("special2,Letters#1-4,part");
		this.t.templates.Add("special1,letters#1-4,space,Letters#1-3,part");
		this.t.templates.Add("letters#1-4,part");
		this.t.templates.Add("letters#1-4,part");
		this.clone("elf_kingdom", "elf_name");
		this.t.add_addition_chance = 0.5f;
		this.t.addition_start = new string[]
		{
			"Great",
			"Green",
			"Arrows of",
			"Spears of",
			"Ancient",
			"Royal"
		};
		this.t.addition_ending = new string[]
		{
			"Keepers",
			"Forest",
			"Children",
			"Brothers",
			"of Fire",
			"of Rain",
			"of Earth",
			"Kingdom",
			"Lands"
		};
		this.t.templates.Add("addition_start,special1,letters#1-4,part,addition_ending");
		this.t.templates.Add("addition_start,special2,Letters#1-4,part,addition_ending");
		this.t.templates.Add("addition_start,special1,letters#1-4,space,Letters#1-3,part,addition_ending");
		this.t.templates.Add("addition_start,Letters#1-4,part,addition_ending");
		this.t.templates.Add("addition_start,Letters#1-4,part,addition_ending");
		this.add(new NameGeneratorAsset
		{
			id = "sheep_name"
		});
		this.t.part_groups.Add("b");
		this.t.part_groups.Add("a,aa,aaa,o,oo,ooo,y,yy,yyy");
		this.t.part_groups.Add("h");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "penguin_name"
		});
		this.t.part_groups.Add("h,w,d,t");
		this.t.part_groups.Add("u,uu,uuu,a,aa,aaa");
		this.t.part_groups.Add("g,gg,d,r,z");
		this.t.part_groups.Add("o,oo,ooo,a,aa,aaa");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "turtle_name"
		});
		this.t.part_groups.Add("l,t,g,z");
		this.t.part_groups.Add("u,uu,a,aa,o,oo");
		this.t.part_groups.Add("k,kk");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "wolf_name"
		});
		this.t.part_groups.Add("w");
		this.t.part_groups.Add("o,oo,oo,a,aa,aaa");
		this.t.part_groups.Add("f");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "greg_name"
		});
		this.t.part_groups.Add("Greg,Gregis,Gregim,Greges,Gregef,Gregonk,Gregos,Gregaz");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "chicken_name"
		});
		this.t.part_groups.Add("k,g,d,b,ka,ko,go,keke,koko");
		this.t.part_groups.Add("o,a,a");
		this.t.templates.Add("part_group");
		this.t.templates.Add("part_group,part_group");
		this.t.templates.Add("part_group,space,Part_group");
		this.t.templates.Add("part_group,part_group");
		this.t.templates.Add("part_group,space,Part_group,space,Part_group");
		this.t.templates.Add("part_group,space,Part_group");
		this.add(new NameGeneratorAsset
		{
			id = "alien_name"
		});
		this.t.part_groups.Add("ze,zo,zo,ze,zog,zag,xag,xeg,xo,xe");
		this.t.part_groups.Add("o,oo,oo,a,aa,a,y,u,,,");
		this.t.part_groups.Add("r,d,z,ze,tze,tza,n");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "ufo_name"
		});
		this.t.part_groups.Add("TK-,XX-,YT-,Z-,RT-,PP-,KL-,QR-");
		this.t.part_groups.Add("0,1,2,3,4,5,6,7,8,9,404");
		this.t.part_groups.Add("0,1,2,3,4,5,6,7,8,9,404");
		this.t.part_groups.Add("0,1,2,3,4,5,6,7,8,9,404");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "cold_one_name"
		});
		this.t.part_groups.Add("ice,icy,co,colo");
		this.t.part_groups.Add("m,n,mem,non");
		this.t.part_groups.Add("e,a,o,y");
		this.t.part_groups.Add("n,c,k,brr,b");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "bug_name"
		});
		this.t.part_groups.Add("e,ee,eee,eeee");
		this.t.part_groups.Add("w,ww,www");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "ant_name"
		});
		this.t.part_groups.Add("an,ant,ano");
		this.t.part_groups.Add(",n,o,on");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "demon_name"
		});
		this.t.part_groups.Add("a'n,e'r,'o'r,o'h,an,am,akan,al,bal,fur,d,dev,gar,kra,val");
		this.t.part_groups.Add("a,i,e");
		this.t.part_groups.Add("r,vel,v,s,on,mon,al");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "fairy_name"
		});
		this.t.part_groups.Add("ar,er,or,ir");
		this.t.part_groups.Add("i,a,e");
		this.t.part_groups.Add("a,e");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "crab_name"
		});
		this.t.part_groups.Add("c,cr");
		this.t.part_groups.Add("a,aa,aaa,o,o,o");
		this.t.part_groups.Add("b,be,ba,bo");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "cow_name",
			parts = new string[]
			{
				"moo",
				"oom",
				"mi"
			}
		});
		this.t.part_groups.Add("m,l,d,b,mad,daa,moo");
		this.t.part_groups.Add("i,o,a,y");
		this.t.part_groups.Add("d,m");
		this.t.templates.Add("part_group");
		this.t.templates.Add("Part,space,Part_group");
		this.t.templates.Add("part_group,space,Part");
		this.t.templates.Add("part_group,space,Part_group");
		this.t.templates.Add("part_group,part_group");
		this.t.templates.Add("part,part_group");
		this.add(new NameGeneratorAsset
		{
			id = "default_name",
			vowels = new string[]
			{
				"a",
				"e",
				"i",
				"o",
				"u",
				"y",
				"oo"
			}
		});
		this.t.part_groups.Add("Pl,P,S,L,D,B");
		this.t.part_groups2.Add("mp,rp,rt,b,");
		this.t.part_groups3.Add("kin,tin,le,ee");
		this.t.templates.Add("part_group,vowel,part_group2,part_group3");
		this.add(new NameGeneratorAsset
		{
			id = "evil_mage_name",
			vowels = new string[]
			{
				"a",
				"e",
				"i",
				"o",
				"u",
				"y"
			},
			consonants = new string[]
			{
				"b",
				"c",
				"d",
				"g",
				"h",
				"ph",
				"ch",
				"k",
				"l",
				"m",
				"n",
				"p",
				"r",
				"s",
				"t",
				"v",
				"w",
				"sh"
			},
			parts = new string[]
			{
				"fez",
				"soh",
				"doroz",
				"uru",
				"hazah",
				"drah",
				"wazah",
				"mah"
			}
		});
		this.t.addition_start = new string[]
		{
			"Great",
			"Awe",
			"Bro",
			"Lo",
			"Le",
			"Blo",
			"Mago",
			"Evol",
			"Fir",
			"Smol",
			"Dro"
		};
		this.t.templates.Add("addition_start,Letters#2-6,part");
		this.add(new NameGeneratorAsset
		{
			id = "rhino_name"
		});
		this.t.part_groups.Add("Sharp,Dull,Broken,Jagged,Long,Short,Soft,Thick,Flat,Serrated,Smooth");
		this.t.part_groups.Add("-Horn,-Nose,-Tip,-Snoot,-Snout");
		this.t.templates.Add("part_group,space,CONSONANT,vowel,consonant,vowel");
		this.t.templates.Add("part_group,space,CONSONANT,vowel,consonant,vowel,consonant");
		this.t.templates.Add("part_group,space,CONSONANT,vowel,consonant,vowel,consonant,vowel");
		this.t.templates.Add("part_group,space,CONSONANT,vowel,consonant,consonant,vowel,consonant");
		this.t.templates.Add("part_group,space,CONSONANT,vowel,consonant,consonant,vowel,consonant,vowel");
		this.t.templates.Add("CONSONANT,vowel,consonant,vowel");
		this.t.templates.Add("CONSONANT,vowel,consonant,vowel,consonant");
		this.t.templates.Add("CONSONANT,vowel,consonant,vowel,consonant,vowel");
		this.t.templates.Add("CONSONANT,vowel,consonant,consonant,vowel,consonant");
		this.t.templates.Add("CONSONANT,vowel,consonant,consonant,vowel,consonant,vowel");
		this.add(new NameGeneratorAsset
		{
			id = "monkey_name",
			consonants = new string[]
			{
				"b",
				"c",
				"d",
				"f",
				"h",
				"j",
				"ch",
				"k",
				"l",
				"m",
				"n",
				"p",
				"r",
				"s",
				"t",
				"v",
				"w",
				"sh"
			},
			vowels = new string[]
			{
				"a",
				"e",
				"i",
				"o",
				"u",
				"y",
				"ee",
				"oo",
				"aa"
			}
		});
		this.t.templates.Add("VOWEL,consonant,vowel");
		this.t.templates.Add("VOWEL,consonant,vowel,vowel");
		this.t.templates.Add("CONSONANT,vowel,vowel,consonant,vowel");
		this.t.templates.Add("CONSONANT,vowel,consonant,vowel");
		this.add(new NameGeneratorAsset
		{
			id = "buffalo_name",
			parts = new string[]
			{
				"Br",
				"F",
				"D",
				"T"
			}
		});
		this.t.part_groups.Add("B");
		this.t.part_groups.Add("uff,eef,ill");
		this.t.part_groups.Add("y,ee,");
		this.t.part_groups2.Add("Tau");
		this.t.part_groups2.Add("ris,rus,ry,ro");
		this.t.part_groups3.Add("an");
		this.t.part_groups3.Add("ger,gy,gys,gus");
		this.t.templates.Add("Part,part_group3");
		this.t.templates.Add("part_group,space,part_group2");
		this.t.templates.Add("part_group,space,Part,part_group3");
		this.add(new NameGeneratorAsset
		{
			id = "fox_name"
		});
		this.t.part_groups.Add("F,Ph,V");
		this.t.part_groups.Add("o,i,u,a");
		this.t.part_groups.Add("x,xx,xz");
		this.t.part_groups.Add(",,y,in,en");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "hyena_name"
		});
		this.t.part_groups.Add("G,B,Sn,W");
		this.t.part_groups.Add("iggl,abbl");
		this.t.part_groups.Add("e,ee,eee,y,yy,yyy");
		this.t.part_groups2.Add("H,F,Wh,B,D");
		this.t.part_groups2.Add("i,y,ai");
		this.t.part_groups2.Add("en,een,eeen");
		this.t.part_groups2.Add("a,er,uh,aa,eer,uuh");
		this.t.part_groups3.Add("C,K");
		this.t.part_groups3.Add("a,aa,aaa,e,ee,eee,i,ii,iii,o,oo,ooo,y,yy,yyy");
		this.t.part_groups3.Add("ch,kh,ck,kc");
		this.t.part_groups3.Add("le,lee,ler,leer,ling,liing,lin,liin");
		this.t.templates.Add("part_group");
		this.t.templates.Add("part_group2");
		this.t.templates.Add("part_group3");
		this.add(new NameGeneratorAsset
		{
			id = "crocodile_name",
			vowels = new string[]
			{
				"o",
				"a",
				"i"
			},
			special1 = new string[]
			{
				"o",
				"ah",
				"i"
			}
		});
		this.t.parts = new string[]
		{
			"Cr",
			"Kr"
		};
		this.t.part_groups.Add("c,k,ck");
		this.t.part_groups2.Add("ill,eel,ile,yl");
		this.t.templates.Add("part,special1,part_group");
		this.t.templates.Add("part,special1,part_group,special1");
		this.t.templates.Add("part,special1,part_group,special1,consonant,part_group2");
		this.t.templates.Add("part,special1,part_group,special1,consonant,part_group2,vowel");
		this.add(new NameGeneratorAsset
		{
			id = "snake_name"
		});
		this.t.part_groups.Add("S,Ss,Sss,Sch,Sh");
		this.t.part_groups.Add("n,m");
		this.t.part_groups2.Add("c,k,ck,ch,kh");
		this.t.templates.Add("part_group,vowel,part_group2");
		this.t.templates.Add("part_group,vowel,part_group2,vowel");
		this.t.templates.Add("part_group,vowel,vowel,part_group2,vowel");
		this.t.part_groups3.Add("azz,ooz,eez,zher");
		this.t.part_groups3.Add("ra,ka,er,erm,ez");
		this.t.templates.Add("part_group,part_group3,vowel");
		this.t.templates.Add("part_group,part_group3");
		this.add(new NameGeneratorAsset
		{
			id = "frog_name"
		});
		this.t.part_groups.Add("R");
		this.t.part_groups.Add("i,y");
		this.t.part_groups.Add("b,bb");
		this.t.part_groups.Add("i,y,o");
		this.t.part_groups.Add("t");
		this.t.part_groups.Add(",er,ing");
		this.t.part_groups2.Add("J,W,Y");
		this.t.part_groups2.Add("i,o,u");
		this.t.part_groups2.Add("mp");
		this.t.part_groups2.Add("y,er,ing");
		this.t.part_groups3.Add("K,C");
		this.t.part_groups3.Add("r");
		this.t.part_groups3.Add("o,e");
		this.t.part_groups3.Add("a,e");
		this.t.part_groups3.Add("q,k,c");
		this.t.part_groups3.Add(",i,y,er,ing");
		this.t.templates.Add("part_group");
		this.t.templates.Add("part_group2");
		this.t.templates.Add("part_group3");
		this.add(new NameGeneratorAsset
		{
			id = "bioblob_name"
		});
		this.t.part_groups.Add("Seeing ,Seeking ,Peeking ,Staring ,Watchful ,");
		this.t.part_groups.Add("Eye ,Eyes ,Sight ,Syght ");
		this.t.part_groups.Add("of ,");
		this.t.part_groups2.Add("K,C");
		this.t.part_groups2.Add("t,th,sh,ch,v,");
		this.t.part_groups2.Add("a,e,i,o,u,y");
		this.t.part_groups2.Add("l,w,v,b,sh");
		this.t.part_groups2.Add("a,e,i,o,u,y");
		this.t.part_groups3.Add("Watch,Peek,Seek,Gaz,See");
		this.t.part_groups3.Add("ing,yng,er,r");
		this.t.templates.Add("part_group,part_group2");
		this.t.templates.Add("part_group3,space,part_group2");
		this.add(new NameGeneratorAsset
		{
			id = "assimilator_name",
			special1 = new string[]
			{
				"-",
				""
			}
		});
		this.t.part_groups.Add("TK-,XX-,YT-,Z-,RT-,PP-,KL-,QR-,ER-");
		this.t.templates.Add("part_group,number,special1,number,special1,number");
		this.add(new NameGeneratorAsset
		{
			id = "fire_skull_name"
		});
		this.t.part_groups.Add("Reap,Bone,Bones,Minion,Doll,Disciple,Zealot,Apostle,Spirit,Skull,Braincase,Vertex,Scalp,Crown,Mind");
		this.t.templates.Add("part_group,vowelchance");
		this.t.templates.Add("part_group,removalchance,vowelchance");
		this.add(new NameGeneratorAsset
		{
			id = "acid_blob_name"
		});
		this.t.part_groups.Add("Bl,Gl,Dr,Sl");
		this.t.part_groups.Add("a,e,er,i,o,oo,u,y");
		this.t.part_groups.Add("b,p,m");
		this.t.part_groups.Add(",er,ir,y,i,ster");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "jumpy_skull_name"
		});
		this.t.part_groups.Add("Unholy,Dark,Knavish,Odious,Shameful,Disgraced,Abominable,Appalling,Abhorrent,Monstrous,Fiendish,Bouncy,Rubber");
		this.t.part_groups2.Add("Reap,Reaper,Bone,Bones,Minion,Doll,Disciple,Zealot,Apostle,Spirit,Skull,Braincase,Vertex,Scalp,Crown,Mind");
		this.t.templates.Add("part_group2");
		this.t.templates.Add("part_group,space,part_group2");
		this.add(new NameGeneratorAsset
		{
			id = "lil_pumpkin_name"
		});
		this.t.part_groups.Add("M,N,X");
		this.t.part_groups.Add("a,aa,o,oo,y");
		this.t.part_groups.Add("x,d,z");
		this.t.part_groups.Add("i,o,a,u");
		this.t.part_groups.Add("m,n,x,z");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "rat_name"
		});
		this.t.part_groups.Add("ma,ar,a,e,i,b,c");
		this.t.part_groups.Add("v,m,n,k,rk,x,rt");
		this.t.part_groups.Add("o,s,a,y,e,i");
		this.t.part_groups.Add("m,n,a,d");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "cat_name"
		});
		this.t.part_groups.Add("M");
		this.t.part_groups.Add("e,ee,ee");
		this.t.part_groups.Add("o,oo,oo,");
		this.t.part_groups.Add("w,ww,ww");
		this.t.templates.Add("part_group");
		this.t.templates.Add("part_group,space,part_group");
		this.add(new NameGeneratorAsset
		{
			id = "rabbit_name"
		});
		this.t.part_groups.Add("J,Dj");
		this.t.part_groups.Add("a,e,o,ae,oe");
		this.t.part_groups.Add("t,p,m");
		this.t.part_groups.Add("o,a,y,eke");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "piranha_name"
		});
		this.t.part_groups.Add("C");
		this.t.part_groups.Add("h");
		this.t.part_groups.Add("o,a,oe,ae");
		this.t.part_groups.Add("p,d,b");
		this.t.templates.Add("part_group,space,part_group");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "snowman_name"
		});
		this.t.part_groups.Add("S,Z,Ss,Zz");
		this.t.part_groups.Add("o,a,o,");
		this.t.part_groups.Add("s,zz,ss,zzz");
		this.t.part_groups.Add("y,e,o");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "bear_name"
		});
		this.t.part_groups.Add("H");
		this.t.part_groups.Add("h,");
		this.t.part_groups.Add("o,a,e");
		this.t.part_groups.Add("n");
		this.t.part_groups.Add("y,e,o,");
		this.t.part_groups.Add("y,e,o,");
		this.t.part_groups.Add("a,");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "skeleton_name"
		});
		this.t.part_groups.Add("B");
		this.t.part_groups.Add("o,a,e");
		this.t.part_groups.Add("n");
		this.t.part_groups.Add("y,e,o,");
		this.t.part_groups.Add("y,e,o,");
		this.t.part_groups.Add("s");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "homie_name"
		});
		this.t.part_groups.Add("H");
		this.t.part_groups.Add("o,a,e");
		this.t.part_groups.Add("m");
		this.t.part_groups.Add("i,y,e,o,");
		this.t.part_groups.Add("y,e,o,");
		this.t.templates.Add("part_group");
		this.add(new NameGeneratorAsset
		{
			id = "living_plant_name"
		});
		this.t.part_groups.Add("Pl");
		this.t.part_groups.Add("o,a,e");
		this.t.part_groups.Add("n");
		this.t.part_groups.Add("t,");
		this.t.part_groups.Add("i,y,e,o,");
		this.t.part_groups.Add("y,e,o,");
		this.t.templates.Add("part_group");
	}

	// Token: 0x0600016E RID: 366 RVA: 0x0001AFA9 File Offset: 0x000191A9
	private void addMottoPart(string pID, string pListString)
	{
		this.t.motto_parts.Add(pID, pListString);
	}

	// Token: 0x0600016F RID: 367 RVA: 0x0001AFC0 File Offset: 0x000191C0
	private static void testAllNamesForUniqueness()
	{
		string str = "";
		foreach (NameGeneratorAsset nameGeneratorAsset in AssetManager.nameGenerator.list)
		{
			str = str + "\n--- asset name: " + nameGeneratorAsset.id + " ---\n";
			HashSet<string> hashSet = new HashSet<string>();
			for (int i = 0; i < 1000; i++)
			{
				string text = NameGenerator.generateNameFromTemplate(nameGeneratorAsset);
				str = str + text + "\n";
				if (!hashSet.Contains(text))
				{
					hashSet.Add(text);
				}
			}
			File.AppendAllText(Application.persistentDataPath + "/name_test3_uniq", string.Concat(new string[]
			{
				"Unique names for asset ",
				nameGeneratorAsset.id,
				": ",
				hashSet.Count.ToString(),
				"\n"
			}));
		}
	}

	// Token: 0x06000170 RID: 368 RVA: 0x0001B0C8 File Offset: 0x000192C8
	private static void testAllNamesOutput()
	{
		string text = "";
		foreach (NameGeneratorAsset nameGeneratorAsset in AssetManager.nameGenerator.list)
		{
			text = text + "\n--- asset name: " + nameGeneratorAsset.id + " ---\n";
			for (int i = 0; i < 20; i++)
			{
				text = text + NameGenerator.generateNameFromTemplate(nameGeneratorAsset) + "\n";
			}
			File.WriteAllText(Application.persistentDataPath + "/name_test3", text);
		}
	}

	// Token: 0x06000171 RID: 369 RVA: 0x0001B16C File Offset: 0x0001936C
	private static void testNamesCulture()
	{
		string text = "";
		text += "\n--- elf_culture name:\n";
		for (int i = 0; i < 20; i++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("elf_culture")) + "\n";
		}
		text += "\n--- dwarf_culture name:\n";
		for (int j = 0; j < 20; j++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("dwarf_culture")) + "\n";
		}
		text += "\n--- orc_culture name:\n";
		for (int k = 0; k < 20; k++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("orc_culture")) + "\n";
		}
		text += "\n--- human_culture name:\n";
		for (int l = 0; l < 20; l++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("human_culture")) + "\n";
		}
		File.WriteAllText(Application.persistentDataPath + "/name_test2", text);
	}

	// Token: 0x06000172 RID: 370 RVA: 0x0001B27C File Offset: 0x0001947C
	private static void testMottos()
	{
		string text = "";
		text += "\n--- Mottos:\n";
		for (int i = 0; i < 100; i++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("motto_gen")) + "\n";
		}
		File.WriteAllText(Application.persistentDataPath + "/name_test_mottos", text);
	}

	// Token: 0x06000173 RID: 371 RVA: 0x0001B2E0 File Offset: 0x000194E0
	private static void testNames()
	{
		string text = "";
		text += "\n--- elf name:\n";
		for (int i = 0; i < 20; i++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("elf_name")) + "\n";
		}
		text += "\n--- elf City:\n";
		for (int j = 0; j < 20; j++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("elf_city")) + "\n";
		}
		text += "\n--- elf Kingdom:\n";
		for (int k = 0; k < 20; k++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("elf_kingdom")) + "\n";
		}
		text += "\n--- dwarf name:\n";
		for (int l = 0; l < 20; l++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("dwarf_name")) + "\n";
		}
		text += "\n--- dwarf City:\n";
		for (int m = 0; m < 20; m++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("dwarf_city")) + "\n";
		}
		text += "\n--- dwarf Kingdom:\n";
		for (int n = 0; n < 20; n++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("dwarf_kingdom")) + "\n";
		}
		text += "\n--- orc name:\n";
		for (int num = 0; num < 20; num++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("orc_name")) + "\n";
		}
		text += "\n--- orc City:\n";
		for (int num2 = 0; num2 < 20; num2++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("orc_city")) + "\n";
		}
		text += "\n--- orc Kingdom:\n";
		for (int num3 = 0; num3 < 20; num3++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("orc_kingdom")) + "\n";
		}
		text += "\n--- Human name:\n";
		for (int num4 = 0; num4 < 20; num4++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("human_name")) + "\n";
		}
		text += "\n--- Human City:\n";
		for (int num5 = 0; num5 < 20; num5++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("human_city")) + "\n";
		}
		text += "\n--- Human Kingdom:\n";
		for (int num6 = 0; num6 < 20; num6++)
		{
			text = text + NameGenerator.generateNameFromTemplate(AssetManager.nameGenerator.get("human_kingdom")) + "\n";
		}
		File.WriteAllText(Application.persistentDataPath + "/name_test2", text);
	}
}
