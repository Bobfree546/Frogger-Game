using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WithoutEnemyRowManager : MonoBehaviour
{
    private const float SPAWN_CHANCE = 0.4f;
    public static Vector3 ROW_SHIFT = new Vector3(0, 0, 1);

    public GameObject obstaclePrefab;
    public Dictionary<int, GameObject> obstacles = new Dictionary<int, GameObject>(); //key is the column, value is the object
    public int guaranteedPath = 0; //the column that will be open

    // Start is called before the first frame update
    void Start()
    {
        CreateObstacles();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateObstacles()
    {
        for (int column = -5; column <= 5; column++)
        {
            float spawnRoll = Random.Range(0f, 1f);
            if (spawnRoll < SPAWN_CHANCE)
            {
                Vector3 spawnLocation = transform.position + new Vector3(column, 1, 0);
                GameObject obstacle = Instantiate(obstaclePrefab, spawnLocation, Quaternion.identity);

                obstacle.transform.parent = transform;
                obstacles.Add(column, obstacle);
            }
        }
    }

    public void MakeGuaranteedPath()
    {
        if (obstacles.ContainsKey(guaranteedPath))
        {
            GameObject obstacle = obstacles[guaranteedPath];
            obstacles.Remove(guaranteedPath);
            Destroy(obstacle);
        }
    }
}
