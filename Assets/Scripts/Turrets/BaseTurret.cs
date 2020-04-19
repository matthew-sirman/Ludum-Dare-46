using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the base turret class that other turrets will inherit from
public class BaseTurret : MonoBehaviour
{

    public float turretHP;
    List<GameObject> enemies;
    public List<GameObject> bulletSpawns;
    public List<GameObject> enemiesHit;
    public float cooldownTimer = 0;
    public float cooldown;
    public float rotateSpeed;
    GameObject turretHead;
    public float wpnDmg;
    Animation shootAnim;
    public Animation destroyed;
    bool playing = false;

    private float timeCount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        cooldownTimer = cooldown;
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
            Vector3 currentRot = new Vector3(-90, turretHead.transform.rotation.y, 0);
            Vector3 targetRot = new Vector3(-90, mult * yVal, 0);
            //print("Curr:" + currentRot.y);
            // print("Next:" + targetRot.y);
            turretHead.transform.rotation = Quaternion.Lerp(Quaternion.Euler(currentRot), Quaternion.Euler(targetRot), timeCount);
            timeCount = timeCount + Time.deltaTime;
        }
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0) 
        {
            tryToFire();
        }
        if (turretHP <= 0) 
        {
            if (destroyed != null && !playing)
            {
                destroyed.Play();
                playing = true;
            }
            else 
            {
                if (destroyed == null) 
                {
                    FindObjectOfType<TurretManager>().destroyTurret(this.gameObject);
                }
            }
            if (playing) 
            {
                if (!destroyed.isPlaying) 
                {
                    FindObjectOfType<TurretManager>().destroyTurret(this.gameObject);
                }
            }
            
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
        foreach (Transform child in turretHead.transform) if (child.CompareTag("BulletSpawn")) 
        {
                bulletSpawns.Add(child.gameObject);
        }
    }

    public void setAnim(Animation anim) 
    {
        shootAnim = anim;
    }

    void tryToFire() 
    {
        if (targetWithinRange()) 
        {
            foreach (GameObject enemy in enemiesHit) 
            {
                enemy.GetComponent<Enemy>().damage(wpnDmg);
            }
            enemiesHit = new List<GameObject>();
            if (shootAnim != null)
            {
                shootAnim.Play();
            }
            cooldownTimer = cooldown;
        }
    }

    bool targetWithinRange() 
    {
        bool targetWithinRange = false;
        foreach (GameObject point in bulletSpawns) 
        {
            RaycastHit hit;
            //Debug.DrawRay(point.transform.position, point.transform.up * -1, Color.red, 100f);
            if(Physics.Raycast(point.transform.position, point.transform.up * -1, out hit)) 
            {
                if (hit.transform.CompareTag("Enemy")) 
                {
                    Debug.Log("HIT: " + hit.transform.gameObject);
                    enemiesHit.Add(hit.transform.gameObject);
                    targetWithinRange = true;
                }
            }
        }
        return targetWithinRange;
    }

    public void takeDamage(float amount) 
    {
        turretHP -= amount;
    }
}
