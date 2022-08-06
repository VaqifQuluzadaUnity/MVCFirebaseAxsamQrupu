using UnityEngine;
using DynamicBox.EventManagement;

public class OnSignUpButtonPressedEvent:GameEvent
{
  public FSPlayerData PlayerData;

  public OnSignUpButtonPressedEvent(FSPlayerData _playerData)
	{
		PlayerData = _playerData;
	}
}
