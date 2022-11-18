using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingSystem : MonoBehaviour
{
    public int lapcount, distance, counter, currentCp,rank;
    
    private Vector3 checkPoint;

    void Start()
    {
        currentCp = 1;
        checkPoint = GameObject.Find("CheckPoint" + currentCp).transform.position;
    }

    
    void Update()
    {
        CalculateDistance();
    }
    void CalculateDistance()
    {
       distance=(int) Vector3.Distance(transform.position, checkPoint);
        counter = lapcount * 1000 + currentCp * 100 + distance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
           currentCp= other.GetComponent<CurrentCheckPoint>().currentCheckNumber;
            checkPoint = GameObject.Find("CheckPoint" + currentCp).transform.position;
        }
        if (other.gameObject.tag == "Finish")
        {
            lapcount++;
            //currentCp = 1;
            GameManager.instance.pass += 1;
        }
    }
}
