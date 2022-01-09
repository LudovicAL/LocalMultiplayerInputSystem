using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelInitializer : MonoBehaviour {
	public float spawnZoneRadius = 5.0f;
	[SerializeField]
    private GameObject playerPrefab;

    // Start is called before the first frame update
    void Start() {
        PlayerConfiguration[] playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0, max = playerConfigs.Length; i < max; i++) {
            GameObject player = Instantiate(playerPrefab, GetRandomSpawnPosition(), Quaternion.identity, gameObject.transform);
            player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

	private Vector3 GetRandomSpawnPosition() {
		return new Vector3(Random.Range(-spawnZoneRadius, spawnZoneRadius), -0.0f, Random.Range(-spawnZoneRadius, spawnZoneRadius));
	}
}
