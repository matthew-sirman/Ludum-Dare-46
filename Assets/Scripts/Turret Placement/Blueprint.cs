using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blueprint : MonoBehaviour
{

    public GameObject turret;
    public int button;

    public RadialMenuController controller;

    public void Start()
    {
        controller = FindObjectOfType<RadialMenuController>();
    }

    private void OnMouseDown()
    {
        if (button == 1) {
            controller.setTurret1(turret);
            controller.getButton1().GetComponentInChildren<Image>().sprite = turret.GetComponent<TurretData>().getSprite();
            controller.getButton1().GetComponentInChildren<Text>().text = turret.GetComponentInChildren<TurretData>().getCost().ToString();
        }
        if (button == 2)
        {
            controller.setTurret2(turret);
            controller.getButton2().GetComponentInChildren<Image>().sprite = turret.GetComponent<TurretData>().getSprite();
            controller.getButton2().GetComponentInChildren<Text>().text = turret.GetComponentInChildren<TurretData>().getCost().ToString();
        }
        if (button == 3)
        {
            controller.setTurret3(turret);
            controller.getButton3().GetComponentInChildren<Image>().sprite = turret.GetComponent<TurretData>().getSprite();
            controller.getButton3().GetComponentInChildren<Text>().text = turret.GetComponentInChildren<TurretData>().getCost().ToString();
        }
        if (button == 4)
        {
            controller.setTurret4(turret);
            controller.getButton4().GetComponentInChildren<Image>().sprite = turret.GetComponent<TurretData>().getSprite();
            controller.getButton4().GetComponentInChildren<Text>().text = turret.GetComponentInChildren<TurretData>().getCost().ToString();
        }
        Debug.Log("YOU HAVE UNLOCKED TURRET " + turret.name);
    }
}
