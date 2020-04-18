using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{

    public int money;

    public int getMoney() 
    {
        return money;
    }

    public void addMoney(int amount) 
    {
        money += amount;
    }

    public void subtractMoney(int amount) 
    {
        money -= amount;
    }
}
