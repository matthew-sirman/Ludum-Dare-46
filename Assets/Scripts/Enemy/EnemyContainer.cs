using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour {
    private List<GameObject> enemies;
    public GameObject target;
    private Vector3 spawnPos;
    private Queue<EnemyType> spawnQueue;
    public GameObject enemyPrefab;

    private float spawnInterval;
    private float lastSpawnTime;
    private bool spawnWave;

    void Start() {
        spawnInterval = 2.0f;
        lastSpawnTime = -spawnInterval;
        spawnWave = true;
        spawnPos = new Vector3(0, 0, 50);

        target = GameObject.FindWithTag("Target");
        enemies = new List<GameObject>();
        spawnQueue = new Queue<EnemyType>();
        spawnQueue.Enqueue(EnemyType.basic);
        spawnQueue.Enqueue(EnemyType.basic);
        spawnQueue.Enqueue(EnemyType.basic);
    }

    void instantiateEnemy(EnemyType type) {
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.transform.SetParent(transform);
        enemy.GetComponent<Enemy>().setParent(this);
        enemies.Add(enemy);
    }

    public void removeEnemy(GameObject enemy) {
        enemies.Remove(enemy);
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

    void Update() {
        if (spawnWave && spawnQueue.Count > 0) {
            spawnEnemiesInQueue();
        }
    }

    public GameObject getTarget() {
        return target;
    }
}
