using UnityEngine;
using System.Collections;

public class Terrain : MonoBehaviour
{
	[SerializeField]
	private Vector2 _size = new Vector2(10,12);
	public Vector2 size { get { return _size; } }
}
