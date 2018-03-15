using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

[RequireComponent(typeof(TraitedMonoBehaviour))]
[System.Serializable]
public abstract class PlayerSpawnTrait : TraitBase, ISendable<TransformValue?>
{
	public override ExtensionEnum Extension { get { return ExtensionEnum.Player; } }

	public Action<TransformValue?> SendAction { get; set; }

	[SerializeField] private Transform SpawnPoint;
	public TransformValue? Data
	{
		get
		{
			if (SpawnPoint == null) { return null; }
			return new TransformValue(SpawnPoint);
		}
	}

	protected void Use()
	{
		if (!GetIsExtensionLoaded() || SendAction == null || Data == null) { return; }
		SendAction(Data);
	}
}

