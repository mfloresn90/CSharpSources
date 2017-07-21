using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EditorTools.Build.Scenes;
using Improbable.Unity;
using Improbable.Unity.EditorTools.Build;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class PlayerBuildProcess : IPlayerBuildEvents
{
    #region Implement IPlayerBuildEvents

    private SpatialSceneNames spatialSceneNames;


    public string[] GetScenes(WorkerPlatform workerType)
    {
        List<string> scenePathsBuilder = new List<string>();
        string[] scenePaths;

        switch (workerType)
        {
            case WorkerPlatform.UnityClient:
                Debug.Log("Client Scene Count: " + spatialSceneNames.Client.Count);
                scenePathsBuilder.AddRange(spatialSceneNames.Client);
                scenePathsBuilder.AddRange(spatialSceneNames.Common);
                scenePaths = FormatSceneList(scenePathsBuilder.ToArray(), scenePathsBuilder[0]);
                break;
            case WorkerPlatform.UnityWorker:
                scenePathsBuilder.AddRange(spatialSceneNames.Worker);
                scenePathsBuilder.AddRange(spatialSceneNames.Common);
                scenePaths = FormatSceneList(scenePathsBuilder.ToArray(), scenePathsBuilder[0]); break;
            default:
                throw new Exception("Attempting to get scenes for unrecognised worker platform");
        }

        EditorBuildSettings.scenes =
            scenePaths.Select(scenePath => new EditorBuildSettingsScene(scenePath, true)).ToArray();

        return scenePaths;
    }

    private string[] FormatSceneList(string[] sceneList, string defaultActiveScene)
    {
        return sceneList.OrderBy(scene => scene != defaultActiveScene).Select(FormatSceneName).ToArray();
    }

    private string FormatSceneName(string sceneName)
    {
        return "Assets/" + sceneName + ".unity";
    }

    public void BeginBuild()
    {
        spatialSceneNames= LoadConfiguation();
    }

    public void EndBuild()
    {
    }

    public void BeginPackage(WorkerPlatform workerType, BuildTarget target, Config config, string packagePath)
    {
    }

    #endregion

    static PlayerBuildProcess()
    {
        SimpleBuildSystem.CreatePlayerBuildEventsAction = () => new PlayerBuildProcess();
    }


        public static readonly string SceneConfigurationFilePath = Path.Combine(Application.dataPath,
            "scene-build-config.json");


        //load our config file for terrains
        public static SpatialSceneNames LoadConfiguation()
        {
            if (File.Exists(SceneConfigurationFilePath))
            {
                return JsonUtility.FromJson<SpatialSceneNames>(File.ReadAllText(SceneConfigurationFilePath));
            }
            return LoadDefault();
        }

        public static SpatialSceneNames SaveConfiguation(SpatialSceneNames sceneNames)
        {
            using (var writer = File.CreateText(SceneConfigurationFilePath))
            {
                writer.Write(JsonUtility.ToJson(sceneNames, true));
            }
            AssetDatabase.Refresh();
            return sceneNames;
        }

        private static SpatialSceneNames LoadDefault()
        {
            return new SpatialSceneNames()
            {
                Common = new List<string>(),
                Client = new List<string>() { "ClientScene" },
                Worker = new List<string>() { "PhysicsServerScene" }
            };
        }
    }