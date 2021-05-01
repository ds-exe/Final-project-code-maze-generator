using Godot;
using System;

public class DeadPaths : VerifierHelper{

    private static int deadPaths = -1;

    public static int FindDeadPaths() {
        ResetExplored();
        deadPaths = 0;
        explored[0, 1] = true;
        FindDeadPaths(1, 1, 0, 1);
        return deadPaths;
    }

    private static bool FindDeadPaths(int x, int y, int parentX, int parentY) {
        if (finished) {
            return false;
        }
        var children = 0;
        explored[x, y] = true;
        if (x == width - 1 && y == height - 2) {
            return true;
        }
        bool one = false;
        bool two = false;
        bool three = false;
        bool four = false;
        if (!explored[x + 1, y] && maze[x + 1, y]) {
            one = FindDeadPaths(x + 1, y, x, y);
            children++;
        }
        if (!explored[x - 1, y] && maze[x - 1, y]) {
            two = FindDeadPaths(x - 1, y, x, y);
            children++;
        }
        if (!explored[x, y + 1] && maze[x, y + 1]) {
            three = FindDeadPaths(x, y + 1, x, y);
            children++;
        }
        if (!explored[x, y - 1] && maze[x, y - 1]) {
            four = FindDeadPaths(x, y - 1, x, y);
            children++;
        }
        if (children > 1) {
            if (one || two || three || four) {
                ResetExplored();
                explored[0, 1] = true;
                explored[x, y] = true;
                explored[parentX, parentY] = true;
                if (one) {
                    explored[x + 1, y] = true;
                } else if (two) {
                    explored[x - 1, y] = true;
                } else if (three) {
                    explored[x, y + 1] = true;
                } else if (four) {
                    explored[x, y - 1] = true;
                }
                if (!explored[x + 1, y] && maze[x + 1, y]) {
                    deadPaths += CountChildren(x + 1, y);
                }
                if (!explored[x - 1, y] && maze[x - 1, y]) {
                    deadPaths += CountChildren(x - 1, y);
                }
                if (!explored[x, y + 1] && maze[x, y + 1]) {
                    deadPaths += CountChildren(x, y + 1);
                }
                if (!explored[x, y - 1] && maze[x, y - 1]) {
                    deadPaths += CountChildren(x, y - 1);
                }
                return false;
            }
        }
        if (one || two || three || four) {
            return true;
        }
        return false;
    }

    private static int CountChildren(int x, int y) {
        int children = 1;
        explored[x, y] = true;
        if (x == width - 1 && y == height - 2) {
            return 1;
        }
        if (!explored[x + 1, y] && maze[x + 1, y]) {
            children += CountChildren(x + 1, y);
        }
        if (!explored[x - 1, y] && maze[x - 1, y]) {
            children += CountChildren(x - 1, y);
        }
        if (!explored[x, y + 1] && maze[x, y + 1]) {
            children += CountChildren(x, y + 1);
        }
        if (!explored[x, y - 1] && maze[x, y - 1]) {
            children += CountChildren(x, y - 1);
        }
        return children;
    }
}
