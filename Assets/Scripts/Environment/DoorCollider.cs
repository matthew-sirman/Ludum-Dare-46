using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{

    Doors parent;
    public PlayerUIController cont;

    private void Start()
    {
        parent = gameObject.GetComponentInParent<Doors>();
        cont = FindObjectOfType<PlayerUIController>();
        cont.doorPrice.enabled = false;
    }
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            parent.doAction();
        }
        if (!(parent.isUnlocked())) 
        {
            Debug.Log("SHOW PRICE");
            cont.doorPrice.text = "£" + parent.getCost().ToString();
            cont.doorPrice.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        cont.doorPrice.enabled = false;
    }
}
