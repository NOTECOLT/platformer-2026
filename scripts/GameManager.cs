using Godot;
using System;

public partial class GameManager : Node {
    private int _state = 0;
    private double _secondsPerBeat = 1;
    private AudioStreamPlayer _audioStream;

    /// <summary>
    /// Ensures the signal is emitted once per beat (and only once)
    /// </summary>
    private bool _signalSent = false;

    [Export]
    public int totalStates = 2;

    /// <summary>
    /// Fires whenever the timer finishes a cycle
    /// </summary>
    /// <param name="state">The current new state after a timer cycle is finished. Goes from 0 to totalStates - 1</param>
    [Signal]
    public delegate void BeatToggleEventHandler(int state);

    public override void _Ready() {
        base._Ready();

        _audioStream = GetNode<AudioStreamPlayer>("AudioStreamPlayer");

        _secondsPerBeat = 60d / 90d;
        _state = 0;
    }

    public override void _Process(double delta) {
        // Switches states and emits beat toggle signal every fourth beat
        if (getCurrentBeat() % 4 == 3 && !_signalSent) {
            GD.Print($"New State {_state}");
            _state = (_state + 1) % totalStates;
            EmitSignal(SignalName.BeatToggle, _state);
            _signalSent = true;
        }
        
        // Resets signal sent flag on next beat
        if (getCurrentBeat() % 4 == 0) {
            _signalSent = false;
        }
    }

    private int getCurrentBeat() {
        double currentSongTime = _audioStream.GetPlaybackPosition() + AudioServer.GetTimeSinceLastMix();
        currentSongTime -= AudioServer.GetOutputLatency();
        return (int)Math.Floor(currentSongTime / _secondsPerBeat) + 0;
    }
}
