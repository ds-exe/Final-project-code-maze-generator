using Godot;
using System;

public class Recursion {

    private int width;
    private int height;
    private bool[,] maze;
    Random rand = new Random();

    public bool[,] RecursionCreate(int width, int height, bool[,] maze) {
        this.maze = maze;
        this.width = width;
        this.height = height;
        
        for (int x = 1; x < width - 1; x++) {
            for (int y = 1; y < height - 1; y++) {
                maze[x, y] = true;
            }
        }

        GenWall(1, 1, width - 2, height - 2);
        return maze;
    }

    private void GenWall(int x1, int y1, int x2, int y2) {
        if (x2 - x1 < 2 || y2 - y1 < 2) {
            return; // min size chamber reached
        }
        int i = rand.Next(2);
        if (i == 0) {
            GenVertical(x1, y1, x2, y2);
        } else {
            GenHorizontal(x1, y1, x2, y2);
        }
    }

    private void GenVertical(int x1, int y1, int x2, int y2) {
        var x = rand.Next(x2 - x1 - 2) + 1 + x1;
        var y = rand.Next(y2 - y1 - 2) + 1 + y1;
        if (x % 2 != 0) {
            if (x == x1) {
                x += 1;
            } else {
                x -= 1;
            }
        }
        if (y % 2 == 0) {
            y -= 1;
        }
        for (int j = y1; j <= y2; j++) {
            maze[x, j] = false;
        }
        maze[x, y] = true;
        GenWall(x1, y1, x - 1, y2);
        GenWall(x + 1, y1, x2, y2);
    }

    private void GenHorizontal(int x1, int y1, int x2, int y2) {
        var x = rand.Next(x2 - x1 - 2) + 1 + x1;
        var y = rand.Next(y2 - y1 - 2) + 1 + y1;
        if (y % 2 != 0) {
            if (y == y1) {
                y += 1;
            } else {
                y -= 1;
            }
        }
        if (x % 2 == 0) {
            x -= 1;
        }
        for (int j = x1; j <= x2; j++) {
            maze[j, y] = false;
        }
        maze[x, y] = true;
        GenWall(x1, y1, x2, y - 1);
        GenWall(x1, y + 1, x2, y2);
    }
}