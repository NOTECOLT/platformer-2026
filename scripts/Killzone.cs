using Godot;
using System;

public partial class Killzone : Node {
    private Timer _timer;

    public override void _Ready() {
        base._Ready();

        _timer = GetNode<Timer>("Timer");
    }


    public void OnBodyEntered(Node2D body) {
        _timer.Start();
    }

    public void OnTimerTimeout() {
        GetTree().ReloadCurrentScene();
    }
}
