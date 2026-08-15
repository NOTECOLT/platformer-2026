using Godot;
using System;

public partial class TimedToggle : TileMapLayer {
	private GameManager _gameManager;
	private TileMapLayer _tileMapLayer;
	private CanvasItem _canvasItem;

	[Export]
	public int onState;
	public override void _Ready() {
		_gameManager = GetNode<GameManager>("%GameManager");
		_tileMapLayer = GetNode<TileMapLayer>(".");
		_canvasItem = GetNode<CanvasItem>(".");
		_gameManager.BeatToggle += toggleState;
	}


	public override void _Process(double delta) {
	}

	/// <summary>
	/// Is called at the end of every timer cycle. Used to change the state of the blocks
	/// </summary>
	/// <param name="state"></param>
	private void toggleState(int state) {
		bool isToggled = state == onState;
		_tileMapLayer.CollisionEnabled = isToggled;

		if (isToggled) {
			_canvasItem.Modulate = new Color(1, 1, 1, 1);
		} else {
			_canvasItem.Modulate = new Color(0.5f, 0.5f, 0.5f, 0.5f);
		}
	}
}
