using UnityEngine;
using TMPro;

public class StartCountdownUI : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI countdownText;
	private Animator animator;
	private int prevNum;

	private const string COUNTDOWN_POPUP = "countdownPopup";

	private void Awake() {
		animator = GetComponent<Animator>();
	}

	private void Start() {
		KitchenGameManager.Instance.OnStateChanged += GameKitchenManager_OnStateChanged;
		prevNum = 99;
		Hide();
	}

	private void GameKitchenManager_OnStateChanged(object sender, System.EventArgs e) {
		if(KitchenGameManager.Instance.IsCountdownToStart()) {
			Show();
		} else {
			Hide();
		}
	}

	private void Update() {
		int countdownTimer = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountDownToStartTimer());
		if(prevNum != countdownTimer) {
			animator.SetTrigger(COUNTDOWN_POPUP);
			SoundManager.Instance.PlayCountdownSound();
			countdownText.text = countdownTimer.ToString();
			prevNum = countdownTimer;
		}
	}
	private void Show() {
		gameObject.SetActive(true);
	}
	private void Hide() {
		gameObject.SetActive(false);
	}

}
