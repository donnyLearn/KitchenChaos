using UnityEngine;

public class StoveCounterSound : MonoBehaviour {

	[SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;

	private float warningSoundTimer;
	private float warningSoundTimerMax = .2f;
	private bool playWarningSound;

	private void Awake() {
		audioSource = GetComponent<AudioSource>();
	}
	private void Start() {
		stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
		stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
	}

	private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
		float burnThreshold = .5f;
		playWarningSound = stoveCounter.IsCooked() && e.progressNormalized > burnThreshold;
		if(playWarningSound) {

		}
	}

	private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
		if(e.state == StoveCounter.CookingState.COOKING || e.state == StoveCounter.CookingState.COOKED) {
			audioSource.Play();
		} else {
			audioSource.Pause();
		}
	}

	private void Update() {
		if(playWarningSound) {
			warningSoundTimer -= Time.deltaTime;
			if(warningSoundTimer <= 0f) {
				warningSoundTimer = warningSoundTimerMax;
				SoundManager.Instance.PlayBurningWarningSound(stoveCounter.transform.position);
			}
		}
	}
}
