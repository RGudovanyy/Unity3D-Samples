using UnityEngine;
using System.Collections;

public class FireTrapManager : MonoBehaviour
{

	[SerializeField] private GameObject[] trapParts;
	// Активация механизма деактивации ловушки :)
	public void Activate(){
		foreach( GameObject part in trapParts)
			Destroy (part);
	}

	public void Deactivate(){
		return;
	}

}

