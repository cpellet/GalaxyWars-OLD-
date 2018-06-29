using UnityEngine;

[System.Serializable]
public class SpawnPoint{
	public string name;
	public Transform obj;
	public enum spawnType{ Weapons, Healing, Specials, All }
	public spawnType type;
	public int resupplyDelay;
	public bool isUsedOnce;
	[HideInInspector]
	public bool full;
}
