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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Is this needed when this will be an inherited class?
        findEnemies();
        aimTurretAtEnemies();
    }

    void aimTurretAtEnemies() 
    {
        GameObject closestEnemy = findClosestEnemy();
        Vector3 targetDirection = (closestEnemy.transform.position - this.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        turretHead.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }

    void findEnemies() 
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
    }
}
