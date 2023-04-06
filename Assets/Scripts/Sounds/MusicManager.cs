using UnityEngine;

public class MusicManager : MonoBehaviour {
	public static MusicManager Instance { get; private set; }

	private AudioSource audioSource;

	// volume
	private float volume = .3f;
	private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";


	private void Awake() {
		Instance = this;
		audioSource = GetComponent<AudioSource>();
		volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, .3f);
		audioSource.volume = volume;
	}

	public float GetVolume() {
		return volume;
	}
	public void SetVolume(float volume) {
		this.volume = volume;
		audioSource.volume = volume;
		PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
		PlayerPrefs.Save();
	}
}
