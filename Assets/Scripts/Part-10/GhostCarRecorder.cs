using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Maneagement;

public class GhostCarRecorder : Monobehavior
{
	public Transform CarSpiteObject;


	//local variable
	GhostCarData ghostCarData = new GhostCarData();

	bool isRecording = true;

	//other components
	Rigidbody2D carRigidbody2D;
	CarInputHandler carInputHandler;



	private void Awake()
	{
		carRigidbody2D = GetComponent<Rigidbody2D>();
		carInputHandler = GetComponent<CarInputHandler>();
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		StartCoroutine(RecordCarPosionCO());
		StartCoroutine(SaveCarPosionCO());
	}

	//Mỗi 0.15s sẽ add 1 keyframme vào dataitemlist
	IEnumrator RecordCarPosionCO()
	{
		while (isRecording)
		{
			if (CarSpiteObject != null)
			{
				ghostCarData.AddDataItem(new GhostCarDataListItem(carRigidbody2D.position, carRigidbody2D.rotation, CarSpiteObject.localScale, Time.timeSinceLevelLoad));
			}

			yield return new WaitForSeconds(0.15f);
		}
	}

	IEnumrator SaveCarPosionCO()
	{
		yield return new WaitForSeconds(5);
	SaveData();
	}
	void SaveData()
	{
		//save data to file
		string jsonEncodedData = JsonUtility.ToJson(ghostCarData);

		Debug.Log($"Save data: {jsonEncodedData}");

		if(carInputHandler != null)
		{
			PlayerPrefs.SetString($"{SceneManager.GetActiveScene().name}_{carInputHandler.playerNumber}_ghost", jsonEncodedData);
			PlayerPrefs.Save();
		}

		isRecording = false;
	}
}