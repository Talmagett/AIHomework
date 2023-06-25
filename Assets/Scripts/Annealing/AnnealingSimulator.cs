using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AnnealingSimulator : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private FunctionDraw _function;
    [SerializeField] private float _minT = 0;
    [SerializeField] private float _maxT = 100;
    [SerializeField] private bool _reduceTemperatureWhenRandom;
    [SerializeField] private float _decreasingScaleOfTemperature = 0.98f;
    private float _currentT;
    [SerializeField] private bool _setStartPosition;
    [ShowIf("_setStartPosition")]
    [SerializeField] Vector3 _startPosition;
    private Vector3 _currentPosition;
    private Vector3 _targetPosition;
    [SerializeField] private bool _isMaximum = true;
    private void Start()
    {
        StartCoroutine( StartSearch());
    }
    private IEnumerator StartSearch()
    {
        _currentT = _maxT;
        if (_setStartPosition) 
            _currentPosition=_startPosition;
        else
            _currentPosition = _function.GetRandomPoint();//start position
        _player.gameObject.SetActive(true);
        MovePlayer();
        while(_currentT>_minT)
        {
            _targetPosition = _function.GetRandomPoint();
            var dE = Energy();
            if (dE <= 0)
            {
                _currentPosition = _targetPosition;
                MovePlayer();
            }
            else
            {
                float rand = Random.value;
                if (rand<System.MathF.Exp(-dE/_currentT))
                {
                    _currentPosition = _targetPosition;
                    MovePlayer();
                    if(_reduceTemperatureWhenRandom)
                        _currentT = _currentT * _decreasingScaleOfTemperature;
                }
            }
            if(!_reduceTemperatureWhenRandom)
                _currentT = _currentT * _decreasingScaleOfTemperature;
            yield return null;
        }
        print(_player.position.z);
    }
    private float Energy()
    {
        return _isMaximum ?  _currentPosition.z- _targetPosition.z : _targetPosition.z - _currentPosition.z;
    }
    private void MovePlayer()
    {
        _player.position = _targetPosition;
    }
}