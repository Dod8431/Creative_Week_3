﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour {

	public Vector2 offset = new Vector2 (-3,-3);

	private SpriteRenderer sprRndCaster;
	private SpriteRenderer sprRndShadow;

	private Transform transCaster;
	private Transform transShadow;

	void Start()
	{
		transCaster = transform;
		transShadow = new GameObject ().transform;
		transShadow.parent = transCaster;
		transShadow.gameObject.name = "shadow";
	}
}