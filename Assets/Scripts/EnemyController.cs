using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isTrainingMode = false; 
    private Perceptron brain;

    public float moveSpeed = 3f;
    public float dashSpeed = 10f;
    public float attackRange = 1.5f;
    
    public Transform player; 
    
    public GameObject attackArea;
    public AudioClip attackSound;

    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audioSource;
    private EnemyHealth myHealth; 
    private bool isBusy = false;
    private float facingDirection = -1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        myHealth = GetComponent<EnemyHealth>();
        
        if(attackArea != null) attackArea.SetActive(false);

        brain = GetComponent<Perceptron>();
        if(brain == null) brain = gameObject.AddComponent<Perceptron>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) 
        {
            player = playerObj.transform;
        }
        
        int aiActive = PlayerPrefs.GetInt("AI_Active", 0);
        string jsonData = PlayerPrefs.GetString("AI_JsonData", "");

        if(!isTrainingMode && aiActive == 1 && !string.IsNullOrEmpty(jsonData))
        {
            if(!brain.LoadWeightsFromText(jsonData)) brain.InitialiseWeights();
        }
        else
        {
            brain.InitialiseWeights(); 
        }

        StartCoroutine(ActionLoop());
    }

    void Update()
    {
        if(player == null) return;
        
        if (!isBusy)
        {
            if (player.position.x > transform.position.x && facingDirection == -1) Flip();
            else if (player.position.x < transform.position.x && facingDirection == 1) Flip();
        }

        if(isTrainingMode && Input.GetKeyDown(KeyCode.S))
        {
            brain.SaveWeights();
        }
    }

    IEnumerator ActionLoop()
    {
        while (true)
        {
            if (player != null)
            {
                float distance = Vector2.Distance(transform.position, player.position);
                double inputDistance = (double)Mathf.Clamp(distance, 0, 10) / 10.0; 
                double inputHealth = (double)myHealth.currentHealth / (double)myHealth.maxHealth;

                double decision = brain.CalcOutput(inputDistance, inputHealth);
                
                if(isTrainingMode)
                {
                    double desiredOutput = (distance <= attackRange) ? 1.0 : 0.0;
                    brain.Train(inputDistance, inputHealth, desiredOutput);
                    decision = desiredOutput; 
                }

                if (decision == 1) 
                {
                    yield return StartCoroutine(PerformAttack());
                }
                else 
                {
                    if (distance > 5f) 
                        yield return StartCoroutine(PerformDash());
                    else
                        yield return StartCoroutine(PerformMove());
                }
            }
            yield return null; 
        }
    }

    IEnumerator PerformMove()
    {
        float duration = 0.5f; 
        anim.SetBool("IsMoving", true);
        
        while (duration > 0 && player != null && Vector2.Distance(transform.position, player.position) > attackRange)
        {
            float moveDir = (player.position.x > transform.position.x) ? 1f : -1f;
            rb.linearVelocity = new Vector2(moveDir * moveSpeed, rb.linearVelocity.y);
            duration -= Time.deltaTime;
            yield return null;
        }
        
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        anim.SetBool("IsMoving", false);
    }

    IEnumerator PerformAttack()
    {
        isBusy = true;
        rb.linearVelocity = Vector2.zero; 
        anim.SetTrigger("Attack");
        PlayAttackSound();

        if(attackArea != null) attackArea.SetActive(true);
        
        yield return new WaitForSeconds(0.4f); 
        
        if(attackArea != null) attackArea.SetActive(false);
        
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

    void Flip()
    {
        facingDirection *= -1;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    
    public void PlayAttackSound()
    {
        if(attackSound != null && audioSource != null) audioSource.PlayOneShot(attackSound);
    }

    public void StopAI()
    {
        StopAllCoroutines();
        isBusy = true;
        rb.linearVelocity = Vector2.zero;
        if(attackArea != null) attackArea.SetActive(false);
        anim.SetBool("IsMoving", false);
        this.enabled = false;
    }
}