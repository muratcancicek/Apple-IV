﻿using UnityEngine;
using System.Collections;
using System;

public class GameScreen : MonoBehaviour {
    public Camera mainCamera;
    public static Vector3 screenVector;
	public static float minimumXOfScreen = 53f; 
	public static float maximumXOfScreen = 212f; 
	public static float minimumYOfScreen = 0f; 
	public static float maximumYOfScreen = 124f;
    public static float width = maximumXOfScreen - minimumXOfScreen;
    public static float height = maximumYOfScreen - minimumYOfScreen;
    
    public static float groundZ = 0f; 
	
	public static float creationSpace = 10f;

    public static float minX = minimumXOfScreen - creationSpace;
    public static float maxX = maximumXOfScreen + creationSpace;
    public static float minY = minimumYOfScreen - creationSpace;
    public static float maxY = maximumYOfScreen + creationSpace;

    public static float appleMinX = minimumXOfScreen + creationSpace;
    public static float appleMaxX = maximumXOfScreen - creationSpace;
    public static float appleMinY = minimumYOfScreen + 2 * creationSpace;
    public static float appleMaxY = maximumYOfScreen - 1.5f * creationSpace;

    public static float centralX = width / 2f; 
	public static float centralY = height / 2f;
    public static float inputX;
    public static float inputY;
    public static Vector2 inputPos;
    
    public static int columnNumber = 3;
    public static float columnBound = -1f;
    public static float columnSpace = (width - columnBound) / (columnNumber - 1);
    public static float leftMostColumnX = minimumXOfScreen + columnBound / 2f;
    public static float rightMostColumnX = maximumXOfScreen - columnBound / 2f;

    public static int rowNumber = 4;
    public static float rowBound = 2f;
    public static float rowSpace = (height - rowBound) / (rowNumber - 1);
    public static float lowestRowY = minimumYOfScreen + rowBound / 2f; 
    public static float highestRowY = maximumYOfScreen - rowBound / 2f;

    void Start()
    {

    }

    void FixedUpdate()
    {

        if (GameLogic.onMobile)
        {
            Touch touch = Input.touches[0];
             if (touch.phase == TouchPhase.Ended)
            {
                inputX = centralX;
                inputY = centralY;
            }
            else
            {
                inputX = mainCamera.ScreenToWorldPoint(touch.position).x;
                inputY = mainCamera.ScreenToWorldPoint(touch.position).y;
            }
        }
        else
        {
            inputX = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
            inputY = mainCamera.ScreenToWorldPoint(Input.mousePosition).y;
        }
        inputPos = new Vector2(inputX, inputY);
        screenVector = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -10));
        maximumXOfScreen = screenVector.x;
        maximumYOfScreen = screenVector.y;

    }

    public static float getSolutionWidth()
    {
        return 480;
    }

    public static float getSolutionHeight()
    {
        return 854;
    }

    public static float getWidth () { 
		return maxX - minX;
	}
	
	public static float getHeight () {
		return maxY - minY;
    }

    public static Vector3 getRandomVec3InAppleArea()
    {
        float randomX = getRandomX();
        float randomY = getRandomY();
        while (randomX <= appleMinX || randomX >= appleMaxX)
        {
            randomX = getRandomX();
        }
        while (randomY <= appleMinY || randomY >= appleMaxY)
        {
            randomY = getRandomY();
        }
        return new Vector3(randomX, randomY, groundZ);
    }
    
    public static Vector3 getRandomVec3FromLeft()
    {
        return new Vector3(minX, getRandomY(), groundZ);
    }

    public static Vector3 getRandomVec3FromRight () {
		return new Vector3 (maxX, getRandomY (), groundZ);
    }

    public static Vector2 getRandomVec3FromUp()
    {
        return new Vector2(getRandomX(), maxY);
    }
    
    public static Vector2 getRandomVec3FromDown()
    {
        return new Vector2(getRandomX(), minY);
    }

    public static float getRandomX () {
		return UnityEngine.Random.Range (minX, maxX);
	}
	
	public static float getRandomY () {
		return UnityEngine.Random.Range (minY, maxY);
    }

    public static Vector2 getPositionOfCharacter()
    {
        return FindObjectOfType<AppleController>().gameObject.transform.position;
    }

    public static Vector2 worldToScreenPoint(MonoBehaviour object1)
    {
        return FindObjectOfType<GameScreen>().mainCamera.WorldToScreenPoint(object1.transform.position);
    }

    public static Vector2 worldToScreenPoint(Vector3 position)
    {
        return FindObjectOfType<GameScreen>().mainCamera.WorldToScreenPoint(position);
    }

    public static float screenDistance(MonoBehaviour object1, MonoBehaviour object2)
    {
        return (worldToScreenPoint(object1) - worldToScreenPoint(object2)).magnitude;
    }

    public static float screenDistance(Vector3 position1, Vector3 position2)
    {
        return (worldToScreenPoint(position1) - worldToScreenPoint(position2)).magnitude;
    }

    public static float distance(Vector3 position1, Vector3 position2)
    {
        return (position1 - position2).magnitude;
    }
}
