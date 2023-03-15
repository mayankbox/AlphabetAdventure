using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundName
{
    GameOver,
    ButtonCllick,
    Collect,
    Correct,
    Wrong,
    forAlpha,
    win,
    GreetingSound,
    GreetingSoundAlphabetMatch,
    DogCatRatRun,
    Ballonpop,
    Ballonwrong,
    Firecraker,
    TrainRunning,
    CargoHit,
    forAlphaTrain,
    CraneMoving
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] public AudioSource Sound;
  
    [SerializeField] public AudioClip GameOver;
    [SerializeField] private AudioClip ButtonClick;
    [SerializeField] private AudioClip CorrectClick;
    [SerializeField] private AudioClip WrongClick;
    [SerializeField] private AudioClip WinClick;
    [SerializeField] private AudioClip[] ForAlphaClip;
    [SerializeField] private AudioClip[] GreetingClip;
    


    [SerializeField] AudioClip Collect_sound;


    [Header("Alphabet_MAtching")]
    [SerializeField] private AudioClip DogcatratRun;
    [SerializeField] private AudioClip GreetingClipAlphabetMatching;
    [SerializeField] private AudioClip BallonPop;
    [SerializeField] private AudioClip WrongBallon;
    [SerializeField] private AudioClip FireCracker;



    [Header("Train")]
    [SerializeField] private AudioClip TrainMovingClip;
    [SerializeField] private AudioClip CargoHitClip;
    [SerializeField] private AudioClip[] ForAlphaClipTrain;
    [SerializeField] private AudioClip CraneMovingClip;


    public static SoundManager instance { get; set; }

    private void Awake()
    {
        instance = this;
    }


    public void Play_Btn_Click_Sound()   
    {
        PlaySound_(ButtonClick);
    }

    public void PauseSound_()
    {
        Sound.Stop();
    }
    public void PlaySound_(AudioClip clip)
    {
        if (Sound != null)
                Sound.PlayOneShot(clip);
    }

    public void SoundPlay(SoundName soundName)
    {
        switch (soundName)
        {
            case SoundName.GameOver:
                PlaySound_(GameOver);
                break;
            case SoundName.Collect:
                PlaySound_(Collect_sound);
                break;
            case SoundName.ButtonCllick:
                PlaySound_(ButtonClick);
                break;
            case SoundName.Correct:
                PlaySound_(CorrectClick);
                break;
            case SoundName.Wrong:
                PlaySound_(WrongClick);
                break;
            case SoundName.win:
                PlaySound_(WinClick);
                break;
            case SoundName.forAlpha:
                PlaySound_(ForAlphaClip[ObjectMatching.instance.AlphaCounter]);
                break;
            case SoundName.GreetingSound:
                PlaySound_(GreetingClip[ObjectMatching.instance.GreetingSoundType]);
                break;
            case SoundName.GreetingSoundAlphabetMatch:
                PlaySound_(GreetingClipAlphabetMatching);
                break;
            case SoundName.Ballonpop:
                PlaySound_(BallonPop);
                break;
            case SoundName.Ballonwrong:
                PlaySound_(WrongBallon);
                break;
            case SoundName.Firecraker:
                PlaySound_(FireCracker);
                break;
            case SoundName.DogCatRatRun:
                PlaySound_(DogcatratRun);
                break;
            case SoundName.TrainRunning:
                PlaySound_(TrainMovingClip);
                break;
            case SoundName.CargoHit:
                PlaySound_(CargoHitClip);
                break;
            case SoundName.forAlphaTrain:
                PlaySound_(ForAlphaClipTrain[TrainManager.instance.AlphabetWiseCounter-1]);
                break;
            case SoundName.CraneMoving:
                PlaySound_(CraneMovingClip);
                break;
        }
    }
}
