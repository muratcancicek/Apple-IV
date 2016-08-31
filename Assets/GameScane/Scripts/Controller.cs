using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    public float speed = 50;
    public float defaultSpeed = 50;
    protected Vector3 direction;
    protected bool isJoystick = true;
    protected Vector3 startPosition;
    protected Rigidbody2D rgdBody2D;
    protected static bool isDragging;
    protected static Controller controller;
    private static Vector2 fixedPosition;
    private Vector2 firstPressPos;
    private float screenDistance;
    private float distance;

    // Use this for initialization
    protected void Start ()
    {
        rgdBody2D = GetComponent<Rigidbody2D>();
        startPosition = gameObject.transform.position;
        if (isJoystick)
        {
            fixedPosition = startPosition;
            controller = this;
        }
    }

    // Update is called once per frame
    protected void Update ()
    {
        screenDistance = GameScreen.screenDistance(fixedPosition, gameObject.transform.position);
        distance = GameScreen.distance(fixedPosition, gameObject.transform.position);
        GameLogic.debugLog(""+isDragging+" "+distance);
        setDrction();
        rgdBody2D.velocity = speed * direction.normalized;
    }

    private void setDrction()
    {
        if (GameScreen.distance(fixedPosition, GameScreen.inputPos) > 16f && !isDragging)
        {
            direction = Vector3.zero;
            isDragging = false;
            return;
        }
        if (GameLogic.onMobile)
            getTouchInput();
        else
            getMouseInput();
        if (distance > 5f)
            isDragging = true;
        if (distance > 16f)
            direction = (Vector3)fixedPosition - gameObject.transform.position;
        else if (screenDistance < 10f && isDragging)
        {
            direction = Vector3.zero;
            isDragging = false;
        }
    }

    private void getTouchInput()
    {
        Touch t = Input.touches[0];
        if (t.phase == TouchPhase.Began || t.phase == TouchPhase.Moved)
        {
            direction = GameScreen.inputPos - fixedPosition;
        }
        else if(!isJoystick)
            direction = Vector2.zero;
    }

    private void getMouseInput()
    {
        Vector2 mousePosition = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            direction = (GameScreen.inputPos - fixedPosition);
        }
        else if (!isJoystick)
            direction = Vector2.zero;
    }


    public void reset()
    {
        gameObject.transform.position = startPosition;
        rgdBody2D.velocity = Vector2.zero;
        direction = Vector2.zero;
        isDragging = false;
    }

    public Vector2 getDirection()
    {
        Vector2 pos = gameObject.transform.position;
        return pos - fixedPosition;
    }
}
