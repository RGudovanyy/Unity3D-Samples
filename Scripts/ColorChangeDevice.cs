using UnityEngine;
using System.Collections;

public class ColorChangeDevice : MonoBehaviour
{
	public void Operate(){
		// числа представляют собой RGB значения в диапазоне от 0 до 1
		Color random = new Color(Random.Range(0f,1f),Random.Range(0f,1f), Random.Range(0f,1f));
		GetComponent<Renderer>().material.color = random; // цвет задается в назначенном объекту материале
	}

}

