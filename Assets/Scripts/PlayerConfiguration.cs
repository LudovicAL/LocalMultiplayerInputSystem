using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfiguration {

	public PlayerInput Input { get; private set; }
	public int PlayerIndex { get; private set; }
	public bool isReady { get; set; }
	public Material color { get; set; }

	//Constructor
	public PlayerConfiguration(PlayerInput pi) {
		PlayerIndex = pi.playerIndex;
		Input = pi;
	}
}