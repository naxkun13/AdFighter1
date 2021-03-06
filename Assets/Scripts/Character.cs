﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
	[SerializeField]
	private float speed;

	private Rigidbody2D rb2d;

	public bool IsMoving {
		get { 
			return direction.x != 0 || direction.y != 0;
		}
	}

	private Animator animator;
	protected Vector2 direction;

	protected virtual void Start () {
		animator = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	protected virtual void Update () {
		HandleLayers ();

	}

	private void FixedUpdate()
	{
		Move();
	}

	public void Move()
	{
		rb2d.velocity = direction.normalized * speed;
	}

	public void HandleLayers()
	{
		if (IsMoving) {
			AnimateMovement  (direction);
			ActivateLayer("WalkLayer");

		} else 
		{
			ActivateLayer ("BaseLayer");
		}
	}

	public void AnimateMovement (Vector2 direction)
	{
		// переключение условий в слоях аниматора
        //  animator.SetLayerWeight (1,1); 
		animator.SetFloat ("x", direction.x);
		animator.SetFloat ("y", direction.y);
	}

	public void ActivateLayer(string layername)
	{
		for (int i = 0; i < animator.layerCount; i++) {
		
			animator.SetLayerWeight (i, 0);
		}

		animator.SetLayerWeight (animator.GetLayerIndex (layername), 1);
	}
}
