using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject spikePrefab;
    [SerializeField] private GameObject sawPrefab;
    [SerializeField] private GameObject kiwiPrefab;

    private float startSpawnRate = 1.7f;
    private float minSpawnRate = 0.5f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        float currentSpawnRate = Mathf.Max(minSpawnRate,startSpawnRate - GameManager.Instance.Score * 0.002f);
        if (timer >= currentSpawnRate)
        {
            SpawnPattern();
            timer = 0;
        }
    }
    private void SpawnPattern()
    {
        bool spawnSpike = Random.value < 0.6f;

        if (spawnSpike)
        {
            SpawnSpikePattern();
        }
        else
        {
            SpawnSawPattern();
        }
    }

    private void SpawnSpikePattern()
    {
        bool topSpike = Random.value > 0.5f;
        if (topSpike)
        {
            SpawnSpike(2.6f, true);
            SpawnKiwiChance(Random.Range(-2.62f, -1f));
        }
        else
        {
            SpawnSpike(-2.62f, false);
            SpawnKiwiChance(Random.Range(1f, 2.6f));
        }
    }

    private void SpawnSawPattern()
    {
        float sawY = Random.Range(-1.8f, 1.8f);
        SpawnSaw(sawY);
        if (sawY > 0)
        {
            SpawnKiwiChance(-2.62f);
        }
        else
        {
            SpawnKiwiChance(2.6f);
        }
    }

    private void SpawnSpike(float y, bool upsideDown)
    {
        Quaternion rotation = Quaternion.identity;

        if (upsideDown)
        {
            rotation = Quaternion.Euler(0,180,180);
        }
        Instantiate(spikePrefab,new Vector3(12f,y,0f),rotation);
    }

    private void SpawnSaw(float y)
    {
        Instantiate(sawPrefab,new Vector3(12f,y,0f),Quaternion.identity);
    }

    private void SpawnKiwiChance(float safeY)
    {
        if (Random.value > 0.7f)
            return;
        Instantiate(kiwiPrefab,new Vector3(Random.Range(14f, 17f),safeY,0f),Quaternion.identity);
    }
}