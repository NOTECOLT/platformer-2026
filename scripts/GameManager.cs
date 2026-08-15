using Godot;
using System;

public partial class GameManager : Node {
    private int _state = 0;
    private double _secondsPerBeat;
    private AudioStreamPlayer _audioStream;

    /// <summary>
    /// Ensures the signal is emitted once per beat (and only once)
    /// </summary>
    private bool _signalSent = false;

    [Export]
    public int totalStates = 2;

    /// <summary>
    /// Triggers events that rely on the beat of the song toggling the state
    /// </summary>
    /// <param name="state">The current new state after a measure is finished. Goes from 0 to totalStates - 1</param>
    [Signal]
    public delegate void BeatToggleEventHandler(int state);

    public override void _Ready() {
        base._Ready();

        _audioStream = GetNode<AudioStreamPlayer>("AudioStreamPlayer");

        // Calculated by 60 seconds divided by beats per minute (song is at 90 bpm)
        _secondsPerBeat = 60d / 90d;
        _state = 0;
    }

    public override void _Process(double delta) {
        // Switches states and emits beat toggle signal every fourth beat
        if (getCurrentBeat() % 4 == 3 && !_signalSent) {
            _state = (_state + 1) % totalStates;
            EmitSignal(SignalName.BeatToggle, _state);
            _signalSent = true;
        }
        
        // Resets signal sent flag on next beat
        if (getCurrentBeat() % 4 == 0) {
            _signalSent = false;
        }
    }

    /// <summary>
    /// Gets the current beat of the song
    /// </summary>
    /// <returns>Integer value with first beat at 0</returns>
    private int getCurrentBeat() {
        double currentSongTime = _audioStream.GetPlaybackPosition() + AudioServer.GetTimeSinceLastMix();
        currentSongTime -= AudioServer.GetOutputLatency();
        return (int)Math.Floor(currentSongTime / _secondsPerBeat) + 0;
    }
}
