using UnityEngine;

public class BurnWarningUI : MonoBehaviour {
	[SerializeField] private StoveCounter stoveCounter;

	private void Start() {
		stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
		Hide();
	}

	private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
		float burnThreshold = .5f;
		bool showUI = stoveCounter.IsCooked() && e.progressNormalized > burnThreshold;
		if (showUI) {
			Show();
		} else {
			Hide();
		}
	}

	private void Show() {
		gameObject.SetActive(true);
	}
	private void Hide() {
		gameObject.SetActive(false);
	}

}
