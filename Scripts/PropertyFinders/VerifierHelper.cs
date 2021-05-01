using Godot;
using System;

public class VerifierHelper {

    protected static bool[,] maze;
    protected static int width;
    protected static int height;
    protected static bool[,] explored;
    protected static bool finished = false;

    public static void SetMapProperties(bool[,] maze, int width) {
        VerifierHelper.maze = maze;
        VerifierHelper.width = width;
        height = maze.Length/width;
    }

    protected static void ResetExplored() {
        finished = false;
        explored = new bool[width, height];
    }
}
