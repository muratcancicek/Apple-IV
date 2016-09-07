using System.Collections;
using UnityEngine;

public class FirebaseManager : MonoBehaviour {

    private IFirebase firebase;
    public static ArrayList rankings = new ArrayList(5);
    private int length = 5;
    private int playerNameLength = 15;

    // Use this for initialization
    void Start()
    {
        firebase = Firebase.CreateNew("https://apple-iv-dcae4.firebaseio.com/");

        //firebase.ChildAdded += (object sender, FirebaseChangedEventArgs e) =>
        //{
        //    Debug.Log("Child added!");
        //};

        //firebase.ChildRemoved += (object sender, FirebaseChangedEventArgs e) =>
        //{
        //    Debug.Log("Child removed!");
        //};

        firebase.Child("Length").ValueUpdated += (object sender, FirebaseChangedEventArgs args) =>
        {
            length = int.Parse(args.DataSnapshot.StringValue);
            Debug.LogError(length);
            //createTable();
        };
            readAllPlayers();
    }

    private void createTable()
    {
        firebase.Child("HighScores").SetValue("");
        for (int i = 0; i < length; i++)
        {
            string value = encodePlayer("" + i + ". Player", 100 - i * 10);
            Debug.LogError(value);
            firebase.Child("HighScores").Child("" + i).SetValue(value);
        }
    }


    private void updateTable()
    {

        rankings.Sort();
        firebase.Child("HighScores").SetValue("");
        for (int i = 0; i < length; i++)
        {
            string value = encodePlayer((DataManager.Player)rankings[i]);
            Debug.LogError(value);
            firebase.Child("HighScores").Child("" + i).SetValue(value);
        }
    }

    private string encodePlayer(DataManager.Player player)
    {
    return encodePlayer(player.name, player.score);
    }

    private string encodePlayer(string name, int score)
    {
        int spaceLength = playerNameLength - name.Length;
        string space = "";
        for (int i = 0; i < spaceLength; i++) { space += " "; }
        return name.Substring(0, name.Length > playerNameLength ? playerNameLength : name.Length) + space + score;
    }


    private DataManager.Player decodePlayer(string value)
    {
        string name = value.Substring(0, playerNameLength);
        int score = int.Parse(value.Substring(playerNameLength, value.Length - playerNameLength));
        return new DataManager.Player(score, name);
    }

    private void readAllPlayers()
    {
        //rankings = rankings = new ArrayList(length);
        for (int i = 0; i < length; i++)
        {
            readPlayer(i);
        }
    }

    private void readPlayer(int index)
    {
        //firebase.Child("HighScores").Child("3").ValueUpdated += (object sender, FirebaseChangedEventArgs args) =>
        firebase.Child("HighScores").Child("" + index).ValueUpdated += (object sender, FirebaseChangedEventArgs args) =>
        {
            rankings.Add(decodePlayer(args.DataSnapshot.StringValue));
            Debug.LogError(rankings[index]);
            //if (rankings.Count >= length)
            //{
            //    updateTable();
            //}
        };
    }

    public ArrayList getOnlineRankings()
    {
        rankings.Sort();
        return rankings;
    }
    void Update()
    {

    }

}
