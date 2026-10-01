using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // Een handige klasse om een golf te definiëren in de Inspector
    [System.Serializable]
    public class WaveData
    {
        public string waveName = "Wave"; // Voor het overzicht in de Inspector
        public float spawnDelay = 2f;    // De vertraging tussen vijanden in DEZE golf
        public GameObject[] enemies;     // De vijanden voor DEZE golf
    }

    [Header("Wave Configuration")]
    [SerializeField] private WaveData[] waves; // Dit wordt je uitbreidbare lijst van golven

    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    private void Start()
    {
        // Start direct de eerste golf als er golven zijn geconfigureerd
        if (waves != null && waves.Length > 0)
        {
            StartCoroutine(SpawnWaveRoutine(waves[currentWaveIndex]));
        }
    }

    private void Update()
    {
        // Controleer continu of de huidige golf klaar is en er een nieuwe moet starten
        CheckForNextWave();
    }

    private IEnumerator SpawnWaveRoutine(WaveData wave)
    {
        isSpawning = true;

        for (int i = 0; i < wave.enemies.Length; i++)
        {
            InitializeEnemy(wave.enemies[i]);
            // Gebruik de specifieke delay van de huidige golf
            yield return new WaitForSeconds(wave.spawnDelay);
        }

        isSpawning = false;
    }

    private void InitializeEnemy(GameObject enemyPrefab)
    {
        if (enemyPrefab != null)
        {
            GameObject spawnedEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            spawnedEnemy.transform.parent = transform;
        }
    }

    private void CheckForNextWave()
    {
        // Als we al aan het spawnen zijn, hoeven we niks te doen
        if (isSpawning) return;

        // Zoek of er nog vijanden in de wereld leven (zowel Bogamannen als Wigmannen)
        int aliveEnemies = GameObject.FindGameObjectsWithTag("Bogamannen").Length
                         + GameObject.FindGameObjectsWithTag("Wigmannen").Length;

        // Als alle vijanden dood zijn...
        if (aliveEnemies == 0)
        {
            // ...en er is nog een volgende golf beschikbaar...
            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(SpawnWaveRoutine(waves[currentWaveIndex]));
            }
            else
            {
                Debug.Log("Alle golven zijn voltooid! Je hebt gewonnen!");
                this.enabled = false; // Schakel de spawner uit
            }
        }
    }
}
