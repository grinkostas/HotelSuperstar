using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Zenject;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TutorialStep _firstStep;
    [SerializeField] private List<GameObject> _hideOnTutorialStart;

    [Inject] private Player _player;
    [Inject] private PlayerArrow _playerArrow;
    [Inject] protected GuestSpawner GuestSpawner;
    
    public bool IsRunning { get; private set; }


    private void Start()
    {
        bool isTutorialPassed = ES3.Load(SaveId.IsTutorialPassed, false);
        if (isTutorialPassed)
        {
            End();
            return;
        }

        IsRunning = true;
        HideAll();
        _player.DisableInteract();
        _firstStep.Enter();
    }
    
    public void End()
    {
        IsRunning = false;
        _player.EnableInteract();
        _player.ZoneDestination = null;
        _playerArrow.ReceiveDestination();
        _playerArrow.Hide();
        ES3.Save(SaveId.IsTutorialPassed, true);
        GuestSpawner.StartSpawn();
        ShowAll();
    }

    public void ForceTutorial()
    {
        ES3.DeleteFile();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ResetAndSkipTutorial()
    {
        ES3.DeleteFile();
        End();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void HideAll() => ChangeActiveItems(false);
    private void ShowAll() => ChangeActiveItems(true);
    private void ChangeActiveItems(bool isActive)
    {
        foreach (var item in _hideOnTutorialStart)
        {
            item.SetActive(isActive);
        }
    }

}
