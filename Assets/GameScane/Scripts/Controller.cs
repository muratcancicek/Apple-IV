using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    public float speed = 50;
    public float defaultSpeed = 50;
    protected Vector3 direction;
    protected bool isJoystick = true;
    protected Vector3 startPosition;
    protected static bool isDragging;
    protected static Controller controller;
    private static Vector2 fixedPosition;
    private static float radius;
    private bool isInputIn;
    private float step;
    private Vector3 nextPosition;

    // Use this for initialization
    protected void Start ()
    {
        radius = 16f;
        startPosition = gameObject.transform.position;
        if (isJoystick)
        {
            fixedPosition = startPosition;
            controller = this;
        }
    }

    // Update is called once per frame " " + fixedPosition"" + isDragging + " " + isInputIn + ((Vector2)nextPosition - fixedPosition)isDragging + " " + isInputIn + " " + (fixedPosition - (Vector2)gameObject.transform.position)
    protected void Update ()
    {
        //GameLogic.debugLog("" + isDragging + " " + isInputIn);
        isInputIn = isDragging ? true : GameScreen.distance(fixedPosition, GameScreen.inputPos) < radius;
    //GameLogic.debugLog(" " + nextPosition + " " + Time.deltaTime);
        
        setDrction();
        moveController();
        step = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nextPosition, step); 
    }

    private void setDrction()
    {
        if (GameLogic.onMobile)
            getTouchInput();
        else
            getMouseInput();
    }

    private void getTouchInput()
    {
        Touch t = Input.touches[0];
        if ((t.phase == TouchPhase.Began || t.phase == TouchPhase.Moved) && isInputIn)
        {
            isDragging = true;
            direction = GameScreen.inputPos - fixedPosition;
        }
        else if (t.phase == TouchPhase.Ended)
        {
            isDragging = false;
            direction = Vector2.zero;
        }
    }

    private void getMouseInput()
    {
        if (Input.GetMouseButton(0) && isInputIn)
        {
            direction = (GameScreen.inputPos - fixedPosition);
            isDragging = true;
        }
        else
            isDragging = false;
    }

    private void moveController()
    {
        step = speed * Time.deltaTime;
        setNextPosition(); //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, isDragging ? nextPosition : new Vector3(fixedPosition.x,fixedPosition.y,0f), step);
    }

    private void setNextPosition()
    {
        if (gameObject.transform.position != nextPosition && (Vector2)nextPosition == fixedPosition)
        {
            return;
        }
        if (!isDragging)
        {
            nextPosition = fixedPosition;
            while (gameObject.transform.position != nextPosition && GameLogic.onMobile)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nextPosition, step);
            }
        }
        else if (direction.magnitude < radius)
            nextPosition = GameScreen.inputPos;
        else
        {
            float rate = direction.magnitude / radius;
            nextPosition = new Vector3(fixedPosition.x + direction.x / rate, fixedPosition.y + direction.y / rate); 
        }
    }


    public void reset()
    {
        gameObject.transform.position = startPosition;
        direction = Vector2.zero;
        isDragging = false;
    }

    public Vector2 getDirection()
    {
        return ((Vector2)nextPosition - fixedPosition) / radius;
    }
}
