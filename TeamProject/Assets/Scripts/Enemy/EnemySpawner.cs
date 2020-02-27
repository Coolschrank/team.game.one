using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    public Wave currentWave;
    public int WaveNumber, enemyNumber, waveReach, currentWaveNumber, addWave;
    int lastWave;
    public float speed, speedUP, startGameTime;
    public bool randomReverse;
    


    void Start()
    {
        if(FindObjectOfType<HighScore>().easyMode)
        {
            speed = speed * FindObjectOfType<HighScore>().spawnSlowDown;
        }
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(startGameTime* speed);
        Wave();
    }

    public void Wave()
    {
        currentWaveNumber =(int)UnityEngine.Random.Range(WaveNumber, WaveNumber + waveReach);
        if (currentWaveNumber >= waves.Length)
        {
            WaveNumber = 0;
            speed = speed / speedUP;
            StartCoroutine(StartGame());
            return;
        }
        else if(lastWave==currentWaveNumber)
        {
            Wave();
            return;
        }
        lastWave = currentWaveNumber;
            
        currentWave = waves[currentWaveNumber];
        Debug.Log(WaveNumber + " " +( waveReach+ WaveNumber) + "=" + currentWaveNumber);
        /*GameObject cPath = Instantiate(currentWave.path.gameObject, transform.position, transform.rotation) as GameObject;
        if(currentWave.reverse)
        {
            cPath.GetComponent<Path>().Reverse();
        }
        */
        if (UnityEngine.Random.Range(0, 1) <= 0.5f)
        {
            currentWave.reverse = !currentWave.reverse;
        }
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(currentWave.startDelay* speed);
        SpawnEnemy();
        while (enemyNumber< currentWave.enemies.Length)
        {
            yield return new WaitForSeconds(currentWave.coolDown* speed);
            SpawnEnemy();
        }
        enemyNumber = 0;
        WaveNumber+= addWave;
        yield return new WaitForSeconds(currentWave.endDelay * speed);
        Wave();
    }

    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(currentWave.enemies[enemyNumber], transform.position, transform.rotation) as GameObject;
        enemy.GetComponent<EnemyMovement>().reverse = currentWave.reverse;
        if(currentWave.reverseLoop)
        {
            currentWave.reverse = !currentWave.reverse;
        }
        enemy.GetComponent<EnemyMovement>().path = currentWave.path;
        enemyNumber++;
    }
    
}
