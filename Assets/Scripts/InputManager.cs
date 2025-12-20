using UnityEngine;

public class InputManager : MonoBehaviour

{
    [SerializeField] private EnableDisable _enableDisable;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            _enableDisable.enabled = true;
        }else if(Input.GetKeyDown(KeyCode.Y))
        {
            _enableDisable.enabled = false;
        }
    }
}
