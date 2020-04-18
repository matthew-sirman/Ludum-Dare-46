using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypes {
    basic
}

public class Enemy : MonoBehaviour
{
    private int id;
    private UnityEngine.AI.NavMeshAgent agent;
    private EnemyContainer parent;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = parent.getTargetPos();
    }

    public void setParent(EnemyContainer parent) {
        this.parent = parent;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = parent.getTargetPos();
    }
}
