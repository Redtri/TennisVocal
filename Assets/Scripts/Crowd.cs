using UnityEngine;
using System.Collections;

public class Crowd : MonoBehaviour
{
	public Sprite[] sprite;

	public int line = 20;
	public int columns = 10;

	public Vector2 scale = new Vector2(5,5);
	public float waveHeight = 5;
	public float speed = 10;
	public float waveLength = 0.2f;

	private SpriteRenderer[] rends;
	private float angle = 0;

	private Camera cam;

	private void Start()
	{
		rends = new SpriteRenderer[line * columns];
		for(int  i =0; i< rends.Length; i++)
		{
			rends[i] = CreateSprite(sprite[(int)Random.Range(0, sprite.Length)]);
		}
		cam = FindObjectOfType<Camera>();
	}

	private void Update()
	{
		angle += Time.deltaTime * speed;
		int i = 0;
		for(int x = 0; x < line; x++)
		{
			for (int y = 0; y < columns; y++)
			{
				rends[i].transform.localPosition = new Vector3(x*scale.x, Mathf.Abs(Mathf.Sin(x * Mathf.PI * waveLength + y *Mathf.PI/12 + angle)*waveHeight), y*scale.y);
				rends[i].transform.LookAt(cam.transform);
				i++;
			}
		}
	}

	private SpriteRenderer CreateSprite(Sprite sprite)
	{
		GameObject g = new GameObject("people");
		g.transform.parent = transform;
		SpriteRenderer rend = g.AddComponent<SpriteRenderer>();
		rend.sprite = sprite;
		rend.sortingOrder = 10;
		return rend;
	}


}
