using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sagittarii : MonoBehaviour
{
    [SerializeField] float damageDelay = 2f;
    [SerializeField] int damage = 20;

    [SerializeField] GameObject projectile;

    private List<GameObject> enemiesInRange = new List<GameObject>();
    private bool isAttacking = false;

    private void Update()
    {
        enemiesInRange.RemoveAll(enemy => enemy == null);

        if (enemiesInRange.Count > 0)
        {
            enemiesInRange = enemiesInRange
                .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position))
                .ToList();

            if (!isAttacking)
            {
                StartCoroutine(AttackRoutine());
            }
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;

        while (enemiesInRange.Count > 0)
        {
            GameObject target = enemiesInRange[0];

            if (target != null)
            {
                Health enemyHealth = target.GetComponent<Health>();
                if (enemyHealth != null && enemyHealth.CurrentHealth > 0)
                {
                    Vector3 direction = target.transform.position - transform.position;
                    if (direction != Vector3.zero)
                    {
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                        Quaternion lookRotation = Quaternion.Euler(0, 0, angle);
                        Instantiate(projectile, transform.position, lookRotation);
                    }
                    enemyHealth.TakeDamage(damage);
                }
            }

            yield return new WaitForSeconds(damageDelay);

            enemiesInRange.RemoveAll(enemy => enemy == null);
        }

        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bogamannen") || collision.CompareTag("Wigmannen"))
        {
            if (!enemiesInRange.Contains(collision.gameObject))
            {
                enemiesInRange.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bogamannen") || collision.CompareTag("Wigmannen"))
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }
}
