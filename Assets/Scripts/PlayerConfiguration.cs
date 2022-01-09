using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfiguration {

	public PlayerInput playerInput { get; private set; }
	public bool isReady { get; set; }
	public Material material { get; set; }

	//Constructor
	public PlayerConfiguration(PlayerInput pi) {
		playerInput = pi;
	}
}