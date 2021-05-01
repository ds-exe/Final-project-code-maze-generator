using Godot;
using System;

public class DFS {

    private int width;
    private int height;
    private bool[,] maze;
    Random rand = new Random();

    public bool[,] DFSCreate(int width, int height, bool[,] maze) {
        this.maze = maze;
        this.width = width;
        this.height = height;

        var x = rand.Next(width - 2) + 1;
        if (x % 2 == 0) {
            x -= 1;
        }
        var y = rand.Next(height - 2) + 1;
        if (y % 2 == 0) {
            y -= 1;
        }

        maze[x,y] = true;
        //GD.Print(x + " " + y);
        DFSLoop(x, y);
        //GD.Print(mazeFilled());
        return maze;
    }

    private void DFSLoop(int x, int y) {
        bool[] valid = ValidNeighbours(x, y);
        if (Found(valid)) {
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
                DFSLoop(x - 2, y);
            } else if (choice == 1) {
                maze[x + 1, y] = true;
                maze[x + 2, y] = true;
                DFSLoop(x + 2, y);
            } else if (choice == 2) {
                maze[x, y - 1] = true;
                maze[x, y - 2] = true;
                DFSLoop(x, y - 2);
            } else if (choice == 3) {
                maze[x, y + 1] = true;
                maze[x, y + 2] = true;
                DFSLoop(x, y + 2);
            } else {
                GD.Print("Error no choice found");
            }
            

            DFSLoop(x, y);
        }
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