using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	private GameObject spawnItem;
	private int randNum;
	private int catNum;
	private List<int> itemPool = new List<int>();
	public Material holoSpawn;
	GameObject itemDrop;
	public int itemDropScale;
	public float spawnVerticalOffset;
	private Renderer rnd;
	private Vector3 spwnPos;
	public ItemCategory[] spawnCategory;
	public SpawnPoint[] spwnPoints;

	void Start () {
		for (int i = 0; i < spwnPoints.Length; i++){
			chooseClass(spwnPoints[i].type.ToString());
			if (spwnPoints[i].isUsedOnce == true){
				Spawn(spawnItem, spwnPoints[i]);
			}else{
				StartCoroutine(spawnCycle(spwnPoints[i].resupplyDelay, spawnItem, spwnPoints[i]));
			}
		}
	}
	
	void chooseClass(string cat){
		if (cat == "Weapons"){
			catNum = 0;
			chooseItem();
		}else if (cat == "Healing"){
			catNum = 1;
			chooseItem();
		}else if (cat == "Specials"){
			catNum = 2;
			chooseItem();
		}else if (cat == "All"){
			for (int y = 0; y < spawnCategory.Length; y++){
				chooseItem();
			}
		}
	}

	void chooseItem(){
		for (int i = 0; i < spawnCategory[catNum].item.Length; i++){
			for (int x = 0; x < spawnCategory[catNum].item[i].spawnMultiplier; x++){
				itemPool.Add(i);
			}
		}
		randNum = (int)(Random.Range(0, itemPool.Count));
		spawnItem = spawnCategory[catNum].item[itemPool[randNum]].prefab;
	}

	public IEnumerator spawnCycle(int time, GameObject spawnItem, SpawnPoint point){
		//Check if an object is still present on top of spawn
		if (point.full == false){
			Spawn(spawnItem, point);
			yield return new WaitForSeconds(time);
		}else{
			yield return new WaitForSeconds(time);
		}
	}

	public IEnumerator rotate(GameObject obj){
		obj.transform.Rotate(Vector3.up,1);
        yield return new WaitForSeconds (0.01f);
		StartCoroutine(rotate(obj));
	}

	void Spawn(GameObject spawnItem, SpawnPoint point){
		spwnPos = point.obj.transform.position + new Vector3(0, spawnVerticalOffset, 0);
		itemDrop = (GameObject) Instantiate(spawnItem, spwnPos, point.obj.transform.rotation);
		itemDrop.transform.localScale = new Vector3(itemDropScale, itemDropScale, itemDropScale);
		Transform[] allChildren = itemDrop.GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			if(child){
				rnd = child.GetComponent<Renderer>();
				if (rnd)
					rnd.material = holoSpawn;
			}
		}
		StartCoroutine(rotate(itemDrop));
	}
}
