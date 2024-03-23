using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using _Game.Scripts.Extensions;
using System;
using System.Linq;

namespace _Game.Scripts.Managers
{
    [Serializable]
    public class SoundItem
    {
        public SoundType id;
        public AudioClip clip;
        [Range(0,1)]public float volume;
    }
    [Serializable]
    public class SoundGroupItem
    {
        public SoundType id;
        public AudioClip[] clip;
        [Range(0, 1)] public float volume;
    }
    public enum SoundType
    {
        Shot,
        Walk,
        Jump,
        Land
    }

    public class SoundManager : Singleton<SoundManager>
    {
        private const string _masterVolume = "MasterVolume";
        private const string _sfxVolumee = "SfxVolume";
        private const string _musicVolume = "MusicVolume";
        private const string _dialogueVolume = "DialogueVolume";

        [SerializeField] private List<SoundItem> sounds;
        [SerializeField] private List<SoundGroupItem> groupSounds;
        [SerializeField] private AudioMixer audioMixer;
        [Range(0, 100)] [SerializeField] private float masterVolume;
        [SerializeField] private AudioSource sfxAudioSource;
        [Range(0, 100)] [SerializeField] private float sfxVolume;
        [SerializeField] private AudioSource musicAudioSource;
        [Range(0, 100)] [SerializeField] private float musicVolume;
        [SerializeField] private AudioSource dialogueAudioSource;
        [Range(0, 100)] [SerializeField] private float dialogueVolume;

        public void SetMasterVolume()
        {
            audioMixer.SetFloat(_masterVolume, masterVolume.Remap(0, 100, -80, 20));
        }
        public void SetSfxVolume()
        {
            audioMixer.SetFloat(_sfxVolumee, sfxVolume.Remap(0, 100, -80, 20));
        }
        public void SetMusicVolume()
        {
            audioMixer.SetFloat(_musicVolume, musicVolume.Remap(0, 100, -80, 20));
        }
        public void SetDialogueVolume()
        {
            audioMixer.SetFloat(_dialogueVolume, dialogueVolume.Remap(0, 100, -80, 20));
        }
        public void PlayOneShotSound(SoundType sound)
        {
            var currentSound = sounds.FirstOrDefault(item => item.id == sound);
            sfxAudioSource.PlayOneShot(currentSound.clip, currentSound.volume);
        }
        public void PlaySoundAtPoint(SoundType sound,Vector3 position)
        {
            var currentSound = sounds.FirstOrDefault(item => item.id == sound);
            AudioSource.PlayClipAtPoint(currentSound.clip,position, currentSound.volume);
        }
        public void PlayRandomSoundOnGroup(SoundType sound)
        {
            var currentSound = groupSounds.FirstOrDefault(item => item.id == sound);
            int rng = UnityEngine.Random.Range(0, currentSound.clip.Length);
            if (currentSound.clip.Length>0)
            {
                sfxAudioSource.PlayOneShot(currentSound.clip[rng], currentSound.volume);
            }
        }
        public void PlayRandomSoundOnGroupAtPoint(SoundType sound, Vector3 position)
        {
            var currentSound = groupSounds.FirstOrDefault(item => item.id == sound);
            int rng = UnityEngine.Random.Range(0, currentSound.clip.Length);
            if (currentSound.clip.Length > 0)
            {
                AudioSource.PlayClipAtPoint(currentSound.clip[rng], position, currentSound.volume);
            }
        }
        public void PlayMusicSound(SoundType sound)
        {
            var currentSound = sounds.FirstOrDefault(item => item.id == sound);
            musicAudioSource.volume = currentSound.volume;
            musicAudioSource.clip = currentSound.clip;
            musicAudioSource.Play();
        }
    }
}