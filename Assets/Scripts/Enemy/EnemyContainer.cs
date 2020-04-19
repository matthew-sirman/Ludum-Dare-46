using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour {
    private List<GameObject> enemies;
    public GameObject target;
    private Queue<EnemyType> spawnQueue;
    public GameObject basicPrefab;
    public GameObject fastPrefab;
    public GameObject strongPrefab;
    private WaveManager waveManager;
    private float spawnInterval;
    private float lastSpawnTime;
    private bool spawnWave;
    private int nActiveEnemies;

    void Start() {
        waveManager = GameObject.FindWithTag("WaveManager").GetComponent<WaveManager>();
        nActiveEnemies = 0;
        spawnInterval = 0.3f;
        lastSpawnTime = -spawnInterval;
        spawnWave = true;
        target = GameObject.FindWithTag("Target");
        enemies = new List<GameObject>();
        spawnQueue = new Queue<EnemyType>();
    }

    void instantiateEnemy(EnemyType type) {
        GameObject enemy = createEnemy(type);
        enemy.transform.SetParent(transform, false);
        enemy.GetComponent<Enemy>().init(this, type);
        enemies.Add(enemy);
        nActiveEnemies++;
    }

    GameObject createEnemy(EnemyType type) {
        switch (type) {
        case EnemyType.basic:
            return Instantiate(basicPrefab, transform.position, Quaternion.identity);
        case EnemyType.fast:
            return Instantiate(fastPrefab, transform.position, Quaternion.identity);
        case EnemyType.strong:
            return Instantiate(strongPrefab, transform.position, Quaternion.identity);
        default:
            return Instantiate(basicPrefab, transform.position, Quaternion.identity);
        }
    }

    public void removeEnemy(GameObject enemy) {
        enemies.Remove(enemy);
        nActiveEnemies--;
        if (nActiveEnemies == 0 && spawnQueue.Count == 0) {
            waveManager.notifyWaveFinished();
        }
    }

    void spawnEnemiesInQueue() {
        if (Time.time > lastSpawnTime + spawnInterval) {
            lastSpawnTime = Time.time;
            instantiateEnemy(spawnQueue.Dequeue());
        }
        if (spawnQueue.Count == 0) {
            spawnWave = false;
        }
    }

    public void setSpawnQueue(Queue<EnemyType> q) {
        spawnQueue = q;
        spawnWave = true;
    }

    void Update() {
        if (spawnWave && spawnQueue.Count > 0) {
            spawnEnemiesInQueue();
        }
    }

    public GameObject getTarget() {
        return target;
    }
}
