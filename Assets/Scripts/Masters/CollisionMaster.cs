using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMaster : MonoBehaviour
{

    [Header("Wall State")]
    public bool isLeftWalled;
    public bool wasLeftWalledLastFrame;
    public bool justGotLeftWalled;

    public bool isRightWalled;
    public bool wasRightWalledLastFrame;
    public bool justGotRightWalled;

    public bool isTopWalled;
    public bool wasTopWalledLastFrame;
    public bool justGotTopWalled;

    public bool isBottomWalled;
    public bool wasBottomWalledLastFrame;
    public bool justGotBottomWalled;

    [Header("Filter")]
    public ContactFilter2D filter;
    public int maxColliders = 1;
    public bool checkWall = true;

    [Header("Box properties")]
    public Vector2 leftBoxPos;
    public Vector2 leftBoxSize;

    public Vector2 rightBoxPos;
    public Vector2 rightBoxSize;

    public Vector2 topBoxPos;
    public Vector2 topBoxSize;

    public Vector2 bottomBoxPos;
    public Vector2 bottomBoxSize;

   /* void FixedUpdate()
    {
        ResetState();

        RightWallDetection();

        LeftWallDetection();

        TopWallDetection();

        BottomWallDetection();
    }

    void ResetState()
    {
        wasRightWalledLastFrame = isWalled;
        isNotWalling = !isWalled;

        isWalled = false;
        justNotWalled = false;
        justGotWalled = false;
    }

    void RightWallDetection()
    {
        if (!checkWall) return;

        Vector3 pos = this.transform.position + (Vector3)wallBoxPos;
        Collider2D[] results = new Collider2D[maxColliders];

        int numColliders = Physics2D.OverlapBox(pos, wallBoxSize, 0, filter, results);

        if (numColliders > 0)
        {
            isWalled = true;
        }
        if (!wasWalledLastFrame && isWalled) justGotWalled = true;
        if (wasWalledLastFrame && !isWalled) justNotWalled = true;

        if (justNotWalled) Debug.Log("Just Walled");
        if (justGotWalled) Debug.Log("Just UnWalled");
    }

    public void Flip(bool isFacingRight)
    {
        wallBoxPos.x = -wallBoxPos.x;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pos2 = this.transform.position + (Vector3)wallBoxPos;
        Gizmos.DrawWireCube(pos2, wallBoxSize);
    }*/
}
