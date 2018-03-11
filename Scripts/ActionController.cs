using UnityEngine;
using System.Collections;
using DSA.Extensions.Base;

namespace DSA.Extensions.PlayerControl
{
	public class ActionController : MonoBehaviour
	{

		private Transform cameraTrans;
		private Animator anim;
		private Interactor interactor;
		[SerializeField] private GameObject viewedObject;
		public GameObject ViewedObject { get { return viewedObject; } }

		public delegate void OnKeyPressEvent(KeyPressEnum.KeyPress sentKey);
		public event OnKeyPressEvent OnKeyPress;

		public delegate void OnPauseGameEvent();
		public event OnPauseGameEvent OnPauseGame;

		public delegate void OnOpenJournalEvent();
		public event OnOpenJournalEvent OnOpenJournal;

		private System.Action<GameObject> InteractAction;

		public void Assign(Transform sentTrans)
		{
			cameraTrans = sentTrans;
			interactor = new Interactor();
		}

		public void SetTraitEvents(TraitedMonoBehaviour sentObj)
		{
			PlayerKeyPressTrait[] keyTraits = sentObj.GetArray<PlayerKeyPressTrait>();
			for (int i = 0; i < keyTraits.Length; i++)
			{
				OnKeyPress += keyTraits[i].CheckKeyPress;
			}
		}

		private void OnEnable()
		{
			viewedObject = null;
		}

		public void SetInteractAtion(System.Action<GameObject> sentAction)
		{
			InteractAction = sentAction;
		}

		public void Update()
		{
			CheckPauseKey();
			CheckJournalKey();
			CheckInteract();
		}

		private void CheckInteract()
		{
			if (interactor == null) return;
			GameObject newViewedObject = interactor.GetInteractable(cameraTrans);
			if (newViewedObject != viewedObject)
			{
				viewedObject = newViewedObject;
				InteractAction(viewedObject);
			}
			if (Input.GetButtonDown("Interact"))
			{
				if (viewedObject != null)
				{
					//Player.Instance.audioController.PlayAudio(0);
					interactor.InteractWith(viewedObject);
				}
			}
		}

		private void CheckPauseKey()
		{
			if (Input.GetButtonDown("Pause"))
			{
				if (OnKeyPress == null) return;
				OnKeyPress(KeyPressEnum.KeyPress.Menu);
			}
		}

		private void CheckJournalKey()
		{
			if (Input.GetButtonDown("ShowJournal"))
			{
				if (OnKeyPress == null) return;
				OnKeyPress(KeyPressEnum.KeyPress.Journal);
			}
		}
	}
}