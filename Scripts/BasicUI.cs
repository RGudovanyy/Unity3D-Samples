// Сценарий отображающий интерфейс. Используется устаревший механизм создания GUI
// назначается объекту Controller

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicUI : MonoBehaviour
{
	void OnGUI(){
		int posX = 10;
		int posY = 10;
		int width = 100;
		int height = 100;
		int buffer = 100;

		List<string> itemList = Managers.Inventory.GetItemList();
		if(itemList.Count == 0){
			GUI.Box (new Rect(posX,posY, width, height), "No Items");
		}
		foreach(string item in itemList){
			int count = Managers.Inventory.GetItemCount(item);
			Texture2D image = Resources.Load<Texture2D>("Icons/"+item); // метод загружает ресурсы из папки Resources
			GUI.Box (new Rect(posX,posY,width,height), new GUIContent("(" + count + ")", image));
			posX += width+buffer; // при каждом прохождении цикла сдвигаемся в сторону
		}
		string equipped = Managers.Inventory.equippedItem;
		if(equipped != null){ // отображение подготовленного элемента
			posX = Screen.width - (width+buffer);
			Texture2D image = Resources.Load("Icons/"+equipped) as Texture2D;
			GUI.Box (new Rect(posX,posY,width,height), new GUIContent("Equipped", image));
		}
		posX = 10;
		posY = height + buffer;

		foreach(string item in itemList){ // просмотр всех элементов в цикле для создания кнопок
			if(GUI.Button (new Rect(posX,posY, width,height),"Equip " + item)){ // запуск вложенного кода при щелчке на кнопке
				Managers.Inventory.EquipItem(item);
			}
			// добавление кнопки здоровья в базовый UI
			if(item == "health") {
				// запуск вложенного кода при щелчке на кнопке
				if(GUI.Button (new Rect(posX, posY + height + buffer, width, height), "Use Health")){  
					Managers.Inventory.ConsumeItem("health");
					Managers.Player.ChangeHealth(25);
				}
			}

			posX += width + buffer;
		}
	
	}

}

