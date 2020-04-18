using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTurretSpot : MonoBehaviour
{

    public GameObject currentTurret;

    bool canInteract = true;

<<<<<<< HEAD
    void doAction()
=======
    void doAction() 
>>>>>>> 781203a1a01aadc99cc82ae7560f4d7f97ae8b19
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
<<<<<<< HEAD
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
=======
        if (Input.GetKeyDown(KeyCode.E) && canInteract) 
>>>>>>> 781203a1a01aadc99cc82ae7560f4d7f97ae8b19
        {
            doAction();
        }
    }

<<<<<<< HEAD
    public void wasClosed()
=======
    public void wasClosed() 
>>>>>>> 781203a1a01aadc99cc82ae7560f4d7f97ae8b19
    {
        canInteract = true;
    }
}
