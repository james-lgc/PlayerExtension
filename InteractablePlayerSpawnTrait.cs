using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

[AddComponentMenu("Trait/Player/(Interactable) Player Spawn Trait")]
[System.Serializable]
public class InteractablePlayerSpawnTrait : PlayerSpawnTrait, IInteractCallable
{
	public void CallInteract()
	{
		Use();
	}
}
