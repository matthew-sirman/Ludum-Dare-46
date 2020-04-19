using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTurretSpot : MonoBehaviour
{

    public GameObject currentTurret;

    bool canInteract = true;

    void doAction()
    {
        canInteract = false;
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

    public void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            doAction();
        }
    }

    public void wasClosed()
    {
        canInteract = true;
    }

    public void turretHasBeenDestroyed() 
    {
        Debug.Log(currentTurret);
        if (currentTurret == null) 
        {
            Debug.Log("HAS BEEN DESTROYED");
            //It was this turret that has been destroyed
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
