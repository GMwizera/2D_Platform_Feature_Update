using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public GameObject mainButtons;
	public GameObject settingsPanel;
	public Slider volumeSlider;
	public Toggle muteToggle;

	void Start() {
		CloseSettings();

		float volume = PlayerPrefs.GetFloat("Volume", 1f);
		bool muted = PlayerPrefs.GetInt("Muted", 0) == 1;

		if (volumeSlider != null) volumeSlider.SetValueWithoutNotify(volume);
		if (muteToggle != null) muteToggle.SetIsOnWithoutNotify(muted);

		AudioListener.volume = muted ? 0f : volume;
	}

	public void PlayGame() {
		SceneManager.LoadScene("GameScene-ALU");
	}

	public void OpenSettings() {
		if (mainButtons != null) mainButtons.SetActive(false);
		if (settingsPanel != null) settingsPanel.SetActive(true);
	}

	public void CloseSettings() {
		if (settingsPanel != null) settingsPanel.SetActive(false);
		if (mainButtons != null) mainButtons.SetActive(true);
	}

	public void SetVolume(float volume) {
		PlayerPrefs.SetFloat("Volume", volume);
		if (muteToggle == null || !muteToggle.isOn) AudioListener.volume = volume;
	}

	public void SetMute(bool muted) {
		PlayerPrefs.SetInt("Muted", muted ? 1 : 0);
		AudioListener.volume = muted ? 0f : PlayerPrefs.GetFloat("Volume", 1f);
	}

	public void QuitGame() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

} 