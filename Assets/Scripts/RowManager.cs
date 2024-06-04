using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class RowManager : MonoBehaviour
{
    private List<GameObject> EnemyList;
    private static Vector3 FIRST_SPAWN_LOCATION = new Vector3(0, 1, 7);

    public GameObject movableEnemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        EnemyList = new List<GameObject>();
        Spawn(FIRST_SPAWN_LOCATION);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEvent(Vector3 nextSpawnGap)
    {
        Vector3 spawnLocation = transform.position + FIRST_SPAWN_LOCATION + nextSpawnGap;
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
        GameObject spawnedEnemy = Instantiate(movableEnemyPrefab, spawnLocation, Quaternion.identity);
        EnemyList.Add(spawnedEnemy);
        spawnedEnemy.GetComponent<MovableEnemy>().nextSpawnGap = Random.Range(2, 10);
    }


}
