using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerJoinedEvent : UnityEvent<PlayerConfiguration> {
}

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerConfigurationManager : MonoBehaviour {

	public static PlayerConfigurationManager Instance { get; private set; }
	public int minPlayerCount = 1;
	public PlayerJoinedEvent playerJoinedEvent;

	[SerializeField]
	private List<Material> materials;
	private List<PlayerConfiguration> playerConfiguationsList;

	private void Awake() {
		if (Instance != null) {
			Debug.Log("[Singleton] Trying to instantiate a seccond instance of a singleton class.");
		} else {
			Instance = this;
			DontDestroyOnLoad(Instance);
			playerConfiguationsList = new List<PlayerConfiguration>();
		}
	}

	// Start is called before the first frame update
	void Start() {

	}

    // Update is called once per frame
    void Update() {
		
	}

	//Triggered when a player joins the game
	public void OnJoin(PlayerInput playerInput) {
		if (!playerConfiguationsList.Any(p => p.playerInput.playerIndex == playerInput.playerIndex)) {
			playerInput.transform.parent = this.gameObject.transform;
			PlayerConfiguration playerConfiguration = new PlayerConfiguration(playerInput, GetRandomMaterial());
			playerConfiguationsList.Add(playerConfiguration);
			playerJoinedEvent.Invoke(playerConfiguration);
			Debug.Log("Player " + (playerConfiguration.playerInput.playerIndex + 1) + " has joined");
		}
	}

	//Returns the list of player configurations
	public List<PlayerConfiguration> GetPlayerConfiguationsList() {
		return playerConfiguationsList;
	}

	//Called to notify that a player has pressed his ready button
	public void PlayerIsReady(PlayerConfiguration playerConfiguration) {
		Debug.Log("Player " + (playerConfiguration.playerInput.playerIndex + 1) + " is ready");
		playerConfiguration.isReady = true;
		if (playerConfiguationsList.Count >= minPlayerCount && playerConfiguationsList.All(p => p.isReady == true)) {
			GetComponent<PlayerInputManager>().enabled = false;
			SceneManager.LoadScene("GameScene");
		}
	}

	//Returns a random material
	private Material GetRandomMaterial() {
		if (materials.Count > 0) {
			int materialIndex = Random.Range(0, materials.Count);
			Material material = materials[materialIndex];
			materials.RemoveAt(materialIndex);
			return material;
		} else {
			return new Material(Shader.Find("Specular"));
		}
	}
}
