using UnityEngine;
using System.Collections;

public class DoorOpenDevice : MonoBehaviour
{
	[SerializeField] private Vector3 dPos; // Смещение применяемое при открывании двери
	private bool _open;
	public bool requireKey;

	public void Operate(){
		// Открываем или закрываем дверь, в зависимости от ее состояния
			if (!_open) {
			if (requireKey && Managers.Inventory.equippedItem == "key") {
				Vector3 pos = transform.position + dPos;
				transform.position = pos;
				Managers.Inventory.ConsumeItem (Managers.Inventory.equippedItem);
			} else if (!requireKey) {
				Vector3 pos = transform.position + dPos;
				transform.position = pos;
			}
			} else {
				
				Vector3 pos = transform.position - dPos;
				transform.position = pos;

		}
		_open = !_open;
	}

	public void Activate(){
		if(!_open){
			Vector3 pos = transform.position + dPos;
			transform.position = pos;
			_open = true;
		}
	}

	public void Deactivate(){
		if(_open){
			Vector3 pos = transform.position - dPos;
			transform.position = pos;
			_open = false;
		}
	}

}

