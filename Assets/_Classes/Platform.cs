﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Platform : MonoBehaviour {

	public bool isOccupied = false;
	public int occupiedShipNumber;
	public int platformNumber;
	public PlatformButton myButton;
	public float removalTime = 2.0f;

	private ShipGenerator shipGen;
	private Ship ship;

	public void OccupyPlatform () {
		shipGen = GameObject.FindGameObjectWithTag ("GameController").GetComponent<ShipGenerator> ();
		ship = shipGen.GetFirstShipInList ();
		ship.myPlatform = this;
		ship.StartCoroutine ("MoveTo");
	}

	public void ShipGetOff () {
		Debug.Log ("Ship№" + occupiedShipNumber + " got off!");
		ship.StartCoroutine ("MoveToExit");
		occupiedShipNumber = 0;
		Debug.Log ("Cleaning up the place");
	}

	IEnumerator ShipOnPlatformCountodown () {
		yield return new WaitForSeconds (removalTime);
		if (occupiedShipNumber != 0) {
			ShipGetOff ();
		}
	}

	public void RemoveShipFromQueue () {
		shipGen.RemoveFirstShipInList ();
		occupiedShipNumber = ship.thisShipNumber;
		Debug.Log ("Platform №" + platformNumber + " is occupied with ship №" + occupiedShipNumber);
	}

}
