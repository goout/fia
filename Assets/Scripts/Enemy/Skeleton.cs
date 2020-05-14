using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Damage(int damage)
    {
        Health-=damage;
        animator.SetTrigger("Hit");
        isHit = true;
        animator.SetBool("InCombat", true);
        if (Health < 1)
        {
            StartCoroutine(DestroyCoroutine());
        }
    }

    IEnumerator DestroyCoroutine() {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
