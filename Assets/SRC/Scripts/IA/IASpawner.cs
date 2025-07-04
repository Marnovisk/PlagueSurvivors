using UnityEngine;

public class IASpawner : MonoBehaviour
{
    public EnemyScriptable[] EnemiesBrains;
    public GameObject EnemyPrefab;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0,10);
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        var enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<IAController>().Init(EnemiesBrains[Random.Range(0,EnemiesBrains.Length)]);
    }
}
