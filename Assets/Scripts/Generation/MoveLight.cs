using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLight : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        pos = player.transform.position;
        pos.y += .5f;
        transform.position = pos;
    }
}
