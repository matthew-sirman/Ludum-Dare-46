using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blueprint : MonoBehaviour
{

    public GameObject turret;
    //public int button;

    RadialMenuController controller;

    bool interacted = false;

    public void Start()
    {
        controller = FindObjectOfType<RadialMenuController>();
    }

    private void doAction()
    {
        interacted = true;
        //Set one of the buttons on the radial menu to the weapon in the blueprint
        /*if (button == 1) {
            controller.setTurret1(turret);
            controller.getButton1().GetComponentsInChildren<Image>()[1].sprite = turret.GetComponent<TurretData>().getSprite();
            controller.getButton1().GetComponentInChildren<Text>().text = turret.GetComponentInChildren<TurretData>().getCost().ToString();
        }
        if (button == 2)
        {
            controller.setTurret2(turret);
            controller.getButton2().GetComponentsInChildren<Image>()[1].sprite = turret.GetComponent<TurretData>().getSprite();
            controller.getButton2().GetComponentInChildren<Text>().text = turret.GetComponentInChildren<TurretData>().getCost().ToString();
        }
        if (button == 3)
        {
            controller.setTurret3(turret);
            controller.getButton3().GetComponentsInChildren<Image>()[1].sprite = turret.GetComponent<TurretData>().getSprite();
            controller.getButton3().GetComponentInChildren<Text>().text = turret.GetComponentInChildren<TurretData>().getCost().ToString();
        }
        if (button == 4)
        {
            controller.setTurret4(turret);
            controller.getButton4().GetComponentsInChildren<Image>()[1].sprite = turret.GetComponent<TurretData>().getSprite();
            controller.getButton4().GetComponentInChildren<Text>().text = turret.GetComponentInChildren<TurretData>().getCost().ToString();
        }*/
        controller.addButton(turret.GetComponentInChildren<TurretData>().getCost(), turret.GetComponent<TurretData>().getSprite(), turret);
        Debug.Log("YOU HAVE UNLOCKED TURRET " + turret.name);
    }

    private void OnMouseOver()
    {
<<<<<<< HEAD
        if (Input.GetKeyDown(KeyCode.E) && !interacted)
        {
=======
        if (Input.GetKeyDown(KeyCode.E) && !interacted) {
>>>>>>> 781203a1a01aadc99cc82ae7560f4d7f97ae8b19
            doAction();
        }
    }
}
