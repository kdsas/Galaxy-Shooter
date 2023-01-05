using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Heat_Seeking : MonoBehaviour
{

  public Transform target;

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
   
    UnityEngine.Vector2 direction = (UnityEngine.Vector2)target.position - _rb.position;

    direction.Normalize();

    float rotateAmount = UnityEngine.Vector3.Cross(direction, transform.up).z;

    _rb.angularVelocity = -rotateAmount * rotateSpeed;

    _rb.velocity = transform.up * speed;
  }

 
}

