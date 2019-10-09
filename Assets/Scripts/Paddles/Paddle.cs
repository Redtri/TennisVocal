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
	[SerializeField]
	private KeyCode upKey = KeyCode.UpArrow;
	[SerializeField]
	private KeyCode downKey = KeyCode.DownArrow;
    public bool isMoving { get; private set; }

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

	public Vector3 GetPosition()
	{
		float f = (float)_currentCase / (float)caseNumber;
		return _startPos + Vector3.forward * Mathf.Lerp(0, terrain.size.x,f);
	}

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			Move(1);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)){
			Move(-1);
		}

		transform.position = GetPosition();
    }
}
