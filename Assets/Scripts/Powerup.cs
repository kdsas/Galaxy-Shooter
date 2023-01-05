using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Powerup : MonoBehaviour
{

  [SerializeField]
  private float _speed = 3.0f;

  [SerializeField]// 0=Triple Shot 1= Speed 2= Shields 3=Ammo
  private int powerupID;

  [SerializeField]
  private AudioClip _clip;

  private Player _player;


  private void Start()
  {

    _player = GameObject.Find("Player").GetComponent<Player>();

  }
    // Update is called once per frame
    void Update()
  {
    transform.Translate(Vector3.down * _speed * Time.deltaTime);


    if (transform.position.y < -4.5f)
    {
      Destroy(gameObject);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Laser" && _player.fromPlayer == false)
    {
      if (_player != null)
      {
        Destroy(GetComponent<Collider2D>());
        Destroy(gameObject, 1.8f);
      }
      
    }

    if (other.tag == "Player")
    {

      Player player = other.transform.GetComponent<Player>();


      AudioSource.PlayClipAtPoint(_clip, transform.position);
      if(player != null)
      {
        

        switch (powerupID)
        {

          case 0:
            player.TripleShotActive();
            break;

          case 1:
            player.SpeedBoostActive();
            break;

          case 2:
            player.ShieldsActive();
            break;

          case 3:
            player.AmmoCountActive();
            break;

          case 4:
            player.HealthPowerupActive();
            break;

          case 5:
            player.HeatSeekingActive();
            break;

          case 6:
            player.NegativeActive();
            break;

          case 7:
            player.HomingActive();
            break;

          default:
            Debug.Log("Default Value");
            break;

        }



      }

      Destroy(gameObject);

    }


  }


}
