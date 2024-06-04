using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static Vector3 PLAYER_SPAWN_LOCATION = new Vector3(0, 1, 0);
    private static Vector3 FIRST_ROW_SPAWN_LOCATION = new Vector3(0, 0, 0);
    private const int ROWS_IN_ADVANCE = 50;

    private int maxRowNum = 0;
    private GameObject player;

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
        if (maxRowNum < (player.GetComponent<Player>().verticalCoordinate + ROWS_IN_ADVANCE))
        {
            SpawnRow();
            maxRowNum++;
        }
    }

    private void SpawnLevel()
    {
        player = Instantiate(playerPrefab, PLAYER_SPAWN_LOCATION, Quaternion.identity); //Spawn Player

        GameObject firstRow = Instantiate(withoutEnemyRowPrefab, FIRST_ROW_SPAWN_LOCATION, Quaternion.identity); //Spawn First Row
        rowQueue.Enqueue(firstRow);

        //Spawn next 19 rows
        for (int i = 0; i < ROWS_IN_ADVANCE; i++)
        {
            SpawnRow();
        }

        maxRowNum += (ROWS_IN_ADVANCE);
    }

    private void SpawnRow()
    {
        GameObject previousRow = rowQueue.Last();
        GameObject row = Instantiate(withEnemyRowPrefab, previousRow.transform.position + WithEnemyRowManager.ROW_SHIFT, Quaternion.identity);
        rowQueue.Enqueue(row);
    }

}
