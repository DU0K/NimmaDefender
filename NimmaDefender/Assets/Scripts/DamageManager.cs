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
        Debug.Log(transform.parent.parent.gameObject.name);
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
        triggerObject = trigger.gameObject;
        if (trigger.gameObject.CompareTag("Gate"))
        {
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
        triggerObject.GetComponent<Health>().TakeDamage(damage);
        if (canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void Update()
    {
        Debug.Log(damage);
        Debug.Log(damageDelay);
    }
}
