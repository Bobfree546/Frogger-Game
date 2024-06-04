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
    private const float PLAYER_SPEED = 10f;

    private Vector3 currentPos;
    private Queue<Vector3> movementQueue;
    private bool moving = false;

    public int horizontalCoordinate = 5; //range from 0 to 10 inclusive
    public int verticalCoordinate = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
        movementQueue = new Queue<Vector3>();
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


        if (movementQueue.Count > 0 && !moving)
        {
            StartCoroutine(MoveToNextDestination());
        }
    }

    void Move(string direction)
    {
        Vector3 positionDifference = new Vector3(0, 0, 0);


        if (movementQueue.Count < 1)
        {
            switch (direction)
            {
                case LEFT:
                    if (horizontalCoordinate > 0)
                    {
                        horizontalCoordinate -= 1;
                        positionDifference = new Vector3(-1, 0, 0);
                    }
                    break;
                case RIGHT:
                    if (horizontalCoordinate < 10)
                    {
                        horizontalCoordinate += 1;
                        positionDifference = new Vector3(1, 0, 0);
                    }
                    break;
                case UP:
                    verticalCoordinate += 1;
                    {
                        positionDifference = new Vector3(0, 0, 1);
                    }
                    break;
                case DOWN:
                    verticalCoordinate -= 1;
                    {
                        positionDifference = new Vector3(0, 0, -1);
                    }
                    break;
            }

            movementQueue.Enqueue(positionDifference);
        }
    }

    private IEnumerator MoveToNextDestination()
    {
        float time = 0;

        //Debug.Log("Start move");
        moving = true;
        Vector3 nextPos = currentPos + movementQueue.Dequeue();
        while (time < 1)
        {
            transform.position = Vector3.Lerp(currentPos, nextPos, time);
            time += Time.deltaTime * PLAYER_SPEED;
            yield return null;
        }
        currentPos = nextPos;
        transform.position = currentPos;
        moving = false;
        //Debug.Log("End move");
    }
}
