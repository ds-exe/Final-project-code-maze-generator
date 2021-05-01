using Godot;
using System;

public class Goal : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    [Signal]
    public delegate void finished();

    public override void _Ready()
    {
        
    }

    public void _on_Goal_body_entered(Node body) {
        if (body.GetType() == typeof(Maze)) {
            return;
        }
        EmitSignal(nameof(finished));
    }

    public void Reset(int x, int y) {
        Visible = true;
        Position = new Vector2(x * 32 + 16, y * 32 + 16);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
