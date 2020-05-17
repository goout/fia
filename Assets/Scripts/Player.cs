using System.Collections;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Examples;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{

    //#if UNITY_IOS || UNITY_ANDROID
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
    bool mobile = false;
#else
 bool mobile = true;
#endif



    private Rigidbody2D rigid;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 5f;
    //  [SerializeField] private LayerMask groundLayer;
    private bool resetJump = false;

    private SkeletonAnimationHandleExample animationHandle;

    private BasicPlatformerController animationController;

    private SpriteRenderer spriteRenderer;

    public string attackButton = "Fire1";

    private float deathBorder = -10f;

    private bool attacking = false;
    private bool damaging = false;

    private bool isAlive = true;

    public int maxHealth = 100;
    public int currentHealth;

    public Healthbar healthbar;

    private bool facingRight = true;

    private SkeletonAnimation skeletonAnimation;

    public BoxCollider2D weaponCollider;

    float horizontalMove = 0f;

    bool isGrounded = false;

    public int Health { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animationHandle = GetComponent<SkeletonAnimationHandleExample>();
        animationController = GetComponent<BasicPlatformerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealth(currentHealth);
        skeletonAnimation = transform.Find("Visuals/Spine").GetComponent<SkeletonAnimation>();
//        Debug.Log(mobile);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }
    public void Movement()
    {
        //Debug.DrawRay(transform.position, Vector2.down * 1.25f, Color.green);
        if (!damaging && isAlive)
        {
            if (!mobile)
                horizontalMove = Input.GetAxisRaw("Horizontal");
            if (horizontalMove != 0)
                facingRight = horizontalMove > 0 ? true : false;

            isGrounded = IsGrounded();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            rigid.velocity = new Vector2(horizontalMove * speed, rigid.velocity.y);


            if (!mobile)
            {
                if (Input.GetButton(attackButton))
                {
                    Attack();

                }
            }


            animationController.Move(horizontalMove, rigid.velocity.y, isGrounded, attacking);

            GameManager.currentPosition = transform.position;

            if (transform.position.y < deathBorder)
            {
                GameManager.saveSettings();
                SceneManager.LoadScene("Scene#1");
            }
        }
    }

    public void Move(float moveH)
    {
        horizontalMove = moveH;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            StartCoroutine(ResetJumpCorutine());
        }
    }

    public void Attack()
    {
        if (isGrounded && !attacking)
        {
            StartCoroutine(AttackCorutine());
        }
    }

    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Scene#1");
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 2.15f, 1 << 9);
        //  RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.25f, groundLayer);

        if (hitInfo.collider != null)
        {
            if (resetJump == false)
                return true;
        }
        return false;
    }

    IEnumerator ResetJumpCorutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }

    IEnumerator AttackCorutine()
    {
        weaponCollider.enabled = true;
        attacking = true;
        yield return new WaitForSeconds(0.8332f);
        attacking = false;
        weaponCollider.enabled = false;
    }

    IEnumerator DamageCorutine()
    {
        skeletonAnimation.skeleton.SetColor(Color.red);
        damaging = true;
        yield return new WaitForSeconds(0.7f);
        damaging = false;
        skeletonAnimation.skeleton.SetColor(Color.white);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Spikes"))
        {
            Damage(20);
        }
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            isAlive = false;
            animationController.Death(facingRight);
            rigid.velocity = new Vector2(0, 0);
            StartCoroutine(RestartScene());
        }
        else
        {
            StartCoroutine(DamageCorutine());
            rigid.velocity = new Vector2(facingRight ? -5 : 5, jumpForce / 1.5f);
            animationController.Damage();
        }
    }

}
