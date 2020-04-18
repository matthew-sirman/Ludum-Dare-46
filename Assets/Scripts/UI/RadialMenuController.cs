using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//THIS SCRIPT IS ATTACHED TO THE TURRET BUILD MANAGER

public class RadialMenuController : MonoBehaviour
{

    public GameObject radialMenu;
    public GameObject buttonParent;

    public GameObject radialButton;
    public List<GameObject> buttons;
    public List<GameObject> turrets;

    TurretManager manager;

    GameObject turretToBuild;
    int currentCost;

    //The turrets that can be built as is on the radial menu
    /*GameObject turret1;
    GameObject turret2;
    GameObject turret3;
    GameObject turret4;*/

    //The buttons on the radial menu
    /*public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;*/
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
        /*if (turret1 != null) {
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
        }*/
    }

    //CLOSE THE RADIAL MENU
    public void unactivate()
    {
        PlayerController p = FindObjectOfType<PlayerController>();
        buildButtonText.text = "0";
        radialMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        p.GetComponent<PlayerController>().UnlockMovement();
        turretToBuild = null;
        manager.getBuilding().GetComponent<ClickTurretSpot>().wasClosed();
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

    public void selectTurret(int ind)
    {
        turretToBuild = turrets[ind];
        currentCost = turrets[ind].GetComponent<TurretData>().getCost();
        buildButtonText.text = turrets[ind].GetComponent<TurretData>().getCost().ToString();
    }

    public void addButton(int cost, Sprite sprite, GameObject turret)
    {
        GameObject newButton = Instantiate(radialButton);
        newButton.transform.SetParent(buttonParent.transform, false);
        newButton.GetComponentsInChildren<Image>()[1].sprite = sprite;
        newButton.GetComponentInChildren<Text>().text = cost.ToString();
        int count = buttons.Count;
        newButton.GetComponent<Button>().onClick.AddListener(delegate { buttonCall(count); });
        buttons.Add(newButton);
        turrets.Add(turret);

        //update button positions
        float angleIncrement = 2 * Mathf.PI / buttons.Count;
        float angleCount = 0;
        foreach (GameObject button in buttons)
        {
            button.GetComponent<RectTransform>().localPosition = new Vector3(Mathf.Sin(angleCount) * 35, Mathf.Cos(angleCount) * 35, 0);
            button.GetComponent<RectTransform>().localScale = new Vector3(0.2f, 0.2f, 1);
            angleCount += angleIncrement;
        }
    }

    public void buttonCall(int pos)
    {
        selectTurret(pos);
    }

    //SELECT FIRST TURRET
    /*public void selectTurretOne() 
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
    }*/

    /*public void setTurret1(GameObject turret) 
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
    }*/

    /*public GameObject getButton1() 
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
    }*/
}
