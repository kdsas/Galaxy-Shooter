using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Homing_Projectile : MonoBehaviour
{
  public Transform[] targets;

  public float speed = 5f;
  public float rotateSpeed = 200f;

  private Rigidbody2D _rb;

  // Use this for initialization
  void Start()
  {
    _rb = GetComponent<Rigidbody2D>();
  }

  void FixedUpdate()
  {
    int randomTarget = UnityEngine.Random.Range(0, targets.Length - 1);
    UnityEngine.Vector2 direction = (UnityEngine.Vector2)targets[randomTarget].position - _rb.position;

    direction.Normalize();

    float rotateAmount = UnityEngine.Vector3.Cross(direction, transform.up).z;

    _rb.angularVelocity = -rotateAmount * rotateSpeed;

    _rb.velocity = transform.up * speed;
  }
}