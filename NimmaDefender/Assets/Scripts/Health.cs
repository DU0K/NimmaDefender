using UnityEngine;

public class Health : MonoBehaviour
{
    private int currentHealth = 100;
    public int CurrentHealth => currentHealth;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        GameManager GameManager = FindAnyObjectByType<GameManager>();

        if (gameObject.CompareTag("Gate"))
        {
            currentHealth = GameManager.Health0;
        }
        else if (gameObject.CompareTag("Wigmannen"))
        {
            currentHealth = GameManager.Health1;
        }
        else if (gameObject.CompareTag("Bogamannen"))
            currentHealth = GameManager.Health2;
    }

    private void Update()
    {
        if (currentHealth <= 0 && !gameObject.CompareTag("Gate"))
        {
            Destroy(gameObject);
        }
        else if (currentHealth <= 0 && gameObject.CompareTag("Gate"))
        {
            spriteRenderer.color = new Color(0f, 0f, 0f, 0f);
            boxCollider.enabled = false;
            //GameOver
        }
    }
    public void TakeDamage(int DamagePoints)
    {
        currentHealth -= DamagePoints;
        float colorValue = Mathf.Clamp01((float)currentHealth / 100f);
        spriteRenderer.color = new Color(spriteRenderer.color.r - colorValue, spriteRenderer.color.g - colorValue, spriteRenderer.color.b - colorValue, spriteRenderer.color.a);
    }
}
