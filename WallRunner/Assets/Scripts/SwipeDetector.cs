using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    private bool detectSwipeOnlyAfterRelease = false;
    private float minDistanceForSwipe = 20f;

    public static event Action<SwipeData> OnSwipe = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches) {
            if(touch.phase == TouchPhase.Began) {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }
            if(!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved) {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
            if(touch.phase == TouchPhase.Ended) {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }
        
    }

    private void DetectSwipe ( ) {
        if (SwipeDistanceCheckMet()) {
            if (IsVerticalSwipe()) {
                SwipeDirection direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                SendSwipe(direction);
            } else {
                SwipeDirection direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                SendSwipe(direction);
            }
        }
    }

    private void SendSwipe(SwipeDirection direction ) {
        SwipeData swipeData = new SwipeData() {
            Direcion = direction,
            StartPosition = fingerDownPosition,
            EndPosition = fingerUpPosition
        };
        OnSwipe(swipeData);
    }

    private bool IsVerticalSwipe ( ) {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet ( ) {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance ( ) {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance ( ) {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }
}

public struct SwipeData {
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direcion;
}

public enum SwipeDirection {
    Up,
    Down,
    Left,
    Right
}
