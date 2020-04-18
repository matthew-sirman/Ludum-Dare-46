using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the base turret class that other turrets will inherit from
public class BaseTurret : MonoBehaviour
{

    public int turretHP;
    List<GameObject> enemies;
    float cooldownTimer = Mathf.Infinity;
    public int cooldown;
    public float rotateSpeed;
    GameObject turretHead;
    float xRot;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Is this needed when this will be an inherited class?
        
    }

    public void aimTurretAtEnemies() 
    {
        findEnemies();
        if (enemies.Count > 0)
        {
            GameObject closestEnemy = findClosestEnemy();

            Vector3 targetDirection = closestEnemy.transform.position - transform.position;
            Vector2 dist = new Vector2(targetDirection.x, targetDirection.z);
            Vector2 forw = new Vector2(transform.forward.x, transform.forward.z);
            int mult = -1;
            if (dist.x > 0) 
            {
                mult = 1;
            }
            float absA = Mathf.Sqrt((dist.x * dist.x) + (dist.y * dist.y));
            float absB = Mathf.Sqrt((forw.x * forw.x) + (forw.y * forw.y));
            float yVal = Mathf.Acos(Vector2.Dot(dist, forw) / (absA * absB)) * 180 / Mathf.PI;
            Debug.Log(yVal);
            turretHead.transform.rotation = Quaternion.Euler(-90, mult*yVal, 0);
        }
    }

    public void findEnemies() 
    {
        enemies = new List<GameObject>();
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyArray) 
        {
            enemies.Add(enemy);
        }

    }

    float findDistance(GameObject enemy) 
    {
        return Vector3.Distance(this.transform.position, enemy.transform.position);
    }

    GameObject findClosestEnemy() 
    {
        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies) 
        {
            float dist = findDistance(enemy);
            if (dist < shortestDistance) 
            {
                shortestDistance = dist;
                closest = enemy;
            }
        }
        return closest;
    }

    public void setTurret(GameObject head)
    {
        turretHead = head;
        xRot = turretHead.transform.rotation.x;
    }
}
