using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelInitializer : MonoBehaviour {

	public float spawnZoneRadius = 5.0f;
	[SerializeField]
	private GameObject panelPlayerControlsVerticalLayout;
	[SerializeField]
	private GameObject panelPlayerControl;
	[SerializeField]
    private GameObject playerPrefab;
	private Transform panelHorizontalLayout;

	// Start is called before the first frame update
	void Start() {
		panelHorizontalLayout = GameObject.Find("Canvas").transform.Find("Panel Controls Display").Find("Panel Horizontal Layout");
		foreach (PlayerConfiguration pc in PlayerConfigurationManager.Instance.GetPlayerConfigs()) {
			pc.playerInput.SwitchCurrentActionMap("InGame");
			SpawnPlayerPrefab(pc);
			PrepareControlsDisplayPanel(pc);
		}
    }

    // Update is called once per frame
    void Update() {
        
    }

	//Spawns a player prefab
	private void SpawnPlayerPrefab(PlayerConfiguration pc) {
		GameObject player = Instantiate(playerPrefab, GetRandomSpawnPosition(), Quaternion.identity, gameObject.transform);
		player.GetComponent<PlayerInputHandler>().InitializePlayer(pc);
		pc.avatar = player;
	}

	//Returns a random spawn position inside the spawn zone
	private Vector3 GetRandomSpawnPosition() {
		Vector3 position = new Vector3(Random.Range(-spawnZoneRadius, spawnZoneRadius), playerPrefab.GetComponent<MeshRenderer>().bounds.size.y / 2f, Random.Range(-spawnZoneRadius, spawnZoneRadius));
		return position;
	}

	//Prepares for each player their controls display panel in the canvas, populating it with their specific set of controls
	private void PrepareControlsDisplayPanel(PlayerConfiguration pc) {
		//Adds a gameObject with a vertical layout group
		GameObject verticalLayoutContainer = Instantiate(panelPlayerControlsVerticalLayout, panelHorizontalLayout);
		verticalLayoutContainer.GetComponent<Image>().color = pc.material.color;
		verticalLayoutContainer.transform.Find("Text Title").GetComponent<TextMeshProUGUI>().text = "Player " + (pc.playerInput.playerIndex + 1).ToString() + " - " + pc.playerInput.currentControlScheme;
		//Adds every controls of the player in the vertical layout group
		Transform verticalLayoutPanel = verticalLayoutContainer.transform.Find("Panel Vertical Layout");
		foreach (InputAction ia in pc.playerInput.currentActionMap) {
			GameObject playerControlContainer = Instantiate(panelPlayerControl, verticalLayoutPanel);
			//Sets the name of the control
			playerControlContainer.transform.Find("Text Control Name").gameObject.GetComponent<TextMeshProUGUI>().text = ia.name;
			//Sets the button for the control
			if (ia.GetBindingDisplayString() != null && ia.GetBindingDisplayString() != "") {
				playerControlContainer.transform.Find("Text Control Button").gameObject.GetComponent<TextMeshProUGUI>().text = ia.GetBindingDisplayString();
			} else if (pc.playerInput.currentControlScheme == "Gamepad") {
				playerControlContainer.transform.Find("Text Control Button").gameObject.GetComponent<TextMeshProUGUI>().text = ia.controls[0].parent.displayName;
			} else {
				int bindingIndex = ia.GetBindingIndex(group: pc.playerInput.currentControlScheme) - 1;
				if (bindingIndex >= 0) {
					playerControlContainer.transform.Find("Text Control Button").gameObject.GetComponent<TextMeshProUGUI>().text = ia.GetBindingDisplayString(bindingIndex);
				} else {
					Debug.LogWarning("The binding for the action " + ia.name + " could not be found.");
				}
			}
		}
	}
}
