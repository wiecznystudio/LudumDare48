using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    #region Singleton
    private void Awake() {
        Singleton();
    }
    private void Singleton() {  
        
        if(instance != null) {
            Debug.LogWarning("Detected more than one AudioManager instance!");
        }
        instance = this;
    }
    #endregion


}

