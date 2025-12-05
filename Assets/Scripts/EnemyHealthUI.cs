using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private Transform parentTransform;
    private RectTransform myTransform;

    void Start()
    {
        parentTransform = transform.parent;
        myTransform = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        if (parentTransform.localScale.x < 0)
        {
            Vector3 newScale = myTransform.localScale;
            newScale.x = -Mathf.Abs(newScale.x);
            myTransform.localScale = newScale;
        }
        else
        {
            Vector3 newScale = myTransform.localScale;
            newScale.x = Mathf.Abs(newScale.x);
            myTransform.localScale = newScale;
        }
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            int heartValue = (i + 1) * 2;

            if (currentHealth >= heartValue)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (currentHealth == heartValue - 1)
            {
                hearts[i].sprite = halfHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}