using UnityEngine;

public class WaterTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == MyTags.PLAYER_TAG) {
			GameManager.instance.PlayerFellInWater ();
		}
	}

} // class