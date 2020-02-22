using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
[SerializeField] int health = 3;

[SerializeField] UnityEngine.Object destructableRef;
bool isShaking = false;
Vector2 startPos;
float shakeAmount = .06f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isShaking) {
            transform.position = startPos + UnityEngine.Random.insideUnitCircle * shakeAmount;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Weapon")) {
           isShaking = true;
           Invoke("StopShaking", .5f);
           health--;
                   if (health <= 0) {
               DestructGameObject();
           }
        }
    }

    private void DestructGameObject()
    {
        GameObject destructable = (GameObject)Instantiate(destructableRef);
        destructable.transform.position = transform.position;
        Destroy(gameObject);
    }

    void StopShaking() {
        isShaking = false;
        transform.position = startPos;

    }
}
