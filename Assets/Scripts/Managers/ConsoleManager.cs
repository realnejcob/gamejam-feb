using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DavidFDev.DevConsole;
using System;
using UnityEngine.SceneManagement;

public class ConsoleManager : MonoBehaviour {
    private void Awake() {
        InitializeConsole();
        AddCommands();
    }

    private void AddCommands() {
        DevConsole.AddCommand(Command.Create(
            name: "reload",
            aliases: "r",
            helpText: "Reload scene",
            callback: () => ReloadScene()
        ));
    }

    private void InitializeConsole() {
        DevConsole.EnableConsole();
        DevConsole.SetToggleKey(KeyCode.Escape);
        DevConsole.Log("Hello world!");
    }

    private void ReloadScene() {
        LeanTween.cancelAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
