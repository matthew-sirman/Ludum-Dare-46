using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private float health;
    
    void Start()
    {
        health = 100.0f;
    }

    public void damage(float damage) {
        health -= damage;
        if (health <= 0.0f) {

        }
    }
}
