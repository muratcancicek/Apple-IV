using UnityEngine;
using System.Collections;
using SimpleFirebaseUnity;
using System;

public class FirebaseServer : MonoBehaviour {
    public Firebase firebase;
    // Use this for initialization
    void Start () {
        firebase = Firebase.CreateNew("apple-iv-dcae4.firebaseio.com");

        firebase.Child("Messages").Push("{ \"name\": \"simple-firebase-unity\", \"message\": \"awesome!\"}", true);

        firebase.OnDeleteSuccess += (Firebase sender, DataSnapshot snapshot) => {
            Debug.Log("[OK] Delete from " + sender.Endpoint + ": " + snapshot.RawJson);
        };

        firebase.OnUpdateFailed += UpdateFailedHandler;
        // Method signature: void UpdateFailedHandler(Firebase sender, FirebaseError err)

        firebase.GetValue("print=pretty");
    }

    private void UpdateFailedHandler(Firebase sender, FirebaseError error)
    {
    }

    // Update is called once per frame
    void Update () {
	
	}
}
