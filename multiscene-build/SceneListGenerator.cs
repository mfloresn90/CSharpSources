using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EditorTools.Build.Scenes;
using Improbable.Unity;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SceneListGenerator {

    [MenuItem("Improbable/Scenes/Validate Scenes")]
    public static void ValidateScenes()
    {
        ValidateClientScenes();
        ValidateWorkerScenes();
    }

    [MenuItem("Twokinds Online/Validate Client Scenes")]
    public static void ValidateClientScenes()
    {
        var buildProc = new PlayerBuildProcess();
        buildProc.BeginBuild();
        var scenes = buildProc.GetScenes(WorkerPlatform.UnityClient);

        for (int cnt = 0; cnt < scenes.Length; cnt++)
        {
            var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(scenes[cnt]);
            //var scene = EditorSceneManager.GetSceneByPath(scenes[cnt]);
            if (asset == null)
            {
                throw new Exception("Scene doesnt exist in Scene Manager: '" + scenes[cnt] + "'");
            }
            Debug.Log(asset.name);
        }
        Debug.Log("Client Scenes Exist");
    }

    [MenuItem("Improbable/Scenes/Validate Worker Scenes")]
    public static void ValidateWorkerScenes()
    {
        var buildProc = new PlayerBuildProcess();
        buildProc.BeginBuild();
        var scenes = buildProc.GetScenes(WorkerPlatform.UnityWorker);

        for (int cnt = 0; cnt < scenes.Length; cnt++)
        {
            var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(scenes[cnt]);
            //var scene = EditorSceneManager.GetSceneByPath(scenes[cnt]);
            if (asset == null)
            {
                throw new Exception("Scene doesnt exist in Scene Manager: '" + scenes[cnt] + "'");
            }
            Debug.Log(asset.name);

        }
        Debug.Log("Worker Scenes Exist");

    }

    private static string[] ReadNames()
    {
        List<string> temp = new List<string>();
        foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                string name = scene.path.Substring(scene.path.IndexOf('/') + 1);
                name = name.Substring(0, name.Length - 6); //remove .unity
                temp.Add(name);
            }
        }
        return temp.ToArray();
    }
}
