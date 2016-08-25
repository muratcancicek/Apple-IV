using UnityEngine;
using System.Collections;
using System;

public class Arrow : MonoBehaviour { 
	public float speed = 30f;
	public Vector2 velocity;
    private Rigidbody2D rgdBody2D;

	void Start () {
        setSize();
        rgdBody2D = GetComponent<Rigidbody2D>();
        setVelocity();
	}

    private void setSize()
    {
        gameObject.transform.localScale = gameObject.transform.localScale * GameLogic.hardnessRate;
    }

    private void setVelocity()
    { 
        // speed = 3;
  //      if (getPosition().x < GameScreen.centralX)
  //          velocity = Vector2.right * (speed); 
		//else if (getPosition().x > GameScreen.centralX)
  //          velocity = Vector2.left * (speed);
  //      if (getPosition().y < GameScreen.centralY)
  //          velocity = Vector2.up * (speed);
  //      else if (getPosition().y > GameScreen.centralY)
  //          velocity = Vector2.down * (speed);
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        move();
		if (isObjectDead ()) {
            eraseItself();
        } 
	}
    
    private void move()
    {
        Vector2 v = (GameLogic.isScrolling ? GameLogic.scrollingVelocity : Vector2.zero);
        rgdBody2D.velocity = velocity + v; 
    }

	private bool isObjectDead () {
        return getPosition().x < GameScreen.minX || getPosition().x > GameScreen.maxX ||
            getPosition().y < GameScreen.minY - 1 || getPosition().y > GameScreen.maxY;
	}
	private Vector2 getPosition () {
		return gameObject.transform.position;
	}
    
    private void eraseItself()
    {
        Destroy(gameObject);
    }
    
    
    public void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.tag == "Apple")
        {
            GameLogic.decreaseHealth(10);
            Destroy(gameObject);
           //GameLogic.GameOver();
        }
    }
        
}
