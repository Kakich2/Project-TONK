using UnityEngine;



public class PlayerControllerManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerInputSettings
    {
        public KeyCode jump = KeyCode.Space;
        public KeyCode fire = KeyCode.Mouse0;
        public KeyCode run = KeyCode.LeftShift;
    }

    public PlayerInputSettings inputSettings;

    void Update()
    {

    }
}