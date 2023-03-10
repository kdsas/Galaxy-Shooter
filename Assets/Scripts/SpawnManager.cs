using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  [SerializeField]
  private GameObject _enemyPrefab;

  [SerializeField]
  private GameObject _spawnWaveGO;

  [SerializeField]
  private GameObject _bossGO;

  [SerializeField]
  private GameObject[] _powerups;

  [SerializeField]
  private GameObject _enemyContainer;

  private bool _stopSpawning=false;

  public void StartSpawning()
  {

    StartCoroutine(SpawnEnemyRoutine());
    StartCoroutine(SpawnPowerupRoutine());
  
  }

  IEnumerator SpawnEnemyRoutine()
  {
    yield return new WaitForSeconds(3.0f);

    while (_stopSpawning==false)
    {
     
      Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f),7, 0);
      GameObject newEnemy= Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
      newEnemy.transform.parent = _enemyContainer.transform;
      yield return new WaitForSeconds(15.0f);


    }
  }


  IEnumerator SpawnPowerupRoutine()
  {
    yield return new WaitForSeconds(3.0f);

    while (_stopSpawning == false)
    {

      Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
      int randomPowerUp = Random.Range(0, 8);
      Instantiate(_powerups[randomPowerUp], posToSpawn, Quaternion.identity);
      yield return new WaitForSeconds(Random.Range(3,15));


    }

  }



  public void OnPlayerDeath()
  {

    _stopSpawning = true;
    _spawnWaveGO.SetActive(false);
    _bossGO.SetActive(false);
  }
}
