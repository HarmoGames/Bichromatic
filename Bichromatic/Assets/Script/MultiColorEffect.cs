using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

// public enum TypeOfColor {SpriteRenderer, Image, Both};

public class MultiColorEffect : MonoBehaviour {

	// public TypeOfColor ColorType;

	public Image[] images;
	public SpriteRenderer[] sprites;
	public Tilemap[] tilemaps;

	private Color myColor;
	private float RedC;
	private float GreenC;
	private float BlueC;
	private float OpacityC;

	public float ColorSpeed;
	public float AddValue;

	void Start()
	{
		RedC = 1;
		GreenC = 0;
		BlueC = 0;
		OpacityC = 1;
		StartCoroutine(GreenPlus());
	}

	void Update()
	{
		foreach(SpriteRenderer sprite in sprites){
			if(sprite){
				sprite.color = myColor;
			}
		}

		foreach(Image image in images){
			if(image){
				image.color = myColor;
			}
		}

		foreach(Tilemap tilemap in tilemaps){
			if(tilemap){
				tilemap.color = myColor;
			}
		}

		myColor = new Color(RedC, GreenC, BlueC, OpacityC);
	}

	IEnumerator GreenPlus()
	{
		if(GreenC > 1)
		{
			GreenC = 1;
			StartCoroutine(RedMinus());
			StopCoroutine(GreenPlus());
		}
		else
		{
			GreenC += AddValue;
			yield return new WaitForSeconds(ColorSpeed);
			StartCoroutine(GreenPlus());
		}
	}

	IEnumerator RedMinus()
	{
		if(RedC < 0)
		{
			RedC = 0;
			StartCoroutine(BluePlus());
			StopCoroutine(RedMinus());
		}
		else
		{
			RedC -= AddValue;
			yield return new WaitForSeconds(ColorSpeed);
			StartCoroutine(RedMinus());
		}
	}

	IEnumerator BluePlus()
	{
		if(BlueC > 1)
		{
			BlueC = 1;
			StartCoroutine(GreenMinus());
			StopCoroutine(BluePlus());
		}
		else
		{
			BlueC += AddValue;
			yield return new WaitForSeconds(ColorSpeed);
			StartCoroutine(BluePlus());
		}
	}

	IEnumerator GreenMinus()
	{
		if(GreenC < 0)
		{
			GreenC = 0;
			StartCoroutine(RedPlus());
			StopCoroutine(GreenMinus());
		}
		else
		{
			GreenC -= AddValue;
			yield return new WaitForSeconds(ColorSpeed);
			StartCoroutine(GreenMinus());
		}
	}

	IEnumerator RedPlus()
	{
		if(RedC > 1)
		{
			RedC = 1;
			StartCoroutine(BlueMinus());
			StopCoroutine(RedPlus());
		}
		else
		{
			RedC += AddValue;
			yield return new WaitForSeconds(ColorSpeed);
			StartCoroutine(RedPlus());
		}
	}

	IEnumerator BlueMinus()
	{
		if(BlueC < 0)
		{
			BlueC = 0;
			StartCoroutine(GreenPlus());
			StopCoroutine(BlueMinus());
		}
		else
		{
			BlueC -= AddValue;
			yield return new WaitForSeconds(ColorSpeed);
			StartCoroutine(BlueMinus());
		}
	}
}
