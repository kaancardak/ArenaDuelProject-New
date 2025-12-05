using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 6;
    [HideInInspector] public int currentHealth;
    public EnemyHealthUI healthUI;
    
    private bool isDead = false;
    private Animator anim;
    private EnemyController aiScript;
    private Collider2D col;
    private Rigidbody2D rb;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        aiScript = GetComponent<EnemyController>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        if (healthUI == null) healthUI = GetComponentInChildren<EnemyHealthUI>();
        if (healthUI != null) healthUI.UpdateHearts(currentHealth);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        
        if (healthUI != null) healthUI.UpdateHearts(currentHealth);

        if (currentHealth <= 0)
        {
            StartCoroutine(DeathSequence());
        }
    }

    private IEnumerator DeathSequence()
    {
        isDead = true;
        
        if(aiScript != null) aiScript.StopAI();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        if(col != null) col.enabled = false;
        
        if(anim != null)
        {
            anim.ResetTrigger("Attack"); 
            anim.ResetTrigger("Dash");
            anim.SetTrigger("Die");
        }
        
        EnemyHealthUI ui = GetComponentInChildren<EnemyHealthUI>();
        if(ui != null) ui.gameObject.SetActive(false);

        yield return new WaitForSeconds(4f);

        if(GameManager.instance != null) GameManager.instance.TriggerWin();
        Destroy(gameObject);
    }
}