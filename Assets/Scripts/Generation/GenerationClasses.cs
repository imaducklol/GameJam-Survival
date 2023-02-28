using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    GameObject[] corners;
    GameObject[] walls;
    public int[] wallNums;

    float size;
    Vector3[] offsets;

    public Block(GenerateRooms script, int[] walls, Vector3 position)
    {
        size = script.gridSize / 2f;
        this.walls = new GameObject[4];
        corners = new GameObject[4];
        wallNums = new int[4];
        offsets = new Vector3[4] { new Vector3(size, 0f, 0f), new Vector3(0f, 0f, size), new Vector3(-size, 0f, 0f), new Vector3(0f, 0f, -size) };
        for(int i = 0; i < 4; i++)
        {
            bool createCorner = true;
            if(i + 1 == 4)
            {
                if(walls[0] == 0 && walls[3] == 0)
                {
                    createCorner = false;
                }
            } else
            {
                if(walls[i] == 0 && walls[i + 1] == 0)
                {
                    createCorner = false;
                }
            }
            if (createCorner)
            {
                corners[i] = Object.Instantiate(script.corner, script.transform);
                corners[i].transform.position = position;
                corners[i].transform.Rotate(0f, -90f * i, 0f);
            }
            if(walls[i] == 0) {
                this.walls[i] = Object.Instantiate(script.noWall, script.transform);
            } else if(walls[i] == 1) {
                this.walls[i] = Object.Instantiate(script.wall, script.transform);
            } else if (walls[i] == 2) {
                this.walls[i] = Object.Instantiate(script.wallWithDoor, script.transform);
            } else {
                this.walls[i] = Object.Instantiate(script.noWall, script.transform);
            }
            this.walls[i].transform.position = position + offsets[i];
            this.walls[i].transform.Rotate(0f, -90f * i, 0f);
            wallNums[i] = walls[i];
        }
    }

    public void destroyGameObjects()
    {
        for(int i = 0; i < 4; i++)
        {
            if (corners[i] != null)
            {
                Object.Destroy(corners[i]);
            }
            if(walls[i] != null)
            {
                Object.Destroy(walls[i]);
            }
        }
    }
}
