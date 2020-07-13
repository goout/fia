using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public GameObject acidPrefab;
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Damage(int damage)
    {
        if (isDead == true)
            return;
        Health -= damage;
        if (Health < 1)
        {
            isDead = true;
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
            animator.SetTrigger("Death");
            StartCoroutine(DestroyCoroutine());
        }
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    public override void Update(){

    }

    public override void Movement()
    {

    }

    public void Attack()
    {
        Instantiate(acidPrefab, new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z), Quaternion.identity);
    }
}

