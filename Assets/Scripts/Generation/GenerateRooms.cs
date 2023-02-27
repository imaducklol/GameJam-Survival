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

    [SerializeField]
    GameObject enemy;

    public float gridSize;
    public int radius;

    int size;
    Block inDirectionX;
    Block inDirectionZ;
    Block[,] grid;

    const int numberOfOptions = 52;
    int[,] blockOptions;
    int[] blockWalls;

    Vector3 gridCenter;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerInScene = GameObject.Find("Player");
        if(playerInScene != null) {
            player = playerInScene;
        }

        gridCenter = player.transform.position;

        blockOptions = new int[numberOfOptions, 4] {
            { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 },
            { 1, 0, 0, 0 }, { 2, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 2, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 2, 0 }, { 0, 0, 0, 1 }, { 0, 0, 0, 2 },
            { 1, 0, 0, 0 }, { 2, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 2, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 2, 0 }, { 0, 0, 0, 1 }, { 0, 0, 0, 2 },
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
            gridCenter.x += gridSize;
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
        }
        else if (player.transform.position.x - gridCenter.x < -gridSize / 2f)
        {
            gridCenter.x -= gridSize;
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
        }

        if (player.transform.position.z - gridCenter.z > gridSize / 2f)
        {
            gridCenter.z += gridSize;
            // Add column to beginning
            Block[] col = new Block[size];
            for (int i = 0; i < size; i++)
            {
                inDirectionX = null;
                inDirectionZ = grid[i, 0];
                if (i != 0)
                {
                    inDirectionX = col[i - 1];
                }
                blockWalls = chooseBlock(inDirectionX, null, null, inDirectionZ);
                Vector3 pos = new Vector3(gridSize * (radius - i) + gridCenter.x, 0f, gridSize * (radius) + gridCenter.z);
                col[i] = new Block(this, blockWalls, pos);
            }
            addFirstColumn(col);
        }
        else if (player.transform.position.z - gridCenter.z < -gridSize / 2f)
        {
            gridCenter.z -= gridSize;
            // Add column to end
            Block[] col = new Block[size];
            for (int i = 0; i < size; i++)
            {
                inDirectionX = null;
                inDirectionZ = grid[i, size - 1];
                if (i != 0)
                {
                    inDirectionX = col[i - 1];
                }
                blockWalls = chooseBlock(inDirectionX, inDirectionZ, null, null);
                Vector3 pos = new Vector3(gridSize * (radius - i) + gridCenter.x, 0f, gridSize * (-radius) + gridCenter.z);
                col[i] = new Block(this, blockWalls, pos);
            }
            addLastColumn(col);
        }
    }

    static int[] getSubarray(int[,] arr, int index, int size)
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
        for(int r = size - 1; r > 0; r--)
        {
            for(int i = 0; i < size; i++)
            {
                grid[r, i] = grid[r - 1, i];
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
        for (int r = 0; r < size - 1; r++)
        {
            for (int i = 0; i < size; i++)
            {
                grid[r, i] = grid[r +  1, i];
            }
        }
        for (int i = 0; i < size; i++)
        {
            grid[size - 1, i] = row[i];
        }
    }

    void addFirstColumn(Block[] col)
    {
        for (int i = 0; i < size; i++)
        {
            grid[i, size - 1].destroyGameObjects();
        }
        for (int c = size - 1; c > 0; c--)
        {
            for (int i = 0; i < size; i++)
            {
                grid[i, c] = grid[i, c - 1];
            }
        }
        for (int i = 0; i < size; i++)
        {
            grid[i, 0] = col[i];
        }
    }

    void addLastColumn(Block[] col)
    {
        for (int i = 0; i < size; i++)
        {
            grid[i, 0].destroyGameObjects();
        }
        for (int c = 0; c < size - 1; c++)
        {
            for (int i = 0; i < size; i++)
            {
                grid[i, c] = grid[i, c + 1];
            }
        }
        for (int i = 0; i < size; i++)
        {
            grid[i, size - 1] = col[i];
        }
    }
}
