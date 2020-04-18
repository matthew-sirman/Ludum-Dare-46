using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTurret : BaseTurret
{
    public string headName = "TurretGuns";
    public string childName = "gun_turret";
    public int cooldownTime;
    // Start is called before the first frame update
    void Start()
    {
       setTurret(gameObject.transform.Find(childName).Find(headName).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        aimTurretAtEnemies();
    }
}
