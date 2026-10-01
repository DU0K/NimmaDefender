using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sagittarii : MonoBehaviour
{
    [SerializeField] float damageDelay = 2f;
    [SerializeField] int damage = 20;

    [SerializeField] GameObject projectile;

    // Lijst van vijanden die zich momenteel BINNEN de trigger bevinden
    private List<GameObject> enemiesInRange = new List<GameObject>();
    private bool isAttacking = false;

    private void Update()
    {
        // 1. Schoon de lijst op (verwijder vijanden die inmiddels dood/Destroyed zijn)
        enemiesInRange.RemoveAll(enemy => enemy == null);

        // 2. Sorteer de overgebleven vijanden binnen bereik op afstand
        if (enemiesInRange.Count > 0)
        {
            enemiesInRange = enemiesInRange
                .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position))
                .ToList();

            // Start de aanval als we dat nog niet aan het doen waren
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
            // Pak de dichtstbijzijnde vijand binnen het bereik
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


            // Wacht na het schot de cooldown af
            yield return new WaitForSeconds(damageDelay);

            // Schoon de lijst na het wachten direct weer op voor de volgende loop-check
            enemiesInRange.RemoveAll(enemy => enemy == null);
        }

        isAttacking = false;
    }

    // Wanneer een vijand de cirkel binnenloopt
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

    // Wanneer een vijand de cirkel uitloopt (omdat hij sneller is, of weggeduwd wordt)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bogamannen") || collision.CompareTag("Wigmannen"))
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }
}
