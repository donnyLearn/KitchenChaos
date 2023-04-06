using UnityEngine;
using System.Collections;

public class LoaderCallback : MonoBehaviour {
	private float waitTillLoad = .1f;
	private void Start() {
		StartCoroutine(StartLoad());
	}

	private IEnumerator StartLoad() {
		yield return new WaitForSeconds(waitTillLoad);
		Loader.LoaderCallback();
	}
}
