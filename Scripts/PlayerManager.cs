using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

namespace DSA.Extensions.PlayerControl
{
	public class PlayerManager : CanvasedManagerBase<HUDCanvas>
	{
		public override ExtensionEnum.Extension Extension { get { return ExtensionEnum.Extension.Player; } }

		[SerializeField] private Player thisPlayer;
		private GameObject viewedObject;
		private TransformValue spawnPoint;

		public override void Initialize()
		{
			base.Initialize();
			thisPlayer = FindObjectOfType<Player>();
			thisPlayer.Assign();
			thisPlayer.SetInteractAction(ToggleInteractHud);
		}

		protected override void StartProcess()
		{
			base.StartProcess();
			thisPlayer.EnableControllers();
		}

		public override void EndProcess()
		{
			base.EndProcess();
			thisPlayer.DisableControllers();
		}

		public override void Load()
		{
			base.Load();
			thisPlayer.Assign();
			thisPlayer.SetInteractAction(ToggleInteractHud);
		}

		public override void PassDelegatesToTraits(TraitedMonoBehaviour sentObj)
		{
			SetTraitActions<PlayerSpawnTrait, TransformValue?>(sentObj, ProcessSpawn);
			thisPlayer.SetTraitEvents(sentObj);
		}

		public void ProcessSpawn(TransformValue? sentTransValue)
		{
			if (sentTransValue == null) return;
			thisPlayer.SetPosition((TransformValue)sentTransValue);
		}

		public PlayerInstruction GetPlayerInstruction()
		{
			PlayerInstruction newInstruction = new PlayerInstruction(spawnPoint);
			return newInstruction;
		}

		public IEnumerator MaintainInteractHUD()
		{
			GameObject currentObj = viewedObject;
			ToggleInteractHud(currentObj);
			while (viewedObject == currentObj)
			{
				yield return null;
			}
			StartCoroutine(MaintainInteractHUD());
		}

		private void ToggleInteractHud(GameObject sentObject)
		{
			if (sentObject == null)
			{
				canvas.HideInteractHUD();
				return;
			}
			canvas.ShowInteractHUD(sentObject);
		}

		public override void AddDataToArrayList(ArrayList sentArrayList)
		{
			PlayerInstruction instruction = new PlayerInstruction(thisPlayer.gameObject.transform);
			sentArrayList.Add(instruction);
		}

		public override void ProcessArrayList(ArrayList sentArrayList)
		{
			for (int i = 0; i < sentArrayList.Count; i++)
			{
				if (sentArrayList[i] is PlayerInstruction)
				{
					PlayerInstruction instruction = (PlayerInstruction)sentArrayList[i];
					ProcessSpawn(instruction.SpawnPoint);
				}
			}
		}
	}
}