using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Genetic : MonoBehaviour
{
	[SerializeField] private FunctionPoints _function;
	[SerializeField] private GameObject _point;
	[SerializeField] private int _maxIterations;
	[SerializeField] private int _count;
	[SerializeField] private int _removeLast;
	[SerializeField] private bool _isMax;
	[SerializeField] private float _mutationProbability;
	[SerializeField] private float _mutationPower;
	private Transform[] _creatures=null;
	
	protected void Start()
	{
		_creatures=new Transform[_count];
		for (int i = 0; i < _count; i++) {
			_creatures[i] = Instantiate(_point,_function.GetRandomPoint(),Quaternion.identity,transform).transform;
		}
		StartCoroutine(Generation());
	}
	
	private IEnumerator Generation()
	{
		for (int i = 0; i < _maxIterations; i++) {
			CreateNewGeneration();
			yield return null;
		}
	}
	private void CreateNewGeneration()
	{
		Vector3[] positions;
		if(_isMax)
			positions=_creatures.Select(t=>t.position).OrderBy(t=>t.z).ToArray();
		else 
			positions=_creatures.Select(t=>t.position).OrderByDescending(t=>t.z).ToArray();
		for (int i = 0; i < _count; i++) {
				if(Random.value<0.5f)
					_creatures[i].position=_function.GetPoint(positions[i].x,positions[Random.Range(0,_count-_removeLast)].y);
				else
					_creatures[i].position=_function.GetPoint(positions[Random.Range(0,_count-_removeLast)].x,positions[i].y);
			
			if(Random.value<_mutationProbability)
				Mutation(_creatures[i]);
		}
	}
	private void Mutation(Transform point)
	{
		point.position=_function.GetPoint(point.position.x+Random.Range(-_mutationPower,_mutationPower),point.position.y+Random.Range(-_mutationPower,_mutationPower));
	}	
}
