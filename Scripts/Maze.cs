using Godot;
using System;

public class Maze : Godot.TileMap
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//  
//  }

    public void DrawMap(bool[,] maze, int width) {
        Clear();
        Show();
        for(int x = 0; x < width + 2; x++) {
            for (int y = 0; y < maze.Length/width + 2; y++) {
                SetCell(x, y, 1);
            }
        }
        for(int x = 0; x < width; x++) {
            for (int y = 0; y < maze.Length/width; y++) {
                SetCell(x + 1, y + 1, maze[x, y] ? 0 : 1);
            }
        }
    }
}
