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
    Block[,] grid;

    int[,] blockOptions;

    // Start is called before the first frame update
    void Start()
    {
        const int numberOfOptions = 52;

        bool[] options;
        int count;
        int k;

        blockOptions = new int[numberOfOptions,4] {
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
                options = new bool[numberOfOptions];
                count = 0;
                for (k = 0; k < numberOfOptions; k++) {
                    options[k] = true;
                    if (i > 0)
                    {
                        if(grid[i - 1, j].wallNums[0] != blockOptions[k,2])
                        {
                            options[k] = false;
                        }
                    }
                    if (j > 0)
                    {
                        if (grid[i, j - 1].wallNums[1] != blockOptions[k, 3])
                        {
                            options[k] = false;
                        }
                    }
                    if(options[k])
                    {
                        count++;
                    }
                }
                int blockNum = Random.Range(0, count);
                count = 0;
                for(k = 0; k < numberOfOptions; k++)
                {
                    if(options[k])
                    {
                        if (count == blockNum)
                        {
                            break;
                        }
                        count++;
                    }
                }
                Vector3 pos = new Vector3(gridSize * (i - radius), 0f, gridSize * (j - radius));
                grid[i, j] = new Block(this, getSubarray(blockOptions, k, 4), pos);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
