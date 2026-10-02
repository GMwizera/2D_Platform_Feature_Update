using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CountdownTimer : MonoBehaviour {

	public TextMeshProUGUI timerText;
	public float startTime = 180f;
	public float warningTime = 30f;
	public Color warningColor = Color.red;
	public string endSceneName = "EndScene";

	private float timeLeft;
	private bool finished;

	void Start() {
		timeLeft = startTime;
		UpdateTimerText();
	}

	void Update() {
		if (finished) {
			return;
		}

		timeLeft -= Time.deltaTime;

		if (timeLeft <= 0f) {
			timeLeft = 0f;
			finished = true;
            UpdateTimerText();
            EndMenuController.SetResult("Time's Up!", "You ran out of time.", new Color(1f, 0.6f, 0f));
			SceneManager.LoadScene(endSceneName);
			return;
		}

		UpdateTimerText();
	}

	void UpdateTimerText() {
		int totalSeconds = Mathf.CeilToInt(timeLeft);
		timerText.text = string.Format("{0:00}:{1:00}", totalSeconds / 60, totalSeconds % 60);

		if (timeLeft <= warningTime) {
			timerText.color = warningColor;
		}
	}

} // class