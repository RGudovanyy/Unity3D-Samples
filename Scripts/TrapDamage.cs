using UnityEngine;
using System.Collections;

public class TrapDamage : MonoBehaviour
{
	public int damage = 20;
	public int multply = 1;

	void OnTriggerStay(Collider other){
		PlayerCharacter player = other.GetComponent<PlayerCharacter> ();
		if(player != null){
			Managers.Player.ChangeHealth (-damage * multply);
	}
}
}