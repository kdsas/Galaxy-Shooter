using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Detect_Shot : MonoBehaviour
{
  private Player _player;

  [SerializeField]
  private Laser _closeLaser;


  private void Start()
  {

    _player = GameObject.Find("Player").GetComponent<Player>();


  }

    // Update is called once per frame
    void Update()
  {


    FindCloseLaser();


  }

  void FindCloseLaser()
  {
    float distanceToClosestLaser = Mathf.Infinity;
    Laser closestLaser = null;
    Laser[] allLasers = GameObject.FindObjectsOfType<Laser>();

    foreach (Laser currentLaser in allLasers)
    {
      float distanceToLaser = (_closeLaser.transform.position - this.transform.position).sqrMagnitude;
      if (distanceToLaser < distanceToClosestLaser)
      {
        distanceToClosestLaser = distanceToLaser;
        closestLaser = _closeLaser;
      }
    }



    if (_closeLaser.tag == "Laser" && _player.fromPlayer == true)
    {
      transform.Translate((transform.right * 10 * Time.deltaTime));


    }
  }
}
