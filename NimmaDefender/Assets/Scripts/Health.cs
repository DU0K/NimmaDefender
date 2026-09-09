using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int currentHealth = 100;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

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
        float colorValue = Mathf.Clamp01((float)currentHealth / 100f);
        spriteRenderer.color = new Color(colorValue, colorValue, colorValue, 1f);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int DamagePoints)
    {
        currentHealth -= DamagePoints;
    }
}
