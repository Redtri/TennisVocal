using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
	[SerializeField]
	private Terrain terrain;
	[SerializeField]
	private int caseNumber = 10;
	[SerializeField]
	private int startCase = 5;


	private int _currentCase;
	private Vector3 _startPos;

	private void Start()
	{
		_currentCase = startCase;
		_startPos = transform.position;
	}

	public void Move(int direction)
	{
		_currentCase += direction;
		_currentCase = Mathf.Clamp(_currentCase, 0, caseNumber);
	}

	private Vector3 GetPosition()
	{
		float f = (float)_currentCase / (float)caseNumber;
		Debug.Log(f);
		return _startPos + Vector3.right * Mathf.Lerp(0, terrain.size.x,f);
	}

    void Update()
    {
		transform.position = GetPosition();
    }
}
