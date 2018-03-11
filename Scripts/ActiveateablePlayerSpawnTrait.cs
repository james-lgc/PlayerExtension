using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

[AddComponentMenu("Trait/Player/(Activeateable) Player Spawn Trait")]
[System.Serializable]
public class ActiveateablePlayerSpawnTrait : PlayerSpawnTrait, IActivateable
{
	public void Activate()
	{
		Use();
	}
}
