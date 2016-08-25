using UnityEngine;
using System.Collections; 

public class ArrowCreator : ObjectCreator
{
    public GameObject[] arrows;  

	protected override void createObject () {
        Vector2 vec2fromLeft = GameScreen.getRandomVec3FromLeft();
        Vector2 vec2fromRight = GameScreen.getRandomVec3FromRight();
        Vector2 vec2fromUp = GameScreen.getRandomVec3FromUp();
        Vector2 vec2fromDown = GameScreen.getRandomVec3FromDown();

        if(!GameLogic.isScrolling) createArrow(0, vec2fromDown);
        createArrow(0, vec2fromUp);
        createArrow(0, vec2fromLeft);
        createArrow(0, vec2fromRight);
        if (counter ++ > 10)
        {
            counter = 0;
        } 
    }

    private void createArrow(int i, Vector2 vec)
    {
        GameObject arrow = (GameObject)Instantiate(arrows[i], vec, Quaternion.identity);
        float speed = ((Arrow)arrow.GetComponent<Arrow>()).speed;
        ((Arrow)arrow.GetComponent<Arrow>()).velocity = Vector2.left * speed;
        if (vec.x < GameScreen.minimumXOfScreen)
        {
            ((SpriteRenderer)arrow.GetComponent<SpriteRenderer>()).flipX = !((SpriteRenderer)arrow.GetComponent<SpriteRenderer>()).flipX;
            ((Arrow)arrow.GetComponent<Arrow>()).velocity = Vector2.right * speed;
        }
         if (vec.y > GameScreen.maximumYOfScreen)
        {
            arrow.transform.Rotate(0, 0, 90, 0);
            ((Arrow)arrow.GetComponent<Arrow>()).velocity = Vector2.down * speed;
        }
        if (vec.y < GameScreen.minimumYOfScreen)
        {
            arrow.transform.Rotate(0, 0, -90, 0); 
            ((Arrow)arrow.GetComponent<Arrow>()).velocity = Vector2.up * speed;
        }
    }
}
