using UnityEngine;
using DynamicBox.UIControllers;
using TMPro;
using UnityEngine.UI;
using System.Collections;

namespace DynamicBox.UIViews
{
	public class SignUpMenuView : MonoBehaviour
	{
		[Header("Controller reference")]
		[SerializeField] private SignUpMenuController controller;

		[Header("View properties")]
		[SerializeField] private TMP_InputField nameIF;

		[SerializeField] private TMP_InputField nickNameIF;

		[SerializeField] private TMP_InputField passIF;

		[SerializeField] private TMP_InputField confirmPassIF;

		[SerializeField] private TMP_Text notificationText;

		[SerializeField] private Button signUpButton;

		public void OnInputFieldChanged()
		{
			if(string.IsNullOrEmpty(nameIF.text)||string.IsNullOrEmpty(nickNameIF.text)||
				string.IsNullOrEmpty(confirmPassIF.text) || string.IsNullOrEmpty(passIF.text))
			{
				signUpButton.interactable = false;
				return;
			}

			signUpButton.interactable = true;
		}

		public void OnSignUpButtonPressed()
		{
			if (confirmPassIF.text != passIF.text)
			{
				StartCoroutine(ShowNotification("Password doesen't match", NotificationType.WARNING));
				return;
			}

			FSPlayerData playerData = new FSPlayerData
			{
				Name = nameIF.text,
				NickName=nickNameIF.text,
				Password = passIF.text
			};

			controller.OnSignUpButtonPressed(playerData);
		}

		public IEnumerator ShowNotification(string notificationInfo,NotificationType notifType)
		{
			switch (notifType)
			{
				case NotificationType.SUCCESS:
					notificationText.color = Color.green;
					break;
				case NotificationType.WARNING:
					notificationText.color = Color.yellow;
					break;
				case NotificationType.FAIL:
					notificationText.color = Color.red;
					break;
			}

			notificationText.text = notificationInfo;

			notificationText.gameObject.SetActive(true);

			yield return new WaitForSeconds(2f);

			notificationText.gameObject.SetActive(false);

		}

	}
}

public enum NotificationType {NONE,SUCCESS,WARNING,FAIL}
