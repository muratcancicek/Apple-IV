using UnityEngine;
using System.Collections;

public abstract class ObjectCreator : MonoBehaviour {
	public float creationCycle = 1f;
	protected float counter;

    protected void FixedUpdate () { 
		counter -= Time.deltaTime; 
		if (counter <= 0) {
			createObject (); 
			counter = creationCycle;
        } 
    }

	protected abstract void createObject(); 
}