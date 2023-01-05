using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Laser : MonoBehaviour
{

  public LineRenderer lineRenderer;
  public Transform laserPosition;

  private Player _player;

  private void Start()
  {

    _player = GameObject.Find("Player").GetComponent<Player>();


  }

    // Update is called once per frame
    void Update()
    {
    RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
        lineRenderer.SetPosition(0, laserPosition.position);

    if (hit)
    {
      lineRenderer.SetPosition(1, hit.point);

      
    }
    else
    {

      lineRenderer.SetPosition(1, transform.right * 100);
      StartCoroutine(DamageRoutine());


    }
       
    }




  IEnumerator DamageRoutine()
  {

    yield return new WaitForSeconds(7.0f);
    if (_player != null)
    {
      _player.Damage();
    }

  }
}
