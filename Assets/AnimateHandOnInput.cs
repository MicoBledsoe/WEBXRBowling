using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//My Directives above

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty PinchAniAction;
    public InputActionProperty GripAniAction;
    public Animator HandAni;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        float TriggerVal = PinchAniAction.action.ReadValue<float>();
        HandAni.SetFloat("Trigger", TriggerVal);

        float GripVal = GripAniAction.action.ReadValue<float>();
        HandAni.SetFloat("Grip", GripVal);
        Debug.Log("TriggerVal");
    }
}