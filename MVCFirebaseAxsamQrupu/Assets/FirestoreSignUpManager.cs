using DynamicBox.EventManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using DynamicBox.UIEvents;

namespace DynamicBox.Managers
{
	public class FirestoreSignUpManager : MonoBehaviour
	{

		private FirebaseFirestore firestoreInstance;

		private void Start()
		{
			firestoreInstance = FirebaseFirestore.DefaultInstance;
		}

		private void OnEnable()
		{
			EventManager.Instance.AddListener<OnSignUpButtonPressedEvent>(OnSignUpButtonPressedEventHandler);
		}

		private void OnDisable()
		{
			EventManager.Instance.RemoveListener<OnSignUpButtonPressedEvent>(OnSignUpButtonPressedEventHandler);
		}


		#region Event Handlers

		private void OnSignUpButtonPressedEventHandler(OnSignUpButtonPressedEvent eventDetails)
		{
			Query playerDataQuery = firestoreInstance.Collection("PlayerLoginData").
				WhereEqualTo("NickName",eventDetails.PlayerData.NickName);

			playerDataQuery.GetSnapshotAsync().ContinueWithOnMainThread(task=> 
			{

				if (task.Result.Count > 0)
				{
					EventManager.Instance.Raise(new OnSignUpFailEvent());
				}
				else
				{
					EventManager.Instance.Raise(new OnSignUpSuccessEvent());
					DocumentReference newPlayerDocRef = firestoreInstance.Collection("PlayerLoginData").
					Document(eventDetails.PlayerData.NickName);

					newPlayerDocRef.SetAsync(eventDetails.PlayerData);
				}


			}
			);




		}


		#endregion

	}
}

