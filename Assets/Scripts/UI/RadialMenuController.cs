using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//THIS SCRIPT IS ATTACHED TO THE TURRET BUILD MANAGER

public class RadialMenuController : MonoBehaviour
{

    GameObject radialMenu;
    public GameObject buttonParent;

    public GameObject radialButton;
    public List<GameObject> buttons;
    public List<GameObject> turrets;

    TurretManager manager;

    GameObject turretToBuild;

    MoneyManager mm;
    int currentCost;

    public Text buildButtonText;


    // Start is called before the first frame update
    void Start()
    {
        radialMenu = GameObject.Find("Turret Placement UI");
        radialMenu.SetActive(true);
        manager = FindObjectOfType<TurretManager>();
        buildButtonText = GameObject.FindGameObjectWithTag("BuildCost").GetComponent<Text>();
        radialMenu.SetActive(false);
        mm = FindObjectOfType<MoneyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (radialMenu.activeInHierarchy)
        {
            if (Input.GetButton("Cancel"))
            {
                unactivate(); //true
            }
        }
    }

    //ACTIVATE THE RADIAL MENU TO BUILD A TURRET
    public void activate()
    {
        PlayerController p = FindObjectOfType<PlayerController>();
        radialMenu.SetActive(true);
        p.GetComponent<PlayerController>().LockMovement();
        p.UnlockCursor();
    }

    //CLOSE THE RADIAL MENU
    public void unactivate(bool escapeUsed = false)
    {
        PlayerController p = FindObjectOfType<PlayerController>();
        buildButtonText.text = "0";
        radialMenu.SetActive(false);
        if (!escapeUsed) 
        {
            p.LockCursor();
        }
        p.GetComponent<PlayerController>().UnlockMovement();
        turretToBuild = null;
        manager.getBuilding().GetComponent<ClickTurretSpot>().wasClosed();
    }

    //CONSTRUCT THE SELECTED TURRET
    public void buildTurret()
    {
        if (turretToBuild != null)
        {
            if (mm.getMoney() >= currentCost)
            {
                mm.subtractMoney(currentCost);
                GameObject turret = manager.getBuilding();
                Debug.Log(turret.GetComponent<MeshRenderer>());
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
}
