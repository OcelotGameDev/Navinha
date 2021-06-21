using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #if UNITY_EDITOR
    [SerializeField] private bool _testingOptions = false;
    #endif
    
    private static GameManager _instance;

    private StateMachine _stateMachine;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        FadeInOutSceneTransition.LoadFadeScene();
        
        _stateMachine = new StateMachine();
        _stateMachine.OnStateChanged += state => OnGameStateChanged?.Invoke(state);

        Menu menu = new Menu();
        LoadLevel loading = new LoadLevel();
        Play play = new Play();
        Pause pause = new Pause();
        Options options = new Options();
        Quit quit = new Quit();
        WinState win = new WinState();

        _stateMachine.SetState(menu);

        _stateMachine.AddTransition(menu, loading, () => LoadLevel.LevelToLoad != null);

        _stateMachine.AddTransition(play, loading, () => LoadLevel.LevelToLoad != null);
        _stateMachine.AddTransition(loading, play, loading.Finish);

        _stateMachine.AddTransition(play, pause, () => Input.GetButtonDown("Pause"));
        _stateMachine.AddTransition(pause, play, () => Input.GetButtonDown("Pause"));
        _stateMachine.AddTransition(pause, play, () => PauseButton.Pressed);

        _stateMachine.AddTransition(pause, menu, () => RestartButton.Pressed);

        _stateMachine.AddTransition(pause, options, () => OptionsButton.Pressed);
        _stateMachine.AddTransition(options, pause,
            () => BackFromOptionsButton.Pressed && _stateMachine.LastState is Pause);
        _stateMachine.AddTransition(options, pause,
            () => Input.GetButtonDown("Pause") && _stateMachine.LastState is Pause);

        _stateMachine.AddTransition(menu, options, () => OptionsButton.Pressed);
        _stateMachine.AddTransition(options, menu, () => BackFromOptionsButton.Pressed && _stateMachine.LastState is Menu);
        _stateMachine.AddTransition(options, menu,
            () => Input.GetButtonDown("Pause") && _stateMachine.LastState is Menu);

        _stateMachine.AddTransition(play, win, () => WinObserver.GameEnded);
        _stateMachine.AddTransition(win, menu, () => RestartButton.Pressed);
        
        _stateMachine.AddAnyTransition(quit, () => QuitButton.Pressed);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }

    public static event Action<IState> OnGameStateChanged;
}

public class Menu : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        if (PoolingSystem.Instance != null)
        {
            PoolingSystem.Instance.DespawnEveryone();
        }
        
        Time.timeScale = 1f;
        LoadLevel.LevelToLoad = null;

        var menuAlreadyLoaded = false;
        var optionsLoaded = false;
        // If any of the loaded scenes is the menu Scene don't load it again
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var currentScene = SceneManager.GetSceneAt(i);
            if (currentScene.name == "Menu")
            {
                menuAlreadyLoaded = true;
            }

            if (currentScene.name == "OptionsMenu")
            {
                optionsLoaded = true;
            }
        }

        if (!optionsLoaded)
        {
            SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Additive);
        }
        
        if (menuAlreadyLoaded) return;
        
        // otherwise load it
        SceneManager.LoadSceneAsync("Menu");
        SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Additive);
    }

    public void OnExit()
    {
    }
}

public class Pause : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        Time.timeScale = 0f;
    }

    public void OnExit()
    {
        Time.timeScale = 1f;
    }
}

public class Play : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        
        Time.timeScale = 1f;
        FadeInOutSceneTransition.Instance.FadeOut();
    }

    public void OnExit()
    {
        //PlayerRespawner.CurrentSpawner = null;
    }
}

public class LoadLevel : IState
{
    public static string LevelToLoad;

    private readonly List<AsyncOperation> _operations = new List<AsyncOperation>();

    public void Tick()
    {
        if (FadeInOutSceneTransition.Instance.FadeInCompleted)
            _operations.ForEach(t => t.allowSceneActivation = true);
    }

    public void OnEnter()
    {
         FadeInOutSceneTransition.Instance.FadeIn();

         if (PoolingSystem.Instance != null)
         {
             PoolingSystem.Instance.DespawnEveryone();
         }
         
         _operations.Add(SceneManager.LoadSceneAsync(LevelToLoad));
         _operations.Add(SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Additive));
         // _operations.Add(SceneManager.LoadSceneAsync("MapSetup", LoadSceneMode.Additive));

         _operations.ForEach(t => t.allowSceneActivation = false);
    }

    public void OnExit()
    {
        LevelToLoad = null;
    }

    public bool Finish()
    {
        return _operations.TrueForAll(t => t.isDone && t.allowSceneActivation);
    }
}

public class Options : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        Time.timeScale = 0f;
    }

    public void OnExit()
    {
        Time.timeScale = 1f;
        //SaveOptions.SaveAllOptions();
    }
}

public class Quit : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        Application.Quit();
    }

    public void OnExit()
    {
    }
}

public class WinState : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        Time.timeScale = 0f;
    }

    public void OnExit()
    {
        Time.timeScale = 1f;
    }
}