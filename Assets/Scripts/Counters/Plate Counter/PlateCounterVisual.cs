using UnityEngine;
using System.Collections.Generic;
public class PlateCounterVisual : MonoBehaviour {
	[SerializeField] private PlateCounter plateCounter;
	[SerializeField] private Transform counterTopPoint;
	[SerializeField] private Transform plateVisualPrefab;

	private List<GameObject> plateVisualGameObectList;

	private void Awake() {
		plateVisualGameObectList = new List<GameObject>();
	}

	private void Start() {
		plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
		plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;

	}
	private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e) {
		Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
		float plateOffsetY = 0.1f;
		plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObectList.Count, 0);
		plateVisualGameObectList.Add(plateVisualTransform.gameObject);
	}

	private void PlateCounter_OnPlateRemoved(object sender, System.EventArgs e) {
		GameObject topPlate = plateVisualGameObectList[plateVisualGameObectList.Count - 1];
		plateVisualGameObectList.Remove(topPlate);
		Destroy(topPlate);
	}

}
