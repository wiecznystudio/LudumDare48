using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private float fadeStrenght;
    [SerializeField] private float fadeSpeed;
    public static AudioManager instance = null;

    private Dictionary<string, AudioClip> clipsStrings = new Dictionary<string, AudioClip>();
        #region Singleton
    private void Awake() {
        Singleton();
        musicSource.volume = 0f;
        PlayMusic();
        FadeMusicIn();
        for(int i=0; i< clips.Length; i++) {
            clipsStrings[clips[i].name] = clips[i];
        }
    }
    private void Singleton() {  
        
        if(instance == null) {
            instance = this; 
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    public void PlaySfx(string name) {       
        AudioSource source = PooledAudioSources.instance.GetPooledSource();
        if (!source || !clipsStrings.ContainsKey(name)) { return; }
        source.gameObject.SetActive(true);
        source.clip = clipsStrings[name];
        source.Play();
        StartCoroutine(DisableAudioSource(source));
    }
    public void PlayMusic() {
        musicSource.clip = music;
        musicSource.Play();
    }
    public void FadeMusicIn() {
        StartCoroutine(FadeInCoroutine());
    }
    public void FadeMusicOut() {
        StartCoroutine(FadeOutCoroutine());
    }
    IEnumerator DisableAudioSource(AudioSource source) {
        while (source.isPlaying) {
            yield return new WaitForSeconds(0.5f);
        }
        source.gameObject.SetActive(false);
    } 
    IEnumerator FadeInCoroutine() {
        musicSource.clip = music;
        musicSource.Play();
        while (musicSource.volume < 1f) {
            musicSource.volume += fadeStrenght;
            yield return new WaitForSeconds(fadeSpeed);
        }
    }
    IEnumerator FadeOutCoroutine() {
        while(musicSource.volume > 0.01f) {
            musicSource.volume -= fadeStrenght;
            yield return new WaitForSeconds(fadeSpeed);
        }
    }

}

