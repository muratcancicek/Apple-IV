using UnityEngine;

public class FirebaseManager : MonoBehaviour {

    IFirebase firebase;
    IFirebase highScores;
    public DataManager.Player[] list;
    int length = 5;

    // Use this for initialization
    void Start()
    {
        firebase = Firebase.CreateNew("https://apple-iv-dcae4.firebaseio.com/");
        //firebase.SetJsonValue("{\"HighScore\" : 31}");.SetValue(666)
        highScores = firebase.Child("HighScores");

        //for (int i = 0; i < 5; i++)
        //{
        //    IFirebase child = highScores.Child("" + i);
        //    child.Child("Name").SetValue("Nobody was here");
        //    child.Child("Score").SetValue(0f);
        //}

        highScores.ChildAdded += (object sender, FirebaseChangedEventArgs e) =>
        {
            Debug.Log("Child added!");
        };

        highScores.ChildRemoved += (object sender, FirebaseChangedEventArgs e) =>
        {
            Debug.Log("Child removed!");
        };

        firebase.ValueUpdated += (object sender, FirebaseChangedEventArgs args) =>
        {
            Debug.Log("loading");
            length = int.Parse(args.DataSnapshot.Child("Length").StringValue);
            //Debug.Log(args.DataSnapshot.Child("Length").StringValue);
            Debug.Log(length);
            //list = new DataManager.Player[length];.Child("" + 3)
            //for (int i = 0; i < length; i++)
            //{
            //    Debug.Log("loading");
            //    string name = args.DataSnapshot.Child("HighScores").Child(""+i).Child("Name").StringValue;
            //    int score = int.Parse(args.DataSnapshot.Child("HighScores").Child("" + i).Child("Score").StringValue);
            //    list[i] = new DataManager.Player(score, name);
            //    Debug.Log(name);

            //}
        }; ;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
