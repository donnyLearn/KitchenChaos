using UnityEngine;
using UnityEngine.UI;

public class GamePlayingTimerUI : MonoBehaviour {
    [SerializeField] private Image timerImage;

	private void Update() {
		timerImage.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
	}
}
