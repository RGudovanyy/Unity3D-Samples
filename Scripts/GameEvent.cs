// Сценарий используется системой диспетчеров для отслеживания изменений
using UnityEngine;
using System.Collections;

public static class GameEvent
{
	public const string ENEMY_HIT = "ENEMY_HIT";

	public const string HEALTH_UPDATED = "HEALTH_UPDATED";
	public const string GAME_COMPLETE = "GAME_COMPLETE";
	public const string GAME_FAILED = "GAME_FILED";
}

