using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{

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
        if (collision.gameObject.name == "AttackCollider") {
           Debug.Log("hit");
           isShaking = true;
           Invoke("StopShaking", .5f);
        }
    }
    void StopShaking() {
        isShaking = false;
        transform.position = startPos;
    }
}
