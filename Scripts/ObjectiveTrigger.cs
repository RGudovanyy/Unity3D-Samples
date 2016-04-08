using UnityEngine;
using System.Collections;

public class ObjectiveTrigger : MonoBehaviour
{
	
	public void Operate(){
		if (Managers.Inventory.equippedItem == "key")
			Managers.Level.ReachObjective ();
		else
			Debug.Log("You need a key to open this door");
	}
		


}

