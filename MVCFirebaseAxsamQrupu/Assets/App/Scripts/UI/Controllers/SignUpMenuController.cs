using UnityEngine;
using DynamicBox.UIViews;
using DynamicBox.EventManagement;
using DynamicBox.UIEvents;
namespace DynamicBox.UIControllers
{
	public class SignUpMenuController : MonoBehaviour
	{
		[SerializeField] private SignUpMenuView view;

		#region Unity Events

		private void OnEnable()
		{
			EventManager.Instance.AddListener<OnSignUpSuccessEvent>(OnSignUpSuccessEventHandler);
			EventManager.Instance.AddListener<OnSignUpFailEvent>(OnSignUpFailEventHandler);
		}

		private void OnDisable()
		{
			EventManager.Instance.RemoveListener<OnSignUpSuccessEvent>(OnSignUpSuccessEventHandler);
			EventManager.Instance.RemoveListener<OnSignUpFailEvent>(OnSignUpFailEventHandler);
		}

		#endregion



		public void OnSignUpButtonPressed(FSPlayerData playerData)
		{
			EventManager.Instance.Raise(new OnSignUpButtonPressedEvent(playerData));
		}


		#region Event Handlers

		private void OnSignUpSuccessEventHandler(OnSignUpSuccessEvent eventDetails)
		{
			StartCoroutine(view.ShowNotification("Signed up successfully",NotificationType.SUCCESS));
		}

		private void OnSignUpFailEventHandler(OnSignUpFailEvent eventDetails)
		{
			StartCoroutine(view.ShowNotification("Same Username Error", NotificationType.FAIL));
		}

		#endregion

	}
}

