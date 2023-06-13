using System;
using Roguelike.Audio.Logic;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.StaticData.Audio;
using Roguelike.StaticData.Levels;
using Roguelike.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Roguelike.Audio.Factory
{
    public class AudioFactory : IAudioFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IPersistentDataService _progressData;
        private readonly IStaticDataService _staticData;

        private Transform _audioRoot;

        public AudioFactory(IAssetProvider assetProvider, IPersistentDataService progressData, 
            IStaticDataService staticData)
        {
            _assetProvider = assetProvider;
            _progressData = progressData;
            _staticData = staticData;
        }

        public Sound CreateAudioSource()
        {
            GameObject gameObject = _assetProvider.Instantiate(AssetPath.SoundPrefabPath, _audioRoot);

            if (gameObject.TryGetComponent(out Sound sound))
                return sound;
            else
                throw new ArgumentNullException(nameof(Sound));
        }

        public void CreateAudioRoot()
        {
            _audioRoot = new GameObject("AudioRoot").transform;
            InitBackgroundMusic();
            CreateAudioTickTimer();
        }

        private void InitBackgroundMusic()
        {
            switch (EnumExtensions.GetCurrentLevelId())
            {
                case LevelId.MainMenu:
                    CreateBackgroundMusic(MusicId.MainMenu);
                    break;
                case LevelId.Hub:
                    CreateBackgroundMusic(MusicId.Hub);
                    break;
                case LevelId.Dungeon:
                    CreateBackgroundMusic(MusicId.Dungeon);
                    break;
            }
        }

        private void CreateBackgroundMusic(MusicId id)
        {
            MusicConfig musicConfig = _staticData.GetDataById<MusicId, MusicConfig>(id);

            GameObject instance = _assetProvider.Instantiate(AssetPath.BackgroundMusicPath);

            if (instance.TryGetComponent(out AudioSource audioSource))
            {
                audioSource.clip = musicConfig.Music;
                audioSource.Play();
            }
            else
            {
                throw new ArgumentNullException(nameof(instance), $"Instance doesn't contain {nameof(AudioSource)}");
            }
        }

        private void CreateAudioTickTimer() => 
            _assetProvider.Instantiate(AssetPath.AudioTickTimerPath, _audioRoot);
    }
}