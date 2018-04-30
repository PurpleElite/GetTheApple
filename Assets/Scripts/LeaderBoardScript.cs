using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LeaderBoardScript {
	public static int EntryCount = 1;

	public struct ScoreEntry {
		public string name;
		public float score;

		public ScoreEntry(string name, float score) {
			this.name = PlayerPrefs.GetString("Username");
			this.score = Time.time - StartGame.startTime;
		}
	}
	private static List<ScoreEntry> s_Entries;

	private static List<ScoreEntry> Entries {
		get {
			if (s_Entries == null) {
				s_Entries = new List<ScoreEntry>();
				LoadScores();
			}
			return s_Entries;
		}
	}

	private const string PlayerPrefsBaseKey = "leaderboard";

	private static void SortScores() {
		s_Entries.Sort((a, b) => b.score.CompareTo(a.score));
	}

	private static void LoadScores() {
		s_Entries.Clear();

		for (int i = 0; i < EntryCount; ++i) {
			ScoreEntry entry;
			entry.name = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + i + "].name", "");
			entry.score = PlayerPrefs.GetFloat(PlayerPrefsBaseKey + "[" + i + "].score", 0);
			s_Entries.Add(entry);
		}

		SortScores();
	}

	private static void SaveScores() {
		for (int i = 0; i < EntryCount; ++i) {
			var entry = s_Entries [i];
			PlayerPrefs.SetString (PlayerPrefsBaseKey + "[" + i + "].name", entry.name);
			PlayerPrefs.SetFloat (PlayerPrefsBaseKey + "[" + i + "].score", entry.score);
		}
	}
	public static ScoreEntry GetEntry(int index) {
		return Entries[index];
	}

	public static void Record(string name, float score) {
        EntryCount = EntryCount + 1;
        Entries.Add (new ScoreEntry (name, score));
		SortScores();
		SaveScores();

	}

    public static float GetTotalScore()
    {
        float total = 0;
        for (int i = 0; i < EntryCount; ++i)
        {
            total += PlayerPrefs.GetFloat(PlayerPrefsBaseKey + "[" + i + "].score", 0);
        }
        return total;
    }
}
