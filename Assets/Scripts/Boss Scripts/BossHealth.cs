using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour {

	private Animator anim;
	private int health = 1;

	private bool canDamage;

	void Awake () {
		anim = GetComponent<Animator> ();
		canDamage = true;
	}

	IEnumerator WaitForDamage()
	{
		yield return new WaitForSeconds(2f);
		canDamage = true;
	}
		IEnumerator LoadVictory() {
		yield return new WaitForSeconds (2f);
		SceneManager.LoadScene ("EndScene");
	}
	
	void OnTriggerEnter2D(Collider2D target) {
		if (canDamage) {
			if (target.tag == MyTags.BULLET_TAG) {
				health--;
				canDamage = false;

if (health == 0) {
					GetComponent<BossScript>().DeactivateBossScript();
					anim.Play("BossDead");

					EndMenuController.SetResult("You Win!", "You defeated the boss!", new Color(0.26f, 0.63f, 0.28f));
					StartCoroutine (LoadVictory ());
				}

				StartCoroutine (WaitForDamage ());
			}
		}
	}

} // class



































