using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfiguration {

	public PlayerInput playerInput { get; private set; }
	public Material material { get; private set; }
	public bool isReady { get; set; }
	public GameObject avatar { get; set; }

	//Constructor
	public PlayerConfiguration(PlayerInput pi, Material material) {
		playerInput = pi;
		this.material = material;
	}
}