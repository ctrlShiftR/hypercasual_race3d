using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private GameObject[] runners;
    public static GameManager instance;
    [HideInInspector]
    public string FirstPlace, SecondPlace, ThirdPlace;
    public int pass;

    List<RankingSystem> ranking=new List<RankingSystem>();
    private void Awake()
    {
       
       
        
            instance = this;
        
    }
    void Start()
    {

        countRunners();
    }

    
    void Update()
    {
        ranking = ranking.OrderBy(x => x.counter).ToList();
        CalculateRank();
        
    }
    void CalculateRank()
    {
        
        switch (ranking.Count)
        {
            case 3:
                {
                   
                    ranking[0].rank = 3;
                    ThirdPlace = ranking[0].gameObject.name;
                    
                    ranking[1].rank = 2;
                    SecondPlace = ranking[1].gameObject.name;
                    ranking[2].rank = 1;
                    FirstPlace = ranking[2].gameObject.name;
                    break;
                }
            case 2:
                {
                    ranking[0].rank = 2;
                    SecondPlace = ranking[0].name;
                    ranking[1].rank = 1;
                    FirstPlace = ranking[1].name;
                    GameUIManager.instance.third.color = Color.red;
                    
                    break;
                }
            case 1:
                {
                    print("winner");
                    break;
                }
        }
        if (pass >(float)ranking.Count/2&&ranking.Count>1)
        {
            pass = 0;
            print(ranking[0]);
            ranking[0].gameObject.SetActive(false);
            ranking.Clear();
            countRunners();

        }
        
    }
    void countRunners()
    {
        runners = GameObject.FindGameObjectsWithTag("runners");
        for (int i = 0; i < runners.Length; i++)
        {
            ranking.Add(runners[i].gameObject.GetComponent<RankingSystem>());
            
        }
    }
}
