using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float baseDamage;
    
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
        
        Ray bulletRay = new Ray(bulletSpawn.transform.position, bulletSpawn.transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(bulletRay, out hit))
        {
            Debug.Log(hit.distance + ", " + hit.collider.name);
        }
    }
}
