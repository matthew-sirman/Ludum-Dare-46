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

    private bool playerAggro;
    public float playerAggroDist;
    public float playerDeAggroDist;

    public float maxHealth;
    private float health;
    private GameObject player;

    void Start() {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = parent.getTargetPos();
        health = maxHealth;
        player = GameObject.FindWithTag("Player");
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

    private bool switchPlayerAggro() {
        if (!playerAggro) {
            return Vector3.Distance(
                transform.position, player.transform.position) < playerAggroDist;
        } else {
            return Vector3.Distance(
                transform.position, player.transform.position) > playerDeAggroDist;
        }
    }

    private void kill() {
        parent.removeEnemy(gameObject);
        Destroy(gameObject);
    }

    void Update() {
        if (playerAggro) {
            agent.destination = player.transform.position;
        }
        if (switchPlayerAggro()) {
            playerAggro = !playerAggro;
            if (!playerAggro) {
                agent.destination = parent.getTargetPos();
            }
        }
    }
}
