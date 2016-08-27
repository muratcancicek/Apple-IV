using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static bool pause = false;
    public static bool mute = false;
    public static bool gameOver = false;
    public static bool isScrolling = !true;
    public static int score = 0;
    public static int health = 100;
    public static int level = 1;
    public static float hardnessRate = 1f;
    public static float maxChanceRate = 1.4f;
    public static float maxSizeRate = 2f;
    public static float chanceRate = 1f;
    public static float sizeRate = 1f;
    public static Vector2 scrollingVelocity = new Vector2(0,-25);
    private static bool[] activeStates =  new bool[3];
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
        score = 1000;
        health = 100;
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

    public static void scored(int point)
    {
        score += point;
        updateLevel();
    }

    public static void updateLevel()
    {
        if (score >= 2000) { level = 5; }
        else if (score >= 1000) { level = 4; }
        else if (score >= 600) { level = 3; }
        else if (score >= 200) { level = 2; }
    }

    public static bool isState(string state)
    {
        switch (state)
        {
            case "Rocketing": return activeStates[0];
            case "Shielding": return activeStates[1];
            case "Poisoned": return activeStates[2];
            case "Crazy": return activeStates[3];
            default:
                return false;
        }
    }

    public static void setState(string state)
    {
        switch (state)
        {
            case "Rocketing": activeStates[0] = true; break;
            case "Shielding":
                if (!activeStates[1])
                    apple.shield.gameObject.SetActive(true);
                activeStates[1] = true; break;
            case "Poisoned": activeStates[2] = true; break;
            case "Crazy": activeStates[3] = true; break;
            default:
                break;
        }
        for (int i = 0; i < activeStates.Length; i++)
            if (activeStates[i])
                stateCounters[i] = stateCycle * hardnessRate;
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
