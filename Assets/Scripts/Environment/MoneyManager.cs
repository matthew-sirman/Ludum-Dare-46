using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{

    public int money;
    private Text moneyText;

    private Dictionary<EnemyType, int> enemyKillRewards = new Dictionary<EnemyType, int>(){
        {EnemyType.basic, 5},
        {EnemyType.fast, 3},
        {EnemyType.strong, 20}
    };

    void Start() {
        moneyText = PlayerUIController.instance.moneyText;
    }

    public int getMoney() 
    {
        return money;
    }

    public void enemyKilled(EnemyType type) {
        money += enemyKillRewards[type];
        moneyText.text = "£" + money.ToString();
    }

    public void subtractMoney(int amount) 
    {
        money -= amount;
        moneyText.text = "£" + money.ToString();
    }
}
