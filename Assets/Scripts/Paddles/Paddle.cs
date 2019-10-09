using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
	private IInput inputs;
	private Terrain terrain;
	public int caseNumber = 10;
	public int startCase = 5;

	private int _currentCase;
	private Vector3 _startPos;

	private bool _IsMoving;
	public bool isMoving => _IsMoving;

	public delegate void Strike(float strikeForce);
	public event Strike onStrike;

	private void Awake()
	{
		inputs = GetComponent<IInput>();
		terrain = FindObjectOfType<Terrain>();
		_currentCase = startCase;
		_startPos = transform.position;
	}

	public void Move(int direction)
	{
		if (direction == 0)
		{
			_IsMoving = false;
			return;
		}
		_IsMoving = true;
		_currentCase += direction;
		_currentCase = Mathf.Clamp(_currentCase, 0, caseNumber);
	}

	public Vector3 GetPosition()
	{
		float f = (float)_currentCase / (float)caseNumber;
		return _startPos + Vector3.forward * Mathf.Lerp(0, terrain.size.x*2,f);
	}


    void Update()
    {
		
		if(inputs.power > 0)
		{
			onStrike?.Invoke(inputs.power);
			Debug.Log("Strike");
		}
		Move(inputs.axis);

		transform.parent.position = GetPosition();
    }
}