using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour
{
    public static bool pause = false;
    public static bool mute = false;
    public static bool gameOver = false;
    public static bool isScrolling = true;
    public static int score = 0;
    public static int health = 100;
    public static int level = 1;
    public static float hardnessRate = 1f; 
    public static Vector2 scrollingVelocity = new Vector2(0,-25);
    private static GameObject logic;
    
    void Start () {
        logic = gameObject;
        logic.GetComponent<AudioSource>().Play();
        gameOver = true;
        Time.timeScale = 0;
        Physics2D.gravity =  Vector2.zero; 
	}
     
    void FixedUpdate () {
        hardnessRate = 1f + (score / 7500f);
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
        gameOver = false;
        score = 1020;
        Time.timeScale = 1; 
    }

    public static void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0;
    }

    public static void scored(int point)
    {
        score += point;
        if (score >= 2000) { level = 5; }
        else if (score >= 1000) { level = 4; }
        else if (score >= 600) { level = 3; }
        else if (score >= 200) { level = 2; }
    }

    public static void recordHighScore(string playerName)
    {
        health = 100;
        DataManager.recordPlayer(score, playerName); 
    }
    
    internal static void decreaseHealth(int damage)
    {
        if(health > 0)
            health -= damage;
        if (health <= 0)
            GameOver();
    }
}
