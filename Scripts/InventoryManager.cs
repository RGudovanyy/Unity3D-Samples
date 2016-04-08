using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class InventoryManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status{get; private set;}
	public string equippedItem{get; private set;}
	private Dictionary<string, int> _items;

	public void Startup(){
		Debug.Log ("Inventory manager starting...");
		_items = new Dictionary<string, int >();
		status = ManagerStatus.Started; // для задач с долгим временем используем состояние Initializing
	}

	private void DisplayItems(){
		string itemDisplay = "Items: ";
		foreach(KeyValuePair<string, int> item in _items){
			itemDisplay += item.Key + "(" + item.Value + ") ";
		}
		Debug.Log (itemDisplay);
	}

	public void AddItem(string name){
		if(_items.ContainsKey(name)){
			_items[name] += 1;
		}else{
			_items[name] = 1;
		}
		DisplayItems();
	}


	public List<string> GetItemList(){
		List<string> list  = new List<string>(_items.Keys); // Возвращаем список всех ключей словаря
		return list;
	}
	
	public int GetItemCount(string name){
		if(_items.ContainsKey(name)){
			return _items[name];
		}
		return 0;
	}

	// метод экипировки вещи из инвентаря
	public bool EquipItem(string name){
		if(_items.ContainsKey(name) && equippedItem != name){
			equippedItem = name;
			Debug.Log("Equipped: " + name);
			return true;
		}
		equippedItem = null;
		Debug.Log("Unequipped");
		return false;
	}

	// метод для использования элемента
	public bool ConsumeItem(string name){
		if(_items.ContainsKey (name)){ // проверка наличия элемента в инвентаре
			_items[name]--;
			if(_items[name] == 0) { // удаление записи, если количество становится равным нулю
				_items.Remove(name);
			}
		} else { // реакция в случае отсутствия в инвентаре нужного элемента
			Debug.Log ("cannot consume " + name);
			return false;
		}
		DisplayItems();
		return true;
	}

}

