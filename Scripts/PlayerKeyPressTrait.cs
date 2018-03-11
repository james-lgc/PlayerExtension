using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Base
{
	[RequireComponent(typeof(TraitedMonoBehaviour))]
	[AddComponentMenu("Trait/Player/(Event Driven) Player KeyPress Trait")]
	[System.Serializable]
	public class PlayerKeyPressTrait : TraitBase
	{
		public override ExtensionEnum.Extension Extension { get { return ExtensionEnum.Extension.Player; } }

		[SerializeField] private UnityEngine.Events.UnityEvent useEvent;

		[SerializeField] private KeyPressEnum.KeyPress data;
		public KeyPressEnum.KeyPress Data { get { return data; } protected set { data = value; } }

		protected void Use()
		{
			if (!GetIsExtensionLoaded() || useEvent == null || Data == KeyPressEnum.KeyPress.None) { return; }
			useEvent.Invoke();
		}

		public void CheckKeyPress(KeyPressEnum.KeyPress sentKey)
		{
			if (sentKey == data)
			{
				Use();
			}
		}
	}
}