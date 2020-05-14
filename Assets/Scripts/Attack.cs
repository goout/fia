using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool canDamage = true;
    [SerializeField] private int damageValue = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit != null)
        {
            if (canDamage == true)
            {
                hit.Damage(damageValue);
                canDamage = false;
                StartCoroutine(ResetCanDamage());
            }
        }
    }

    IEnumerator ResetCanDamage()
    {
        yield return new WaitForSeconds(0.5f);
        canDamage = true;
    }

}
