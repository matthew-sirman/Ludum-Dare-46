using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTurretSpot : MonoBehaviour
{

    public GameObject currentTurret;

    void OnMouseDown() 
    {
        FindObjectOfType<TurretManager>().setBuilding(this.gameObject);
        FindObjectOfType<RadialMenuController>().activate();
    }

    public void setTurret(GameObject turret) 
    {
        currentTurret = turret;
    }

    public GameObject getTurret() 
    {
        return this.currentTurret;
    }
}
