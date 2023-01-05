using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Boss : MonoBehaviour
{

  [SerializeField]
  private float _speed = 2.0f;

  private Player _player;


  // Start is called before the first frame update
  void Start()
  {
    _player = GameObject.Find("Player").GetComponent<Player>();

  }

  // Update is called once per frame
  void Update()
  {
    CalculateMovement();
  }



  void CalculateMovement()
  {




    transform.Translate(UnityEngine.Vector3.left * _speed * Time.deltaTime);


    if (transform.position.x > 10.25f)
    {

      transform.position = new UnityEngine.Vector3(-10.25f, transform.position.y, 0);
    }
    else if (transform.position.x < -10.25f)
    {
      transform.position = new UnityEngine.Vector3(10.25f, transform.position.y, 0);
    }


  }

  private void OnTriggerEnter2D(Collider2D other)
  {


    

    if (other.tag == "Laser" && _player.fromPlayer == true&& _player.shoot >=6)
    {
   

      Destroy(other.gameObject);

      Destroy(GetComponent<Collider2D>());

      Destroy(gameObject, 1.8f);
      
      


    }

  }
}
