using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public PlayerMovement player;
	public float respawnHeight = 0.5f;

	private PlayerDamage playerDamage;
	private Rigidbody2D playerBody;

	void Awake() {
		instance = this;
	}

	void Start() {
		playerDamage = player.GetComponent<PlayerDamage>();
		playerBody = player.GetComponent<Rigidbody2D>();
	}

	public void PlayerFellInWater() {
		// PlayerDamage removes a life, updates LifeText and loads EndScene on the last life
		playerDamage.DealDamage();

		// PlayerDamage freezes time when lives run out, so only respawn while the game is still running
		if (Time.timeScale > 0f) {
			RespawnPlayer();
		}
	}

	void RespawnPlayer() {
		Vector3 respawnPosition = player.LastGroundedPosition;
		respawnPosition.y += respawnHeight;

		playerBody.linearVelocity = Vector2.zero;
		playerBody.position = respawnPosition;
		player.transform.position = respawnPosition;
	}

} // class