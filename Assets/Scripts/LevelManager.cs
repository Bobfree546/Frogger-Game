using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static Vector3 PLAYER_SPAWN_LOCATION = new Vector3(0, 1, 0);
    private static Vector3 FIRST_ROW_SPAWN_LOCATION = new Vector3(0, 0, 0);
    private const int ROWS_IN_ADVANCE = 20;


    public GameObject playerPrefab;
    public GameObject withEnemyRowPrefab;
    public GameObject withoutEnemyRowPrefab;
    public Queue<GameObject> rowQueue;

    // Start is called before the first frame update
    void Start()
    {
        rowQueue = new Queue<GameObject>();
        SpawnLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnLevel()
    {
        Instantiate(playerPrefab, PLAYER_SPAWN_LOCATION, Quaternion.identity); //Spawn Player

        GameObject previousRow = Instantiate(withoutEnemyRowPrefab, FIRST_ROW_SPAWN_LOCATION, Quaternion.identity); //Spawn First Row
        rowQueue.Enqueue(previousRow);

        //Spawn next 19 rows
        for (int i = 0; i < ROWS_IN_ADVANCE; i++)
        {
            previousRow = SpawnRow(previousRow);
            rowQueue.Enqueue(previousRow);
        }


    }

    private GameObject SpawnRow(GameObject previousRow)
    {
        GameObject row = Instantiate(withEnemyRowPrefab, previousRow.transform.position + WithEnemyRowManager.ROW_SHIFT, Quaternion.identity);
        return row;
    }

}
