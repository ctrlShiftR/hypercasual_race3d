using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    public Vector3 offset;
    void Start()
    {
        player = GameObject.Find("player").transform;
    }

    
    void Update()
    {
        offset.x = player.forward.x * 2.5f;
      transform.position= Vector3.MoveTowards(transform.position,new Vector3(player.position.x-offset.x, player.position.y + offset.y,
           player.position.z + offset.z),50*Time.deltaTime);
    }
}
