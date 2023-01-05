using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField]
  private float _speed = 3.5f;

  private float _speedMultiplier=2;

  private float _thrustMultiplier = 2.75f;

  [SerializeField]
  private GameObject _laserPrefab;

  [SerializeField]
  private GameObject _homingPrefab;

  [SerializeField]
  private GameObject _tripleShotPrefab;

  [SerializeField]
  private GameObject _heatSeekingPrefab;

  [SerializeField]
  private float _fireRate = 0.5f;

 
  private float _canFire = -1f;

  [SerializeField]
  private int _ammoCount = 15;

  [SerializeField]
  private int _currentAmmoCount;

  [SerializeField]
  private int _lives = 3;

  private UI_Manager _uiManager;


  private SpawnManager _spawnManager;

  public bool fromPlayer = false;

  private bool _thrustActive = false;

  private bool _isTripleShotActive = false;

  private bool _isSpeedBoostActive = false;

  private bool _isShieldsActive = false;

  private bool _isAmmoActive = false;

  private bool _isHealthActive = false;

  private bool _isHeatActive = false;

  private bool _isNegativeActive = false;

  private bool _isHomingActive = false;


  public int shoot;

  [SerializeField]
  private GameObject _shieldVisualizer;

  [SerializeField]
  private int _score;

  [SerializeField]
  private GameObject _rightEngine, _leftEngine;

  [SerializeField]
  private AudioClip _laserSoundClip;

  [SerializeField]
  private AudioSource _audioSource;

  private Vector3 _cameraInitialPosition;

  public float shakeMagnetude = 0.05f, shakeTime = 0.5f;

  public Camera mainCamera;

  // Start is called before the first frame update
  void Start()
    {
    
    transform.position = new Vector3(0, 0, 0);
    _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    _uiManager= GameObject.Find("Canvas").GetComponent<UI_Manager>();
    _audioSource = GetComponent<AudioSource>();

    _currentAmmoCount = _ammoCount;

    if (_spawnManager == null)
    {
      Debug.LogError("The Spawn Manager is null");

    }

    if (_uiManager == null)
    {
      Debug.LogError("The UI Manager is null");

    }

    if (_audioSource == null)
    {
      Debug.LogError("AudioSource on the player is NULL");

    }
    else
    {
      _audioSource.clip = _laserSoundClip;
    }
  }

  // Update is called once per frame
  void Update()
  {
    CalculateMovement();
    IncreaseSpeed();

    if (_isAmmoActive == true && _currentAmmoCount == 0) 
    {
      _currentAmmoCount = 15;
      _uiManager.UpdateAmmoCount(_currentAmmoCount);
    }

    

    if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && _currentAmmoCount> 0)
    {
     _currentAmmoCount -= 1;

      _uiManager.UpdateAmmoCount(_currentAmmoCount);
      if (_currentAmmoCount < 0)
      {
        _currentAmmoCount = 0;
      }


  

      FireLaser();
    }
  }

  public void ShakeIt()
  {
    _cameraInitialPosition = mainCamera.transform.position;
    InvokeRepeating("StartCameraShaking", 0f, 0.005f);
    Invoke("StopCameraShaking", shakeTime);
  }


  void StartCameraShaking()
  {
    float cameraShakingOffsetX = Random.value * shakeMagnetude * 2 - shakeMagnetude;
    float cameraShakingOffsetY = Random.value * shakeMagnetude * 2 - shakeMagnetude;
    Vector3 cameraIntermadiatePosition = mainCamera.transform.position;
    cameraIntermadiatePosition.x += cameraShakingOffsetX;
    cameraIntermadiatePosition.y += cameraShakingOffsetY;
    mainCamera.transform.position = cameraIntermadiatePosition;
  }


  void StopCameraShaking()
  {
    CancelInvoke("StartCameraShaking");
    mainCamera.transform.position = _cameraInitialPosition;
  }

  void CalculateMovement()
  {

    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

 
    transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
    transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

    



    if (transform.position.y >= 0)
    {

      transform.position = new Vector3(transform.position.x, 0, 0);
    }
    else if (transform.position.y <= -3.8f)
    {
      transform.position = new Vector3(transform.position.x, -3.8f, 0);
    }

    if (transform.position.x > 11.3f)
    {

      transform.position = new Vector3(-11.3f, transform.position.y, 0);
    }
    else if (transform.position.x < -11.3f)
    {
      transform.position = new Vector3(11.3f, transform.position.y, 0);
    }

  }

  void FireLaser()
  {

      _canFire = Time.time + _fireRate;

    shoot += 1;


    if (_isHeatActive == true)
    {
      Instantiate(_heatSeekingPrefab, transform.position, Quaternion.identity);



    }

    else if (_isTripleShotActive == true)
    {
      Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
      LaserFromPlayer();

    }
    else
    {
      Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
      LaserFromPlayer();
    }

    _audioSource.Play();
  }

  public void Damage()
  {

    ShakeIt();
    if (_isShieldsActive == true)
    {
      _isShieldsActive = false;
      _shieldVisualizer.gameObject.SetActive(false);
      ShieldsColor();
      return;
    }

    _lives--;

    if (_lives == 2)
    {
      _leftEngine.SetActive(true);
    } else if (_lives == 1)
    {

      _rightEngine.SetActive(true);
    }

    _uiManager.UpdateLives(_lives);
    if (_lives < 1)
    {
      _spawnManager.OnPlayerDeath();
      Destroy(gameObject);
    }

   
  
  }

  public void NegativeActive()
  {
    _isNegativeActive = true;

    if (_isNegativeActive == true)
    {
      _lives--;
      _uiManager.UpdateLives(_lives);
    }
    StartCoroutine(NegativePowerDownRoutine());

  }

  IEnumerator NegativePowerDownRoutine()
  {


    yield return new WaitForSeconds(1.0f);
    _isNegativeActive = false;
  }


  public void TripleShotActive()
  {
    _isTripleShotActive = true;
    StartCoroutine(TripleShotPowerDownRoutine());

  }

  IEnumerator TripleShotPowerDownRoutine()
  {


    yield return new WaitForSeconds(5.0f);
    _isTripleShotActive = false;
  }

  public void LaserFromPlayer()
  {
    fromPlayer = true;
    StartCoroutine(LaserFromPlayerDownRoutine());

  }

  IEnumerator LaserFromPlayerDownRoutine()
  {


    yield return new WaitForSeconds(1.0f);
    fromPlayer = false;
  }

  public void HeatSeekingActive()
  {
    _isHeatActive = true;

 

    StartCoroutine(HeatSeekingRoutine());
  }

  IEnumerator HeatSeekingRoutine()
  {


    yield return new WaitForSeconds(5.0f);
    _isHeatActive = false;
  }

  public void HomingActive()
  {
    _isHomingActive = true;

    if (_isHomingActive == true)
    {
      Instantiate(_homingPrefab, transform.position, Quaternion.identity);



    }


    StartCoroutine(HomingRoutine());
  }

  IEnumerator HomingRoutine()
  {


    yield return new WaitForSeconds(5.0f);
    _isHomingActive = false;
  }

  public void AmmoCountActive()
  {
    _isAmmoActive = true;
    StartCoroutine(AmmoCountActiveRoutine());

  }

  IEnumerator AmmoCountActiveRoutine()
  {


    yield return new WaitForSeconds(5.0f);
    _isAmmoActive = false;
  }

  public void HealthPowerupActive()
  {
    _isHealthActive = true;
    if (_isHealthActive == true)
    {
      _lives+=1;
      _uiManager.UpdateLives(_lives);
    }
    StartCoroutine(HealthPowerupActiveRoutine());

  }

  IEnumerator HealthPowerupActiveRoutine()
  {


    yield return new WaitForSeconds(5.0f);
    _isHealthActive = false;
  }


  public void IncreaseSpeed()
  {
    if (Input.GetKeyDown(KeyCode.LeftShift))
    {
      _speed *= _thrustMultiplier;
      _thrustActive = true;

      _uiManager.PowerBarRise();

    }
    if (Input.GetKeyUp(KeyCode.LeftShift))
    {
      StartCoroutine(IncreaseSpeedRoutine());
      _uiManager.PowerBarLow();
    }
  }

  IEnumerator IncreaseSpeedRoutine()
  {

    yield return new WaitForSeconds(1.0f);
    _thrustActive = false;
    _speed /= _thrustMultiplier;

  }


  public void SpeedBoostActive()
  {

    _isSpeedBoostActive = true;
    _speed *= _speedMultiplier;
    StartCoroutine(SpeedBoostPowerDownRoutine());

  }

  IEnumerator SpeedBoostPowerDownRoutine()
  {

    yield return new WaitForSeconds(5.0f);
    _isSpeedBoostActive = false;
    _speed /= _speedMultiplier;

  }

  public void ShieldsActive()
  {

    _isShieldsActive = true;
    _shieldVisualizer.gameObject.SetActive(true);
  }

  public void ShieldsColor()
  {

  
    _shieldVisualizer.gameObject.GetComponent<Renderer>().material.color = new Color(99, 221, 47);
  }

  public void AddScore(int points)
  {
    _score += points;

    _uiManager.UpdateScore(_score);
  }




}
