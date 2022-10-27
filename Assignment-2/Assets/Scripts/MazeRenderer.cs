using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    private int width = 5;
    [SerializeField]
    private int height = 5;
    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        var maze = MazeGenerator.Generate(width,height);
        Draw(maze, width, height);
    }

    private void Draw(WallState[,] maze, int width, int height) {
        for (int i = 0; i < width; ++i)
        {
           for (int j = 0; j < height; ++j)
            {
                var cell = maze[i,j];
                var position = new Vector3(-width/2 + i,0,-height/2 + j);
                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, size/2);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }
                
            } 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
