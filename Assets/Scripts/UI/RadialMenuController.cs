using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//THIS SCRIPT IS ATTACHED TO THE TURRET BUILD MANAGER

public class RadialMenuController : MonoBehaviour
{

    public GameObject radialMenu;

    TurretManager manager;

    GameObject turretToBuild;
    int currentCost;

    //The turrets that can be built as is on the radial menu
    GameObject turret1;
    GameObject turret2;
    GameObject turret3;
    GameObject turret4;

    //The buttons on the radial menu
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public Text buildButtonText;


    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<TurretManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (radialMenu.activeInHierarchy) 
        {
            if (Input.GetButton("Cancel"))
            {
                unactivate();
            }
        }
    }

    //ACTIVATE THE RADIAL MENU TO BUILD A TURRET
    public void activate() 
    {
        PlayerController p = FindObjectOfType<PlayerController>();
        radialMenu.SetActive(true);
        p.GetComponent<PlayerController>().LockMovement();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (turret1 != null) {
            button1.SetActive(true);
        }
        if (turret2 != null)
        {
            button2.SetActive(true);
        }
        if (turret3 != null)
        {
            button3.SetActive(true);
        }
        if (turret4 != null)
        {
            button4.SetActive(true);
        }
    }

    //CLOSE THE RADIAL MENU
    public void unactivate() 
    {
        PlayerController p = FindObjectOfType<PlayerController>();
        radialMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        p.GetComponent<PlayerController>().UnlockMovement();
        buildButtonText.text = "0";
        turretToBuild = null;
    }

    //CONSTRUCT THE SELECTED TURRET
    public void buildTurret() 
    {
        if (turretToBuild != null) 
        {
            MoneyManager mm = FindObjectOfType<MoneyManager>();
            if (mm.getMoney() >= currentCost)
            {
                mm.subtractMoney(currentCost);
                GameObject turret = manager.getBuilding();
                turret.GetComponent<MeshRenderer>().enabled = false;
                Destroy(turret.GetComponent<ClickTurretSpot>().getTurret());
                turret.GetComponent<ClickTurretSpot>().setTurret(Instantiate(turretToBuild, turret.transform.position, Quaternion.identity));
            }
            else 
            {
                Debug.Log("NOT ENOUGH MONEY!");
            }
        }
        unactivate();
    }

    //SELECT FIRST TURRET
    public void selectTurretOne() 
    {
        if (turret1 != null) 
        {
            turretToBuild = turret1;
            currentCost = turret1.GetComponent<TurretData>().getCost();
            buildButtonText.text = turret1.GetComponent<TurretData>().getCost().ToString();
        }
    }

    public void selectTurretTwo()
    {
        if (turret2 != null)
        {
            turretToBuild = turret2;
            currentCost = turret2.GetComponent<TurretData>().getCost();
            buildButtonText.text = turret2.GetComponent<TurretData>().getCost().ToString();
        }
    }

    public void selectTurretThree()
    {
        if (turret3 != null)
        {
            turretToBuild = turret3;
            currentCost = turret3.GetComponent<TurretData>().getCost();
            buildButtonText.text = turret3.GetComponent<TurretData>().getCost().ToString();
        }
    }

    public void selectTurretFour()
    {
        if (turret4 != null)
        {
            turretToBuild = turret4;
            currentCost = turret4.GetComponent<TurretData>().getCost();
            buildButtonText.text = turret4.GetComponent<TurretData>().getCost().ToString();
        }
    }

    public void setTurret1(GameObject turret) 
    {
        turret1 = turret;
    }

    public void setTurret2(GameObject turret)
    {
        turret2 = turret;
    }

    public void setTurret3(GameObject turret)
    {
        turret3 = turret;
    }

    public void setTurret4(GameObject turret)
    {
        turret4 = turret;
    }

    public GameObject getButton1() 
    {
        return button1;
    }

    public GameObject getButton2()
    {
        return button2;
    }

    public GameObject getButton3()
    {
        return button3;
    }

    public GameObject getButton4()
    {
        return button4;
    }
}
