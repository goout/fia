﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{

    private void Start()
    {
        Destroy(this.gameObject, 8f);
    }
    private void Update()
    {
        transform.Translate(Vector3.left * 3 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IDamageable hit = other.GetComponent<IDamageable>();
            if (hit != null)
            {
                hit.Damage(50);
                Destroy(this.gameObject);
            }
        }
    }
}
