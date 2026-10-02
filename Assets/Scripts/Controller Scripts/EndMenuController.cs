using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenuController : MonoBehaviour {

	private const string DEFAULT_TITLE = "Game Over";
	private const string DEFAULT_MESSAGE = "You ran out of lives!";

	private static string resultTitle = DEFAULT_TITLE;
	private static string resultMessage = DEFAULT_MESSAGE;

	public TextMeshProUGUI titleText;
    public TextMeshProUGUI messageText;
    private static Color resultColor = new Color(0.9f, 0.22f, 0.21f);

	public static void SetResult(string title, string message) {
		resultTitle = title;
		resultMessage = message;
	}

		void Start() {
		if (titleText != null) {
			titleText.text = resultTitle;
			titleText.color = resultColor;
		}
		if (messageText != null) messageText.text = resultMessage;

		// reset so the next game over shows the normal message
		resultTitle = DEFAULT_TITLE;
		resultMessage = DEFAULT_MESSAGE;
		resultColor = new Color(0.9f, 0.22f, 0.21f);
	}

    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene-ALU");
    }
    	public static void SetResult(string title, string message, Color color) {
		resultTitle = title;
		resultMessage = message;
		resultColor = color;
	}

	public void QuitGame() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

} // class