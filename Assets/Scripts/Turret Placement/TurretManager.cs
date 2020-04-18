using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public GameObject currentlyBuilding;

    public void setBuilding(GameObject turret)
    {
        currentlyBuilding = turret;
    }

    public void clearBuilding()
    {
        currentlyBuilding = null;
    }

    public GameObject getBuilding()
    {
        return currentlyBuilding;
    }
}
