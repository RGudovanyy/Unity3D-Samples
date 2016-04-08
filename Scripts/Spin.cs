﻿using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

	public float speed = 3.0f; // Объявление глобальной переменной для скорости вращения

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (0,speed,0, Space.World); // 0 - вращение вдоль оси Х(перекат вбок), speed - вращение вдоль оси У, 0 - вращение 
									  // вдоль оси Z(кувырок).
	
	}
}
