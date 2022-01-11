using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour {

	[SerializeField]
	private GameObject avatarPrefab;

	private void Awake() {
		foreach (PlayerConfiguration playerConfiguration in PlayerConfigurationManager.Instance.GetPlayerConfiguationsList()) {
			playerConfiguration.playerInput.SwitchCurrentActionMap("Play");
			SpawnAvatar(playerConfiguration);
			CreatePanelControl(playerConfiguration);
		}
	}

	// Start is called before the first frame update
	void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

	//Creates a player's controls info panel
	private void CreatePanelControl(PlayerConfiguration playerConfiguration) {
		GameObject.Find("Canvas").GetComponent<CanvasInfoManager>().CreatePanelControl(playerConfiguration);
	}

	//Spawns and initialize a player's avatar
	private void SpawnAvatar(PlayerConfiguration playerConfiguration) {
		Vector3 randomSpawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range((Screen.width / 10) * 3, (Screen.width / 10) * 7), Random.Range((Screen.height / 10) * 3, (Screen.height / 10) * 7), -Camera.main.transform.position.z));
		GameObject avatar = Instantiate(avatarPrefab, randomSpawnPosition, Quaternion.identity);
		avatar.GetComponent<AvatarController>().Initialize(playerConfiguration);
	}
}
