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
            Debug.Log(targetDirection);
            float yVal = Mathf.Asin(targetDirection.x/targetDirection.z);
            Debug.Log(yVal);
            turretHead.transform.rotation = Quaternion.Euler(-90, yVal, 0);

            /*// Determine which direction to rotate towards
            Vector3 targetDirection = closestEnemy.transform.position - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = rotateSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(turretHead.transform.forward, targetDirection, singleStep, 0.0f);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            turretHead.transform.rotation = Quaternion.LookRotation(newDirection);
            //turretHead.transform.rotation = Quaternion.Euler(0, turretHead.transform.rotation.y, turretHead.transform.rotation.z);*/
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
