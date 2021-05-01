using Godot;
using System;

public class PathLength : VerifierHelper{

    private static int length = -1;

    public static int LengthToEnd() {
        ResetExplored();
        length = -1;
        explored[0, 1] = true;
        LengthToEnd(1, 1, 1);
        return length;
    }

    private static void LengthToEnd(int x, int y, int length) {
        if (finished) {
            return;
        }
        explored[x, y] = true;
        if (x == width - 1 && y == height - 2) {
            finished = true;
            PathLength.length = length;
            return;
        }
        if (!explored[x + 1, y] && maze[x + 1, y]) {
            LengthToEnd(x + 1, y, length + 1);
        }
        if (!explored[x - 1, y] && maze[x - 1, y]) {
            LengthToEnd(x - 1, y, length + 1);
        }
        if (!explored[x, y + 1] && maze[x, y + 1]) {
            LengthToEnd(x, y + 1, length + 1);
        }
        if (!explored[x, y - 1] && maze[x, y - 1]) {
            LengthToEnd(x, y - 1, length + 1);
        }
        return;
    }
}
