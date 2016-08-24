using UnityEngine;
using System.Collections;

public abstract class ObjectCreator : MonoBehaviour {
	public static float creationCycle = 1f;
	protected static float counter = creationCycle; 
 
	protected void FixedUpdate () { 
		counter -= Time.deltaTime; 
		if (counter <= 0) {
			createObject (); 
			counter = creationCycle;
        } 
    }

	protected abstract void createObject(); 
}