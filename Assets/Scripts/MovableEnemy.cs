using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovableEnemy : MonoBehaviour
{
    public const float SPEED = 2f;

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
        transform.position += Time.deltaTime * SPEED * new Vector3(-1, 0, 0);
    }
}
