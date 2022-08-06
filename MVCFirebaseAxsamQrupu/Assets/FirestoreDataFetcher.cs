using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;
using System;
using UnityEngine.UI;

public class FirestoreDataFetcher : MonoBehaviour
{
  [SerializeField] private TMP_Text userNameText;

  [SerializeField] private TMP_Text userNickNameText;

	[SerializeField] private string userName;

	[SerializeField] private string userNickName;

	[SerializeField] private Button setButton;

	private FirebaseFirestore firestoreInstance;

	private string playerLoginCollectionPath = "PlayerLoginData";

	private void Start()
	{
		firestoreInstance = FirebaseFirestore.DefaultInstance;
	}

	private void GetDataFromDB()
	{
		DocumentReference playerDocRef = firestoreInstance.
			Collection(playerLoginCollectionPath).Document("VaqifQuluzada");

		playerDocRef.GetSnapshotAsync().ContinueWithOnMainThread(
			task => 
		{
			if (task.Result.Exists)
			{
				FSPlayerData playerData = task.Result.ConvertTo<FSPlayerData>();

				userNameText.text = playerData.Name;

				userNickNameText.text = playerData.NickName;
			}
			else
			{
				Debug.Log("Data not found");
			}
		
		});
	}

	public void SetDataToDB()
	{
		DocumentReference playerDocRef = firestoreInstance.
			Collection(playerLoginCollectionPath).Document("VaqifQuluzada");

		FSPlayerData playerData = new FSPlayerData 
		{ 
		Name=userName,
		NickName=userNickName
		};

		playerDocRef.SetAsync(playerData);
	}
}
