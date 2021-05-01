using Godot;
using System;

public class MazeSize : VerifierHelper{

    private static int size = -1;

    public static int Size() {
        ResetExplored();
        size = 1;
        explored[0, 1] = true;
        Size(1, 1);
        return size;
    }

    private static void Size(int x, int y) {
        explored[x, y] = true;
        if (x == width - 1 && y == height - 2) {
            return;
        }
        if (!explored[x + 1, y] && maze[x + 1, y]) {
            size++;
            Size(x + 1, y);
        }
        if (!explored[x - 1, y] && maze[x - 1, y]) {
            size++;
            Size(x - 1, y);
        }
        if (!explored[x, y + 1] && maze[x, y + 1]) {
            size++;
            Size(x, y + 1);
        }
        if (!explored[x, y - 1] && maze[x, y - 1]) {
            size++;
            Size(x, y - 1);
        }
        return;
    }
}
