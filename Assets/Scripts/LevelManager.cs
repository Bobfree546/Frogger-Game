using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static Vector3 PLAYER_SPAWN_LOCATION = new Vector3(0, 1, 0);
    private static Vector3 FIRST_ROW_SPAWN_LOCATION = new Vector3(0, 0, 0);
    private const int ROWS_IN_ADVANCE = 50;
    private const float FLIP_CHANCE = 1f; //chance for the next spawn to flip (0 to 1)
    private const float NO_ENEMY_ROW_CHANCE = 0.45f;

    private int maxRowNum = 0;
    private int rowDirection = WithEnemyRowManager.SPAWN_FROM_RIGHT_MOVE_TO_LEFT;
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
            float noEnemyResult = Random.Range(0f, 1f);
            
            if (noEnemyResult <= NO_ENEMY_ROW_CHANCE)
            {
                SpawnWithoutEnemyRow();
            }
            else
            {
                SpawnWithEnemyRow();
            }
            maxRowNum++;
        }
    }

    private void SpawnLevel()
    {
        player = Instantiate(playerPrefab, PLAYER_SPAWN_LOCATION, Quaternion.identity); //Spawn Player

        GameObject firstRow = Instantiate(withoutEnemyRowPrefab, FIRST_ROW_SPAWN_LOCATION, Quaternion.identity); //Spawn First Row
        rowQueue.Enqueue(firstRow);
    }

    private void SpawnWithEnemyRow()
    {
        float flipResult = Random.Range(0f, 1f);

        if (flipResult <= FLIP_CHANCE)
        {
            rowDirection *= -1;
        }

        GameObject previousRow = rowQueue.Last();
        Vector3 newPosition = previousRow.transform.position + WithEnemyRowManager.ROW_SHIFT;
        if (rowDirection == WithEnemyRowManager.SPAWN_FROM_RIGHT_MOVE_TO_LEFT)
        {
            GameObject row = Instantiate(withEnemyRowPrefab, newPosition, Quaternion.identity);
            row.GetComponent<WithEnemyRowManager>().SetDirection(WithEnemyRowManager.SPAWN_FROM_RIGHT_MOVE_TO_LEFT);
            rowQueue.Enqueue(row);
        }
        else
        {
            GameObject row = Instantiate(withEnemyRowPrefab, newPosition, Quaternion.Euler(new Vector3 (0, 180, 0)));
            row.GetComponent<WithEnemyRowManager>().SetDirection(WithEnemyRowManager.SPAWN_FROM_LEFT_MOVE_TO_RIGHT);
            rowQueue.Enqueue(row);
        }
    }

    private void SpawnWithoutEnemyRow()
    {
        GameObject previousRow = rowQueue.Last();
        Vector3 newPosition = previousRow.transform.position + WithoutEnemyRowManager.ROW_SHIFT;
        GameObject row = Instantiate(withoutEnemyRowPrefab, newPosition, Quaternion.identity);
        rowQueue.Enqueue(row);
    }
}
