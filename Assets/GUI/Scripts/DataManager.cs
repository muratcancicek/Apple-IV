using UnityEngine;
using System.Collections;
using System;

public class DataManager : MonoBehaviour
{
    public static ArrayList rankings = new ArrayList(5);
    private static int maxRanking = 5;
    private static FirebaseManager firebaseManager; 
    // Use this for initialization
    void Start () {
        GameGUI view = gameObject.GetComponent<GameGUI>();
        maxRanking = view.rankings.Length;
        firebaseManager = gameObject.GetComponent<FirebaseManager>();
        loadHighScoresromMemory();
    }

    private void loadHighScoresromMemory()
    {
        for (int index = 0; index < maxRanking; index++)
        {
            readPlayerFromMemory(index);
        }

    }

    private void readPlayerFromMemory(int index)
    {
        int playerScore = PlayerPrefs.GetInt("playerScore" + index);
        string playerName = PlayerPrefs.GetString("playerName" + index);
        playerScore = playerScore == default(int) ? 0 : playerScore;
        rankings.Add(new Player(playerScore, playerName != "" ? playerName : "Nobody was here"));
        //rankings.Add(firebaseManager.list[index]);

    }
	
	// Update is called once per frame
	void Update () {
    }

    void OnApplicationQuit()
    {
        rankings.Sort();
        saveRankings();
        PlayerPrefs.Save();
    }

    private void saveRankings()
    {
        for (int index = 0; index < maxRanking; index++)
        {
            savePlayerToMemory(index, getPlayer(index));
        }  
    }

    private static Player getPlayer(int index)
    {
        return (Player)rankings[index];
    }

    private void savePlayerToMemory(int index, Player player) 
    {
        PlayerPrefs.SetInt("playerScore" + index, player.score);
        PlayerPrefs.SetString("playerName" + index, player.name);
    }

    public static void recordPlayer(int score, string playerName)
    {
        if (score > getPlayer(maxRanking-1).score)
        {
            rankings.Add(new Player(score, playerName));
        }
        rankings.Sort();
    }

    public static string[] getPlayerTexts()
    {
        rankings = firebaseManager.getOnlineRankings();
        string[] playerTexts = new string[maxRanking];
        for (int ranking = 0; ranking < maxRanking; ranking++)
        {
            string r = "" + (ranking + 1) + ". ";
            string name = getPlayer(ranking).name;
            string score = " "+getPlayer(ranking).score;
            int spaceLength = 35 - (r.Length + name.Length + score.Length);
            string space = "";
            for (int i = 0; i < spaceLength; i++)
            {
                space += " ";
            }
            playerTexts[ranking] = r + name + space + score;
        }
        return playerTexts;
    }

    public class Player : System.IComparable
    { 
        public int score;
        public string name;

        public Player(int score, string name)
        { 
            this.score = score;
            this.name = name;
        }

        public int CompareTo(object obj)
        {
            Player p = (Player)obj;
            if (this.score < p.score)
                return 1;
            if (this.score > p.score)
                return -1;
            else
                return 0;
        }

        override public string ToString()
        {
            return "(" + name + ", " + score + ")";
        }
    }
}
