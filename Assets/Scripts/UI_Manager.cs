using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
  [SerializeField]
  private Text _scoreText;
  // Start is called before the first frame update

  [SerializeField]
  private Text _ammoCountText;

  [SerializeField]
  private int _storedAmmoCount=15;

  [SerializeField]
  private Image _LivesImg;

  [SerializeField]
  private Sprite[] _liveSprites;

  [SerializeField]
  private Text _gameOverText;

  [SerializeField]
  private Text _restartText;

  private GameManager _gameManager;

  public GameObject powerBarGO;

  public Image PowerBarMask;

  public float barChangeSpeed = 1;

  private float _maxPowerBarValue = 100;

  private float _currentPowerBarValue;

  private bool _powerIsIncreasing;

  private bool _isPowerBarON;

  void Start()
    {
    _scoreText.text = "Score: " + 0;
    _gameOverText.gameObject.SetActive(false);
    _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
   

    if (_gameManager == null)
    {
      Debug.LogError("The Game Manager is null");

    }

    


  }

  public void PowerBarRise()
  {
    _currentPowerBarValue = 0;
    _powerIsIncreasing = true;
    _isPowerBarON = true;
    StartCoroutine(UpdatePowerBar());

  }

 public void PowerBarLow()
  {
    
      _isPowerBarON = false;
     
      

    
  }

   public void UpdateScore(int playerScore)
  {

    _scoreText.text = "Score:" + playerScore.ToString();
  }


  public void UpdateAmmoCount(int playerAmmo)
  {

    _ammoCountText.text = "Ammo Count:" + playerAmmo.ToString()+ " / "+ _storedAmmoCount.ToString();
  }


  public void UpdateLives(int currentLives)
  {
    _LivesImg.sprite = _liveSprites[currentLives];

    if (currentLives == 0)
    {
      GameOverSequence();
    }

  }

  void GameOverSequence()
  {
    _gameManager.GameOver();
    _gameOverText.gameObject.SetActive(true);
    _restartText.gameObject.SetActive(true); 
    StartCoroutine(GameOverFlickerRoutine());
  }


  IEnumerator GameOverFlickerRoutine()
  {

    while (true)
    {
      _gameOverText.text = "GAME OVER";
      StartCoroutine(TurnOffPowerBar());
      yield return new WaitForSeconds(0.5f);
      _gameOverText.text = "";
      yield return new WaitForSeconds(0.5f);

    }
  }

 IEnumerator UpdatePowerBar()
  {
    while (_isPowerBarON)
    {
      if (!_powerIsIncreasing)
      {
        _currentPowerBarValue -= barChangeSpeed;
        if (_currentPowerBarValue <= 0)
        {
          _powerIsIncreasing = true;
        }
      }
      if (_powerIsIncreasing)
      {
        _currentPowerBarValue += barChangeSpeed;
        if (_currentPowerBarValue >= _maxPowerBarValue)
        {
          _powerIsIncreasing = false;
        }
      }

      float fill = _currentPowerBarValue / _maxPowerBarValue;
      PowerBarMask.fillAmount = fill;
      yield return new WaitForSeconds(0.02f);
    

    }
    yield return null;
  }
 IEnumerator TurnOffPowerBar()
  {
    yield return new WaitForSeconds(1.0f);
    powerBarGO.SetActive(false);
  }

 
}

