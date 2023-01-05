using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Move_Powerup : MonoBehaviour
{

  [SerializeField]
  private GameObject _player;

  [SerializeField]
  private float _speed = 20.5f;

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.C))
    {

      
      transform.position = UnityEngine.Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);

      transform.right = _player.transform.position - transform.position;
    }


  }

}

