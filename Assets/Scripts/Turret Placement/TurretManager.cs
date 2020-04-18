using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{

    public List<GameObject> allTurretTypes;

    public List<GameObject> unlockedTurrets;

    public GameObject currentlyBuilding;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void unlockTurret(GameObject turret) 
    {
        unlockedTurrets.Add(turret);
    }

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
