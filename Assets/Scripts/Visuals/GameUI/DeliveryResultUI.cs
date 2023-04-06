using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryResultUI : MonoBehaviour {
	[SerializeField] private Image backgroundImage;
	[SerializeField] private Image iconImage;
	[SerializeField] private TextMeshProUGUI messageText;
	[SerializeField] private Color succesColor;
	[SerializeField] private Color failedColor;
	[SerializeField] private Sprite successSprite;
	[SerializeField] private Sprite failedSprite;

	private Animator animator;
	private const string DELIVERY_RESULT_POPUP = "deliveryResultPopup";
	private void Awake() {
		animator = GetComponent<Animator>();
	}
	private void Start() {
		DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
		DeliveryManager.Instance.OnDeliveryFail += DeliveryManager_OnDeliveryFail;
		gameObject.SetActive(false);
	}

	private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e) {
		gameObject.SetActive(true);
		animator.SetTrigger(DELIVERY_RESULT_POPUP);
		backgroundImage.color = succesColor;
		iconImage.sprite = successSprite;
		messageText.text = "DELIVERY\nSUCCESS";
	}

	private void DeliveryManager_OnDeliveryFail(object sender, System.EventArgs e) {
		gameObject.SetActive(true);
		animator.SetTrigger(DELIVERY_RESULT_POPUP);
		backgroundImage.color = failedColor;
		iconImage.sprite = failedSprite;
		messageText.text = "DELIVERY\nFAILED";
	}
}
