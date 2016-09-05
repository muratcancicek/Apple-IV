/*
Copyright 2015 Google Inc. All Rights Reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
﻿using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {
    IFirebase firebase;
    IFirebase foo;
  
    // Use this for initialization
    void Start()
    {
        firebase = Firebase.CreateNew("https://apple-iv-dcae4.firebaseio.com/");
        //firebase.SetJsonValue("{\"HighScore\" : 31}");
        //firebase.Child("HighScore").SetValue(666);
        //Debug.Log(firebase.Child("HighScore").ToString());

        //firebase.ChildAdded += (object sender, FirebaseChangedEventArgs e) => {
        //    Debug.Log("Child added!");
        //};

        //firebase.ChildRemoved += (object sender, FirebaseChangedEventArgs e) => {
        //    Debug.Log("Child removed!");
        //};

        firebase.ValueUpdated += (object sender, FirebaseChangedEventArgs args) =>
        {
            Debug.Log("loading");
            Debug.Log(args.DataSnapshot.Child("HighScore").StringValue);
            //answer1Btn.text = args.DataSnapshot.Child("a").StringValue;
            //answer2Btn.text = args.DataSnapshot.Child("b").StringValue;
        };
    }

    // Update is called once per frame
    void Update () {
	
	}
}
