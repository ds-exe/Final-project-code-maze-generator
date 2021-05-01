using Godot;
using System;

public class PropertyVerifier {

    private int type;
    private int difficulty;
    private int length = -1;
    private int branches = -1;
    private int branchesToEnd = -1;
    private int size = -1;
    private int deadPaths = -1;
    private ulong time = 0;

    // change return to an array of ints for different values
    public void MazeProperties(bool[,] maze, int width, int type, int difficulty) {
        this.type = type;
        this.difficulty = difficulty;
        VerifierHelper.SetMapProperties(maze, width);

        LengthToEnd();
        NumberOfBranches();
        NumberBranchesToEnd();
        Size();
        FindDeadPaths();
    }

    public void ShowProperties() {
        GD.Print("Type: " + type);
        GD.Print("Difficulty: " + difficulty);
        GD.Print("Length: " + length);
        GD.Print("Branches: " + branches);
        GD.Print("Branches to end: " + branchesToEnd);
        GD.Print("Size: " + size);
        GD.Print("Dead cells: " + deadPaths);
    }

    public string GetProperties() {
        string properties = "";
        properties += "Type: ";
        properties += type + "\n";
        properties += "Difficulty: ";
        properties += difficulty + "\n";
        properties += "Length: ";
        properties += length + "\n";
        properties += "Branches: ";
        properties += branches + "\n";
        properties += "Branches to end: ";
        properties += branchesToEnd + "\n";
        properties += "Size: ";
        properties += size + "\n";
        properties += "Dead cells: ";
        properties += deadPaths + "\n";
        properties += "Time: ";
        properties += time + "\n";
        return properties;
    }

    public string GetPropertiesString() {
        string properties = "";
        properties += "T: ";
        properties += type + ", ";
        properties += "D: ";
        properties += difficulty + ", ";
        properties += "L: ";
        properties += length + ", ";
        properties += "B: ";
        properties += branches + ", ";
        properties += "BE: ";
        properties += branchesToEnd + ", ";
        properties += "S: ";
        properties += size + ", ";
        properties += "DC: ";
        properties += deadPaths + ", ";
        properties += "TE: ";
        properties += time;// + ", ";
        return properties;
    }

    public bool ValidMaze() {
        if (deadPaths > size * 0.1) {
            //GD.Print("fail1");
            return false;
        }
        if (type == 0) {
            return ValidMazeDFS();
        } else if (type == 1) {
            return ValidMazeRecursive();
        } else {
            return ValidMazePrims();
        }
    }

    private bool ValidMazeDFS() {
        if (branches < length * 0.5) {
            //GD.Print("fail2");
            return false;
        }
        if (branchesToEnd < length * 0.2) {
            //GD.Print("fail3");
            return false;
        }
        return true;
    }

    private bool ValidMazeRecursive() {
        if (branches > length) {
            //GD.Print("fail2");
            return false;
        }
        if (branches < length * 0.75) {
            //GD.Print("fail3");
            return false;
        }
        if (branchesToEnd > length * 0.5) {
            //GD.Print("fail4");
            return false;
        }
        return true;
    }

    private bool ValidMazePrims() {
        if (branches > length * 4) {
            //GD.Print("fail2");
            return false;
        }
        if (branchesToEnd > length * 0.6) {
            //GD.Print("fail3");
            return false;
        }
        return true;
    }

    public void SetTime(ulong time) {
        this.time = time;
    }

    private void LengthToEnd() {
        length = PathLength.LengthToEnd();
    }

    private void NumberOfBranches() {
        branches = Branches.NumberOfBranches();
    }

    private void NumberBranchesToEnd() {
        branchesToEnd = BranchesToEnd.NumberOfBranches();
    }

    private void Size() {
        size = MazeSize.Size();
    }

    private void FindDeadPaths() {
        deadPaths = DeadPaths.FindDeadPaths();
    }
}
