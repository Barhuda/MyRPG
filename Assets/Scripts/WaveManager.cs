using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    GameObject[] arrayOfSpawnpoints;
    [SerializeField] Wave[] arrayOfWaves;
    int currentWaveIndex = 0;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        arrayOfSpawnpoints = GameObject.FindGameObjectsWithTag("Spawnpoints");
        StartCoroutine(spawnEnemies());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnEnemies()
    {
        Wave currentWave = arrayOfWaves[currentWaveIndex];
        for(int i = 0; i < currentWave.amountOfEnemies; i++)
        {
            GameObject spawningPoint = arrayOfSpawnpoints[Random.Range(0, arrayOfSpawnpoints.Length)];
            Vector3 spawnPosition = new Vector3(spawningPoint.transform.position.x, spawningPoint.transform.position.y, 0);
            Debug.Log("Position von Enemy" + spawningPoint.transform.position);
            Enemy enemy = Instantiate(currentWave.arrayOfEnemies[Random.Range(0, currentWave.arrayOfEnemies.Length)], spawnPosition, Quaternion.identity, parent: spawningPoint.transform) as Enemy;
            player.AddEnemyToTargetList(enemy);
            Debug.Log("Spawning");
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
        

    }

}
