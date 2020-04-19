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

    public RuntimeAnimatorController controller;
    
    internal Animator playerAnimator;
    internal GameObject bulletSpawn;
    internal Text ammoInfoText;

    private int _currentAmmo;
    private float _lastFired;
    private bool _reloading;

    private Text reloadWarningText;

    private bool _weaponEnabled = true;

    private const float ReloadWarningPercent = 0.25f;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        _lastFired = Time.time;
        _currentAmmo = clipSize;

        reloadWarningText = PlayerUIController.instance.reloadWarningText;

        playerAnimator.runtimeAnimatorController = controller;

        playerController = playerAnimator.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(ammoInfoText is null))
        {
            ammoInfoText.text = _currentAmmo + "/" + clipSize;
        }

        reloadWarningText.gameObject.SetActive(clipSize * ReloadWarningPercent > _currentAmmo);

        if (!_weaponEnabled || playerController.locked)
        {
            return;
        }
        
        if (!autoFire)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time - _lastFired > 1.0f / fireRate && _currentAmmo > 0 && !_reloading)
                {
                    FireBullet();
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (Time.time - _lastFired > 1.0f / fireRate && _currentAmmo > 0 && !_reloading)
                {
                    FireBullet();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !_reloading)
        {
            if (_currentAmmo < clipSize)
            {
                StartCoroutine(ReloadGun());
            }
        }

        if (_currentAmmo == 0 && !_reloading)
        {
            StartCoroutine(ReloadGun());
        }
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
        playerAnimator.SetTrigger("Reload");
        _reloading = true;
        
        yield return new WaitForSeconds(reloadTime);

        _currentAmmo = clipSize;
        _reloading = false;
    }

    public void Switch()
    {
        playerAnimator.SetTrigger("Switch");
        _weaponEnabled = false;
    }
}
