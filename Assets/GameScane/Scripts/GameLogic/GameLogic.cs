using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour
{
    public static bool pause = false;
    public static bool mute = false;
    public static bool gameOver = false;
    public static bool isScrolling = !true;
    public static bool[] activeStates =  new bool[3];
    public static bool isShielding = false;
    public static bool isRocketing = false;
    public static int score = 0;
    public static int health = 100;
    public static int level = 1;
    public static float hardnessRate = 1f;
    public static float maxChanceRate = 1.4f;
    public static float maxSizeRate = 2f;
    public static float chanceRate = 1f;
    public static float sizeRate = 1f;
    public static Vector2 scrollingVelocity = new Vector2(0,-25);
    private static GameObject logic;
    private static float stateCycle = 10;
    private static float[] stateCounters = new float[3];
    private static float shieldingCounter;
    private static float rocketingCounter; 
    private static AppleController apple;

    void Start () {
        logic = gameObject;
        logic.GetComponent<AudioSource>().Play();
        gameOver = true;
        Time.timeScale = 0;
        Physics2D.gravity =  Vector2.zero; 
        apple = FindObjectOfType<AppleController>();
	}
     
    void FixedUpdate () {
        hardnessRate = 1f + (score / 7500f);
        chanceRate = chanceRate < maxChanceRate ? hardnessRate : maxChanceRate;
        sizeRate = sizeRate < maxSizeRate ? hardnessRate : maxSizeRate;
        checkState();
    }

    private void checkState()
    {
        for (int i = 0; i < activeStates.Length; i++)
        {
            if (stateCounters[i] >= 0)
            {
                stateCounters[i] -= Time.deltaTime;
            }
            else
            {
                if (activeStates[1]) apple.shield.gameObject.SetActive(false);
                activeStates[i] = false;
            }
        }
        
        //if (rocketingCounter >= 0)
        //{
        //    rocketingCounter -= Time.deltaTime;
        //}
        //else
        //{
        //    isRocketing = false;
        //}

        //if (shieldingCounter >= 0)
        //{
        //    shieldingCounter -= Time.deltaTime;
        //}
        //else
        //{
        //    isShielding = false;
        //    apple.shield.gameObject.SetActive(false);
        //}
    }

    public static void setPaused(bool paused)
    {
        pause = paused;
        Time.timeScale = pause ? 0 : 1;
    }

    public static void setMuted(bool muted)
    {
        logic.GetComponent<AudioSource>().mute = muted;
        GameLogic.mute = muted;
        //if (!mute)
        //{
        //    logic.GetComponent<AudioSource>().mute = mute;
        //    mute = false;
        //}
        //else
        //{
        //    logic.GetComponent<AudioSource>().Pause();
        //    mute = true;
        //}
    }
     
    public static void startGame()
    {
        resetGame();
        Time.timeScale = 1;
    }

    public static void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0;
        FindObjectOfType<GameGUI>().showGameOver();
    }

    public static void resetGame()
    {
        gameOver = false;
        score = 0;
        health = 10;
        apple.gameObject.transform.position = new Vector3(GameScreen.centralX, GameScreen.centralY, GameScreen.groundZ);
        Arrow[] arrows = FindObjectsOfType<Arrow>();
        foreach (Arrow arrow in arrows)
        {
            Destroy(arrow.gameObject);
        }
        Target[] targets = FindObjectsOfType<Target>();
        foreach (Target target in targets)
        {
            Destroy(target.gameObject);
        }
        for (int i = 0; i < activeStates.Length; i++)
        {
            activeStates[i] = false;
        } 
    }

    public static void scored(int point, bool isRocketing = false, bool isShielding = false)
    {
        score += point;
        updateLevel();
        bool[] states = { isRocketing, isShielding };
        updateState(states);
    }

    public static void updateLevel()
    {
        if (score >= 2000) { level = 5; }
        else if (score >= 1000) { level = 4; }
        else if (score >= 600) { level = 3; }
        else if (score >= 200) { level = 2; }
    }

    private static void updateState(bool[] states)
    {
        bool[] currentStates = {states[0], states[1], activeStates[2] };
        if (!activeStates[1] && currentStates[1])
            apple.shield.gameObject.SetActive(true);
        for (int i = 0; i < currentStates.Length; i++)
        {
            if (currentStates[i])
            {
                activeStates[i] = currentStates[i];
                stateCounters[i] = stateCycle * hardnessRate;
            }
        }
        //if (isRocketing)
        //{
        //    GameLogic.isRocketing = true;
        //    GameLogic.rocketingCounter = 10 * hardnessRate;
        //}
        //if (isShielding)
        //{
        //    if (!GameLogic.isShielding)
        //    {
        //        apple.shield.gameObject.SetActive(true);
        //    }
        //    GameLogic.isShielding = true;
        //    GameLogic.shieldingCounter = 10 * hardnessRate;
        //}
    }

    private void updateShield()
    {
        //shield.transform.localScale = isShielding ? new Vector3(1.459055f, 1.497772f, 1) : Vector3.zero;
        //Destroy(shield.GetComponent<PolygonCollider2D>());
        //shield.AddComponent<PolygonCollider2D>();

    }

    public static void recordHighScore(string playerName)
    {
        health = 100;
        DataManager.recordPlayer(score, playerName); 
    }
    
    public static void decreaseHealth(int damage)
    {
        if(health > 0)
            health -= damage;
        if (health <= 0)
            GameOver();
    }
}
