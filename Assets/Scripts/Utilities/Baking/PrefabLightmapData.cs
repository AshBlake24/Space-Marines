using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Roguelike.Utilities.Baking
{
    [ExecuteAlways]
    public class PrefabLightmapData : MonoBehaviour
    {
        [SerializeField] private RendererInfo[] _rendererInfo;
        [SerializeField] private Texture2D[] _lightmaps;
        [SerializeField] private Texture2D[] _lightmapsDir;
        [SerializeField] private Texture2D[] _shadowMasks;
        [SerializeField] private LightInfo[] _lightInfo;

        private void Awake() => 
            Init();

        private void OnEnable() => 
            SceneManager.sceneLoaded += OnSceneLoaded;

        private void OnDisable() => 
            SceneManager.sceneLoaded -= OnSceneLoaded;

        private void Init()
        {
            if (_rendererInfo == null || _rendererInfo.Length == 0)
                return;

            LightmapData[] lightmaps = LightmapSettings.lightmaps;
            int[] offsetIndexes = new int[_lightmaps.Length];
            int totalCount = lightmaps.Length;
            List<LightmapData> combinedLightmaps = new();

            for (int i = 0; i < _lightmaps.Length; i++)
            {
                bool exists = false;

                for (int j = 0; j < lightmaps.Length; j++)
                {
                    if (_lightmaps[i] == lightmaps[j].lightmapColor)
                    {
                        exists = true;
                        offsetIndexes[i] = j;
                    }
                }

                if (!exists)
                {
                    offsetIndexes[i] = totalCount;
                    LightmapData newlightmapdata = new()
                    {
                        lightmapColor = _lightmaps[i],
                        lightmapDir = _lightmapsDir.Length == _lightmaps.Length ? _lightmapsDir[i] : default(Texture2D),
                        shadowMask = _shadowMasks.Length == _lightmaps.Length ? _shadowMasks[i] : default(Texture2D),
                    };

                    combinedLightmaps.Add(newlightmapdata);

                    totalCount += 1;
                }
            }

            LightmapData[] combinedLightmaps2 = new LightmapData[totalCount];

            lightmaps.CopyTo(combinedLightmaps2, 0);
            combinedLightmaps.ToArray().CopyTo(combinedLightmaps2, lightmaps.Length);

            bool directional = _lightmapsDir.All(texture2D => texture2D != null);

            LightmapSettings.lightmapsMode = (_lightmapsDir.Length == _lightmaps.Length && directional)
                ? LightmapsMode.CombinedDirectional
                : LightmapsMode.NonDirectional;
        
            ApplyRendererInfo(_rendererInfo, offsetIndexes, _lightInfo);
            LightmapSettings.lightmaps = combinedLightmaps2;
        }

        private static void ApplyRendererInfo(RendererInfo[] infos, int[] lightmapOffsetIndex, LightInfo[] lightsInfo)
        {
            foreach (RendererInfo info in infos)
            {
                info.Renderer.lightmapIndex = lightmapOffsetIndex[info.LightmapIndex];
                info.Renderer.lightmapScaleOffset = info.LightmapOffsetScale;

                // You have to release shaders.
                Material[] mat = info.Renderer.sharedMaterials;

                for (int j = 0; j < mat.Length; j++)
                {
                    if (mat[j] != null && Shader.Find(mat[j].shader.name) != null)
                        mat[j].shader = Shader.Find(mat[j].shader.name);
                }
            }

            for (int i = 0; i < lightsInfo.Length; i++)
            {
                LightBakingOutput bakingOutput = new LightBakingOutput();
                bakingOutput.isBaked = true;
                bakingOutput.lightmapBakeType = (LightmapBakeType) lightsInfo[i].LightmapBaketype;
                bakingOutput.mixedLightingMode = (MixedLightingMode) lightsInfo[i].MixedLightingMode;

                lightsInfo[i].Light.bakingOutput = bakingOutput;
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => 
            Init();

#if UNITY_EDITOR
        [MenuItem("Assets/Bake Prefab Lightmaps")]
        private static void GenerateLightmapInfo()
        {
            if (Lightmapping.giWorkflowMode != Lightmapping.GIWorkflowMode.OnDemand)
            {
                Debug.LogError("ExtractLightmapData requires that you have baked you lightmaps and Auto mode is disabled.");

                return;
            }

            Lightmapping.Bake();

            PrefabLightmapData[] prefabs = FindObjectsByType<PrefabLightmapData>(FindObjectsSortMode.None);

            foreach (PrefabLightmapData instance in prefabs)
            {
                GameObject gameObject = instance.gameObject;
                List<RendererInfo> rendererInfos = new();
                List<Texture2D> lightmaps = new();
                List<Texture2D> lightmapsDir = new();
                List<Texture2D> shadowMasks = new();
                List<LightInfo> lightsInfos = new();

                GenerateLightmapInfo(gameObject, rendererInfos, lightmaps, lightmapsDir, shadowMasks, lightsInfos);

                instance._rendererInfo = rendererInfos.ToArray();
                instance._lightmaps = lightmaps.ToArray();
                instance._lightmapsDir = lightmapsDir.ToArray();
                instance._lightInfo = lightsInfos.ToArray();
                instance._shadowMasks = shadowMasks.ToArray();

                GameObject targetPrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(instance.gameObject);

                if (targetPrefab != null)
                {
                    GameObject root = PrefabUtility.GetOutermostPrefabInstanceRoot(instance.gameObject); // 根结点

                    if (root != null)
                    {
                        GameObject rootPrefab = PrefabUtility.GetCorrespondingObjectFromSource(instance.gameObject);
                        string rootPath = AssetDatabase.GetAssetPath(rootPrefab);
                        PrefabUtility.UnpackPrefabInstanceAndReturnNewOutermostRoots(root, PrefabUnpackMode.OutermostRoot);

                        try
                        {
                            PrefabUtility.ApplyPrefabInstance(instance.gameObject, InteractionMode.AutomatedAction);
                        }
                        catch
                        {
                        }
                        finally
                        {
                            PrefabUtility.SaveAsPrefabAssetAndConnect(root, rootPath, InteractionMode.AutomatedAction);
                        }
                    }
                    else
                    {
                        PrefabUtility.ApplyPrefabInstance(instance.gameObject, InteractionMode.AutomatedAction);
                    }
                }
            }
        }

        private static void GenerateLightmapInfo(GameObject root, List<RendererInfo> rendererInfos, List<Texture2D> lightmaps,
            List<Texture2D> lightmapsDir, List<Texture2D> shadowMasks, List<LightInfo> lightsInfo)
        {
            MeshRenderer[] renderers = root.GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer renderer in renderers)
            {
                if (renderer.lightmapIndex != -1)
                {
                    RendererInfo info = new();
                    info.Renderer = renderer;

                    if (renderer.lightmapScaleOffset != Vector4.zero)
                    {
                        if (renderer.lightmapIndex is < 0 or 0xFFFE) continue;

                        info.LightmapOffsetScale = renderer.lightmapScaleOffset;

                        Texture2D lightmap = LightmapSettings.lightmaps[renderer.lightmapIndex].lightmapColor;
                        Texture2D lightmapDir = LightmapSettings.lightmaps[renderer.lightmapIndex].lightmapDir;
                        Texture2D shadowMask = LightmapSettings.lightmaps[renderer.lightmapIndex].shadowMask;

                        info.LightmapIndex = lightmaps.IndexOf(lightmap);

                        if (info.LightmapIndex == -1)
                        {
                            info.LightmapIndex = lightmaps.Count;
                            lightmaps.Add(lightmap);
                            lightmapsDir.Add(lightmapDir);
                            shadowMasks.Add(shadowMask);
                        }

                        rendererInfos.Add(info);
                    }
                }
            }

            Light[] lights = root.GetComponentsInChildren<Light>(true);

            foreach (Light l in lights)
            {
                LightInfo lightInfo = new();
                lightInfo.Light = l;
                lightInfo.LightmapBaketype = (int) l.lightmapBakeType;
                lightInfo.MixedLightingMode = (int) Lightmapping.lightingSettings.mixedBakeMode;
                lightsInfo.Add(lightInfo);
            }
        }
#endif
    
        [System.Serializable]
        private struct RendererInfo
        {
            public Renderer Renderer;
            public int LightmapIndex;
            public Vector4 LightmapOffsetScale;
        }

        [System.Serializable]
        private struct LightInfo
        {
            public Light Light;
            public int LightmapBaketype;
            public int MixedLightingMode;
        }
    }
}