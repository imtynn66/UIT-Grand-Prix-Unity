using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCars : MonoBehaviour
{
    public GameObject[] carPrefabs; // Gán trong Inspector

    void Awake()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform spawnPoint = spawnPoints[i].transform;

            int playerSelectedCarID = PlayerPrefs.GetInt($"P{i + 1}SelectedCarID", 0); // Default là 0

            if (playerSelectedCarID < carPrefabs.Length)
            {
                GameObject playerCar = Instantiate(carPrefabs[playerSelectedCarID], spawnPoint.position, spawnPoint.rotation);

                // Nếu có CarInputHandler
                CarInputHandler inputHandler = playerCar.GetComponent<CarInputHandler>();
                if (inputHandler != null)
                {
                    inputHandler.playerNumber = i + 1;
                }
            }
            else
            {
                Debug.LogWarning($"Invalid car ID: {playerSelectedCarID} for player P{i + 1}");
            }
        }
    }
}
