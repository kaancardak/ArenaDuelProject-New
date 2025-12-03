using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Inspector'dan ayarlanabilir hız değeri
    public float moveSpeed = 5f; 
    private Rigidbody2D rb;
    private Vector2 movementInput; 

    // Arena Sınırları (Clamp değerleri)
    private readonly float minX = -8.65f;
    private readonly float maxX = 8.65f;
    private readonly float minY = -4.75f;
    private readonly float maxY = 4.75f;

    void Start()
    {
        // Rigidbody bileşenini al
        rb = GetComponent<Rigidbody2D>();
        // Rigidbody'nin dönmesini engelle (Top-down 2D oyunlar için genellikle istenir)
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
    }

    void Update()
    {
        // Klavyeden girdileri al
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        movementInput = new Vector2(horizontalInput, verticalInput).normalized;
    }

    void FixedUpdate()
    {
        // Hareketi uygulamak için hız ayarla
        Vector2 targetVelocity = movementInput * moveSpeed;
        rb.linearVelocity = targetVelocity;

        // Karakterin mevcut pozisyonunu al
        Vector3 currentPosition = transform.position;

        // Pozisyonu belirlenen sınırlar içinde tut (Clamp)
        float clampedX = Mathf.Clamp(currentPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(currentPosition.y, minY, maxY);

        // Yeni, sınırlandırılmış pozisyonu uygula
        transform.position = new Vector3(clampedX, clampedY, currentPosition.z);
    }
}