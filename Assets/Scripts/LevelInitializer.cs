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
			//pc.playerInput.SwitchCurrentActionMap("InGame");
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
	}

	//Returns a random spawn position inside the spawn zone
	private Vector3 GetRandomSpawnPosition() {
		return new Vector3(Random.Range(-spawnZoneRadius, spawnZoneRadius), -0.0f, Random.Range(-spawnZoneRadius, spawnZoneRadius));
	}

	private void PrepareControlsDisplayPanel(PlayerConfiguration pc) {
		GameObject verticalLayoutContainer = Instantiate(panelPlayerControlsVerticalLayout, panelHorizontalLayout);
		verticalLayoutContainer.GetComponent<Image>().color = pc.material.color;
		verticalLayoutContainer.transform.Find("Text Title").GetComponent<TextMeshProUGUI>().text = "Player " + pc.playerInput.playerIndex;
		Transform verticalLayoutPanel = verticalLayoutContainer.transform.Find("Panel Vertical Layout");
		foreach (InputAction ia in pc.playerInput.currentActionMap) {
			GameObject playerControlContainer = Instantiate(panelPlayerControl, verticalLayoutPanel);
			playerControlContainer.transform.Find("Text Control Name").gameObject.GetComponent<TextMeshProUGUI>().text = ia.name;
			string controlName = "";
			if (ia.controls.Count == 4 && pc.playerInput.currentControlScheme == "Gamepad") {
				controlName = ia.controls[0].parent.displayName;
				InputControl pc2 = ia.controls[0].parent;
			} else if (ia.controls.Count > 1) {
				bool firstPass = true;
				foreach (InputControl ic in ia.controls) {
					if (!firstPass) {
						controlName += " | ";
					}
					controlName += ic.displayName;
					firstPass = false;
				}
			} else {
				controlName = ia.controls[0].displayName;
			}
			playerControlContainer.transform.Find("Text Control Button").gameObject.GetComponent<TextMeshProUGUI>().text = controlName;
		}
	}
}
