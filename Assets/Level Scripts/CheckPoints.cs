using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] checkpoints;
    [HideInInspector]
    public int currentcheckpt=1;
    void Awake()
    {
        checkpoints = GameObject.FindGameObjectsWithTag("CheckPoint");
    }


    private void Start()
    {
        foreach(var cp in checkpoints)
        {
            cp.AddComponent<CurrentCheckPoint>();
            cp.GetComponent<CurrentCheckPoint>().currentCheckNumber=currentcheckpt;
            cp.name = "CheckPoint" + currentcheckpt;
            currentcheckpt++;
        }
    }
}
