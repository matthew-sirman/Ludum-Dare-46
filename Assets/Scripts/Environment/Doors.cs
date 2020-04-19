using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{

    public int unlockCost;
    
    MoneyManager mm;

    Vector3 closedRot = new Vector3(0, 0, 0);
    public float openRot = 90;

    bool unlocked = false;
    bool doorOpening = false;
    float rotCount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        mm = FindObjectOfType<MoneyManager>();
    }

    private void Update()
    {
        if (doorOpening)
        {
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(closedRot), Quaternion.Euler(0,openRot,0), rotCount);
            rotCount += Time.deltaTime;
            if (rotCount > 5)
            {
                doorOpening = false;
            }
        }
    }

    public void doAction() 
    {
        if (mm.getMoney() >= unlockCost && !unlocked)
        {
            Debug.Log("OPEN");
            mm.subtractMoney(unlockCost);
            unlocked = true;
            doorOpening = true;
        }
        else if (mm.getMoney() < unlockCost && !unlocked) 
        {
            Debug.Log("TOO POOR TO OPEN THE DOOR");
        }
    }

    public bool isUnlocked() 
    {
        return unlocked;
    }

    public int getCost() 
    {
        return unlockCost;
    }
}
