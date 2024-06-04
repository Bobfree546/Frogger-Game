using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        MovableEnemy movableEnemy = collider.gameObject.GetComponent<MovableEnemy>();
        if (movableEnemy != null)
        {
            gameObject.GetComponentInParent<WithEnemyRowManager>().DespawnEvent(collider.gameObject);
        }
    }
}
