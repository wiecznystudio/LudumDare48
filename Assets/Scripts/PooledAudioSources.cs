using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledAudioSources : MonoBehaviour
{
    [SerializeField] List<AudioSource> pooledSources = new List<AudioSource>();
    [SerializeField] private int ammountToPool = 0;
    public static PooledAudioSources instance;

    private void Awake() {
        instance = this;
    }
    private void Start() {
        foreach(AudioSource source in pooledSources) {
            source.gameObject.SetActive(false);
        }
    }
    public AudioSource GetPooledSource() {
        for (int i=0; i < ammountToPool; i++){
            if (!pooledSources[i].gameObject.activeInHierarchy) {
                return pooledSources[i];             
            }
        }
        return null;
    }
}
