using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType {
    basic
}

public class Enemy : MonoBehaviour
{
    private int id;
    private UnityEngine.AI.NavMeshAgent agent;
    private EnemyContainer parent;

    private float health;

    void Start() {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = parent.getTargetPos();
        health = 10.0f;
    }

    public void setParent(EnemyContainer parent) {
        this.parent = parent;
    }

    public void damage(float damage) {
        health -= damage;
        if (health <= 0) {
            kill();
        }
    }

    private void kill() {
        parent.removeEnemy(gameObject);
        Destroy(gameObject);
    }

    void Update() {
        agent.destination = parent.getTargetPos();
    }
}
