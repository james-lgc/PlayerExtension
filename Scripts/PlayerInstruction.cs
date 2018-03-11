using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerInstruction
{
	[SerializeField] private TransformValue spawnPoint;
	public TransformValue SpawnPoint { get { return spawnPoint; } }

	public PlayerInstruction(TransformValue sentTransValue)
	{
		spawnPoint = sentTransValue;
	}
	public PlayerInstruction(Transform sentTrans)
	{
		spawnPoint = new TransformValue(sentTrans);
	}

}
