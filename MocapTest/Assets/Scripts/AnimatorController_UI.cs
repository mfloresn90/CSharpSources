using UnityEngine;
using System.Collections;

public class AnimatorController_UI : MonoBehaviour 
{
	private Animator animator;

	void Start()
	{
		this.animator = this.GetComponent<Animator>();
	}

	void Update()
	{
		animator.SetFloat("walk", Input.GetAxis("Vertical"));
		animator.SetFloat("turn", Input.GetAxis("Horizontal"));
	}
		
}
