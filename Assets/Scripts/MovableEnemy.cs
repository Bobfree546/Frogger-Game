using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovableEnemy : MonoBehaviour
{
    public const float SPEED = 2f;
    public const int LEFT_TO_RIGHT = 1;
    public const int RIGHT_TO_LEFT = -1;

    private int moveDirection = RIGHT_TO_LEFT;
    private bool collided = false;
    public int nextSpawnGap = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!collided)
        {
            Move();
        }
    }

    //On contact with player
    private void OnCollisionEnter(Collision collision)
    {
        collided = true;
    }
        
    private void Move()
    {
        if (moveDirection == RIGHT_TO_LEFT)
            transform.position += Time.deltaTime * SPEED * new Vector3(-1, 0, 0);
        else
            transform.position += Time.deltaTime * SPEED * new Vector3(1, 0, 0);
    }

    public void SetDirection(int direction)
    {
        moveDirection = direction;
    }
}
