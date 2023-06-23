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
using Object = UnityEngine.Object;

namespace Roguelike.Audio.Factory
{
    public class AudioFactory : IAudioFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IPersistentDataService _progressData;
        private readonly IStaticDataService _staticData;

        private Transform _audioRoot;
        private AudioSource _backgroundMusic;
        private LevelId _previousLevelId;

        public AudioFactory(IAssetProvider assetProvider, IPersistentDataService progressData, 
            IStaticDataService staticData)
        {
            _assetProvider = assetProvider;
            _progressData = progressData;
            _staticData = staticData;
            _previousLevelId = LevelId.Unknown;
            CreateMusicRoot();
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

        private void CreateMusicRoot()
        {
            if (_backgroundMusic == null)
            {
                _backgroundMusic = _assetProvider.Instantiate(AssetPath.BackgroundMusicPath).GetComponent<AudioSource>();
                Object.DontDestroyOnLoad(_backgroundMusic);
            }
        }

        private void InitBackgroundMusic()
        {
            LevelId currentLevelId = EnumExtensions.GetCurrentLevelId();
            
            if (_previousLevelId == currentLevelId)
                return;
            
            switch (currentLevelId)
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

            _previousLevelId = currentLevelId;
        }

        private void CreateBackgroundMusic(MusicId id)
        {
            MusicConfig musicConfig = _staticData.GetDataById<MusicId, MusicConfig>(id);

            _backgroundMusic.clip = musicConfig.Music;
            _backgroundMusic.Play();
        }

        private void CreateAudioTickTimer() => 
            _assetProvider.Instantiate(AssetPath.AudioTickTimerPath, _audioRoot);
    }
}