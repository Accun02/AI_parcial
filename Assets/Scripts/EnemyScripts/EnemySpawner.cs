using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Tipo 0 = Dispara, Tipo 1 = Persigue, Tipo 2 = Huye
    public float[] spawnChances; // Por ejemplo: [0.5f, 0.3f, 0.2f]
    public Transform[] spawnPoints;

    void Start()
    {
        SpawnEnemies(2); // por ejemplo, 10 enemigos
    }

    void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject prefab = GetRandomEnemy();
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        }
    }

    GameObject GetRandomEnemy()
    {
        float total = 0f;
        foreach (float chance in spawnChances)
            total += chance;

        float randomValue = Random.Range(0f, total);
        float cumulative = 0f;

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            cumulative += spawnChances[i];
            if (randomValue <= cumulative)
                return enemyPrefabs[i];
        }

        return enemyPrefabs[0]; // fallback
    }
}

