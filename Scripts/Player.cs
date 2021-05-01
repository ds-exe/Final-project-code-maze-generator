using Godot;
using System;

public class Player : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    const int FRICTION = 5000;
    [Export] int MAX_SPEED = 250;
    const int ROLL_SPEED = 120;
    const int ACCELERATION = 5000;
    private Vector2 velocity;
    private int zoomLevel = 0;
    private bool zoomEnabled = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void Reset() {
        GetNode<Camera2D>("Camera").Current = true;
        Visible = true;
        Position = new Vector2(48, 80);
    }

    public void DisableZoom() {
        zoomEnabled = false;
        zoomLevel = 0;
        GetNode<Camera2D>("Camera").Zoom = new Vector2(0.5f, 0.5f);
    }

    public void EnambleZoom() {
        zoomEnabled = true;
        zoomLevel = 0;
        GetNode<Camera2D>("Camera").Zoom = new Vector2(0.5f, 0.5f);
    }

    public override void _Process(float delta)
    {
        if (zoomEnabled && Input.IsActionJustPressed("zoom_toggle")) {
            if (zoomLevel == 0) {
                GetNode<Camera2D>("Camera").Zoom = new Vector2(1f, 1f);
            } else if (zoomLevel == 1) {
                GetNode<Camera2D>("Camera").Zoom = new Vector2(1.5f, 1.5f);
            } else if (zoomLevel == 2) {
                GetNode<Camera2D>("Camera").Zoom = new Vector2(2f, 2f);
            } else if (zoomLevel == 3) {
                GetNode<Camera2D>("Camera").Zoom = new Vector2(2.5f, 2.5f);
            } else if (zoomLevel == 4) {
                GetNode<Camera2D>("Camera").Zoom = new Vector2(3f, 3f);
            } else if (zoomLevel == 5) {
                GetNode<Camera2D>("Camera").Zoom = new Vector2(3.5f, 3.5f);
            } else if (zoomLevel == 6) {
                GetNode<Camera2D>("Camera").Zoom = new Vector2(4f, 4f);
            }
            zoomLevel++;
            if (zoomLevel > 7) {
                GetNode<Camera2D>("Camera").Zoom = new Vector2(0.5f, 0.5f);
                zoomLevel = 0;
            }
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        var inputVector = Vector2.Zero;
        inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

        if (inputVector != Vector2.Zero) {
            velocity = velocity.MoveToward(inputVector.Normalized() * MAX_SPEED, ACCELERATION * delta);
        } else {
            velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
        }
        Move();
    }

    private void Move() {
        velocity = MoveAndSlide(velocity);
    }
}
