using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //added
    private AudioSource audioSource;
    //added 2
    private AudioSource loopAudioSource;
    //added 3
    AudioSource backgroundMusicSource;

    [Header("Top Priority Sounds")]
    public AudioClip backgroundMusic;
    public AudioClip menuMusic;

    [Header("Priority 2 Sounds")]
    public AudioClip cookWalk;
    public AudioClip hunterWalk;
    public AudioClip dishReadyForStoveAndOven;
    public AudioClip itemDrop;
    public AudioClip itemPickup;
    public AudioClip knifeHit;
    public AudioClip knifeOnCuttingBoard;
    public AudioClip newOrderHasArrived;
    public AudioClip ovenSound;
    public AudioClip placingItemsOnKitchenCounter;
    public AudioClip playersLosingTime;
    public AudioClip stoveSound;

    [Header("Priority 3 Sounds")]
    public AudioClip batDying;
    public AudioClip batSoundWhenShooting;
    public AudioClip charonRiverMonsterDeath;
    public AudioClip heatedPan;
    public AudioClip playerDiggingGrave;
    public AudioClip playerHittingZombieWhileDigging;
    public AudioClip riverMonsterPullingSound;
    public AudioClip riverSound;
    public AudioClip zombieDeath;
    public AudioClip zombieWakingUp;

    [Header("Priority 4 Sounds")]
    public AudioClip bloodSound;
    public AudioClip chefAndHunterBookClose;
    public AudioClip chefAndHunterBookOpen;
    public AudioClip demonWingsSlapChef;
    public AudioClip depressedSoulsDying;
    public AudioClip heartbeatStops;
    public AudioClip limbsStealingItem;
    public AudioClip playersGettingDishOutOfOvenWithOvenMitts;
    public AudioClip playerGettingBurnedNoOvenMitts;


    public static SoundManager instance { get; private set; }

    public System.Action<GameObject> onPickup;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
    }

    //added
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //added 2
        loopAudioSource = gameObject.AddComponent<AudioSource>();
        loopAudioSource.loop = true;
        //added 3
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource.clip = backgroundMusic; 
        backgroundMusicSource.loop = true; 
        backgroundMusicSource.volume = 0.35f; 
    }
    //added
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    //added 2
    public void PlayLoopingSound(AudioClip clip)
    {
        if (clip != null && loopAudioSource.clip != clip)
        {
            loopAudioSource.clip = clip;
            loopAudioSource.Play();
        }
    }

    // added 2
    public void StopLoopingSound()
    {
        if (loopAudioSource.isPlaying)
        {
            loopAudioSource.Stop();
            loopAudioSource.clip = null;
        }
    }

    // Top Priority Sounds
    public void PlayBackgroundMusic()
    {
        if (!backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }
    public void StopBackgroundMusic()
    {
        if (backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }
    }

    void PlayMenuMusic()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.menuMusic);
    }

    void StopMenuMusic()
    {
        SoundManager.instance.StopLoopingSound();
    }

    // Priority 2 Sounds
    void PlayCookWalk()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.cookWalk);
    }

    void StopCookWalk()
    {
        SoundManager.instance.StopLoopingSound();
    }

    // Hunter walking sound (looped)
    void PlayHunterWalk()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.hunterWalk);
    }

    void StopHunterWalk()
    {
        SoundManager.instance.StopLoopingSound();
    }

    void DishReadyForStoveAndOven()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.dishReadyForStoveAndOven);
    }

    void ItemDrop()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.itemDrop);
    }

    void ItemPickup()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.itemPickup);
    }

    void KnifeHit()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.knifeHit);
    }

    void PlayKnifeOnCuttingBoard()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.knifeOnCuttingBoard);
    }

    void StopKnifeOnCuttingBoard()
    {
        SoundManager.instance.StopLoopingSound();
    }

    void NewOrderHasArrived()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.newOrderHasArrived);
    }

    void PlayOvenSound()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.ovenSound);
    }

    void StopOvenSound()
    {
        SoundManager.instance.StopLoopingSound();
    }

    void PlacingItemsOnKitchenCounter()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.placingItemsOnKitchenCounter);
    }

    void PlayersLosingTime()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.playersLosingTime);
    }

    void PlayStoveSound()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.stoveSound);
    }

    void StopStoveSound()
    {
        SoundManager.instance.StopLoopingSound();
    }

    // Priority 3 Sounds
    void BatDying()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.batDying);
    }

    void BatSoundWhenShooting()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.batSoundWhenShooting);
    }

    void CharonRiverMonsterDeath()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.charonRiverMonsterDeath);
    }

    void PlayHeatedPan()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.heatedPan);
    }

    void StopHeatedPan()
    {
        SoundManager.instance.StopLoopingSound();
    }

    void PlayerDiggingGrave()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.playerDiggingGrave);
    }

    void PlayerHittingZombieWhileDigging()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.playerHittingZombieWhileDigging);
    }

    void PlayRiverMonsterPullingSound()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.riverMonsterPullingSound);
    }

    void StopRiverMonsterPullingSound()
    {
        SoundManager.instance.StopLoopingSound();
    }

    void PlayRiverSound()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.riverSound);
    }

    void StopRiverSound()
    {
        SoundManager.instance.StopLoopingSound();
    }


    void ZombieDeath()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.zombieDeath);
    }

    void ZombieWakingUp()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.zombieWakingUp);
    }

    // Priority 4 Sounds
    void PlayBloodSound()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.bloodSound);
    }

    void StopBloodSound()
    {
        SoundManager.instance.StopLoopingSound();
    }

    void ChefAndHunterBookClose()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.chefAndHunterBookClose);
    }

    void ChefAndHunterBookOpen()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.chefAndHunterBookOpen);
    }

    void DemonWingsSlapChef()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.demonWingsSlapChef);
    }

    void PlayDepressedSoulsDying()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.depressedSoulsDying);
    }

    void StopDepressedSoulsDying()
    {
        SoundManager.instance.StopLoopingSound();
    }

    void HeartbeatStops()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.heartbeatStops);
    }

    void LimbsStealingItem()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.limbsStealingItem);
    }

    void PlayersGettingDishOutOfOvenWithOvenMitts()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.playersGettingDishOutOfOvenWithOvenMitts);
    }

    void PlayerGettingBurnedNoOvenMitts()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.playerGettingBurnedNoOvenMitts);
    }



}
