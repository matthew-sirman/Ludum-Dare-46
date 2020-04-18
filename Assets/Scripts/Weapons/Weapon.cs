using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    
    public float baseDamage;
    public AnimationCurve falloffCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);
    public float maxEffectiveDistance = 100f;

    public int clipSize;
    public float reloadTime;

    // Shots per second
    public float fireRate = 1f;
    public bool autoFire = false;

    public ParticleSystem bulletExplosion;
    
    internal Animator playerAnimator;
    internal GameObject bulletSpawn;
    public Text ammoInfoText;

    private int _currentAmmo;
    private float _lastFired;
    private float _reloadStarted;
    private bool _reloading;

    private Text reloadWarningText;

    private const float ReloadWarningPercent = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        _lastFired = Time.time;
        _currentAmmo = clipSize;

        reloadWarningText = PlayerUIController.instance.reloadWarningText;
    }

    // Update is called once per frame
    void Update()
    {
        ammoInfoText.text = _currentAmmo + "/" + clipSize;

        if (!autoFire)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time - _lastFired > 1.0f / fireRate && _currentAmmo > 0)
                {
                    FireBullet();
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (Time.time - _lastFired > 1.0f / fireRate && _currentAmmo > 0)
                {
                    FireBullet();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_currentAmmo < clipSize)
            {
                StartCoroutine(ReloadGun());
            }
        }

        if (_currentAmmo == 0)
        {
            StartCoroutine(ReloadGun());
        }

        reloadWarningText.gameObject.SetActive(clipSize * ReloadWarningPercent > _currentAmmo);
    }

    private void FireBullet()
    {
        playerAnimator.SetTrigger("Shoot");
        bulletExplosion.Emit(1);

        _lastFired = Time.time;

        _currentAmmo -= 1;
        
        Ray bulletRay = new Ray(bulletSpawn.transform.position, bulletSpawn.transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(bulletRay, out hit))
        {
            Debug.Log(hit.transform.tag);
            if (!hit.transform.CompareTag("Enemy")) {
                return;
            }
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            float distancePercent = hit.distance / maxEffectiveDistance;
            if (distancePercent >= 1)
            {
                return;
            }
            
            float damage = baseDamage * falloffCurve.Evaluate(distancePercent);
            
            enemy.damage(damage);
        }
    }

    private IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(reloadTime);

        _currentAmmo = clipSize;
    }
}
