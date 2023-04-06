using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {
	public static OptionsUI Instance { get; private set; }

	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider sfxSlider;
	[SerializeField] private TextMeshProUGUI musicSliderText;
	[SerializeField] private TextMeshProUGUI sfxSliderText;
	[SerializeField] private Button keybindButton;
	[SerializeField] private Button closeButton;

	private Action onCloseButtonAction;

	private void Awake() {
		Instance = this;
		musicSlider.onValueChanged.AddListener((float value) => {
			MusicManager.Instance.SetVolume(value/100f);
			UpdateVisual();
		});
		sfxSlider.onValueChanged.AddListener((float value) => {
			SoundManager.Instance.SetVolume(value/100f);
			UpdateVisual();
		});
		keybindButton.onClick.AddListener(() => {
			Hide();
			KeybindUI.Instance.Show(ShowAgain);
		});
		closeButton.onClick.AddListener(() => {
			Hide();
			onCloseButtonAction();
		});
	}

	private void Start() {
		KitchenGameManager.Instance.OnUnpause += KitchenGameManager_OnUnpause;
		musicSlider.value = MusicManager.Instance.GetVolume() * 100;
		sfxSlider.value = SoundManager.Instance.GetVolume() * 100;
		UpdateVisual();
		Hide();
	}

	private void KitchenGameManager_OnUnpause(object sender, System.EventArgs e) {
		Hide();
	}

	private void UpdateVisual() {
		musicSliderText.text = Mathf.Round(MusicManager.Instance.GetVolume() * 100f).ToString();
		sfxSliderText.text = Mathf.Round(SoundManager.Instance.GetVolume() * 100f).ToString();
	}

	public void ShowAgain() {
		gameObject.SetActive(true);
		closeButton.Select();
	}
	public void Show(Action onCloseButtonAction) {
		this.onCloseButtonAction = onCloseButtonAction;

		gameObject.SetActive(true);
		musicSlider.Select();
	}
	private void Hide() {
		gameObject.SetActive(false);
	}

}
