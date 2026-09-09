using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    bool canAttack;
    GameObject triggerObject;
    private int damage;
    private float damageDelay;

    private void Start()
    {
        GameManager gameManager = FindAnyObjectByType<GameManager>();
        if (transform.parent.parent.gameObject.CompareTag("Wigmannen"))
        {
            damage = gameManager.Damage1;
            damageDelay = gameManager.DamageDelay1;
        }
        if (transform.parent.parent.gameObject.CompareTag("Bogamannen"))
        {
            damage = gameManager.Damage2;
            damageDelay = gameManager.DamageDelay2;
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Gate"))
        {
            triggerObject = trigger.gameObject;
            canAttack = true;
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Gate"))
        {
            canAttack = false;
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(damageDelay);
        if (triggerObject && triggerObject.GetComponent<Health>().CurrentHealth > 0)
        {
            triggerObject.GetComponent<Health>().TakeDamage(damage);
        }
        if (canAttack)
        {
            StartCoroutine(Attack());
        }
    }
}
