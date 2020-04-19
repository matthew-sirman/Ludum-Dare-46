using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{

    public int unlockCost;
    
    GameObject doorBody;
    MoneyManager mm;

    Vector3 closedRot = new Vector3(0, 0, 0);
    public Vector3 openRot = new Vector3(0, 90, 0);

    bool unlocked = false;
    bool doorOpening = false;
    float rotCount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        mm = FindObjectOfType<MoneyManager>();
        doorBody = transform.Find("DoorBody").gameObject;
    }

    private void Update()
    {
        if (doorOpening) 
        {
            doorBody.transform.rotation = Quaternion.Slerp(Quaternion.Euler(closedRot), Quaternion.Euler(openRot), rotCount);
            rotCount += Time.deltaTime;
            if (rotCount > 5) 
            {
                doorOpening = false;
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            doAction();
        }
    }

    void doAction() 
    {
        if (mm.getMoney() >= unlockCost && !unlocked)
        {
            mm.subtractMoney(unlockCost);
            unlocked = true;
            doorOpening = true;
        }
        else if (mm.getMoney() < unlockCost && !unlocked) 
        {
            Debug.Log("TOO POOR TO OPEN THE DOOR");
        }
    }
}
