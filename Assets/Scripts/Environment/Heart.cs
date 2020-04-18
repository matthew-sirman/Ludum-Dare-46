using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private int health;
    
    void Start()
    {
        health = 100;
    }

    public void damage(int damage) {
        health -= damage;
        if (health <= 0) {
            
        }
    }
}
