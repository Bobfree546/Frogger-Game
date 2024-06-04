using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string LEFT = "Left";
    private const string RIGHT = "Right";
    private const string UP = "Up";
    private const string DOWN = "Down";

    public int horizontalCoordinate = 5; //range from 0 to 10 inclusive
    public int verticalCoordinate = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Player Movement
        if (Input.GetButtonDown(LEFT))
        {
            Debug.Log(LEFT);
            Move(LEFT);
        }
        if (Input.GetButtonDown(RIGHT))
        {
            Debug.Log(RIGHT);
            Move(RIGHT);
        }
        if (Input.GetButtonDown(UP))
        {
            Debug.Log(UP);
            Move(UP);
        }
        if (Input.GetButtonDown(DOWN))
        {
            Debug.Log(DOWN);
            Move(DOWN);
        }

    }

    void Move(string direction)
    {
        switch (direction)
        {
            case LEFT:
                if (horizontalCoordinate > 0) horizontalCoordinate -= 1;
                break;
            case RIGHT:
                if (horizontalCoordinate < 10) horizontalCoordinate += 1;
                break;
            case UP:
                verticalCoordinate += 1;
                break;
            case DOWN:
                verticalCoordinate -= 1;
                break;
        }
    }
}
