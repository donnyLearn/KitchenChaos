using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI orderDeliveredText;

	private void Start() {
		KitchenGameManager.Instance.OnStateChanged += GameKitchenManager_OnStateChanged;
		Hide();
	}

	private void GameKitchenManager_OnStateChanged(object sender, System.EventArgs e) {
		if (KitchenGameManager.Instance.IsGameOver()) {
			Show();
			orderDeliveredText.text = DeliveryManager.Instance.GetSuccessfulOrderAmount().ToString();
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
