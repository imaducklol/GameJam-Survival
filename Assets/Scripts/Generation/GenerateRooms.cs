using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRooms : MonoBehaviour
{
    public GameObject player;
    public GameObject corner;
    public GameObject noWall;
    public GameObject wall;
    public GameObject wallWithDoor;

    public float gridSize;
    public int radius;

    int size;
    Block inDirectionX;
    Block inDirectionZ;
    Block[,] grid;

    const int numberOfOptions = 52;
    int[,] blockOptions;
    int[] blockWalls;

    public Vector3 gridCenter;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerInScene = GameObject.Find("Player");
        if(playerInScene != null) {
            player = playerInScene;
        }


        blockOptions = new int[numberOfOptions, 4] {
            { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 },
            { 1, 0, 0, 0 }, { 2, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 2, 0, 0 }, { 0, 1, 0, 0 }, { 0, 2, 0, 0 }, { 0, 1, 0, 0 }, { 0, 2, 0, 0 },
            { 1, 0, 0, 0 }, { 2, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 2, 0, 0 }, { 0, 1, 0, 0 }, { 0, 2, 0, 0 }, { 0, 1, 0, 0 }, { 0, 2, 0, 0 },
            { 1, 1, 0, 0 }, { 1, 1, 0, 0 }, { 0, 1, 1, 0 }, { 0, 1, 1, 0 }, { 0, 0, 1, 1 }, { 0, 0, 1, 1 }, { 1, 0, 0, 1 }, { 1, 0, 0, 1 },
            { 1, 2, 0, 0 }, { 2, 1, 0, 0 }, { 0, 1, 2, 0 }, { 0, 2, 1, 0 }, { 0, 0, 1, 2 }, { 0, 0, 2, 1 }, { 2, 0, 0, 1 }, { 1, 0, 0, 2 },
            { 2, 2, 0, 0 }, { 0, 2, 2, 0 }, { 0, 0, 2, 2 }, { 2, 0, 0, 2 }
        };
        size = 2 * radius + 1;
        grid = new Block[size, size];
        for (int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                inDirectionX = null;
                inDirectionZ = null;
                if(i != 0)
                {
                    inDirectionX = grid[i - 1, j];
                } 
                if (j != 0)
                {
                    inDirectionZ = grid[i, j - 1];
                }
                blockWalls = chooseBlock(inDirectionX, inDirectionZ, null, null);
                Vector3 pos = new Vector3(gridSize * (radius - i) + gridCenter.x, 0f, gridSize * (radius - j) + gridCenter.z);
                grid[i, j] = new Block(this, blockWalls, pos);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x - gridCenter.x > gridSize / 2f)
        {
            // Add row to beginning
            Block[] row = new Block[size];
            for(int i = 0; i < size; i++)
            {
                inDirectionX = grid[0, i];
                inDirectionZ = null;
                if(i != 0)
                {
                    inDirectionZ = row[i - 1];
                }
                blockWalls = chooseBlock(null, inDirectionZ, inDirectionX, null);
                Vector3 pos = new Vector3(gridSize * (radius) + gridCenter.x, 0f, gridSize * (radius - i) + gridCenter.z);
                row[i] = new Block(this, blockWalls, pos);
            }
            addFirstRow(row);
            gridCenter.x += gridSize;
        } else if (player.transform.position.x - gridCenter.x < -gridSize / 2f)
        {
            // Add row to end
            Block[] row = new Block[size];
            for (int i = 0; i < size; i++)
            {
                inDirectionX = grid[size - 1, i];
                inDirectionZ = null;
                if (i != 0)
                {
                    inDirectionZ = row[i - 1];
                }
                blockWalls = chooseBlock(inDirectionX, inDirectionZ, null, null);
                Vector3 pos = new Vector3(gridSize * (-radius) + gridCenter.x, 0f, gridSize * (radius - i) + gridCenter.z);
                row[i] = new Block(this, blockWalls, pos);
            }
            addLastRow(row);
            gridCenter.x -= gridSize;
        }
    }

    int[] getSubarray(int[,] arr, int index, int size)
    {
        int[] subarray = new int[size];
        for(int i = 0; i < size; i++)
        {
            subarray[i] = arr[index, i];
        }
        return subarray;
    }

    int[] chooseBlock(Block positiveX, Block positiveZ, Block negativeX, Block negativeZ)
    {
        bool[] options = new bool[numberOfOptions];
        int count = 0;
        int k;
        for (k = 0; k < numberOfOptions; k++)
        {
            options[k] = true;
            if (positiveX != null)
            {
                if (positiveX.wallNums[2] != blockOptions[k, 0])
                {
                    options[k] = false;
                }
            }
            if (positiveZ != null)
            {
                if (positiveZ.wallNums[3] != blockOptions[k, 1])
                {
                    options[k] = false;
                }
            }
            if (negativeX != null)
            {
                if (negativeX.wallNums[0] != blockOptions[k, 2])
                {
                    options[k] = false;
                }
            }
            if (negativeZ != null)
            {
                if (negativeZ.wallNums[1] != blockOptions[k, 3])
                {
                    options[k] = false;
                }
            }
            if (options[k])
            {
                count++;
            }
        }

        int blockNum = Random.Range(0, count);
        count = 0;
        for (k = 0; k < numberOfOptions; k++)
        {
            if (options[k])
            {
                if (count == blockNum)
                {
                    break;
                }
                count++;
            }
        }

        return getSubarray(blockOptions, k, 4);
    }

    void addFirstRow(Block[] row)
    {
        for(int i = 0; i < size; i++)
        {
            grid[size - 1, i].destroyGameObjects();
        }
        for(int i = size - 1; i > 0; i--)
        {
            for(int j = 0; j < size; j++)
            {
                grid[i, j] = grid[i - 1, j];
            }
        }
        for(int i = 0; i < size; i++)
        {
            grid[0, i] = row[i];
        }
    }

    void addLastRow(Block[] row)
    {
        for (int i = 0; i < size; i++)
        {
            grid[0, i].destroyGameObjects();
        }
        for (int i = 1; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                grid[i - 1, j] = grid[i, j];
            }
        }
        for (int i = 0; i < size; i++)
        {
            grid[size - 1, i] = row[i];
        }
    }
}
