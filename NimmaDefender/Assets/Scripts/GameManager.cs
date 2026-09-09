using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Gate")]
    [SerializeField] private int health0;
    public int Health0 => health0;

    [Header("Wigmannen")]
    [SerializeField] private int health1;
    [SerializeField] private int damage1;
    [SerializeField] private float damageDelay1;
    public int Health1 => health1;
    public int Damage1 => damage1;
    public float DamageDelay1 => damageDelay1;

    [Header("Bogamannen")]
    [SerializeField] private int health2;
    [SerializeField] private int damage2;
    [SerializeField] private float damageDelay2;
    public int Health2 => health2;
    public int Damage2 => damage2;
    public float DamageDelay2 => damageDelay2;
}
