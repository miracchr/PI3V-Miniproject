using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Variables for spawn points, enemy prefab, and spawn timer
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public float spawnTimer = 5f;
    
    // List to store spawned enemies
    private List<GameObject> enemies = new List<GameObject>(); 

    private void Start()
    {
        // Spawn the first enemy when the game starts
        SpawnEnemies(); 
    }

    private void SpawnEnemies()
    {
        // Only spawn a new enemy if the amount of enemies is less than 15
        if (enemies.Count < 15) 
        {
            // Randomly select a spawn point from the spawnPoints list
            int randomNumber = Random.Range(0, spawnPoints.Length - 1); 
            
            // Instantiate a new enemy at the selected spawn point
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[randomNumber].position, Quaternion.identity);
           
            // Add the spawned enemy to the list
            enemies.Add(newEnemy); 
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        // Remove the enemy from the list when it is killed
        enemies.Remove(enemy); 
    }

    void Update()
    {
        // A countdown for spawning enemies
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            // Run the SpawnEnemies method each time timer reaches zero
            SpawnEnemies(); 
            
            // Reset the spawn timer 
            spawnTimer = 5f; 
        }
    }
}
