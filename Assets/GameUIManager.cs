using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
   public  TMP_Text first,second, third;
    public static GameUIManager instance;
    void Start()
    {
        instance = this;
        
    }

    
    void Update()
    {
        first.text = GameManager.instance.FirstPlace;
        second.text = GameManager.instance.SecondPlace;
        third.text = GameManager.instance.ThirdPlace;
    }
}
