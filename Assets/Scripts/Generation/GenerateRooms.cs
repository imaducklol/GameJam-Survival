using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRooms : MonoBehaviour
{
    public GameObject player;
    public GameObject noWall;
    public GameObject wall;
    public GameObject wallWithDoor;

    public float gridSize;
    public int radius;

    int size;
    Block[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        size = 2 * radius + 1;
        grid = new Block[size, size];
        for(int i = 0; i < size; i++) {
            for(int j = 0; j < size; j++) {
                int[] walls = new int[4];
                for(int k = 0; k < 4; k++) {
                    walls[k] = Random.Range(0,3);
                }
                Vector3 pos = new Vector3(gridSize * ((float)i - radius), 0f, gridSize * ((float)j - radius));
                grid[i,j] = new Block(this, walls, pos);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
