using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float baseDamage;
    public AnimationCurve falloffCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);
    public float maxEffectiveDistance = 100f;

    public ParticleSystem bulletExplosion;
    
    internal Animator playerAnimator;
    internal GameObject bulletSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        playerAnimator.SetTrigger("Shoot");
        bulletExplosion.Emit(1);
        
        Ray bulletRay = new Ray(bulletSpawn.transform.position, bulletSpawn.transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(bulletRay, out hit))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy is null)
            {
                return;
            }

            float distancePercent = hit.distance / maxEffectiveDistance;

            if (distancePercent >= 1)
            {
                return;
            }
            
            float damage = baseDamage * falloffCurve.Evaluate(distancePercent);
            
            Debug.Log(damage);
        }
    }
}
