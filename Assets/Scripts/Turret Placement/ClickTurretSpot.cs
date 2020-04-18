using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTurretSpot : MonoBehaviour
{

    public GameObject currentTurret;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

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
