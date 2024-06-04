using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class WithEnemyRowManager : MonoBehaviour
{
    private List<GameObject> EnemyList;
    private static Vector3 FIRST_ENEMY_SPAWN_LOCATION = new Vector3(7, 1, 0);

    public static Vector3 ROW_SHIFT = new Vector3(0, 0, 1);

    public GameObject movableEnemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        EnemyList = new List<GameObject>();
        Spawn(FIRST_ENEMY_SPAWN_LOCATION);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEvent(int nextSpawnGap)
    {
        Vector3 spawnLocation = FIRST_ENEMY_SPAWN_LOCATION + nextSpawnGap * new Vector3(1, 0, 0) - new Vector3(0.2f, 0, 0);
        Debug.Log(spawnLocation);
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
        EnemyList.Add(spawnedEnemy);
        spawnedEnemy.GetComponent<MovableEnemy>().nextSpawnGap = Random.Range(4 , 10);
    }


}
