using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public GameObject[] walls;

    GenerateRooms script;
    float size;
    Vector3[] offsets;

    public Block(GenerateRooms script, int[] walls, Vector3 position)
    {
        this.script = script;
        size = script.gridSize / 2f;
        this.walls = new GameObject[4];
        offsets = new Vector3[4] { new Vector3(size, 0f, 0f), new Vector3(0f, 0f, size), new Vector3(-size, 0f, 0f), new Vector3(0f, 0f, -size) };
        for(int i = 0; i < 4; i++)
        {
            GameObject w;
            if(walls[i] == 0)
            {
                w = GameObject.Instantiate(script.noWall, script.transform);
            } else if(walls[i] == 1) {
                w = GameObject.Instantiate(script.wall, script.transform);
            } else if (walls[i] == 2) {
                w = GameObject.Instantiate(script.wallWithDoor, script.transform);
            } else {
                w = GameObject.Instantiate(script.noWall, script.transform);
            }
            w.transform.position = position + offsets[i];
            w.transform.Rotate(0f, -90f * i, 0f);
            this.walls[i] = w;
        }
    }
}
