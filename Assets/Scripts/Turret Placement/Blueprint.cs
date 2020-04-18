using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{

    public GameObject turret;
    public int button;

    public RadialMenuController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<RadialMenuController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (button == 1) {
            controller.setTurret1(turret);
        }
        if (button == 2)
        {
            controller.setTurret2(turret);
        }
        if (button == 3)
        {
            controller.setTurret3(turret);
        }
        if (button == 4)
        {
            controller.setTurret4(turret);
        }
        Debug.Log("YOU HAVE UNLOCKED TURRET " + turret.name);
    }
}
