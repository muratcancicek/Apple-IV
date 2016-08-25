using UnityEngine;
using System.Collections;
using System;

public class AppleController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rgdBody2D;
    public Sprite[] apples; 
    private SpriteRenderer spriteRenderer;
    private Vector2 firstPressPos; 
    private Vector2 direction;
    private bool isDragging;
    private int currentApple;

    // Use this for initialization
    void Start()
    {
        currentApple = 0;
        rgdBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
     
    void FixedUpdate()
    {
        setDrction();
        move();
        updateSprite();
    }

    private void setDrction()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
            getTouchInput();
        else
            getMouseInput(); 
    }
    private void getTouchInput()
    {
        Touch t = Input.touches[0];
        if (t.phase == TouchPhase.Began)
        {
            firstPressPos = t.position;
            isDragging = true;
        }
        else if (t.phase == TouchPhase.Ended)
        {
            isDragging = true;
        }
        if (isDragging)
        { 
            direction = t.position - firstPressPos;
        }
        else
            direction = Vector2.zero;
    }

    private void getMouseInput()
    {
     if (Input.GetMouseButtonDown(0))
        { 
            firstPressPos = Input.mousePosition;
            isDragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {  
            isDragging = false; 
        }
        if (isDragging)
        {
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            direction = mousePos - firstPressPos;
        } else
            direction = Vector2.zero;
    }

    private void checkBoundaries()
    {
        if (rgdBody2D.position.x < GameScreen.appleMinX && direction.x < 0)
        {
            direction.x = 0;
        }
        if(rgdBody2D.position.x > GameScreen.appleMaxX && direction.x > 0)
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

    private void move()
    {
        checkBoundaries(); 
        rgdBody2D.velocity = speed * direction.normalized;
    }

    private void updateSprite()
    {
        int nextApple = (100 - GameLogic.health) / 20;
        if (currentApple != nextApple)
        {
            currentApple = nextApple;
            spriteRenderer.sprite = apples[currentApple];
            Destroy(gameObject.GetComponent<PolygonCollider2D>());
            gameObject.AddComponent<PolygonCollider2D>();
        }
    }
}