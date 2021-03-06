﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public Text ammoInfoText;
    public Text moneyText;
    public Text reloadWarningText;
    public Text nextWaveText;
    public Text doorPrice;

    public static PlayerUIController instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("No Player UI is set in this scene.");
            }
            return _instance;
        }
        private set => _instance = value;
    }

    private static PlayerUIController _instance;

    void Awake()
    {
        instance = this;
    }
}
