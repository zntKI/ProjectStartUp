using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; // Added for AudioMixer support



public class SoundManager : MonoBehaviour
{

    public AudioMixer audioMixer;

    private AudioSource audioSource;
    private AudioSource loopAudioSource;
    private AudioSource backgroundMusicSource;

    [Header("Audio Mixer Groups")]
    public AudioMixerGroup sfxGroup;
    public AudioMixerGroup musicGroup;

    [Header("Top Priority Sounds")]
    public AudioClip backgroundMusic;
    public AudioClip menuMusic;

    [Header("Priority 2 Sounds")]
    public AudioClip cookWalk;
    public AudioClip hunterWalk;
    public AudioClip dishReadyForStoveAndOven;
    public AudioClip itemDrop;
    public AudioClip itemPickup;
    public AudioClip panHittingProjectile;
    public AudioClip knifeHit;
    public AudioClip knifeOutgoing;
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
    public AudioClip beefScreaming;
    public AudioClip chefAndHunterBookClose;
    public AudioClip chefAndHunterBookOpen;
    public AudioClip demonWingsSlapChef;
    public AudioClip depressedSoulsDying;
    public AudioClip heartbeatStops;
    public AudioClip limbsStealingItem;
    public AudioClip playersGettingDishOutOfOvenWithOvenMitts;
    public AudioClip playerGettingBurnedNoOvenMitts;


    public static SoundManager instance { get; private set; }

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
        audioSource = gameObject.AddComponent<AudioSource>();
        loopAudioSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();

        // Assign AudioMixerGroups
        if (sfxGroup != null)
        {
            audioSource.outputAudioMixerGroup = sfxGroup;
            loopAudioSource.outputAudioMixerGroup = sfxGroup;
        }

        if (musicGroup != null)
        {
            backgroundMusicSource.outputAudioMixerGroup = musicGroup;
        }

        loopAudioSource.loop = true;
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.volume = 0.35f;
    }

    public void SetVolume(string parameter, float volume)
    {
        audioMixer.SetFloat(parameter, Mathf.Log10(volume) * 20); // Convert linear to logarithmic
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
            //Debug.Log(loopAudioSource.clip);
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

    public void PlayMenuMusic()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.menuMusic);
    }

    public void StopMenuMusic()
    {
        SoundManager.instance.StopLoopingSound();
    }

    // Priority 2 Sounds
    public void PlayCookWalk()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.cookWalk);
    }

    // Hunter walking sound (looped)
    public void PlayHunterWalk()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.hunterWalk);
    }

    public void StopWalk()
    {
        SoundManager.instance.StopLoopingSound();
    }

    public void DishReadyForStoveAndOven()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.dishReadyForStoveAndOven);
    }

    public void ItemDrop()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.itemDrop);
    }

    public void ItemPickup()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.itemPickup);
    }
    public void PanHittingProjectile()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.panHittingProjectile);
    }

    public void KnifeHit()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.knifeHit);
    }

    public void KnifeOutgoing()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.knifeOutgoing);
    }

    public void PlayKnifeOnCuttingBoard()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.knifeOnCuttingBoard);
    }

    public void StopKnifeOnCuttingBoard()
    {
        SoundManager.instance.StopLoopingSound();
    }

    public void NewOrderHasArrived()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.newOrderHasArrived);
    }

    public void PlayOvenSound()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.ovenSound);
    }

    public void StopOvenSound()
    {
        SoundManager.instance.StopLoopingSound();
    }

    public void PlayersLosingTime()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.playersLosingTime);
    }

    public void PlayStoveSound()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.stoveSound);
    }

    public void StopStoveSound()
    {
        SoundManager.instance.StopLoopingSound();
    }

    // Priority 3 Sounds
    public void BatDying()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.batDying);
    }

    public void BatSoundWhenShooting()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.batSoundWhenShooting);
    }

    public void CharonRiverMonsterDeath()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.charonRiverMonsterDeath);
    }

    public void PlayHeatedPan()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.heatedPan);
    }

    public void StopHeatedPan()
    {
        SoundManager.instance.StopLoopingSound();
    }

    public void PlayerDiggingGrave()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.playerDiggingGrave);
    }

    public void PlayerHittingZombieWhileDigging()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.playerHittingZombieWhileDigging);
    }

    public void PlayRiverMonsterPullingSound()
    {
        if (loopAudioSource.clip != riverSound)
            SoundManager.instance.PlayLoopingSound(SoundManager.instance.riverMonsterPullingSound);
    }

    public void StopRiverMonsterPullingSound()
    {
        SoundManager.instance.StopLoopingSound();
    }

    public void PlayRiverSound()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.riverSound);
    }

    public void StopRiverSound()
    {
        SoundManager.instance.StopLoopingSound();
    }


    public void ZombieDeath()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.zombieDeath);
    }

    public void ZombieWakingUp()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.zombieWakingUp);
    }

    // Priority 4 Sounds
    public void PlayBloodSound()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.bloodSound);
    }

    public void StopBloodSound()
    {
        SoundManager.instance.StopLoopingSound();
    }

    public void PlayBeefScreaming()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.beefScreaming);
    }

    public void StopBeefScreaming()
    {
        SoundManager.instance.StopLoopingSound();
    }

    public void ChefAndHunterBookClose()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.chefAndHunterBookClose);
    }

    public void ChefAndHunterBookOpen()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.chefAndHunterBookOpen);
    }

    public void DemonWingsSlapChef()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.demonWingsSlapChef);
    }

    public void PlayDepressedSoulsDying()
    {
        SoundManager.instance.PlayLoopingSound(SoundManager.instance.depressedSoulsDying);
    }

    public void StopDepressedSoulsDying()
    {
        SoundManager.instance.StopLoopingSound();
    }

    public void HeartbeatStops()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.heartbeatStops);
    }

    public void LimbsStealingItem()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.limbsStealingItem);
    }

    public void PlayersGettingDishOutOfOvenWithOvenMitts()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.playersGettingDishOutOfOvenWithOvenMitts);
    }

    public void PlayerGettingBurnedNoOvenMitts()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.playerGettingBurnedNoOvenMitts);
    }



}
