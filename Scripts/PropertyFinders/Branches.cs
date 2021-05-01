using Godot;
using System;

public class Branches : VerifierHelper{

    private static int branches = -1;

    public static int NumberOfBranches() {
        ResetExplored();
        branches = 0;
        explored[0, 1] = true;
        NumberOfBranches(1, 1);
        return branches;
    }

    private static void NumberOfBranches(int x, int y) {
        var children = 0;
        explored[x, y] = true;
        if (x == width - 1 && y == height - 2) {
            branches--;
            return;
        }
        if (!explored[x + 1, y] && maze[x + 1, y]) {
            NumberOfBranches(x + 1, y);
            children++;
        }
        if (!explored[x - 1, y] && maze[x - 1, y]) {
            NumberOfBranches(x - 1, y);
            children++;
        }
        if (!explored[x, y + 1] && maze[x, y + 1]) {
            NumberOfBranches(x, y + 1);
            children++;
        }
        if (!explored[x, y - 1] && maze[x, y - 1]) {
            NumberOfBranches(x, y - 1);
            children++;
        }
        if (children > 1) {
            branches += children;
        }
        return;
    }
}
