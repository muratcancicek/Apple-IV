using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    public float speed = 50;
    public float defaultSpeed = 50;
    protected bool isJoystick = true;
    protected Vector3 startPosition;
    private Rigidbody2D rgdBody2D;
    private Vector3 direction;
    private Vector2 firstPressPos;
    private bool isDragging;
    private float rocketSpeed;

    // Use this for initialization
    protected void Start ()
    {
        rocketSpeed = speed * 3;
        rgdBody2D = GetComponent<Rigidbody2D>();
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    protected void FixedUpdate ()
    {
        setDrction();checkBoundaries();
        move();
    }

    private void setDrction()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
            getTouchInput();
        else
            getMouseInput();
        if (isJoystick)
        {
            if (Vector3.Distance(startPosition, gameObject.transform.position) < 1f && !isDragging)
                direction = Vector3.zero;
            else if (Vector3.Distance(startPosition, gameObject.transform.position) > 16f || !isDragging)
                direction = startPosition - gameObject.transform.position;
            
        }
    }

    private void getTouchInput()
    {
        Touch t = Input.touches[0];
        if (t.phase == TouchPhase.Began)
        {
            firstPressPos = t.position;
            isDragging = true;
        }
        if (t.phase == TouchPhase.Ended)
        {
            isDragging = false;
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
        if (Input.GetMouseButtonUp(0) || GameLogic.gameOver)
        {
            isDragging = false;
        }
        if (isDragging)
        {
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            direction = mousePos - firstPressPos;
        }
        else
            direction = Vector2.zero;
    }

    private void checkBoundaries()
    {
        if (isJoystick)
        {
                return;
            //if (Vector3.Distance(startPosition, (gameObject.transform.position + 14 * direction)) > 20)
            //{
            //    direction = Vector3.zero;
            //}
        }
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

    private void move()
    {
        if (GameLogic.pause || GameLogic.gameOver)
        {
            rgdBody2D.velocity = Vector2.zero;
            direction = Vector2.zero;
            isDragging = false;
        }
        else if (isJoystick)
        {
            rgdBody2D.velocity = speed * direction.normalized;

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
    public void reset()
    {
        gameObject.transform.position = startPosition;
        rgdBody2D.velocity = Vector2.zero;
        direction = Vector2.zero;
        isDragging = false;
    }

}
