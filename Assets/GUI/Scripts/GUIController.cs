﻿using UnityEngine;
using System.Collections;


public class GUIController : MonoBehaviour {
    private GameGUI view;
    private float healthBarScaleX;
    private float healthBarPosX;

    // Use this for initialization
    void Start () {
        view = gameObject.GetComponent<GameGUI>();
        healthBarPosX = view.healthBer.transform.position.x;
        healthBarScaleX = view.healthBer.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameLogic.pause)
        {
            view.menuButtonText.text = GameLogic.pause ? "X" : "MENU";
            //view.muteButonText.text = GameLogic.mute ? "UNMUTE" : "MUTE";
            view.scoreText.text = "" + GameLogic.score;
            view.scoreToSaveText.text = "" + GameLogic.score;
            updateHealth();
        }
    }

    private void updateHealth()
    {
        view.healthText.text = GameLogic.isDebugging ? GameLogic.debugText : ("" + GameLogic.health);
        //Vector3 healthBarScale = new Vector3(view.healthBer.transform.localScale.x, healthBarScaleX * (GameLogic.health / 100f), view.healthBer.transform.localScale.z);
        Vector3 healthBarScale = new Vector3(healthBarScaleX * (GameLogic.health / 100f), view.healthBer.transform.localScale.y, view.healthBer.transform.localScale.z);
        view.healthBer.transform.localScale = healthBarScale;
        //Vector3 healthBarPos = new Vector3(view.healthBer.transform.position.x, healthBarPosX - (healthBarScaleX - healthBarScale.y) * 7.2f, view.healthBer.transform.position.z);
        Vector3 healthBarPos = new Vector3(healthBarPosX - (healthBarScaleX - healthBarScale.x) * 1.2f, view.healthBer.transform.position.y, view.healthBer.transform.position.z);
        view.healthBer.transform.position = healthBarPos; 
    }
    public void playButtonClicked()
    {
        if (GameLogic.gameOver)
        {
            GameLogic.startGame();
        }
        else
            GameLogic.setPaused(false);
        view.showGameInterface();
}

    public void howToPlayButtonClicked()
    {
        view.showHowToPlay();
    }

    public void highScoresButtonClicked()
    {
        view.showHighScores();
    }

    public void aboutUsButtonClicked()
    {
        view.showAboutUs();
    }
     
    public void okButtonClicked()
    {
        view.showMainMenu();
    }

    public void pauseButtonClicked()
    {
        GameLogic.setPaused(!GameLogic.pause);
        Debug.Log(GameLogic.pause);
        view.showMainMenu();
    }

    public void muteButtonClicked()
    {
        GameLogic.setMuted(!GameLogic.mute);
    }
     
    public void highScoreNameEntered() 
    {
        if (view.highScoreNameText.text.Length >= 3)
        {
        GameLogic.recordHighScore(view.highScoreNameText.text);
        view.highScoreNameText.text = "";
        view.showMainMenu();
        }
    }

}
