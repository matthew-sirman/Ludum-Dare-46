using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour {
    private List<GameObject> enemies;
    public Vector3 target;
    private Vector3 spawnPoint;
    private Queue<EnemyType> spawnQueue;
    public GameObject enemyPrefab;

    private float spawnInterval;
    private float lastSpawnTime;
    private bool spawnWave;

    void Start() {
        spawnInterval = 2.0f;
        lastSpawnTime = -spawnInterval;
        spawnWave = true;

        target = new Vector3(5, 2, 5);
        enemies = new List<GameObject>();
        spawnQueue = new Queue<EnemyType>();
        spawnQueue.Enqueue(EnemyType.basic);
        spawnQueue.Enqueue(EnemyType.basic);
        spawnQueue.Enqueue(EnemyType.basic);
    }

    void instantiateEnemy(EnemyType type) {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        enemy.transform.SetParent(transform, false);
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
        target = new Vector3(4 * Mathf.Sin(Time.time), 0, 4 * Mathf.Cos(Time.time));
    }

    public Vector3 getTargetPos() {
        return target;
    }
}
