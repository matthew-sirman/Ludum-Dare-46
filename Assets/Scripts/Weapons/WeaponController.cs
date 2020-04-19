using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WeaponController : MonoBehaviour
{
    public GameObject rightHandHandle;
    public GameObject leftHandHandle;
    public GameObject bulletSpawnPoint;

    public float switchSpeed = 1f;
    
    public Weapon[] weapons;
    public int startWeaponIndex = 0;

    public Animator playerAnimator;

    private Weapon _equipped;
    private int _equippedIndex;
    
    private Text _ammoInfoText;

    // Start is called before the first frame update
    void Start()
    {
        _ammoInfoText = PlayerUIController.instance.ammoInfoText;
        
        EquipWeapon(startWeaponIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _equipped.Switch();
            StartCoroutine(SwitchWeapon());
        }
    }

    IEnumerator SwitchWeapon()
    {
        yield return new WaitForSeconds(switchSpeed);
        
        EquipWeapon((_equippedIndex + 1) % weapons.Length);
    }

    public void EquipWeapon(int weaponIndex)
    {
        if (!(_equipped is null))
        {
            GameObject.Destroy(_equipped);
        }

        _equippedIndex = weaponIndex;

        _equipped = Instantiate(weapons[weaponIndex], rightHandHandle.transform);
        _equipped.playerAnimator = playerAnimator;
        _equipped.bulletSpawn = bulletSpawnPoint;
        _equipped.ammoInfoText = _ammoInfoText;
    }
}
