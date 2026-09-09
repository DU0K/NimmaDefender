using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Wave 1-10")]
    [SerializeField] private float Wave10SpawnDelay = 2f;
    [SerializeField] private GameObject[] Wave10;

    //[Header("Wave 10-20+")]
    //[SerializeField] private float Wave20SpawnDelay = 2f;
    //[SerializeField] private GameObject[] Wave20;

    private void Start()
    {
        StartCoroutine(Wait(Wave10, Wave10SpawnDelay));
        //StartCoroutine(Wait(Wave20, Wave20SpawnDelay));
    }

    private IEnumerator Wait(GameObject[] wave, float seconds)
    {
        for (int i = 0; i < wave.Length;)
        {
            InitializeWave(wave[i]);
            yield return new WaitForSeconds(seconds);
            i++;
        }
    }

    private void InitializeWave(GameObject enemy)
    {
        enemy = Instantiate(enemy, transform.position, Quaternion.identity);
        enemy.transform.parent = transform;
    }
}
