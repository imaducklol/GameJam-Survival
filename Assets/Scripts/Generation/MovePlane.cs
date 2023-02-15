using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlane : MonoBehaviour
{
    [SerializeField]
    GameObject player;


    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pos = player.transform.position;
        pos.y = 0f;
        transform.position = pos;
    }
}
