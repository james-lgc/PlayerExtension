using UnityEngine;
using System.Collections;
using DSA.Extensions.Base;

//[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(BoxCollider2D))]
namespace DSA.Extensions.PlayerControl
{
	[System.Serializable]
	public class Player : TraitedMonoBehaviour
	{
		public override ExtensionEnum Extension { get { return ExtensionEnum.Player; } }

		[Header("Classes")] [SerializeField] private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;
		[SerializeField] private Camera cam;
		[SerializeField] private CapsuleCollider coll;

		[Header("Controllers")] [SerializeField] private ActionController actionController;
		public ActionController ActionController { get { return actionController; } }

		private static Player instance;

		public System.Action<GameObject> InteractAction;

		public void SetInstance()
		{
			if (instance == null) instance = this;
			Player[] players = FindObjectsOfType<Player>();
			for (int i = 0; i < players.Length; i++)
			{
				if (players[i] != instance)
				{
					Destroy(players[i].gameObject);
				}
			}
		}

		public void SetTraitEvents(TraitedMonoBehaviour sentObj)
		{
			if (actionController == null) SetControllers();
			actionController.SetTraitEvents(sentObj);
		}

		private void Awake()
		{
			SetInstance();
		}

		public void SetInteractAction(System.Action<GameObject> sentAction)
		{
			InteractAction = sentAction;
			actionController.SetInteractAtion(sentAction);
		}

		public void Assign()
		{
			coll = GetComponentInChildren<CapsuleCollider>();
			SetControllers();
			actionController.Assign(cam.transform);
			SetInstance();
		}

		private void SetControllers()
		{
			fpsController = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
			actionController = GetComponent<ActionController>();
		}

		//Called by OnConversation and OnUIMenu Events
		public void EnableControllers()
		{
			if (actionController == null) SetControllers();
			actionController.enabled = true;
			fpsController.enabled = true;
		}

		public void SetPosition(TransformValue sentTransValue)
		{
			Show();
			if (sentTransValue.Position == new Vector3(0, 0, 0))
			{
				Debug.Log("Player found null transform");
				return;
			}
			transform.SetPositionAndRotation(sentTransValue.Position, sentTransValue.Rotation);
			fpsController.ResetRotation();
			Hide();
		}

		public void DisableControllers()
		{
			if (actionController == null) SetControllers();
			actionController.enabled = false;
			fpsController.enabled = false;
		}

		public void Display(bool isVisible)
		{
			if (isVisible == true) { Show(); }
			else { Hide(); }
		}

		private void Show()
		{
			coll.enabled = true;
			EnableControllers();
		}

		private void Hide()
		{
			coll.enabled = false;
			DisableControllers();
		}
	}
}