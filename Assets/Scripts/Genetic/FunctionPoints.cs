using UnityEngine;

public class FunctionPoints : MonoBehaviour
{
	[SerializeField] private bool _spawn;
	[SerializeField] private GameObject _point;
	[SerializeField] private FunctionType _functionType;
	[SerializeField] private Vector3 _scale;
	public Vector3 Scale => _scale;
	[SerializeField] private float _step;
	[SerializeField] private float _minRange;
	[SerializeField] private float _maxRange;
	public float MinRange => _minRange;
	public float MaxRange => _maxRange;
	public float Step => _step;
	private void Start()
	{
		if (!_spawn) return;
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
				return (x * x + y - 11) * (x * x + y - 11) + (x + y * y - 7) * (x + y * y - 7);
			case FunctionType.ekli:
				return -20 * Mathf.Exp(-0.2f * Mathf.Sqrt(0.5f * (x * x + y * y))) - Mathf.Exp(0.5f * Mathf.Cos(2 * Mathf.PI * x) + Mathf.Cos(2 * Mathf.PI * y)) + Mathf.Exp(1) + 20;
			case FunctionType.izoma:
				return -Mathf.Cos(x) * Mathf.Cos(y) * Mathf.Exp(-((x - Mathf.PI) * (x - Mathf.PI) + (y - Mathf.PI) * (y - Mathf.PI)));
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
