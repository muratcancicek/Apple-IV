using UnityEngine;

public class ArrowCreator : ObjectCreator
{
    public static bool[] activeTypes = new bool[5];
    public GameObject[] arrows;
    private float maxCreationCycle;

    void Start()
    {
        creationCycle = 2;
        maxCreationCycle = creationCycle;
        counter = creationCycle;
    }

    protected override void createObject () {
        Vector2 vec2fromLeft = GameScreen.getRandomVec3FromLeft();
        Vector2 vec2fromRight = GameScreen.getRandomVec3FromRight();
        Vector2 vec2fromUp = GameScreen.getRandomVec3FromUp();
        Vector2 vec2fromDown = GameScreen.getRandomVec3FromDown();

        if(!GameLogic.isScrolling) createArrow(0, vec2fromDown);
        createArrow(0, vec2fromUp);
        createArrow(0, vec2fromLeft);
        createArrow(0, vec2fromRight);

        creationCycle = creationCycle <= 0.3f ? 0.3f : maxCreationCycle - (GameLogic.hardnessRate -1) * maxCreationCycle;
    }

    private void createArrow(int i, Vector2 vec)
    {
        int type = getRandomArrowType();
        GameObject arrow = (GameObject)Instantiate(arrows[type], vec, Quaternion.identity);
        Arrow arrowInstance = arrow.GetComponent<Arrow>();
        float speed = arrowInstance.speed;
        arrowInstance.velocity = Vector2.left * speed;
        arrowInstance.type = type;
         if (vec.y > GameScreen.maximumYOfScreen)
        {
            arrow.transform.Rotate(0, 0, 90, 0);
            arrow.GetComponent<Arrow>().velocity = Vector2.down * speed;
        }
        if (vec.y < GameScreen.minimumYOfScreen)
        {
            arrow.transform.Rotate(0, 0, -90, 0); 
            arrow.GetComponent<Arrow>().velocity = Vector2.up * speed;
        }
        if (vec.x < GameScreen.minimumXOfScreen && arrowInstance.velocity == Vector2.left * speed)
        {
            arrow.GetComponent<SpriteRenderer>().flipX = !arrow.GetComponent<SpriteRenderer>().flipX;
            arrow.GetComponent<Arrow>().velocity = Vector2.right * speed;
        }
    }

    private int getRandomArrowType()
    {
        float chance = Random.Range(0, 100) * GameLogic.chanceRate;
        int type = 0;
        switch (GameLogic.level)
        {
            case 1:
                type = 0; break;
            case 2:
                type = chance > 70 ? 1 : 0; break;
            case 3:
                type = chance > 50 ? (chance > 75 ? 2 : 1) : 0; break;
            default:
                type = chance > 40 ? (chance > 60 ? (chance > 80 ? (chance > 110 ? 4 : 3) : 2) : 1) : 0; break;
        }
        if (type > 1)
        {
            if (!activeTypes[type] && (type < 4 || (type == 4 && GameLogic.health < 50)))
                activeTypes[type] = true;
            else 
                return getRandomArrowType();
        }
        return type;
    }
}
