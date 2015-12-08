using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour {

	#region Public Fields & Properties
	public float time = 8f;
	#endregion
	
	#region System Methods
	
	// Update is called once per frame
	private void Update()
	{
		time -= Time.deltaTime;
		if (time < 0f) {
			Destroy(gameObject);
		}
		
	}
	#endregion
}
