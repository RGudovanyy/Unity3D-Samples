using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(LevelManager))]

public class Managers : MonoBehaviour
{
	public static PlayerManager Player {get; private set;}
	public static InventoryManager Inventory {get; private set;}
	public static AudioManager Audio {get; private set;}
	public static LevelManager Level {get; private set;}

	// список диспетчеров, который просматривается в цикле во время стартовой последовательности
	private List<IGameManager> _startSequence;


	// метод выводит подпоследовательность запуска, а затем запускает сопрограмму, начинающую работу всех диспетчеров
	void Awake(){
		Audio = GetComponent<AudioManager>();
		Player = GetComponent<PlayerManager>();
		Inventory = GetComponent<InventoryManager>();
		Level = GetComponent<LevelManager>();
		_startSequence = new List<IGameManager>();
		_startSequence.Add(Player);
		_startSequence.Add(Inventory);
		_startSequence.Add(Audio);
		_startSequence.Add(Level);
		// асинхронно загружаем стартовую последовательность
		StartCoroutine (StartupManagers());
	}

	// функция в цикле просматривает список диспетчеров и для каждого из них вызывает метод Startup.
	// затем она входит в цикл, проверяющий, загрузился ли каждый из диспетчеров, и ожидает завершение этого процесса
	// после этого уведомляет нас о загрузке и завершает свою работу	

	private IEnumerator StartupManagers(){
		foreach(IGameManager manager in _startSequence){
			manager.Startup();
		}
		yield return null;

		int numModules = _startSequence.Count;
		int numReady = 0;

		while(numReady < numModules){ // продолжаем цикл, пока не начнут работать все диспетчеры
			int lastReady = numReady;
			numReady = 0;

			foreach(IGameManager manager in _startSequence){
				if(manager.status == ManagerStatus.Started){
					numReady++;
				}
			}

			if(numReady > lastReady){
				Debug.Log ("Progress: " + numReady + "/" + numModules);
				yield return null; //остановка на один кадр перед проверкой
			}
		}
	}


}

