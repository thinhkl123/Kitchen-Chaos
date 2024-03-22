using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public enum Scene
    {
        GameScene,
        MainMenu,
        LoadingScene
    }

    private static Scene scene;

    public static void Load(Scene scene)
    {
        Loader.scene = scene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
