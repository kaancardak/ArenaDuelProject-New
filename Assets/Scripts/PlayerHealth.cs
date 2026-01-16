using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 8;
    public int currentHealth;
    
    private bool isDead = false;
    private Animator anim;
    private KnightController movementScript;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        movementScript = GetComponent<KnightController>();
        UpdateUI();
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
            
            UpdateUI();
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateUI();

        if (currentHealth == 0)
        {
            StartCoroutine(DeathSequence());
        }
        Debug.Log("Player Health: " + currentHealth);
    }

    private void UpdateUI()
    {
        if (UIManager.instance != null)
        {
            UIManager.instance.UpdateHealthUI(currentHealth);
        }
    }

    private IEnumerator DeathSequence()
    {
        isDead = true;
        if(movementScript != null) movementScript.enabled = false;
        if(anim != null) anim.SetTrigger("Die");
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        
        yield return new WaitForSeconds(3f);
        
        GameManager.instance.TriggerLose();
    }
}