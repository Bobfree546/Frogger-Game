using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    //Triggered by objects hitting spawn
    private void OnTriggerEnter(Collider collider)
    {
        MovableEnemy movableEnemy = collider.gameObject.GetComponent<MovableEnemy>();
        if (movableEnemy != null)
        {
            gameObject.GetComponentInParent<RowManager>().SpawnEvent(movableEnemy.nextSpawnGap * new Vector3(0, 0, 1));
        }
    }
}
