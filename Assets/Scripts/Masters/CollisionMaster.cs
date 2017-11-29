using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMaster : MonoBehaviour
{

    [Header("Wall State")]
    bool isLeftWalled;
    bool wasLeftWalledLastFrame;
    bool justGotLeftWalled;

    bool isRightWalled;
    bool wasRightWalledLastFrame;
    bool justGotRightWalled;

    bool isTopWalled;
    bool wasTopWalledLastFrame;
    bool justGotTopWalled;

    bool isBottomWalled;
    bool wasBottomWalledLastFrame;
    bool justGotBottomWalled;

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

    void FixedUpdate()
    {
        ResetState();

        LeftWallDetection();

        RightWallDetection();

        TopWallDetection();

        BottomWallDetection();
    }

    void ResetState()
    {
        wasLeftWalledLastFrame = isLeftWalled;
        wasRightWalledLastFrame = isRightWalled;
        wasTopWalledLastFrame = isTopWalled;
        wasBottomWalledLastFrame = isBottomWalled;

        isLeftWalled = false;
        isRightWalled = false;
        isTopWalled = false;
        isBottomWalled = false;

        justGotLeftWalled = false;
        justGotRightWalled = false;
        justGotTopWalled = false;
        justGotBottomWalled = false;
    }

    #region DETECTION METHODS

    void LeftWallDetection()
    {
        if (!checkWall) return;

        Vector3 pos = this.transform.position + (Vector3)leftBoxPos;
        Collider2D[] results = new Collider2D[maxColliders];

        int numColliders = Physics2D.OverlapBox(pos, leftBoxSize, 0, filter, results);

        if (numColliders > 0) isLeftWalled = true;

        if (!wasLeftWalledLastFrame && isLeftWalled) justGotLeftWalled = true;
    }

    void RightWallDetection()
    {
        if(!checkWall) return;

        Vector3 pos = this.transform.position + (Vector3)rightBoxPos;
        Collider2D[] results = new Collider2D[maxColliders];

        int numColliders = Physics2D.OverlapBox(pos, rightBoxSize, 0, filter, results);

        if(numColliders > 0) isRightWalled = true;

        if(!wasRightWalledLastFrame && isRightWalled) justGotRightWalled = true;
    }

    void TopWallDetection()
    {
        if(!checkWall) return;

        Vector3 pos = this.transform.position + (Vector3)topBoxPos;
        Collider2D[] results = new Collider2D[maxColliders];

        int numColliders = Physics2D.OverlapBox(pos, topBoxSize, 0, filter, results);

        if(numColliders > 0) isTopWalled = true;

        if(!wasTopWalledLastFrame && isTopWalled) justGotTopWalled = true;
    }

    void BottomWallDetection()
    {
        if(!checkWall) return;

        Vector3 pos = this.transform.position + (Vector3)bottomBoxPos;
        Collider2D[] results = new Collider2D[maxColliders];

        int numColliders = Physics2D.OverlapBox(pos, bottomBoxSize, 0, filter, results);

        if(numColliders > 0) isBottomWalled = true;

        if(!wasBottomWalledLastFrame && isBottomWalled) justGotBottomWalled = true;
    }

    #endregion

    #region GETTERS/SETTERS

    public bool IsLeftWalled { get { return isLeftWalled; } }

    public bool WasLeftWalledLastFrame { get { return wasLeftWalledLastFrame; } }

    public bool JustGotLeftWalled { get { return justGotLeftWalled; } }

    public bool IsRightWalled { get { return isRightWalled; } }

    public bool WasRightWalledLastFrame { get { return wasRightWalledLastFrame; } }

    public bool JustGotRightWalled { get { return justGotRightWalled; } }

    public bool IsTopWalled { get { return isTopWalled; } }

    public bool WasTopWalledLastFrame { get { return wasTopWalledLastFrame; } }

    public bool JustGotTopWalled { get { return justGotTopWalled; } }

    public bool IsBottomWalled { get { return isBottomWalled; } }

    public bool WasBottomWalledLastFrame { get { return wasBottomWalledLastFrame; } }

    public bool JustGotBottomWalled { get { return justGotBottomWalled; } }

    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pos = this.transform.position + (Vector3)leftBoxPos;
        Gizmos.DrawWireCube(pos, leftBoxSize);

        Gizmos.color = Color.green;
        Vector3 pos2 = this.transform.position + (Vector3)rightBoxPos;
        Gizmos.DrawWireCube(pos2, rightBoxSize);

        Gizmos.color = Color.blue;
        Vector3 pos3 = this.transform.position + (Vector3)topBoxPos;
        Gizmos.DrawWireCube(pos3, topBoxSize);

        Gizmos.color = Color.yellow;
        Vector3 pos4 = this.transform.position + (Vector3)bottomBoxPos;
        Gizmos.DrawWireCube(pos4, bottomBoxSize);
    }
}
