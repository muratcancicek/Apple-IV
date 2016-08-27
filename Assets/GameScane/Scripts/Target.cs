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
        switch (gameObject.tag)
        {
            case "Mouth":
                GameLogic.scored(GameLogic.level * 10); break;
            case "Golds":
                GameLogic.scored(GameLogic.level * 50); break;
            case "Rocket":
                GameLogic.scored(GameLogic.level * 25); GameLogic.setState("Rocketing"); break;
            case "Leaf":
                GameLogic.scored(GameLogic.level * 10); GameLogic.setState("Shielding"); break;
            default:
                break;
        }
        Destroy(gameObject);
    }

}
