using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
	public GameObject[] objectPrefabs;

	private readonly float spawnInterval = 1.5f;
	private readonly float spawnDelay = 2;

	private PlayerControllerX playerControllerScript;

	// Start is called before the first frame update
	void Start()
	{
		playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
		InvokeRepeating(nameof(SpawnObjects), spawnDelay, spawnInterval);
	}

	// Spawn obstacles
	void SpawnObjects()
	{
		// Set random spawn location and random object index
		Vector3 spawnLocation = new(30.0f, Random.Range(5, 15), 0.0f);
		int index = Random.Range(0, objectPrefabs.Length);

		// If game is still active, spawn new object
		if (!playerControllerScript.isGameOver)
		{
			Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
		}
	}
}
