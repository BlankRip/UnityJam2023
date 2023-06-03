using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Gameplay;

public class DebugPerformanceProtection : MonoBehaviour
{
    private void Awake() 
    {
#if UNITY_EDITOR
    Debug.unityLogger.logEnabled = true;
#else
    Debug.unityLogger.logEnabled = false;
#endif    
    }
}
