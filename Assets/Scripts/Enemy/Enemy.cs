using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType {
    basic,
    fast,
    strong
}

public class Enemy : MonoBehaviour
{
    private int id;
    private UnityEngine.AI.NavMeshAgent agent;
    private EnemyContainer parent;
    private GameObject target;

    private bool playerAggro;
    public float playerAggroDist;
    public float playerDeAggroDist;

    public float maxHealth;
    public float health;
    public GameObject player;
    public float attackRange;
    public float attackDamage;
    private float lastAttackTime;
    public float attackSpeed;
    public EnemyType type;

    void Start() {
    }

    public void init(EnemyContainer parent, EnemyType type) {
        this.parent = parent;
        this.type = type;
        this.target = this.parent.getTarget();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        lastAttackTime = Time.time;
        agent.destination = this.target.transform.position;
    }

    public void damage(float damage) {
        health -= damage;
        if (health <= 0) {
            kill();
        }
    }

    private bool inRangeOfTarget() {
        return Vector3.Distance(transform.position, agent.destination) < attackRange;
    }

    private void attackTarget() {
        if (Time.time < lastAttackTime + attackSpeed) {
            return;
        } else if (target.tag == "Target") {
            target.GetComponent<Heart>().damage(attackDamage);
        } else if (target.tag == "Player") {
            target.GetComponent<PlayerController>().Damage(attackDamage);
        }
        lastAttackTime = Time.time;
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
        player.GetComponent<PlayerController>().notifyEnemyKilled(type);
        parent.removeEnemy(gameObject);
        Destroy(gameObject);
    }

    void Update() {
        if (playerAggro) {
            target = player;
            agent.destination = player.transform.position;
        }
        if (switchPlayerAggro()) {
            playerAggro = !playerAggro;
            if (!playerAggro) {
                target = parent.getTarget();
                agent.destination = target.transform.position;
            }
        }
        if (inRangeOfTarget()) {
            attackTarget();
        }
    }
}
