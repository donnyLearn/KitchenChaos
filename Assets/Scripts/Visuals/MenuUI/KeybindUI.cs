using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KeybindUI : MonoBehaviour {
	public static KeybindUI Instance { get; private set; }

	[SerializeField] private Button moveUpButton;
	[SerializeField] private Button moveDownButton;
	[SerializeField] private Button moveLeftButton;
	[SerializeField] private Button moveRightButton;
	[SerializeField] private Button interactButton;
	[SerializeField] private Button interactAlternativeButton;
	[SerializeField] private Button pauseButton;

	[SerializeField] private TextMeshProUGUI moveUpButtonText;
	[SerializeField] private TextMeshProUGUI moveDownButtonText;
	[SerializeField] private TextMeshProUGUI moveLeftButtonText;
	[SerializeField] private TextMeshProUGUI moveRightButtonText;
	[SerializeField] private TextMeshProUGUI interactButtonText;
	[SerializeField] private TextMeshProUGUI interactAlternativeButtonText;
	[SerializeField] private TextMeshProUGUI pauseButtonText;

	[SerializeField] private Button closeButton;
	[SerializeField] private Transform pressToRebindTransform;
	private Action onCloseButtonAction;

	private void Awake() {
		Instance = this;
		moveUpButton.onClick.AddListener(() => {
			RebindBinding(GameInput.Binding.Move_Up);
		});
		moveDownButton.onClick.AddListener(() => {
			RebindBinding(GameInput.Binding.Move_Down);
		});
		moveLeftButton.onClick.AddListener(() => {
			RebindBinding(GameInput.Binding.Move_Left);
		});
		moveRightButton.onClick.AddListener(() => {
			RebindBinding(GameInput.Binding.Move_Right);
		});
		interactButton.onClick.AddListener(() => {
			RebindBinding(GameInput.Binding.Interact);
		});
		interactAlternativeButton.onClick.AddListener(() => {
			RebindBinding(GameInput.Binding.Interact_Alternate);
		});
		pauseButton.onClick.AddListener(() => {
			RebindBinding(GameInput.Binding.Pause);
		});

		closeButton.onClick.AddListener(() => {
			Hide();
			onCloseButtonAction();
		});
	}

	private void Start() {
		KitchenGameManager.Instance.OnUnpause += KitchenGameManager_OnUnpause; ;
		UpdateVisual();
		HidePressToRebindScreen();
		Hide();
	}


	private void UpdateVisual() {
		moveUpButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
		moveDownButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
		moveLeftButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
		moveRightButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
		interactButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
		interactAlternativeButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
		pauseButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
	}

	private void RebindBinding(GameInput.Binding binding) {
		ShowPressToRebindScreen();
		GameInput.Instance.RebindBinding(binding, () => {
			HidePressToRebindScreen();
			UpdateVisual();
		});
	}



	public void Show(Action onCloseButtonAction) {
		this.onCloseButtonAction = onCloseButtonAction;

		gameObject.SetActive(true);
		moveUpButton.Select();
	}
	private void Hide() {
		gameObject.SetActive(false);
	}
	private void ShowPressToRebindScreen() {
		pressToRebindTransform.gameObject.SetActive(true);
	}
	private void HidePressToRebindScreen() {
		pressToRebindTransform.gameObject.SetActive(false);
	}

	private void KitchenGameManager_OnUnpause(object sender, System.EventArgs e) {
		Hide();
	}







}
