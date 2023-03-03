using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoof : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject generator;


    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
        if(generator == null)
        {
            generator = GameObject.Find("Generation");
        }
        if(generator != null)
        {
            GenerateRooms script = generator.GetComponent<GenerateRooms>();
            transform.localScale = new Vector3(script.radius*10 + 1f, .05f, script.radius*10 + 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        pos = player.transform.position;
        pos.y = 4f;
        transform.position = pos;
    }
}
