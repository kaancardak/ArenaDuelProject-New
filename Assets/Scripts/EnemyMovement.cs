using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Vector2 randomDirection;
    public float changeDirectionInterval = 3f;
    private float timer;

    // Arena Sınırları (Clamp değerleri)
    private readonly float minX = -8.65f;
    private readonly float maxX = 8.65f;
    private readonly float minY = -4.75f;
    private readonly float maxY = 4.75f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        // Başlangıçta rastgele bir yön belirle
        SetRandomDirection();
        timer = changeDirectionInterval;
    }

    void Update()
    {
        // Yön değiştirme sayacını azalt
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SetRandomDirection();
            timer = changeDirectionInterval;
        }
    }

    void FixedUpdate()
    {
        // Belirlenen yöne doğru git
        rb.linearVelocity = randomDirection * moveSpeed;

        // Karakterin mevcut pozisyonunu al
        Vector3 currentPosition = transform.position;

        // Pozisyonu belirlenen sınırlar içinde tut (Clamp)
        float clampedX = Mathf.Clamp(currentPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(currentPosition.y, minY, maxY);

        // Yeni, sınırlandırılmış pozisyonu uygula
        transform.position = new Vector3(clampedX, clampedY, currentPosition.z);
    }

    void SetRandomDirection()
    {
        // Rastgele bir 2D vektör oluştur ve normalleştir
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}