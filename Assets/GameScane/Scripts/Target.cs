using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    private int counter = 0;

	void Start () {
        counter = 100;
    }

    void FixedUpdate() {;
        if (--counter <= 0)
        {
            Destroy(gameObject);
        }
        move();
    }

    private void move()
    {
        Vector2 v = (GameLogic.isScrolling ? GameLogic.scrollingVelocity : Vector2.zero);
        GetComponent<Rigidbody2D>().velocity = v;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Apple")
        {
            GameLogic.scored(10);
            Destroy(gameObject);
        }
    }

}
