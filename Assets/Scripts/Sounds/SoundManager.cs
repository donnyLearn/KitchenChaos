using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance { get; private set; }

	[SerializeField] private AudioClipRefsSO audioClipRefsSO;

	// volume
	private float volume = 1f;
	private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume"; 

	private void Awake() {
		Instance = this;

		volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
	}

	private void Start() {
		DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
		DeliveryManager.Instance.OnDeliveryFail += DeliveryManager_OnDeliveryFail;
		CuttingCounter.OnCutAny += CuttingCounter_OnAnyCut;
		Player.Instance.OnPickUp += Player_OnPickUp;
		BaseCounter.OnItemPlacedAny += BaseCounter_OnItemPlacedAny;
		TrashCounter.OnTrashAny += TrashCounter_OnTrashAny;
	}

	public float GetVolume() {
		return volume;
	}
	public void SetVolume (float volume) {
		this.volume = volume;
		PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
		PlayerPrefs.Save();
	}
	private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f) {
		PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier);
	}

	private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) {
		AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
	}

	private void TrashCounter_OnTrashAny(object sender, System.EventArgs e) {
		TrashCounter trashCounter = sender as TrashCounter;
		PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
	}

	private void BaseCounter_OnItemPlacedAny(object sender, System.EventArgs e) {
		Debug.Log(transform.position);
		BaseCounter baseCounter = sender as BaseCounter;
		PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
	}

	private void Player_OnPickUp(object sender, System.EventArgs e) {
		PlaySound(audioClipRefsSO.objectPick, Player.Instance.transform.position);
	}

	private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e) {
		CuttingCounter cuttingCounter = sender as CuttingCounter;
		PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
	}

	private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e) { 
		PlaySound(audioClipRefsSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
	}

	private void DeliveryManager_OnDeliveryFail(object sender, System.EventArgs e) {
		PlaySound(audioClipRefsSO.deliveryFail, DeliveryCounter.Instance.transform.position);
	}

	public void PlayFootstepSound(Vector3 position, float volume = 1f) {
		PlaySound(audioClipRefsSO.footstep, position, volume);
	}

	public void PlayCountdownSound() {
		PlaySound(audioClipRefsSO.warning, Vector3.zero, volume);
	}
	public void PlayBurningWarningSound(Vector3 position) {
		PlaySound(audioClipRefsSO.warning, position, volume);
	}


}
