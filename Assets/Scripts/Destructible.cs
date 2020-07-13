using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] int health = 3;

    [SerializeField] UnityEngine.Object destructableRef;
    [SerializeField] int reward = 0;
    bool isShaking = false;
    Vector2 startPos;
    float shakeAmount = .06f;

    private GameManager gameManager;

    void Start()
    {
        startPos = transform.position;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        startPos = transform.position;
        if (isShaking)
        {
            transform.position = startPos + UnityEngine.Random.insideUnitCircle * shakeAmount;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            if (!isShaking)
            {
                isShaking = true;
                Invoke("StopShaking", .6f);
            }
            health--;
            if (health <= 0)
            {
                DestructGameObject();
            }
        }
    }

    private void DestructGameObject()
    {
        GameObject destructable = (GameObject)Instantiate(destructableRef);
        destructable.transform.position = transform.position;
        destructable.transform.rotation = transform.rotation;
        Destroy(gameObject);
        if (reward != 0)
            gameManager.ChangeKarma(reward);
    }

    void StopShaking()
    {
        isShaking = false;
        transform.position = startPos;

    }
}
