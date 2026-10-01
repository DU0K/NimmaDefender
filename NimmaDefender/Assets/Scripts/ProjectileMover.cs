using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bogamannen") || collision.CompareTag("Wigmannen"))
        {
            Destroy(gameObject);
        }
    }
}
