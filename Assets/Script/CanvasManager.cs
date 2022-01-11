using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasManager : MonoBehaviour {

	[SerializeField]
	private GameObject panelReadyPrefab;
	private GameObject panelInfomation;
	private GameObject panelGridlayout;

	// Start is called before the first frame update
	void Start() {
		PlayerConfigurationManager.Instance.playerJoinedEvent.AddListener(OnJoin);
		panelInfomation = transform.Find("Panel Background").Find("Panel Information").gameObject;
		panelGridlayout = transform.Find("Panel Background").Find("Panel Gridlayout").gameObject;
		panelInfomation.transform.Find("Text Minimum").GetComponent<TextMeshProUGUI>().text = "Minimum: " + PlayerConfigurationManager.Instance.minPlayerCount + " player(s)";
		panelInfomation.transform.Find("Text Maximum").GetComponent<TextMeshProUGUI>().text = "Maximum: " + PlayerConfigurationManager.Instance.GetComponent<PlayerInputManager>().maxPlayerCount + " player(s)";
	}

    // Update is called once per frame
    void Update() {
        
    }

	//Called when a join action is triggered
	public void OnJoin(PlayerConfiguration playerConfiguration) {
		GameObject panelReady = Instantiate(panelReadyPrefab, panelGridlayout.transform);
		panelReady.GetComponent<PanelReady>().Initialize(playerConfiguration);
		panelInfomation.SetActive(false);
		panelGridlayout.SetActive(true);
	}
}
