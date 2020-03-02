using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;

    public Camera mainCamera;

    float horizontalMove = 0f;
    bool jump = false;
    bool isAttacking = false;
    private float deathBorder = -30f;

    void Start()
    {
        animator.SetBool("isAlive", true);
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (animator.GetBool("isAlive"))
        {
            animator.SetFloat("speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("jump");
                jump = true;
                animator.SetBool("isJumping", true);
            }

            if (Input.GetButtonDown("Fire1") && !isAttacking && !animator.GetBool("isJumping"))
            {
                isAttacking = true;
                animator.Play("Attack");
                StartCoroutine(DoAttack());
            }
        }
    }

    IEnumerator DoAttack()
    {
        controller.attackHitBox.SetActive(true);
        yield return new WaitForSeconds(.2f);
        controller.attackHitBox.SetActive(false);
        isAttacking = false;
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
        if (transform.position.y < deathBorder)
        {
            RestartScene();
        }
    }

    public void OnLanding()
    {
        Debug.Log("ground");
        animator.SetBool("isJumping", false);
    }

    public void OnDeath()
    {
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
