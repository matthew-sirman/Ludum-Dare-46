using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour {
    private List<GameObject> enemies;
    public GameObject target;
    private Vector3 spawnPos;
    private Queue<EnemyType> spawnQueue;
    public GameObject enemyPrefab;
    private WaveManager waveManager;

    private float spawnInterval;
    private float lastSpawnTime;
    private bool spawnWave;
    private int nActiveEnemies;

    void Start() {
        waveManager = GameObject.FindWithTag("WaveManager").GetComponent<WaveManager>();
        nActiveEnemies = 0;
        spawnInterval = 2.0f;
        lastSpawnTime = -spawnInterval;
        spawnWave = true;
        spawnPos = new Vector3(0, 0, 50);
        target = GameObject.FindWithTag("Target");
        enemies = new List<GameObject>();
        spawnQueue = new Queue<EnemyType>();
    }

    void instantiateEnemy(EnemyType type) {
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.transform.SetParent(transform);
        enemy.GetComponent<Enemy>().init(this, type);
        enemies.Add(enemy);
        nActiveEnemies++;
    }

    public void removeEnemy(GameObject enemy) {
        enemies.Remove(enemy);
        nActiveEnemies--;
        if (nActiveEnemies == 0) {
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
