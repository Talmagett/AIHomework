using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionDraw : MonoBehaviour
{
    [SerializeField] private FunctionType _functionType;
    [SerializeField] private GameObject _point;
    [SerializeField] private Vector3 _scale;
    public Vector3 Scale => _scale;
    [SerializeField] private float _step;
    [SerializeField] private float _minRange;
    [SerializeField] private float _maxRange;
    public float MinRange => _minRange;
    public float MaxRange => _maxRange;
    private void Start()
    {
        for (float x = MinRange; x <= MaxRange; x += 0.1f)
        {
            for (float y = MinRange; y <= MaxRange; y += 0.1f)
            {
                Instantiate(_point, GetPoint(x, y), Quaternion.identity, transform);
            }
        }
    }
    public float Function(float x, float y)
    {
        switch (_functionType)
        {
            case FunctionType.paraboloid:
                return x * x + y * y;
            case FunctionType.himmelblau:
                return (x * x + y - 11)* (x * x + y - 11) + (x + y * y - 7) * (x + y * y - 7);
            case FunctionType.ekli:
                return -20 * Mathf.Exp(-0.2f*Mathf.Sqrt(0.5f*(x*x+y*y)))-Mathf.Exp(0.5f*Mathf.Cos(2*Mathf.PI*x)+Mathf.Cos(2*Mathf.PI*y))+Mathf.Exp(1)+20;
            case FunctionType.izoma:
                return -Mathf.Cos(x) * Mathf.Cos(y) * Mathf.Exp(-((x-Mathf.PI) * (x - Mathf.PI) + (y - Mathf.PI)* (y - Mathf.PI)));
        }
        return 0;
    }
    public Vector3 GetPoint(float x, float y)
    {
        return new Vector3(x * Scale.x, y * Scale.y, Function(x, y) * Scale.z);
    }
    public Vector3 GetRandomPoint()
    {
        float randPointX = Random.Range(MinRange, MaxRange);
        float randPointY = Random.Range(MinRange, MaxRange);
        return GetPoint(randPointX, randPointY);
    }
    public enum FunctionType
    { 
        paraboloid,
        himmelblau,
        ekli,
        izoma
    }
}
/*
 f(x)=x2+5sin(5x)/(abs(x)+0.1) функциясынын  3.14 <= x <= 3.14 
 */

/*
 f'(x*)=((fx*+h)-f(x*))/h h=0.001
 */

/*
 
1.	Параболоид функция.       -5.0<= x,y <=5.0
f(x,y)=x**2.0 + y**2.0
2.	  Химмельблау функциясы.     -5.0<= x,y <=5.0
f(x,y)=(x**2 + y - 11)**2 + (x + y**2 -7)**2
3.	Экли функциясы .       -5.0<= x,y <=5.0
f(x,y)=-20.0 * exp(-0.2 * sqrt(0.5 * (x**2 + y**2))) - exp(0.5 * (cos(2 * pi * x) + cos(2 * pi * y))) + e + 20
4.	Изома функциясы	    -10.0<= x,y <=10.0
f(x,y)=-cos(x) * cos(y) * exp(-((x - pi)**2 + (y - pi)**2))

 
 */