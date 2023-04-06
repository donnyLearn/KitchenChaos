using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Button playButton;
	[SerializeField] private Button quitButton;

	private void Awake() {
		playButton.onClick.AddListener(() => {
			Debug.Log("Click start button " + Time.realtimeSinceStartup);
			Loader.Load(Loader.Scene.GameScene);
		});
		quitButton.onClick.AddListener(() => {
			Application.Quit();
		});
	}
}
