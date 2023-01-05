using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
  public List<WaveEnemies> enemies = new List<WaveEnemies>();

  public int currWave;

  private int _waveValue;

  public List<GameObject> enemiesToSpawn = new List<GameObject>();

  public Transform[] spawnLocation;

  public int spawnIndex;

  public int waveDuration;

  private float _waveTimer;

  private float _spawnInterval;

  private float _spawnTimer;

  public List<GameObject> spawnedEnemies = new List<GameObject>();
  // Start is called before the first frame update
  void Start()
  {


    GenerateWave();

  }


  // Update is called once per frame
  void FixedUpdate()
  {
    if (_spawnTimer <= 0)
    {
      //spawn an enemy
      if (enemiesToSpawn.Count > 0)
      {
        GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], spawnLocation[spawnIndex].position, UnityEngine.Quaternion.identity); // spawn first enemy in our list
        enemiesToSpawn.RemoveAt(0); // and remove it
        spawnedEnemies.Add(enemy);
        _spawnTimer = _spawnInterval;

        if (spawnIndex + 1 <= spawnLocation.Length - 1)
        {
          spawnIndex++;
        }
        else
        {
          spawnIndex = 0;
        }
      }
      else
      {
        _waveTimer = 0; // if no enemies remain, end wave
      }
    }
    else
    {
      _spawnTimer -= Time.fixedDeltaTime;
      _waveTimer -= Time.fixedDeltaTime;
    }

    if (_waveTimer <= 0 && spawnedEnemies.Count <= 0)
    {
      currWave++;
      GenerateWave();
    }
  }

  public void GenerateWave()
  {
    _waveValue = currWave * 10;
    GenerateEnemies();

    _spawnInterval = waveDuration / enemiesToSpawn.Count; // gives a fixed time between each enemies
    _waveTimer = waveDuration; // wave duration is read only
  }

  public void GenerateEnemies()
  {
    

    List<GameObject> generatedEnemies = new List<GameObject>();
    while (_waveValue > 0 || generatedEnemies.Count < 50)
    {
      int randEnemyId = UnityEngine.Random.Range(0, enemies.Count);
      int randEnemyCost = enemies[randEnemyId].cost;

      if (_waveValue - randEnemyCost >= 0)
      {
        generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
        _waveValue -= randEnemyCost;
      }
      else if (_waveValue <= 0)
      {
        break;
      }
    }
    enemiesToSpawn.Clear();
    enemiesToSpawn = generatedEnemies;
  }

}




[System.Serializable]
  public class WaveEnemies
  {
    public GameObject enemyPrefab;
    public int cost;
  }




