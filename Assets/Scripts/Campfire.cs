using System.Collections;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    private Coroutine healCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                if (healCoroutine != null) StopCoroutine(healCoroutine);
                healCoroutine = StartCoroutine(HealOverTime(playerHealth));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (healCoroutine != null)
            {
                StopCoroutine(healCoroutine);
                healCoroutine = null;
            }
        }
    }

    private IEnumerator HealOverTime(PlayerHealth playerHealth)
    {
        while (true)
        {
            playerHealth.Heal(1);
            yield return new WaitForSeconds(3f);
        }
    }
}