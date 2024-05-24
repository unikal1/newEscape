using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mental : MonoBehaviour
{
	public float sanity = 100.0f;
	public float maxSanity = 100.0f;
	public float minSanity = 0.0f;

	public float sanityDrainRate = 0.1f;
	public float sanityGainRate = 0.1f;
	public float sanityDrainRateMultiplier = 1.0f;
	public float sanityGainRateMultiplier = 1.0f;

	private void Update()
	{
		if (sanity > minSanity)
		{
			sanity -= sanityDrainRate * sanityDrainRateMultiplier * Time.deltaTime;
		}  else {
			// game over
		}
	}

	public void ReduceSanity(float amount) {

	}

	public void GainSanity() { 
		
	}

}
