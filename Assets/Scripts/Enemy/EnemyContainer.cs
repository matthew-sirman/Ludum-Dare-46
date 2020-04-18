using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    private List<GameObject> enemies;
    public Vector3 target;
    private Queue<EnemyTypes> spawnQueue;
    public GameObject enemyPrefab;

    void Start()
    {
        target = new Vector3(5, 2, 5);
        enemies = new List<GameObject>();
        for (int i = 0; i < 3; i++) {
            instantiateEnemy();
        }
    }

    void instantiateEnemy() {
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(0, 2, 0), Quaternion.identity);
        enemy.transform.SetParent(transform, false);
        enemy.GetComponent<Enemy>().setParent(this);
    }

    void Update()
    {
        target = new Vector3(4 * Mathf.Sin(Time.time), 0, 4 * Mathf.Cos(Time.time));
    }

    public Vector3 getTargetPos() {
        return target;
    }
}
