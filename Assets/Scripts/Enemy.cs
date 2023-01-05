using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

  [SerializeField]
  private float _speed = 4.0f;

  [SerializeField]
  private Transform _getPlayer;

  [SerializeField]
  private GameObject _getEnemy;

  private float _distance;

  public float howClose;

  private Player _player;

  private Animator _anim;

  [SerializeField]
  private AudioSource _audioSource;

  [SerializeField]
  private GameObject _laserPrefab;

  [SerializeField]
  private GameObject _shieldGO;


  [SerializeField]
  private float _fireRate=3.0f;

  [SerializeField]
  private float _canFire = -1;

  private bool _enemyShieldActive = true;


  private int _shieldCollisionCount;




  private void Start()
  {

    _player = GameObject.Find("Player").GetComponent<Player>();

    _getPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().transform;
 
    _audioSource = GetComponent<AudioSource>();

    if (_player == null)
    {
      Debug.LogError("The Player is null");

    }
    _anim = GetComponent<Animator>();

    if (_anim == null)
    {
      Debug.LogError("The Animator is null");

    }

  }

  // Update is called once per frame
  void Update()
  {
    CalculateMovement();
    EnemyShieldActive();


    _distance = Vector3.Distance(_getPlayer.position, transform.position);

    if (_distance <= howClose)
    {
      transform.LookAt(_getPlayer);
      GetComponent<Rigidbody2D>().AddForce(transform.forward * 10);
    }

    if (_distance <= 1.5f)
    {



      transform.Translate((transform.up * 50 * Time.deltaTime));
    }

    if (Time.time> _canFire)
    {
      _fireRate = Random.Range(3f, 7f);
      _canFire = Time.time + _fireRate;
     GameObject enemyLaser= Instantiate(_laserPrefab, transform.position, Quaternion.identity);
      Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

      for(int i=0; i< lasers.Length; i++)
      {

        lasers[i].AssignEnemyLaser();
      }
        
    }


  }

  

  


  void CalculateMovement()
  {

    
    transform.Translate(Vector3.down * _speed * Time.deltaTime);

    if (transform.position.y < -5f)
    {
    float randomX = Random.Range(-8f, 8f);
    transform.position = new Vector3(randomX, 7, 0);
    }

    Vector3 dir = Vector3.left;
    transform.Translate(dir * _speed * Time.deltaTime);

    if (transform.position.x <= -4)
    {
      dir = Vector3.right;
    }
    else if (transform.position.x >= 4)
    {
      dir = Vector3.left;
    }


  }

  public void EnemyShieldActive()
  {
    _enemyShieldActive = true;
   
    StartCoroutine(EnemyShieldActiveDownRoutine());

  }


  IEnumerator EnemyShieldActiveDownRoutine()
  {


    yield return new WaitForSeconds(5.0f);
    
    _enemyShieldActive = false;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    _shieldCollisionCount += 1;
    if (other.tag == "Player" && _shieldCollisionCount==3)
    {
     
      Player player = other.transform.GetComponent<Player>();
      if (player != null)
      {
        player.Damage();
      }
      
      _anim.SetTrigger("OnEnemyDeath");
      _speed = 0;
      _audioSource.Play();
      Destroy(gameObject, 2.8f);
  
    }

 
      if (other.tag == "Laser" && _player.fromPlayer==true)
    {
      Destroy(other.gameObject);

      if (_enemyShieldActive == true)
      {
        if (_player != null)
        {
          _player.AddScore(0);
        }
      }

      if (_player != null && _enemyShieldActive==false)
      {
        _player.AddScore(10);
        _anim.SetTrigger("OnEnemyDeath");
        _speed = 0;
        _audioSource.Play();

        Destroy(GetComponent<Collider2D>());
        Destroy(gameObject, 2.8f);


      }
    

    
      
    }


    if (other.tag == "heat_seeking")
    {
      Destroy(other.gameObject);
      if (_player != null)
      {
        _player.AddScore(10);
      }
      _anim.SetTrigger("OnEnemyDeath");
      _speed = 0;
      _audioSource.Play();
      Destroy(GetComponent<Collider2D>());
      Destroy(gameObject, 2.8f);

    }


    if (other.tag == "homing_projectile")
    {
      Destroy(other.gameObject);
      if (_player != null)
      {
        _player.AddScore(10);
      }
      _anim.SetTrigger("OnEnemyDeath");
      _speed = 0;
      _audioSource.Play();
      Destroy(GetComponent<Collider2D>());
      Destroy(gameObject, 2.8f);

    }

  }
}
