using UnityEngine;
using System.Collections;

public class DeviceTrigger : MonoBehaviour
{
	// Список целевых объектов, которые будет активировать триггер
	[SerializeField] private GameObject[] targets;

	public bool requireKey;

	// Метод вызывается при попадании объекта в зону триггера
	void OnTriggerEnter(Collider other){
		if(requireKey && Managers.Inventory.equippedItem != "key"){			
			return;
		}
		foreach (GameObject target in targets){
			target.SendMessage ("Activate");

		}
	}

	// Метод вызывается когда объект покидает зону триггера
	void OnTriggerExit(Collider other){
		foreach(GameObject target in targets){
			target.SendMessage("Deactivate");
		}
	}
	
}

