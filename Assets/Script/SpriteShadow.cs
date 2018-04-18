using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour {

	public Vector3 offset = new Vector3 (0,0,-10);

	private SpriteRenderer sprRndCaster;
	private SpriteRenderer sprRndShadow;

	private Transform transCaster;
	private Transform transShadow;

	public Material shadowMaterial;
	public Color shadowColor;

	void Start()
	{
		transCaster = transform;
		transShadow = new GameObject ().transform;
		transShadow.parent = transCaster;
		transShadow.gameObject.name = "Shadow";
		transShadow.localRotation = Quaternion.Euler (new Vector3 (90, 0, 0));
		transShadow.localScale = new Vector3 (1, 1, 1);

		sprRndCaster = GetComponent<SpriteRenderer> ();
		sprRndShadow = transShadow.gameObject.AddComponent<SpriteRenderer> ();

		sprRndShadow.material = transCaster.GetComponent<Renderer>().material;
		sprRndShadow.color = new Color(0,0,0,0.5f);
		sprRndShadow.sortingLayerName = sprRndCaster.sortingLayerName;
		sprRndShadow.sortingOrder = sprRndCaster.sortingOrder - 1;
		sprRndShadow.flipX = true;

	}

	void LateUpdate()
	{
		transShadow.position = new Vector3(transCaster.position.x + offset.x, 0,transCaster.position.z + offset.z);
		sprRndShadow.sprite = sprRndCaster.sprite;
	}
}
