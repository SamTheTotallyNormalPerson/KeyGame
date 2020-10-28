using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class GameManager : MonoBehaviour
{

    public int currentGold;
    public TMP_Text goldText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGold(int goldtoAdd)
    {
        currentGold += goldtoAdd;
        goldText.text = "Gold: " + currentGold + "!";
    }
}
