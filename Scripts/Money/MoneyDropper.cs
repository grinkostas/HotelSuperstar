using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using Zenject;

public class MoneyDropper : MonoBehaviour
{
    [SerializeField] private MoneyNode _moneyNodePrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Vector2 _xDeltaRange;
    [SerializeField] private float _dropDelay;
    [SerializeField] private float _duration;
    [SerializeField] private float _heightOffset;
    [SerializeField] private float _zOffset;

    [Inject] private DiContainer _container;
    
    private bool _isDropping = false;
    
    [Button("Start Drop")]
    public void StartDrop()
    {
        _isDropping = true;
        StartCoroutine(Droping());
    }

    [Button("StopDrop")]
    public void StopDrop()
    {
        _isDropping = false;
    }


    private void SpawnAndAnimateNode()
    {
        var nodeTransform = SpawnMoneyNode().transform;
        nodeTransform.localScale = Vector3.zero;
        nodeTransform.DOScale(Vector3.one, _duration);
        nodeTransform.DOMoveX(nodeTransform.position.x + GetXDelta(), _duration);

        float yStart = nodeTransform.transform.position.y;
        nodeTransform.DOMoveY(yStart + _heightOffset, _duration / 2);
        nodeTransform.DOMoveY(yStart, _duration / 2).SetDelay(_duration/2);
    }

    
    private MoneyNode SpawnMoneyNode()
    {
        var prefab = _container.InstantiatePrefab(_moneyNodePrefab);
        prefab.transform.position = _spawnPoint.position;
        prefab.transform.rotation = Quaternion.identity;
        return prefab.GetComponent<MoneyNode>();
    }

    private float GetXDelta()
    {
        return Random.Range(_xDeltaRange.Min(), _xDeltaRange.Max());
    }
    

    private IEnumerator Droping()
    {
        while (_isDropping)
        {
            SpawnAndAnimateNode();
            yield return new WaitForSeconds(_dropDelay);
        }
    }
    
}
