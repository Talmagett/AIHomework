using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronController : MonoBehaviour
{
    [SerializeField] private Function _function;
    private float _w1,_w2;
    private float n = 0.001f;
    private void Start()
    {
        System.Random random = new System.Random(_function.RandSeed);
        _w1 = ((float)random.NextDouble());
        _w2 = ((float)random.NextDouble());
        Generation();
    }
    private void Generation()
    {
        for (int i = 0; i < _function.SetsCount; i++)
        {

        }
    }
    private void ErrorDetection()
    { 
    
    }
    private void Correction()
    { 
    
    }
}