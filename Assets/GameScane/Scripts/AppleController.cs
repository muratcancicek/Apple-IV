using UnityEngine;
using System.Collections;
using System;

public class AppleController : Controller
{
    public Sprite[] apples;
    private int currentApple;
    public GameObject shield;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        isJoystick = false;
        startPosition = new Vector3(GameScreen.centralX, GameScreen.centralY, GameScreen.groundZ);
        currentApple = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        updateSprite();
    }

    public void resetApple()
    {
        reset();
        FindObjectOfType<Controller>().reset();
        updateSprite();
    }

    private void updateSprite()
    {
        int nextApple = GameLogic.isState("Crazy") ? (apples.Length-1) : (100 - GameLogic.health) / 20;
        if (currentApple != nextApple)
        {
            currentApple = nextApple;
            spriteRenderer.sprite = apples[currentApple];
            Destroy(gameObject.GetComponent<PolygonCollider2D>());
            gameObject.AddComponent<PolygonCollider2D>();
        }
    }

}