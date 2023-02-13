using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public GameObject[] walls;

    GenerateRooms script;
    float size;
    float height;
    Vector3[] offsets;

    public Block(bool[] walls, Vector3 position)
    {
        size = script.gridSize / 2f;
        height = script.height / 2f;
        offsets = new Vector3[4] { new Vector3(size, height, 0f), new Vector3(0f, height, size), new Vector3(-size, height, 0f), new Vector3(0f, height, -size) };
        for(int i = 0; i < 4; i++)
        {
            if(walls[i])
            {
                this.walls[i] = GameObject.Instantiate(script.wall, script.transform);
            } else
            {
                this.walls[i] = GameObject.Instantiate(script.noWall, script.transform);
                this.walls[i].SetActive(false);
            }
            this.walls[i].transform.position = position + offsets[i];
        }
    }
}
