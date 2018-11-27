using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("SkythianCat/Glowing Forest/WaterGroup")]

public class WaterGroup : MonoBehaviour {
	
	public float waveSpeed;
	public Vector2 waveDirection;
	/// <summary>
	/// Name of each water wave plain that child for WaterGroup script.
	/// </summary>
	public string nameToFind;
	/// <summary>
	/// List that contains all water wave plains after game start.
	/// </summary>
	private List<GameObject> waterWavePlains = new List<GameObject>();


	void Start(){
		if(nameToFind == null || nameToFind == ""){
			Debug.LogError (gameObject.name + " | nameToFind is null.");
		}

		//Find all transforms that child for this script
		Transform[] allChildTransforms = GetComponentsInChildren<Transform> ();

		//Find transforms in allChildTransforms that have nameToFind name and add their to waterWavePlains
		foreach(Transform t in allChildTransforms){
			if(t.name == nameToFind){
				waterWavePlains.Add (t.gameObject);
			}
		}

		if(waterWavePlains.Count == 0){
			Debug.LogError (gameObject.name + " contains no one GameObject with \"" + nameToFind + "\" name.");
		}
	}

	void LateUpdate () {
		if(waterWavePlains.Count != 0){
			foreach(GameObject g in waterWavePlains){
				WaveAnimation (g);
			}
		}
	}


	/// <summary>
	/// Changes texture offset of waterWavePlain.
	/// </summary>
	/// <param name="waterWavePlain">Water wave plain GameObject.</param>
	public void WaveAnimation(GameObject waterWavePlain){
		if(waterWavePlain != null){
			float dirX = Time.time * waveSpeed * waveDirection.x;
			float dirY = Time.time * waveSpeed * waveDirection.y;
			waterWavePlain.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(dirX, dirY));
		}
	}

}
