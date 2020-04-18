using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuController : MonoBehaviour
{

    public GameObject radialMenu;

    public GameObject[] turretList;

    public Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (radialMenu.activeInHierarchy) 
        {
            moveInput.x = Input.mousePosition.x;
            moveInput.y = Input.mousePosition.y;

            Debug.Log(moveInput);
        }
    }

    public void activate() 
    {
        radialMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void unactivate() 
    {
        radialMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
