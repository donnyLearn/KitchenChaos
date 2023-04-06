using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {
	[SerializeField] private Button resumeButton;
	[SerializeField] private Button optionsButton;
	[SerializeField] private Button mainMenuButton;


	private void Awake() {
		resumeButton.onClick.AddListener(() => {
			KitchenGameManager.Instance.TogglePause();
		});
		optionsButton.onClick.AddListener(() => {
			Hide();
			OptionsUI.Instance.Show(Show);
		});
		mainMenuButton.onClick.AddListener(() => {
			Time.timeScale = 1;
			Loader.Load(Loader.Scene.MainMenuScene);
		});
	}

	private void Start() {
		KitchenGameManager.Instance.OnPause += KitchenGameManager_OnPause;
		KitchenGameManager.Instance.OnUnpause += KitchenGameManager_OnUnpause;
		Hide();
	}

	private void KitchenGameManager_OnPause(object sender, System.EventArgs e) {
		Show();
	}
	private void KitchenGameManager_OnUnpause(object sender, System.EventArgs e) {
		Hide();
	}

	private void Show() {
		gameObject.SetActive(true);
		resumeButton.Select();
	}
	private void Hide() {
		gameObject.SetActive(false);
	}


}
