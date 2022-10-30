using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// code from https://www.youtube.com/watch?v=ya1HyptE5uc
[Flags]
public enum WallState
{
    // 0000 NO WALLS
    // 1111 LEFT, RIGHT, UP, DOWN (READ FROM RIGHT TO LEFT)
    LEFT = 1, //0001
    RIGHT = 2, //0010
    UP = 4, //0100

    DOWN = 8, //1000

    VISITED = 128 // 1000 0000

}

public struct Position 
{
    public int X;
    public int Y;
}

public struct Neighbour
{
    public Position Position;
    public WallState SharedWall;
}

public static class MazeGenerator
{
    private static WallState GetOppositeWall(WallState wall)
    {
        switch(wall)
        {
            case WallState.RIGHT: return WallState.LEFT;
            case WallState.LEFT: return WallState.RIGHT;
            case WallState.UP: return WallState.DOWN;
            case WallState.DOWN: return WallState.UP;
            default: return WallState.LEFT; // just to make the function work
        }
    }

    private static WallState[,] RemoveRandomNeighbour(List<Neighbour> neighbours, WallState[,] maze, Position current, System.Random rng)
    {
        var randIndex = rng.Next(0,neighbours.Count);
        var randomNeighbour = neighbours[randIndex];

        var neighbourPosition = randomNeighbour.Position;
        //remove shared wall
        maze[current.X, current.Y] &= ~randomNeighbour.SharedWall;
        //remove opposite wall
        maze[neighbourPosition.X, neighbourPosition.Y] &= ~GetOppositeWall(randomNeighbour.SharedWall);

        maze[neighbourPosition.X, neighbourPosition.Y] |= WallState.VISITED;
        return maze;
    }
    private static WallState[,] ApplyRecursiveBackTracker(WallState[,] maze, int width, int height)
    {   
        Position finish = new Position { X=0, Y=0};
        Position start = new Position { X=0, Y=0};
        var rng = new System.Random();
        var positionStack = new Stack<Position>();
        //make a random position
        var position = new Position { X = rng.Next(0,width), Y = rng.Next(0,height)};
        //mark spot on maze as visited
        maze[position.X,position.Y] |= WallState.VISITED; // 1000 1111
        start = position;
        positionStack.Push(position);
        
        var current = start;
        while (positionStack.Count > 0)
        {
            
            current = positionStack.Pop();
            var neighbours = GetUnvisitedNeighbours(current,maze, width, height);

            
            if(neighbours.Count > 0)
            {
                positionStack.Push(current);

                var randIndex = rng.Next(0,neighbours.Count);
                var randomNeighbour = neighbours[randIndex];

                var neighbourPosition = randomNeighbour.Position;
                //remove shared wall
                maze[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                //remove opposite wall
                maze[neighbourPosition.X, neighbourPosition.Y] &= ~GetOppositeWall(randomNeighbour.SharedWall);

                maze[neighbourPosition.X, neighbourPosition.Y] |= WallState.VISITED;

                positionStack.Push(neighbourPosition);
            }

            
        }
        finish = current;
        
        // //making exit
        // if (current.Y == 0){
        //     maze[current.X, current.Y] &= ~WallState.UP;
        // } 
        // if (current.X == width - 1) {
        //     maze[current.X, current.Y] &= ~WallState.LEFT;
        // }
        // if (current.X == 0) {
        //     maze[current.X, current.Y] &= ~WallState.RIGHT;
        // } 
        // if (current.Y == height - 1) {
        //     maze[current.X, current.Y] &= ~WallState.DOWN;
        // }

        // if(current.X < width - 1 || current.Y < height - 1)
        // {
        //     maze[width - 1, height -1] &= ~maze[width - 1, 0];
        // }
        // Debug.Log(maze[current.X, current.Y]);
        // maze[current.X, current.Y] |= WallState.VISITED;
        maze = MakeDoor(maze, width,height,current, true);
        maze = MakeDoor(maze, width,height,start, false);
        return maze;
    }

    private static WallState[,] MakeDoor(WallState[,] maze, int width, int height, Position current, bool isExit)
    {
        //Debug.Log(current.X + " " + current.Y);
        //making exit
        // if (current.Y == 0){
        //     maze[current.X, current.Y] &= ~WallState.UP;
        // } 
        // if (current.X == width - 1) {
        //     maze[current.X, current.Y] &= ~WallState.LEFT;
        // }
        // if (current.X == 0) {
        //     maze[current.X, current.Y] &= ~WallState.RIGHT;
        // } 
        // if (current.Y == height - 1) {
        //     maze[current.X, current.Y] &= ~WallState.DOWN;
        // }

        if(isExit)
        {
            //top right exit default
            maze[width - 1, height -1] &= ~WallState.UP;
        } else {
            //bottom left entrace default
            maze[0, 0] &= ~WallState.DOWN;
        }
        Debug.Log(maze[current.X, current.Y]);
        maze[current.X, current.Y] |= WallState.VISITED;
        return maze;
    }
    private static List<Neighbour> GetUnvisitedNeighbours(Position p, WallState[,] maze , int width, int height)
    {
        var list = new List<Neighbour>();

        if (p.X > 0) // left
        {
            if(!maze[p.X-1, p.Y].HasFlag(WallState.VISITED)){
                list.Add(new Neighbour
                        {
                            Position = new Position
                            {
                               X = p.X - 1,
                               Y = p.Y 
                            },
                            SharedWall = WallState.LEFT
                        });
            }
        }

        if (p.Y > 0) // Down
        {
            if(!maze[p.X, p.Y-1].HasFlag(WallState.VISITED)){
                list.Add(new Neighbour
                        {
                            Position = new Position
                            {
                               X = p.X,
                               Y = p.Y - 1
                            },
                            SharedWall = WallState.DOWN
                        });
            }
        }

        if (p.Y < height - 1) // Up
        {
            if(!maze[p.X, p.Y + 1].HasFlag(WallState.VISITED)){
                list.Add(new Neighbour
                        {
                            Position = new Position
                            {
                               X = p.X,
                               Y = p.Y + 1
                            },
                            SharedWall = WallState.UP
                        });
            }
        }

        if (p.X < width - 1) // Right
        {
            if(!maze[p.X + 1 , p.Y].HasFlag(WallState.VISITED)){
                list.Add(new Neighbour
                        {
                            Position = new Position
                            {
                               X = p.X + 1,
                               Y = p.Y
                            },
                            SharedWall = WallState.RIGHT
                        });
            }
        }

        return list;

    }
    public static WallState[,] Generate(int width, int height)
    {
        WallState[,] maze = new WallState[width, height];

        WallState initial = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN;

        for (int i = 0; i < width; ++i)
        {
           for (int j = 0; j < height; ++j)
            {
                maze[i,j] = initial; // 1111
            } 
        }

        var generatedMaze = ApplyRecursiveBackTracker(maze, width,height);

        return generatedMaze;
    }
}
