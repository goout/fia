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
        float randomTorque = UnityEngine.Random.Range(-1.5f, 1.5f);
        rigid2D = GetComponent<Rigidbody2D>();
        rigid2D.AddForce(forceDirection);
        rigid2D.AddTorque(torque * randomTorque);
        Invoke("DestroySelf", UnityEngine.Random.Range(2.5f, 5.5f));
    }

    void DestroySelf() {
        Destroy(gameObject);
    }
}
