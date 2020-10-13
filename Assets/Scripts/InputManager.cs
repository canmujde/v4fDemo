using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static InputManager instance;

	private Vector2 fingerDownPos;
	private Vector2 fingerUpPos;
	[SerializeField] private bool canPlay;
	[SerializeField] private bool detectSwipeAfterRelease = true;
	[SerializeField] private float swipeThreshold = 20f;

    public bool CanPlay { get => canPlay; set => canPlay = value; }

    private void Awake()
    {
        instance = this;
	}

    private void Start()
    {
		CanPlay = false;
	}

    private void Update()
    {
		if (!CanPlay) return;
#if UNITY_EDITOR
        HandleStandaloneInput();
#endif
#if UNITY_ANDROID || UNITY_IOS
        HandleMobileInput();
#endif
    }

    private void HandleMobileInput()
    {
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				fingerUpPos = touch.position;
				fingerDownPos = touch.position;
			}

			if (touch.phase == TouchPhase.Moved)
			{
				if (!detectSwipeAfterRelease)
				{
					fingerDownPos = touch.position;
					DetectSwipe();
				}
			}

			if (touch.phase == TouchPhase.Ended)
			{
				fingerDownPos = touch.position;
				DetectSwipe();
			}
		}
	}

    private void HandleStandaloneInput()
    {
		if (Input.GetMouseButtonDown(0))
		{
			fingerUpPos = Input.mousePosition; 
			fingerDownPos = Input.mousePosition;
		}

		if (Input.GetMouseButton(0))
		{
			if (!detectSwipeAfterRelease)
			{
				fingerDownPos = Input.mousePosition;
				DetectSwipe();
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			fingerDownPos = Input.mousePosition;
			DetectSwipe();
		}
	}



	void DetectSwipe()
	{

		if (VerticalMoveValue() > swipeThreshold && VerticalMoveValue() > HorizontalMoveValue())
		{
			//Debug.Log("Vertical");
			if (fingerDownPos.y - fingerUpPos.y > 0)
			{
				OnSwipeUp();
			}
			else if (fingerDownPos.y - fingerUpPos.y < 0)
			{
				OnSwipeDown();
			}
			fingerUpPos = fingerDownPos;

		}
		else if (HorizontalMoveValue() > swipeThreshold && HorizontalMoveValue() > VerticalMoveValue())
		{
			//Debug.Log("Horizontal");
			if (fingerDownPos.x - fingerUpPos.x > 0)
			{
				OnSwipeRight();
			}
			else if (fingerDownPos.x - fingerUpPos.x < 0)
			{
				OnSwipeLeft();
			}
			fingerUpPos = fingerDownPos;

		}
		else
		{
			//Debug.Log("No Swipe");
		}
	}

	float VerticalMoveValue()
	{
		return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
	}

	float HorizontalMoveValue()
	{
		return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
	}

	void OnSwipeUp()
	{
		PlayerController.instance.Jump();
	}

	void OnSwipeDown()
	{
		//no action 
	}

	void OnSwipeLeft()
	{
		PlayerController.instance.Move(0);
	}

	void OnSwipeRight()
	{
		PlayerController.instance.Move(1);
	}
}