using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour { 
    public Text menuButtonText;
    public Text muteButtonText;
    public Text playButtonText;
    public Text scoreText;
    public Text healthText;
    public Text highScoreNameText;
    public Text[] rankings = new Text[5];
    public GameObject healthBer;
    public GameObject background;
    public Canvas MAIN_MENU;
    public Canvas HOWTOPLAY;
    public Canvas HIGHSCORES; 
    public Canvas ABOUT_US;
    public Canvas GAME_OVER;
    public Canvas GAME_INTERFACE;
    private Canvas CURRENT_CANVAS;

    // Use this for initialization
    void Start ()
    {
        setCurrentPanel(MAIN_MENU);
        
    }

    private void showPanel(Canvas canvas)
    {
        canvas.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        playButtonText.text = GameLogic.pause ? "RESUME" : "PLAY";
        background.transform.localScale = new Vector3(GameLogic.pause || GameLogic.gameOver ? 1000 : 0, 1000);
    }

    
    public void showMainMenu()
    { 
        setCurrentPanel(MAIN_MENU);
    }
	
	public void setCurrentPanel(Canvas canvas) {
        if (CURRENT_CANVAS != null) 
             CURRENT_CANVAS.enabled = false;
        CURRENT_CANVAS = canvas;
        showPanel(CURRENT_CANVAS);
	}
    
    public void showGameOver()
    { 
        setCurrentPanel(GAME_OVER);
    }

    internal void showGameInterface()
    {
        setCurrentPanel(GAME_INTERFACE);
    }

    public void showHowToPlay()
    {
        setCurrentPanel(HOWTOPLAY);
    }
	
	public void showHighScores() {
        //showHighScorePlayers();
        setCurrentPanel(HIGHSCORES);
    }

    private void showHighScorePlayers()
    {
        string[] playerTexts = DataManager.getPlayerTexts();
        for (int ranking = 0; ranking < rankings.Length; ranking++)
        {
            playerTexts[ranking] = playerTexts[ranking] == null ? "" : playerTexts[ranking];
            rankings[ranking].text = playerTexts[ranking]; 
        }
        try
        {

        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.Log("of");
            throw;
        }
    } 

    public void showAboutUs() {
        setCurrentPanel(ABOUT_US);
    }
	
}
