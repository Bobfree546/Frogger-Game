using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static Vector3 PLAYER_SPAWN_LOCATION = new Vector3(0, 1, 0);
    private static Vector3 FIRST_ROW_SPAWN_LOCATION = new Vector3(0, 0, 0);
    private const int ROWS_AHEAD = 50; //How many rows ahead to spawn (excluding the one that player is standing on)
    private const int ROWS_BEHIND = 20; //How many rows behind to keep (excluding the one that player is standing on)
    private const float FLIP_CHANCE = 1f; //chance for the next spawn to flip (0 to 1)

    private int rowCountdown = 0;
    private bool spawnSafe = false;
    private int maxRowNum = 0; //row number of furthest row in front
    private int minRowNum = 0; //row number of furthest row behind
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
        if (maxRowNum < (player.GetComponent<Player>().verticalCoordinate + ROWS_AHEAD))
        {
            SpawnRow();
        }

        if (minRowNum < (player.GetComponent<Player>().verticalCoordinate - ROWS_BEHIND))
        {
            RemoveRow();
        }
    }

    private void SpawnLevel()
    {
        player = Instantiate(playerPrefab, PLAYER_SPAWN_LOCATION, Quaternion.identity); //Spawn Player

        GameObject firstRow = Instantiate(withoutEnemyRowPrefab, FIRST_ROW_SPAWN_LOCATION, Quaternion.identity); //Spawn First Row (Row num is 0)
        rowQueue.Enqueue(firstRow);
        rowCountdown = Random.Range(1, 4); //set first few spawns to be dangerous
    }

    private void SpawnRow()
    {
        int guaranteedPath = 0;
        if (spawnSafe)
        {
            guaranteedPath = rowQueue.Last().GetComponent<WithoutEnemyRowManager>().guaranteedPath;
        }

        if (rowCountdown == 0)
        {
            spawnSafe = !spawnSafe;
            if (spawnSafe)
            {
                guaranteedPath = Random.Range(Player.MIN_HORIZONTAL_COORDINATE, Player.MAX_HORIZONTAL_COORDINATE + 1); //Max Exclusive
                rowCountdown = Random.Range(1, 3); //1 to 2
            }
            else
                rowCountdown = Random.Range(1, 4); //1 to 3
        }

        if (spawnSafe)
        {
            SpawnWithoutEnemyRow(guaranteedPath);
        }
            
        else
            SpawnWithEnemyRow();

        rowCountdown--;
        maxRowNum++;
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

    private void SpawnWithoutEnemyRow(int guaranteedPath)
    {
        GameObject previousRow = rowQueue.Last();
        Vector3 newPosition = previousRow.transform.position + WithoutEnemyRowManager.ROW_SHIFT;
        GameObject row = Instantiate(withoutEnemyRowPrefab, newPosition, Quaternion.identity);
        row.GetComponent<WithoutEnemyRowManager>().guaranteedPath = guaranteedPath;
        row.GetComponent<WithoutEnemyRowManager>().MakeGuaranteedPath();
        rowQueue.Enqueue(row);
    }

    private void RemoveRow()
    {
        GameObject row = rowQueue.Dequeue();
        if (row.GetComponent<WithEnemyRowManager>() != null) 
        {
            row.GetComponent<WithEnemyRowManager>().RemoveObjects();
        }

        Destroy(row);

        minRowNum++;
    }
}
