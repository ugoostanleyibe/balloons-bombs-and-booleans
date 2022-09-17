using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
	public bool isGameOver;
	public bool isLowEnough;

	public float floatForce;

	private readonly float gravityModifier = 1.5f;
	private readonly float upperCeiling = 14.5f;

	public ParticleSystem explosionParticle;
	public ParticleSystem fireworksParticle;
	public AudioClip explodeSound;
	public AudioClip bounceSound;
	public AudioClip moneySound;

	private AudioSource playerAudio;
	private Rigidbody playerRb;

	// Start is called before the first frame update
	void Start()
	{
		Physics.gravity *= gravityModifier;
		playerAudio = GetComponent<AudioSource>();
		playerRb = GetComponent<Rigidbody>();

		// Apply a small upward force at the start of the game
		playerRb.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
	}

	// Update is called once per frame
	void Update()
	{
		isLowEnough = transform.position.y < upperCeiling;

		// While space is pressed and player is low enough, float up
		if (Input.GetKey(KeyCode.Space) && isLowEnough && !isGameOver)
		{
			playerRb.AddForce(Vector3.up * floatForce);

		}
		else if (!isLowEnough)
		{
			playerRb.AddForce(Vector3.down * floatForce);
			playerRb.velocity = Vector3.zero;
		}
	}

	private void DestroyPlayer() => Destroy(gameObject);

	private void OnCollisionEnter(Collision other)
	{
		// if player collides with bomb, explode and set gameOver to true
		if (other.gameObject.CompareTag("Bomb"))
		{
			playerAudio.PlayOneShot(explodeSound);
			Invoke(nameof(DestroyPlayer), 1.0f);
			Destroy(other.gameObject);
			explosionParticle.Play();
			Debug.Log("Game Over!");
			isGameOver = true;
		}

		// if player collides with money, fireworks
		else if (other.gameObject.CompareTag("Money"))
		{
			playerAudio.PlayOneShot(moneySound);
			Destroy(other.gameObject);
			fireworksParticle.Play();
		}

		// if player collides with ground, bounce
		else if (other.gameObject.CompareTag("Ground"))
		{
			playerRb.AddForce(Vector3.up * 7.0f, ForceMode.Impulse);
		}
	}
}
