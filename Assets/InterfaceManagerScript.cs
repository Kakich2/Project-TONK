using UnityEngine;
using System.Collections.Generic;
/*


public class ChangeTankWindowScript : MonoBehaviour
{
	public struct ObjectData
	{
		public KeyCode Keycode;
		public bool IsActive;
	}

	private Dictionary<GameObject, ObjectData> Objects = new Dictionary<GameObject, ObjectData>();
	
	[Header("Настройка объектов и клавиш")]
	[SerializeField] private List<GameObject> ObjectsStart = new List<GameObject>();
	[SerializeField] private List<KeyCode> KeysStart = new List<KeyCode>();
	[SerializeField] private List<bool> IsActiveStart = new List<bool>();

	void Start()
	{
//		for (int i = 0; i < ObjectsStart.Count; i++) {
//			private ObjectData OBJECT_DATA = new ObjectData();
//			OBJECT_DATA.Key = KeysStart[i];
//			OBJECT_DATA.Value = IsActiveStart[i];
//			Objects.Add(ObjectsStart[i], OBJECT_DATA);
//		}
	}

	void Update()
	{
		foreach (KeyValuePair<GameObject, ObjectData> OBJECT in Objects) {
			GameObject OBJ = OBJECT.Key;
			KeyCode ObjectKey = OBJECT.Value.Keycode;
			bool ObjectActive = OBJECT.Value.IsActive;

			if (Input.GetKeyDown(ObjectKey)) {
				OBJECT.Value.IsActive = !OBJECT.Value.IsActive;
				OBJ.SetActive(OBJECT.Value.IsActive);
			}
		}
	}
}
*/