using System.Collections.Generic;
using UnityEngine;

public class FindGameObjects : MonoBehaviour {

	private static FindGameObjects instance;

	public static FindGameObjects Instance {
		get {
			if (instance == null) {
				instance = new FindGameObjects();
			}
			return instance;
		}
	}

	public static List<GameObject> ContainsString(string searchString) {
		List<GameObject> matchingObjects = new List<GameObject>();
		GameObject[] allObjects = FindObjectsOfType<GameObject>();

		foreach (GameObject obj in allObjects) {
			if (obj.name.Contains(searchString)) {
				matchingObjects.Add(obj);
			}
		}

		return matchingObjects;
	}
}