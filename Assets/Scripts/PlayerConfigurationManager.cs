using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour {

	public static PlayerConfigurationManager Instance { get; private set; }

	[SerializeField]
	private int minPlayers = 1;
	private List<PlayerConfiguration> playerConfigs;    

    void Awake() {
        if(Instance != null) {
            Debug.Log("[Singleton] Trying to instantiate a seccond instance of a singleton class.");
        } else {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

	private void Start() {
		//The minimum number of players required can not be higher than the maximum number allowed to join
		int maxPlayers = this.GetComponent<PlayerInputManager>().maxPlayerCount;
		if (minPlayers > maxPlayers) {
			minPlayers = maxPlayers;
			Debug.LogWarning("The minPlayers value was higher than the maxPlayers value. The minPlayers value was changed for that of the maxPlayers value.");
		}
		TextMeshProUGUI tmpg = GameObject.Find("Text Instruction").GetComponent<TextMeshProUGUI>();
		if (tmpg) {
			tmpg.text = "Press a button to join\nMinimum " + minPlayers + " player(s)\nMaximum " + maxPlayers + " player(s)";
		}
	}

	public void HandlePlayerJoin(PlayerInput pi) {
        Debug.Log("Player " + pi.playerIndex + " has joined.");
        pi.transform.SetParent(transform);
		//If the provided playerIndex is not taken yet
		if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex)) {
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigs() {
        return playerConfigs;
    }

    public void SetPlayerColor(int index, Material mat) {
        playerConfigs[index].color = mat;
    }

    public void ReadyPlayer(int index) {
        playerConfigs[index].isReady = true;
        if (playerConfigs.Count >= minPlayers && playerConfigs.All(p => p.isReady == true)) {
            SceneManager.LoadScene("GameScene");
        }
    }
}