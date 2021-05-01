using Godot;
using System;

public class BranchesToEnd : VerifierHelper{

    private static int branches = -1;

    public static int NumberOfBranches() {
        ResetExplored();
        branches = 0;
        explored[0, 1] = true;
        NumberOfBranches(1, 1);
        return branches;
    }

    private static bool NumberOfBranches(int x, int y) {
        var children = 0;
        explored[x, y] = true;
        if (x == width - 1 && y == height - 2) {
            branches--;
            return true;
        }
        bool one = false;
        bool two = false;
        bool three = false;
        bool four = false;
        if (!explored[x + 1, y] && maze[x + 1, y]) {
            one = NumberOfBranches(x + 1, y);
            children++;
        }
        if (!explored[x - 1, y] && maze[x - 1, y]) {
            two = NumberOfBranches(x - 1, y);
            children++;
        }
        if (!explored[x, y + 1] && maze[x, y + 1]) {
            three = NumberOfBranches(x, y + 1);
            children++;
        }
        if (!explored[x, y - 1] && maze[x, y - 1]) {
            four = NumberOfBranches(x, y - 1);
            children++;
        }
        if (children > 1) {
            if (one || two || three || four) {
                branches += children;
                return true;
            }
        }
        if (one || two || three || four) {
            return true;
        }
        return false;
    }
}
