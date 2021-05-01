using Godot;
using System;
using System.Collections.Generic;

public class Prims {

    private int count = 0;
    private int width;
    private int height;
    private bool[,] maze;
    private Random rand = new Random();
    private List<Vector2> filled;

    public bool[,] PrimsCreate(int width, int height, bool[,] maze) {
        this.maze = maze;
        this.width = width;
        this.height = height;
        filled = new List<Vector2>();

        var x = rand.Next(width - 2) + 1;
        if (x % 2 == 0) {
            x -= 1;
        }
        var y = rand.Next(height - 2) + 1;
        if (y % 2 == 0) {
            y -= 1;
        }

        filled.Add(new Vector2(x, y));
        maze[x, y] = true;

        PrimsLoop();
        return maze;
    }

    private void PrimsLoop() {
        while (!MazeFilled()) {
        
            int[] option = ChooseCell();
            int x = option[0];
            int y = option[1];

            bool[] valid = ValidNeighbours(x, y);
            if (!Found(valid)) {
                RemoveCell(x, y);
            } else {
                count = 0;
                bool selected = false;
                int choice = 0;
                while (!selected) {
                    
                    choice = rand.Next(valid.Length);
                    if (valid[choice]) {
                        selected = true;
                    }
                }

                if (choice == 0) {
                    maze[x - 1, y] = true;
                    maze[x - 2, y] = true;
                    filled.Add(new Vector2(x - 2, y));
                } else if (choice == 1) {
                    maze[x + 1, y] = true;
                    maze[x + 2, y] = true;
                    filled.Add(new Vector2(x + 2, y));
                } else if (choice == 2) {
                    maze[x, y - 1] = true;
                    maze[x, y - 2] = true;
                    filled.Add(new Vector2(x, y - 2));
                } else if (choice == 3) {
                    maze[x, y + 1] = true;
                    maze[x, y + 2] = true;
                    filled.Add(new Vector2(x, y + 2));
                } else {
                    GD.Print("Error no choice found");
                }
            }
        }
    }

    private bool MazeFilled() {
        return (filled.Count == 0);
    }

    private int[] ChooseCell() {
        int val = rand.Next(filled.Count);
        int x = (int) filled[val].x;
        int y = (int) filled[val].y;
        int[] n = new int[2];
        n[0] = x;
        n[1] = y;
        return n;
    }

    private void RemoveCell(int x, int y) {
        filled.Remove(new Vector2(x, y));
    }

    private bool[] ValidNeighbours(int x, int y) {
        bool[] tmp = new bool[4];
        if (x - 2 >= 1) {
            tmp[0] = ValidCell(x - 2, y);
        }
        if (x + 2 <= width - 2) {
            tmp[1] = ValidCell(x + 2, y);
        }
        if (y - 2 >= 1){
            tmp[2] = ValidCell(x, y - 2);
        }
        if (y + 2 <= height - 2) {
            tmp[3] = ValidCell(x, y + 2);
        }
        return tmp;
    }

    private bool ValidCell(int x, int y) {
        if (maze[x + 1, y]) {
            return false;
        }
        if (maze[x - 1, y]) {
            return false;
        }
        if (maze[x, y + 1]) {
            return false;
        }
        if (maze[x, y - 1]) {
            return false;
        }
        return true;
    }

    private bool Found(bool[] cells) {
        if (cells[0] || cells[1] || cells[2] || cells[3]) {
            return true;
        }
        return false;
    }
}