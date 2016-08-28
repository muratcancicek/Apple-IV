using UnityEngine;
using System.Collections;
using System;

public class TargetCreator : ObjectCreator
{
    public GameObject[] targets;
    
    void Start()
    {
        creationCycle = 0;
        counter = creationCycle;
    }
    protected override void createObject()
    {
        Vector2 vec2 = GameScreen.getRandomVec3InAppleArea();
        while ((GameScreen.getPositionOfCharacter() - vec2).magnitude < 20)
        {
            vec2 = GameScreen.getRandomVec3InAppleArea();
        }
        if (FindObjectsOfType<Target>().Length == 0)
        {
            createTarget(vec2);
        }
    }

    private void createTarget(Vector2 vec)
    {
        int i = 0;
        float chance = UnityEngine.Random.Range(0, 100) * GameLogic.chanceRate;
        switch (GameLogic.level)
        {
            case 1:
                i = 0; break;
            case 2:
                i = chance > 90 ? 1 : 0; break;
            case 3:
                i = chance > 70 ? (chance > 90 ? 2 : 1) : 0; break;
            default:
                i = chance > 50 ? (chance > 75 ? (chance > 90 * GameLogic.chanceRate ? 3 : 2) : 1) : 0; break;
        }
        Instantiate(targets[i], vec, Quaternion.identity);
    }
}
