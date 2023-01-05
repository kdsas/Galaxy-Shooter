using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect_Powerup : MonoBehaviour
{
  [SerializeField]
  private GameObject _laserPrefab;

  [SerializeField]
  private Powerup[] _currentPowerup;

  [SerializeField]
  private float _fireRate = 3.0f;

  [SerializeField]
  private float _canFire = -1;

  // Update is called once per frame
  void Update()
  {


    FindClosePowerup();


  }

  void FindClosePowerup()
  {
    float distanceToClosestPowerup = Mathf.Infinity;
    Powerup closestPowerup = null;
    Powerup[] allPowerups = GameObject.FindObjectsOfType<Powerup>();

    foreach (Powerup currentPowerup in allPowerups)
    {
      int randomPowerUp = UnityEngine.Random.Range(0, _currentPowerup.Length - 1);
      float distanceToPowerup = (_currentPowerup[randomPowerUp].transform.position - this.transform.position).sqrMagnitude;
      if (distanceToPowerup < distanceToClosestPowerup)
      {
        distanceToClosestPowerup = distanceToPowerup;
        closestPowerup = _currentPowerup[randomPowerUp];
      }
    }



    if (Time.time > _canFire)
    {
      _fireRate = UnityEngine.Random.Range(2f, 4f);
      _canFire = Time.time + _fireRate;
      GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, UnityEngine.Quaternion.identity);


    }
  }
}