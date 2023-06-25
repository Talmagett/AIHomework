using UnityEngine;
public class Function : MonoBehaviour
{
    [SerializeField] private int _setsCount;
    [SerializeField] private int _randSeed;
    private float[] _xValues;
    private float[] _yDeltaValues;
    public int SetsCount => _setsCount;
    public int RandSeed=>_randSeed;
    void Start()
    {
        System.Random randomizer = new System.Random(_randSeed);

        _xValues = new float[_setsCount];
        _yDeltaValues = new float[_setsCount];

        for (int i = 0; i < _setsCount; i++)
        {
            _yDeltaValues[i] = ((float)randomizer.NextDouble()) / 10f;
            _xValues[i] = ((float)randomizer.NextDouble());
        }

        for (int i = 0; i < _setsCount; i++)
        {
            print(GetPoints(i));
        }
    }
    public (float, float) GetPoints(int index)
    {
        if (index < _setsCount)
            return (GetX(index), GetY(index));
        else
        {
            Debug.LogError("Index was outside of range");
            return (0, 0);
        }
    }
    public float GetX(int index) => _xValues[index];
    public float GetY(int index)
    {
        return 6 * _xValues[index] + 5 + _yDeltaValues[index];
    }
    public float Differential(float x)
    {
        return 6;
    }
}