using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;

    public GameObject respawnPoint;
    public Camera mainCamera;

    float horizontalMove = 0f;
    bool jump = false;

    private float deathBorder = -30f;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump")) {
            Debug.Log("jump");
            jump = true;
            animator.SetBool("isJumping", true);
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
            if (transform.position.y < deathBorder) {
                transform.position = respawnPoint.transform.position;
                mainCamera.transform.position = new Vector3(0,0,-10);
            }    
    }

    public void OnLanding() {
        Debug.Log("ground");
        animator.SetBool("isJumping", false);
    }
    
}
