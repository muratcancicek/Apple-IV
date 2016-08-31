using UnityEngine;
using System.Collections;
using System;

public class AppleController : Controller
{
    public Sprite[] apples;
    private int currentApple;
    public GameObject shield;
    private float rocketSpeed;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        rocketSpeed = speed * 3;
        isJoystick = false;
        startPosition = new Vector3(GameScreen.centralX, GameScreen.centralY, GameScreen.groundZ);
        currentApple = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    new void Update()
    {
        direction = isDragging ? controller.getDirection() : Vector2.zero;
        checkAppleBoundaries();
        move();
        updateSprite();
    }
    private void checkAppleBoundaries()
    {
        if (rgdBody2D.position.x < GameScreen.appleMinX && direction.x < 0)
        {
            direction.x = 0;
        }
        if (rgdBody2D.position.x > GameScreen.appleMaxX && direction.x > 0)
        {
            direction.x = 0;
        }
        if (rgdBody2D.position.y < GameScreen.appleMinY && direction.y < 0)
        {
            direction.y = 0;
        }
        if (rgdBody2D.position.y > GameScreen.appleMaxY && direction.y > 0)
        {
            direction.y = 0;
        }
    }

    protected void move()
    {
        if (GameLogic.pause || GameLogic.gameOver)
        {
            rgdBody2D.velocity = Vector2.zero;
            direction = Vector2.zero;
        }
        else if (GameLogic.isState("Poisoned"))
        {
            rgdBody2D.velocity = speed / 2 * direction.normalized;

        }
        else if (GameLogic.isState("Rocketing"))
        {
            rgdBody2D.velocity = rocketSpeed * direction.normalized;
        }
        else
        {
            rgdBody2D.velocity = speed * direction.normalized;
        }
    }

    public void resetApple()
    {
        reset();
        controller.reset();
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