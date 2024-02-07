using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour, IProgressible
{
    [SerializeField] private List<LoadingOperation> _loadingOperations;
    
    
    public UnityAction<float> ProgressChanged { get; set; }
    
    private int _currentLoadingId = 0;
    private void Start()
    {
        NextLoadingOperation();
    }

    private void NextLoadingOperation()
    {
        if (_currentLoadingId >= _loadingOperations.Count)
        {
            StartGame();
            return;
        }

        if (_currentLoadingId - 1 >= 0)
        {
            _loadingOperations[_currentLoadingId - 1].End -= NextLoadingOperation;
        }

        int index = _currentLoadingId;
        _currentLoadingId++;
        _loadingOperations[index].End += NextLoadingOperation;
        _loadingOperations[index].Load();
    }
    

    private void StartGame()
    {
        StartCoroutine(Load());
    }

    private AsyncOperation _operation;
    private IEnumerator Load()
    {
        _operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        _operation.allowSceneActivation = false;
        while (_operation.progress <= 0.91f)
        {
            var progress = Mathf.Clamp(_operation.progress + 0.1f, 0.0f, 1.0f);
            ProgressChanged?.Invoke(progress/2);
            ProgressChanged?.Invoke(progress);
            yield return null;
        }
    }

    public void StartLoad()
    {
        _operation.allowSceneActivation = true;
    }

}
