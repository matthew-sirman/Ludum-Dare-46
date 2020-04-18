using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//THIS SCRIPT IS ATTACHED TO THE TURRET BUILD MANAGER

public class RadialMenuController : MonoBehaviour
{

    public GameObject radialMenu;

    public GameObject[] turretList;

    public Vector2 moveInput;

    TurretManager manager;

    public GameObject turretToBuild;

    public GameObject turret1;
    public GameObject turret2;
    public GameObject turret3;
    public GameObject turret4;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;


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
            moveInput.x = Input.mousePosition.x;
            moveInput.y = Input.mousePosition.y;

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
    }

    //CONSTRUCT THE SELECTED TURRET
    public void buildTurret() 
    {
        if (turretToBuild != null) 
        {
            GameObject turret = manager.getBuilding();
            turret.GetComponent<MeshRenderer>().enabled = false;
            Destroy(turret.GetComponent<ClickTurretSpot>().getTurret());
            turret.GetComponent<ClickTurretSpot>().setTurret(Instantiate(turretToBuild, turret.transform.position, Quaternion.identity));
        }
        unactivate();
    }

    //SELECT FIRST TURRET
    public void selectTurretOne() 
    {
        if (turret1 != null) 
        {
            turretToBuild = turret1;
        }
    }

    public void selectTurretTwo()
    {
        if (turret2 != null)
        {
            turretToBuild = turret2;
        }
    }

    public void selectTurretThree()
    {
        if (turret3 != null)
        {
            turretToBuild = turret3;
        }
    }

    public void selectTurretFour()
    {
        if (turret4 != null)
        {
            turretToBuild = turret4;
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
}
