using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

[AddComponentMenu("Trait/Player/(Sequenced) Player Spawn Trait")]
[System.Serializable]
public class SequencedPlayerSpawnTrait : PlayerSpawnTrait, ISequenceable
{
	[SerializeField] private int sequenceOrder;
	public int SequenceOrder { get { return sequenceOrder; } }

	public void CallInSequence(int sequenceID)
	{
		if (sequenceOrder != sequenceID) { return; }
		Use();
	}
}
