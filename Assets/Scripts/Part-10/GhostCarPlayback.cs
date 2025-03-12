using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GhostCarPlayback : Monobehavior
{
	//local variable
	GhostCarData ghostCarData = new GhostCarData();
	List<GhostCarDataListItem> ghostCarDataList = new List<GhostCarDataListItem>();

	//playback index
	int currentPlaybackIndex = 0;



	//playback information
	float lastStoredTime = 0.1f;
	Vector2 lastStoredPosition = Vector2.zero;
	float lastStoredRotation = 0;
	Vector3 lastStoredlocalScale=Vector3.zero;

	//duration
	float duration = 0.1f;

	void Start()
	{

	
	}

	void Update()
	{
		//we can only playback if we have data
		if (ghostCarDataList.Count == 0)
			return;
		if(Time.timeSinceLevelLoad >= ghostCarDataList[currentPlaybackIndex].timeSinceLevelLoaded)
		{
			//update playback information
			lastStoredTime=ghostCarDataList[currentPlaybackIndex].lastSinceLevelLoaded;
			lastStoredPosition=ghostCarDataList[currentPlaybackIndex].position;
			lastStoredRotation=ghostCarDataList[currentPlaybackIndex].rotationZ;
			lastStoredlocalScale=ghostCarDataList[currentPlaybackIndex].localScale;

			//update car position
			if (currentPlaybackIndex < ghostCarDataList.Count - 1 )
				currentPlaybackIndex++;
			
			duration = ghostCarDataList[currentPlaybackIndex].timeSinceLevelLoaded - lastStoredTime;
		}

		//calculate how much of data frame that completed
		float timePassed = Time.timeSinceLevelLoad - lastStoredTime;
		float lerpPercentage = timePassed / duration;

		//lerp everything
		transform.position = Vector2.Lerp(lastStoredPosition, ghostCarDataList[currentPlaybackIndex].position, lerpPercentage);
		transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, lastStoredRotation), Quaternion.Euler(0, 0, ghostCarDataList[currentPlaybackIndex].rotationZ), lerpPercentage);
		transform.localScale = Vector3.Lerp(lastStoredlocalScale, ghostCarDataList[currentPlaybackIndex].localScale, lerpPercentage);

	}


	public void LoadData(int playerNumber)
	{
		if(!PlayerPrefs.HasKey($"{SceneManager.GetActiveScene().name}_{playerNumber}_ghost"))
			Destroy(this.gameObject);
		else
		{
			string jsonEncodedData = PlayerPrefs.GetString($"{SceneManager.GetActiveScene().name}_{playerNumber}_ghost");
			
			
			ghostCarData = JsonUtility.FromJson<GhostCarData>(jsonEncodedData);


			ghostCarDataList = ghostCarData.GetDataList();
		}
	}

}