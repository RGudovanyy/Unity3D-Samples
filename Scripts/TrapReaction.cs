using UnityEngine;
using System.Collections;

public class TrapReaction : MonoBehaviour
{
	[SerializeField] public Animation ani;

	void Reaction(Collider other){
		if(other.gameObject.name == "Player" && !ani.isPlaying)
			ani.Play ("Arma_spiketrapAction");
		Debug.Log ("PLayer on trap!");	
	}

	void OnTriggerEnter(Collider other){
		Reaction(other);	
	}

	void OnTriggerStay(Collider other){
		Reaction(other);
	}
}

