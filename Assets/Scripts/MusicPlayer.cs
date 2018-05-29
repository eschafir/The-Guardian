using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    [SerializeField]
    private AudioClip menuMusic;

    [SerializeField]
    private AudioClip winMusic;

    [SerializeField]
    private AudioClip loseMusic;

    [SerializeField]
    private AudioClip[] levelMusic;

    //  [SerializeField]
    private AudioSource source;

    static private MusicPlayer instance;


    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        // If the game starts in a menu scene, play the appropriate music
        source = GetComponent<AudioSource>();
        PlayMenuMusic();
    }

    static public void PlayMenuMusic() {
        if (instance != null) {
            if (instance.source != null) {
                instance.source.Stop();
                instance.source.clip = instance.menuMusic;
                instance.source.Play();
                instance.source.loop = true;
            }
        } else {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayGameMusic() {
        if (instance != null) {
            if (instance.source != null) {
                instance.source.Stop();
                instance.source.clip = instance.levelMusic[LevelManager.ActualScene()];
                instance.source.Play();
                instance.source.loop = true;
            }
        } else {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayWinMusic() {
        if (instance != null) {
            if (instance.source != null) {
                instance.source.Stop();
                instance.source.clip = instance.winMusic;
                instance.source.Play();
                instance.source.loop = false;
            }
        } else {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayLoseMusic() {
        if (instance != null) {
            if (instance.source != null) {
                instance.source.Stop();
                instance.source.clip = instance.loseMusic;
                instance.source.Play();
                instance.source.loop = false;
            }
        } else {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }
}
