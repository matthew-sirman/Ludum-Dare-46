using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CONTAINS METADATA FOR THE TURRET TYPE
public class TurretData : MonoBehaviour
{

    public int cost;
    public Sprite sprite;

    public int getCost()
    {
        return cost;
    }

    public Sprite getSprite()
    {
        return sprite;
    }
}
