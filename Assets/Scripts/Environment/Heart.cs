using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public float maxHealth;
    private float health;
    
    void Start()
    {
        health = maxHealth;
    }

    public void damage(float damage) {
        health -= damage;
        if (health <= 0.0f) {

        }
    }
}
