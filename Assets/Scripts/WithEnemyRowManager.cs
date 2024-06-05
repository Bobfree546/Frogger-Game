using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.VFX;

public class WithEnemyRowManager : MonoBehaviour
{
    private List<GameObject> EnemyList;
    private static Vector3 FIRST_ENEMY_SPAWN_LOCATION = new Vector3(7, 1, 0);
    public const int SPAWN_FROM_RIGHT_MOVE_TO_LEFT = 1;
    public const int SPAWN_FROM_LEFT_MOVE_TO_RIGHT = -1;

    private int spawnDirection = SPAWN_FROM_RIGHT_MOVE_TO_LEFT; //default direction is R to L
    public static Vector3 ROW_SHIFT = new Vector3(0, 0, 1);
    public GameObject movableEnemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        EnemyList = new List<GameObject>();
        if (spawnDirection == SPAWN_FROM_RIGHT_MOVE_TO_LEFT)
            Spawn(FIRST_ENEMY_SPAWN_LOCATION);
        else
            Spawn(Quaternion.Euler(0, 180, 0) * FIRST_ENEMY_SPAWN_LOCATION);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEvent(int nextSpawnGap)
    {
        Vector3 spawnLocation = FIRST_ENEMY_SPAWN_LOCATION + nextSpawnGap * new Vector3(1, 0, 0) - new Vector3(0.2f, 0, 0);

        if (spawnDirection == SPAWN_FROM_LEFT_MOVE_TO_RIGHT)
            spawnLocation = Quaternion.Euler(0, 180, 0) * spawnLocation;

        Spawn(spawnLocation);
    }

    public void DespawnEvent(GameObject enemyToDespawn)
    {
        EnemyList.Remove(enemyToDespawn);
        Destroy(enemyToDespawn);
    }

    void Spawn(Vector3 spawnLocation)
    {
        spawnLocation += transform.position; //set spawn relative to row

        GameObject spawnedEnemy = Instantiate(movableEnemyPrefab, spawnLocation, Quaternion.identity);

        if (spawnDirection == SPAWN_FROM_LEFT_MOVE_TO_RIGHT)
            spawnedEnemy.GetComponent<MovableEnemy>().SetDirection(MovableEnemy.LEFT_TO_RIGHT);
        else
            spawnedEnemy.GetComponent<MovableEnemy>().SetDirection(MovableEnemy.RIGHT_TO_LEFT);

        EnemyList.Add(spawnedEnemy);
        spawnedEnemy.GetComponent<MovableEnemy>().nextSpawnGap = Random.Range(4 , 10);
    }

    //For setting direction of the row
    public void SetDirection(int direction)
    {
        spawnDirection = direction;
    }

    public void RemoveObjects()
    {
        foreach (GameObject enemy in EnemyList)
        {
            Destroy(enemy);
        }
    }
}
