using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public int numEnemies = 20;
    public GameObject enemyPrefab;
    public GameObject championPrefab;
    public float groundLevel = 0;
    public float mapSize;
    public float safeRadius = 20;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject championTextCanvas;

    void Start()
    {
        InstantiateChampion();
        InstantiateEnemies();
    }

    public void ResetGame()
    {
        //delete champion
        DestroyAll("Champion");
        
        //delete enemies
        DestroyAll("Enemy");

        //respawn champion
        InstantiateChampion();

        //respawn enemies
        InstantiateEnemies();

        //reset player position
        player.transform.position = new Vector3(0, 0, 0);

        //hide win and loss screens
        loseScreen.SetActive(false);
        winScreen.SetActive(false);

    }

    private void InstantiateChampion()
    {
        Vector3 championPos = RandomPosition();
        Instantiate(championPrefab, championPos, Quaternion.identity);
    }

    private void InstantiateEnemies()
    {
        List<Vector3> enemyPositions = EnemyPositions();
        for (int i = 0; i < enemyPositions.Count; i++)
        {
            Vector3 enemyPos = enemyPositions.ElementAt(i);
            //https://forum.unity.com/threads/failed-to-create-agent-because-it-is-not-close-enough-to-the-navmesh.125593/
            UnityEngine.AI.NavMeshAgent enemy = Instantiate(enemyPrefab, enemyPos, Quaternion.identity).GetComponent<UnityEngine.AI.NavMeshAgent>();
            enemy.enabled = false;
            enemy.enabled = true;
        }
    }

    public void LoseGame()
    {
        UnityEngine.Debug.Log("You lose.");
        loseScreen.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    public void WinGame()
    {
        UnityEngine.Debug.Log("You win.");
        winScreen.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    public void seeChampionText()
    {
        championTextCanvas.SetActive(true);
    }

    public void removeChampionText()
    {
        championTextCanvas.SetActive(false);
    }

    private float mapRadius()
    {
        return mapSize / 2;
    }

    private Vector3 RandomPosition()
    {
        float xPos;
        float zPos;

        if (Utility.RandomBool())
        {
            xPos = UnityEngine.Random.Range(-mapRadius(), -safeRadius);
        }
        else
        {
            xPos = UnityEngine.Random.Range(safeRadius, mapRadius());
        }

        if (Utility.RandomBool())
        {
            zPos = UnityEngine.Random.Range(-mapRadius(), -safeRadius);
        }
        else
        {
            zPos = UnityEngine.Random.Range(safeRadius, mapRadius());
        }

        Vector3 newPoint = new Vector3(xPos, groundLevel, zPos);
        return newPoint;
    }

    private List<Vector3> EnemyPositions()
    {
        List<Vector3> points = new List<Vector3>();

        //Top Left
        for (int i = 0; i < numEnemies / 4; i++)
        {
            float xPos = UnityEngine.Random.Range(-mapRadius(), -safeRadius);
            float zPos = UnityEngine.Random.Range(safeRadius, mapRadius());
            Vector3 newPoint = new Vector3(xPos, groundLevel, zPos);
            points.Add(newPoint);
        }

        //Top Right
        for (int i = 0; i < numEnemies / 4; i++)
        {
            float xPos = UnityEngine.Random.Range(safeRadius, mapRadius());
            float zPos = UnityEngine.Random.Range(safeRadius, mapRadius());
            Vector3 newPoint = new Vector3(xPos, groundLevel, zPos);
            points.Add(newPoint);
        }

        //Bottom Right
        for (int i = 0; i < numEnemies / 4; i++)
        {
            float xPos = UnityEngine.Random.Range(safeRadius, mapRadius());
            float zPos = UnityEngine.Random.Range(-mapRadius(), -safeRadius);
            Vector3 newPoint = new Vector3(xPos, groundLevel, zPos);
            points.Add(newPoint);
        }

        //Bottom Left
        for (int i = 0; i < numEnemies / 4; i++)
        {
            float xPos = UnityEngine.Random.Range(-mapRadius(), -safeRadius);
            float zPos = UnityEngine.Random.Range(-mapRadius(), -safeRadius);
            Vector3 newPoint = new Vector3(xPos, groundLevel, zPos);
            points.Add(newPoint);
        }

        //Make sure exact number of enemies is generated
        while (points.Count < numEnemies)
        {
            points.Add(RandomPosition());
        }

        UnityEngine.Debug.Log("Locations generated: " + points.Count);

        return points;
    }

    //https://answers.unity.com/questions/314503/destroy-all-objects-with-tag-enemy.html
    void DestroyAll(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
