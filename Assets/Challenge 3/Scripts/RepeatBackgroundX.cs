using UnityEngine;

public class RepeatBackgroundX : MonoBehaviour
{
	private float repeatWidth;
	
	private Vector3 startPos;

	private void Start()
	{
		repeatWidth = GetComponent<BoxCollider>().size.x / 2; // Set repeat width to half of the background
		startPos = transform.position; // Establish the default starting position 
	}

	private void Update()
	{
		// If background moves left by its repeat width, move it back to start position
		if (transform.position.x < startPos.x - repeatWidth)
		{
			transform.position = startPos;
		}
	}
}
