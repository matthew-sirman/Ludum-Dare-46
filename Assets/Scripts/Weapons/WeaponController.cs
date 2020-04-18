using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject rightHandHandle;
    public GameObject leftHandHandle;

    public Weapon[] weapons;
    public int startWeaponIndex = 0;

    private Weapon _equipped;

    // Start is called before the first frame update
    void Start()
    {
        _equipped = Instantiate(weapons[startWeaponIndex], rightHandHandle.transform, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
