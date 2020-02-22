using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleRigidbody : MonoBehaviour
{
[SerializeField] Vector2 forceDirection;
[SerializeField] float torque;
Rigidbody2D rigid2D;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        rigid2D.AddForce(forceDirection);
        rigid2D.AddTorque(torque);
    }

 
    void Update()
    {
      
    }
}
