//extern alias ncms;
//using NCMS = ncms.NCMS;
using NCMS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;

namespace BrannyTestMods
{
	public partial class WorldBoxMod
	{
		public static ActorStatus killLeader;

		public struct LeaderboardEntry
		{
			public string actorId;
			public int position;
			public int statValue;

			public LeaderboardEntry(string id, int pos, int statValue)
			{
				this.actorId = id;
				this.position = pos;
				this.statValue = statValue;
			}
		}

		public static List<LeaderboardEntry> killLeaderboard;
		public static List<LeaderboardEntry> childrenLeaderboard;

		public static int GetPositionOnLeaderboard(string type, int numStat) 
		{
			List<LeaderboardEntry> targetLeaderboard = GetLeaderboardFromType(type);

			// if there are none in this leaderboard
			// TO-DO add new leaderboard creation
			if (targetLeaderboard.Count <= 0)
				return 999;

			int lastEntry = targetLeaderboard[targetLeaderboard.Count - 1].statValue;
			// if our stat is higher than that of the last entry on the leaderboard
			if (numStat > lastEntry)
			{
				int bestIndex = 999;
				foreach (LeaderboardEntry e in targetLeaderboard)
				{
					// The new entry belongs higher
					if (numStat > e.statValue)
					{
						if (bestIndex < targetLeaderboard.IndexOf(e))
							bestIndex = targetLeaderboard.IndexOf(e);
					}
				}

				if (bestIndex == 999)
					Debug.Log("No Best Index found, does not belong");

				return bestIndex;
			}
			else 
			{
				return 999;
			}
		}

		static List<LeaderboardEntry> GetLeaderboardFromType(string type) 
		{
			List<LeaderboardEntry> targetLeaderboard;

			switch (type)
			{
				case "Kills":
					targetLeaderboard = killLeaderboard;
					break;
				case "Children":
					targetLeaderboard = childrenLeaderboard;
					break;
				default:
					targetLeaderboard = childrenLeaderboard;
					break;
			}

			return targetLeaderboard;
		}

		static public bool tryAddToLeaderboard(string type, string actorId, int numStat) 
		{
			if (CompareStatToLeaderboards(type, actorId, numStat)) 
			{
				int pos = GetPositionOnLeaderboard(type, numStat);

				if (pos == 999)
				{
					Debug.Log("Position on leaderboard returned 999");
					return false;
				}

				List<LeaderboardEntry> leaderboard = GetLeaderboardFromType(type);
				if (leaderboard.Count >= 10)
					leaderboard.RemoveAt(leaderboard.Count - 1);
				LeaderboardEntry newEntry = new LeaderboardEntry(actorId, pos, numStat);
				leaderboard.Insert(pos, newEntry);

				if (pos == 0)
					// Do something special
					Debug.Log("Brand new leader");

				CreateNewStatLeaderboard(type, leaderboard);

				return true;
			}

			Debug.Log("Reached end of tryAdd method");
			return false;
		}

		// Returns true if the stat is higher than in the leaderboard
		static public bool CompareStatToLeaderboards(string type, string ActorId, int numStat) 
		{
			List<LeaderboardEntry> targetLeaderboard = GetLeaderboardFromType(type);

			// Are there any entries yet?
			if (targetLeaderboard.Count <= 0)
				return true;

			// If our value is higher than the current heightest
			if (targetLeaderboard[targetLeaderboard.Count - 1].statValue > numStat) 
			{
				return true;	
			}

			return false;
		}
	}
}
