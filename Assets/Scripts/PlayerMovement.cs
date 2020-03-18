using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

#if UNITY_IOS || UNITY_ANDROID
    bool mobile = true;
#else
 bool mobile = false;
#endif

    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    private float deathBorder = -30f;

    void Start()
    {
        animator.SetBool("isAlive", true);
    }

    void Update()
    {
        if (!mobile)
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (animator.GetBool("isAlive"))
        {
            animator.SetFloat("speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            if (!mobile && Input.GetButtonDown("Fire1") && !animator.GetBool("isAttacking") && !animator.GetBool("isJumping"))
            {
                Attack();
            }
        }
    }

    public void Move(float InputAxis)
    {
        horizontalMove = InputAxis * runSpeed;
    }

    public void Jump()
    {
        jump = true;
        animator.SetBool("isJumping", true);
    }

    public void Attack()
    {
        animator.SetBool("isAttacking", true);
        animator.Play("Attack");
        StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        controller.attackHitBox.SetActive(true);
        yield return new WaitForSeconds(.2f);
        controller.attackHitBox.SetActive(false);
        animator.SetBool("isAttacking", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
        if (transform.position.y < deathBorder)
        {
            GameManager.saveSettings();
            RestartScene();
        }
    }

    public void OnLanding()
    {
        // Debug.Log("ground");
        animator.SetBool("isJumping", false);
    }

    public void OnDeath()
    {
        GameManager.saveSettings();
        animator.SetBool("isAlive", false);
        Invoke("RestartScene", 3f);
    }

    public void OnDamage()
    {
        animator.Play("Flinch");
    }

    private void RestartScene()
    {
        SceneManager.LoadScene("Scene#1");
    }

}
