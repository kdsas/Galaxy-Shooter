using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Detect_Player : MonoBehaviour
{

  [SerializeField]
  private GameObject _laserPrefab;

  [SerializeField]
  private Player _currentPlayer;

  [SerializeField]
  private float _fireRate = 3.0f;

  [SerializeField]
  private float _canFire = -1;

  // Update is called once per frame
  void Update()
  {


    FindClosePlayer();


  }

  void FindClosePlayer()
  {
    float distanceToClosestPlayer = Mathf.Infinity;
    Player closestPlayer = null;
    Player[] allPlayers = GameObject.FindObjectsOfType<Player>();

    foreach (Player currentPlayer in allPlayers)
    {
      float distanceToPlayer = (_currentPlayer.transform.position - this.transform.position).sqrMagnitude;
      if (distanceToPlayer < distanceToClosestPlayer)
      {
        distanceToClosestPlayer = distanceToPlayer;
        closestPlayer = _currentPlayer;
      }
    }



    if (Time.time > _canFire)
    {
      _fireRate = UnityEngine.Random.Range(2f, 4f);
      _canFire = Time.time + _fireRate;
      GameObject enemyLaser = Instantiate(_laserPrefab, transform.right, UnityEngine.Quaternion.identity);


    }
  }
}
