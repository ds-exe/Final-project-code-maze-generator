using Godot;
using System;

public class MainScene : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    // replace width and height with random odd numbers in a range
    private int width = 0;
    private int height = 0;
    private int maxSize = 0;
    private int minSize = 15;
    private int DFSMin = 15;
    private int DFSMax = 18;
    private int recursiveMin = 17;
    private int recursiveMax = 19;
    private int primsMin = 18;
    private int primsMax = 21;
    private bool[,] maze;
    private PropertyVerifier verifier;
    private int dfsID = 0;
    private int recursiveID = 1;
    private int primsID = 2;
    private Random rand = new Random();
    private bool verify = true;
    private ulong startTime = 0;

    public override void _Ready()
    {
        //Debug buttons
        GetNode("CanvasLayer").GetNode("DataMenu").GetNode("Recursive").Connect("pressed", this, nameof(RandomRecursive));
        GetNode("CanvasLayer").GetNode("DataMenu").GetNode("Prims").Connect("pressed", this, nameof(RandomPrims));
        GetNode("CanvasLayer").GetNode("DataMenu").GetNode("DFS").Connect("pressed", this, nameof(RandomDFS));
        GetNode("CanvasLayer").GetNode("DataMenu").GetNode("Random").Connect("pressed", this, nameof(RandomMaze));
        GetNode("CanvasLayer").GetNode("Difficulty").GetNode("1").Connect("pressed", this, nameof(DifficultyOne));
        GetNode("CanvasLayer").GetNode("Difficulty").GetNode("2").Connect("pressed", this, nameof(DifficultyTwo));
        GetNode("CanvasLayer").GetNode("Difficulty").GetNode("3").Connect("pressed", this, nameof(DifficultyThree));
        GetNode("CanvasLayer").GetNode("Difficulty").GetNode("4").Connect("pressed", this, nameof(DifficultyFour));
        GetNode("CanvasLayer").GetNode("Difficulty").GetNode("5").Connect("pressed", this, nameof(DifficultyFive));
        GetNode("CanvasLayer").GetNode("Difficulty").GetNode("6").Connect("pressed", this, nameof(DifficultySix));
        GetNode("CanvasLayer").GetNode("Difficulty").GetNode("7").Connect("pressed", this, nameof(DifficultySeven));
        GetNode("CanvasLayer").GetNode("Difficulty").GetNode("8").Connect("pressed", this, nameof(DifficultyEight));
        GetNode("CanvasLayer").GetNode("Difficulty").GetNode("9").Connect("pressed", this, nameof(DifficultyNine));
        GetNode("CanvasLayer").GetNode("Difficulty").GetNode("10").Connect("pressed", this, nameof(DifficultyTen));
        GetNode("CanvasLayer").GetNode("DifficultySwap").Connect("pressed", this, nameof(SwapMode));
        GetNode("CanvasLayer").GetNode("CopyStats").Connect("pressed", this, nameof(CopyStats));
        GetNode("Maze").GetNode("Goal").Connect("finished", this, nameof(Finished));
    }

    public void DFSCreate(int difficulty) {
        minSize = DFSMin;
        maxSize = DFSMax;
        maze = CreateBaseMaze(difficulty);
        DFS dfs = new DFS();
        maze = dfs.DFSCreate(width, height, maze);
        maze[0, 1] = true;
        maze[width - 1, height - 2] = true;
        verifier = new PropertyVerifier();
        verifier.MazeProperties(maze, width, dfsID, difficulty);
        while (verify && !ValidMaze(difficulty, dfsID)) {
            maze = CreateBaseMaze(difficulty);
            maze = dfs.DFSCreate(width, height, maze);
            maze[0, 1] = true;
            maze[width - 1, height - 2] = true;
        }
        Draw(maze);
    }

    public void PrimsCreate(int difficulty) {
        minSize = primsMin;
        maxSize = primsMax;
        maze = CreateBaseMaze(difficulty);
        Prims prims = new Prims();
        maze = prims.PrimsCreate(width, height, maze);
        maze[0, 1] = true;
        maze[width - 1, height - 2] = true;
        verifier = new PropertyVerifier();
        verifier.MazeProperties(maze, width, primsID, difficulty);
        while (verify && !ValidMaze(difficulty, primsID)) {
            maze = CreateBaseMaze(difficulty);
            maze = prims.PrimsCreate(width, height, maze);
            maze[0, 1] = true;
            maze[width - 1, height - 2] = true;
        }
        Draw(maze);
    }

    public void RecursiveCreate(int difficulty) {
        minSize = recursiveMin;
        maxSize = recursiveMax;
        maze = CreateBaseMaze(difficulty);
        Recursion recursion = new Recursion();
        maze = recursion.RecursionCreate(width, height, maze);
        maze[0, 1] = true;
        maze[width - 1, height - 2] = true;
        verifier = new PropertyVerifier();
        verifier.MazeProperties(maze, width, recursiveID, difficulty);
        while (verify && !ValidMaze(difficulty, recursiveID)) {
            maze = CreateBaseMaze(difficulty);
            maze = recursion.RecursionCreate(width, height, maze);
            maze[0, 1] = true;
            maze[width - 1, height - 2] = true;
        }
        Draw(maze);
    }

    private bool ValidMaze(int difficulty, int type) {
        verifier = new PropertyVerifier();
        verifier.MazeProperties(maze, width, type, difficulty);
        return verifier.ValidMaze();
    }

    public void Draw(bool[,] maze) {
        GetNode<Maze>("Maze").Show();
        GetNode<Sprite>("Background").Hide();
        GetNode("CanvasLayer").GetNode<Label>("FinishedText").Hide();
        GetNode("CanvasLayer").GetNode<Button>("CopyStats").Hide();
        GetNode<Maze>("Maze").DrawMap(maze, width);
        GetNode<Maze>("Maze").GetNode<Player>("Player").Reset();
        GetNode<Maze>("Maze").GetNode<Goal>("Goal").Reset(width, height - 1);
        verifier.ShowProperties();
        startTime = OS.GetSystemTimeSecs();
    }

    private bool[,] CreateBaseMaze(float difficulty) {
        float multiplier = difficulty - (difficulty - 1) * 3 / 4.0f;
        width = (int) ((rand.Next(maxSize - minSize + 1) + minSize) * multiplier);
        height = (int) ((rand.Next(maxSize - minSize + 1) + minSize) * multiplier);
        if (width % 2 == 0) {
            width++;
        }
        if (height % 2 == 0) {
            height++;
        }
        bool[,] maze = new bool[width, height];
        return maze;
    }

    private void Finished() {
        GetNode<Maze>("Maze").Hide();
        //verifier.ShowProperties();
        verifier.SetTime(OS.GetSystemTimeSecs() - startTime);
        GetNode("CanvasLayer").GetNode<Label>("FinishedText").Text = verifier.GetProperties();
        GetNode("CanvasLayer").GetNode<Label>("FinishedText").Show();
        GetNode("CanvasLayer").GetNode<Button>("CopyStats").Show();
    }

    private void CopyStats() {
        OS.Clipboard = (verifier.GetPropertiesString());
    }

    private void SwapMode() {
        verify = !verify;
        GetNode("CanvasLayer").GetNode<GridContainer>("Difficulty").Visible = !GetNode("CanvasLayer").GetNode<GridContainer>("Difficulty").Visible;
        GetNode("CanvasLayer").GetNode<GridContainer>("DataMenu").Visible = !GetNode("CanvasLayer").GetNode<GridContainer>("DataMenu").Visible;
    }

    private void RandomMaze() {
        GetNode<Maze>("Maze").GetNode<Player>("Player").DisableZoom();
        int i = rand.Next(3);
        if (i == 0) {
            RandomRecursive();
        } else if (i == 2) {
            RandomPrims();
        } else {
            RandomDFS();
        }
    }

    private void RandomRecursive() {
        GetNode<Maze>("Maze").GetNode<Player>("Player").DisableZoom();
        RecursiveCreate(rand.Next(3) + 1);
    }

    private void RandomPrims() {
        GetNode<Maze>("Maze").GetNode<Player>("Player").DisableZoom();
        PrimsCreate(rand.Next(3) + 1);
    }

    private void RandomDFS() {
        GetNode<Maze>("Maze").GetNode<Player>("Player").DisableZoom();
        DFSCreate(rand.Next(3) + 1);
    }

    private void DifficultyOne() {
        DFSCreate(1);
    }

    private void DifficultyTwo() {
        DFSCreate(2);
    }

    private void DifficultyThree() {
        DFSCreate(3);
    }

    private void DifficultyFour() {
        RecursiveCreate(4);
    }

    private void DifficultyFive() {
        RecursiveCreate(5);
    }

    private void DifficultySix() {
        RecursiveCreate(6);
    }

    private void DifficultySeven() {
        PrimsCreate(7);
    }

    private void DifficultyEight() {
        PrimsCreate(8);
    }

    private void DifficultyNine() {
        PrimsCreate(9);
    }

    private void DifficultyTen() {
        PrimsCreate(10);
    }
}
