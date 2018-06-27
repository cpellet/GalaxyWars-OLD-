using UnityEngine;

public class WeaponSwitching : MonoBehaviour {

	public int activeSlot = 0;

	// Use this for initialization
	void Start () {
		SelectSlot();
	}
	
	// Update is called once per frame
	void Update () {

		int previousSelectedSlot = activeSlot;

		//Scroll wheel support
		if (Input.GetAxis("Mouse ScrollWheel") > 0f){
			if (activeSlot >= transform.childCount -1){
				activeSlot = 0;
			}else{
				activeSlot++;
			}
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0f){
			if (activeSlot <= 0){
				activeSlot = transform.childCount -1;
			}else{
				activeSlot--;
			}
		}

		//Numeric keys support
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			activeSlot = 0;
		}

		if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2){
			activeSlot = 1;
		}

		if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3){
			activeSlot = 2;
		}

		if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4){
			activeSlot = 3;
		}

		if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5){
			activeSlot = 4;
		}

		//If changed, update active slot
		if (previousSelectedSlot != activeSlot){
			SelectSlot();
		}
	}

	//Enable and disable slots
	void SelectSlot(){
		int i = 0;
		foreach (Transform slot in transform){
			if (i == activeSlot){
				slot.gameObject.SetActive(true);
			}else{
				slot.gameObject.SetActive(false);
			}
			i++;
		}
	}
}
