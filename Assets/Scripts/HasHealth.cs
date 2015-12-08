using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour {

	public float hitPoints = 100f;

	public void ReceiveDamage(float amt) {
		hitPoints -= amt;
		if (hitPoints <= 0) {
			Die();
		}
	}

	private void Die() {
		Destroy (gameObject);
	}
}
