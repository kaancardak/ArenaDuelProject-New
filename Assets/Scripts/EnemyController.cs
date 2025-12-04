using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float dashSpeed = 10f;
    public float attackRange = 1.5f;
    public Transform player;
    public GameObject attackArea;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isBusy = false;
    private float facingDirection = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        
        StartCoroutine(ActionLoop());
    }

    void Update()
    {
        if (player.position.x > transform.position.x && facingDirection == -1)
            Flip();
        else if (player.position.x < transform.position.x && facingDirection == 1)
            Flip();
    }

    IEnumerator ActionLoop()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, player.position) < attackRange)
            {
                yield return StartCoroutine(PerformAttack());
            }
            else
            {
                int randomAction = Random.Range(0, 4);
                
                switch (randomAction)
                {
                    case 0: 
                    case 1: 
                        yield return StartCoroutine(PerformMove());
                        break;
                    case 2:
                        yield return StartCoroutine(PerformDash());
                        break;
                    case 3:
                        yield return StartCoroutine(PerformTaunt());
                        break;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator PerformMove()
    {
        float duration = 2f;
        anim.SetBool("IsMoving", true);
        
        while (duration > 0 && Vector2.Distance(transform.position, player.position) > attackRange)
        {
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.deltaTime);
            rb.MovePosition(newPos);
            duration -= Time.deltaTime;
            yield return null;
        }

        anim.SetBool("IsMoving", false);
    }

    IEnumerator PerformAttack()
    {
        isBusy = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger("Attack");
        
        yield return new WaitForSeconds(0.4f);
        if (attackArea != null) attackArea.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        if (attackArea != null) attackArea.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        
        isBusy = false;
    }

    IEnumerator PerformDash()
    {
        isBusy = true;
        anim.SetTrigger("Dash");
        
        float dashDirection = (player.position.x > transform.position.x) ? 1 : -1;
        rb.linearVelocity = new Vector2(dashDirection * dashSpeed, 0);
        
        yield return new WaitForSeconds(0.5f);
        rb.linearVelocity = Vector2.zero;
        isBusy = false;
    }

    IEnumerator PerformTaunt()
    {
        isBusy = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger("Taunt");
        yield return new WaitForSeconds(1.5f);
        isBusy = false;
    }

    void Flip()
    {
        facingDirection *= -1;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}