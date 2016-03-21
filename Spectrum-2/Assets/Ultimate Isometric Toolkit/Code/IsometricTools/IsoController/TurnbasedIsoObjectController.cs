using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Stores actions (forward,turnRight/Left, jump) in a queue and performs these over time. New Actions may be added during runtime.
/// May use for turnbased games, such as chess,risk, monopoly, LUDO, etc.
/// </summary>

public class TurnbasedIsoObjectController : AbstractIsoObjectController {

	/// <summary>
	/// current direction to move to
	/// </summary>
    IsoDirection currentForwardDirection = IsoDirection.North;
    public float forwardSpeed = .5f;
    public float turnSpeed = .1f;

	/// <summary>
	/// flag to determine weather or not we are moving
	/// </summary>
    private bool inAction = false;

	/// <summary>
	/// Queue of actions to perform 
	/// </summary>
    Queue<Action> actionQueue = new Queue<Action>();

    [SerializeField]
    GenericGridMap<IsoObject> map;

    public Vector3 positionInMap;


    void Update() {
        if (Input.GetKeyDown(KeyCode.W))
            actionQueue.Enqueue(forward);
        if (Input.GetKeyDown(KeyCode.A))
            actionQueue.Enqueue(turnCounterclockWise);
        else if (Input.GetKeyDown(KeyCode.D))
            actionQueue.Enqueue(turnClockwise);
        if (Input.GetKeyDown(KeyCode.Space)) {
            actionQueue.Enqueue(jump);
        }

        if (!inAction && actionQueue.Count > 0) {
            actionQueue.Dequeue().Invoke();
        }
    }


    void forward() {
        if (canMoveForward()) {
            inAction = true;
            var newPos = this.IsoObj.Position + Isometric.getVector(currentForwardDirection);
            positionInMap += Isometric.getVector(currentForwardDirection);
            moveTo(newPos, (x) => EasingFunctions.QuadEaseOut(x, 0, 1, forwardSpeed), () => {
                inAction = false;
            }, 0, forwardSpeed);
        }
    }

    void turnClockwise() {
        inAction = true;
        waitForSeconds(turnSpeed, () => {
            currentForwardDirection = (IsoDirection)(((int)currentForwardDirection + 1) % 4);
            inAction = false;
        });
    }

    void turnCounterclockWise() {
        var newDirectionValue = ((int)currentForwardDirection - 1);
        if (newDirectionValue < 0)
            newDirectionValue += 4;
        newDirectionValue = newDirectionValue % 4;

        inAction = true;
        waitForSeconds(turnSpeed, () => {
            currentForwardDirection = (IsoDirection)newDirectionValue;
            inAction = false;
        });

    }

    void jump() {
        if (canJumpUp())
            jumpUp();
        else if (canJumpDown())
            jumpDown();
    }

    private void jumpUp() {
        var finalNewPos = posInMapToWorldPos(positionInMap + Isometric.getVector(currentForwardDirection) + Isometric.getVector(IsoDirection.Up));
        var tempNewPos = posInMapToWorldPos(positionInMap + Isometric.getVector(currentForwardDirection) * 2 / 3f + Isometric.getVector(IsoDirection.Up) * 4 / 3f);
        inAction = true;
        moveTo(tempNewPos, (x) => EasingFunctions.Linear(x, 0, 1, forwardSpeed * 2 / 3f), () => {
            moveTo(finalNewPos, (x) => EasingFunctions.Linear(x, 0, 1, forwardSpeed * 1 / 3f), () => {
                positionInMap = positionInMap + Isometric.getVector(currentForwardDirection) + Isometric.getVector(IsoDirection.Up);
                inAction = false; 
            }, 0, forwardSpeed * 1 / 3f);
        }, 0, forwardSpeed * 2 / 3f);
    }

    private void jumpDown() {
        var finalNewPos = posInMapToWorldPos(positionInMap + Isometric.getVector(currentForwardDirection) + Isometric.getVector(IsoDirection.Down));
        var tempNewPos = posInMapToWorldPos(positionInMap + Isometric.getVector(currentForwardDirection) * 1 / 2f + Isometric.getVector(IsoDirection.Up));
        inAction = true;
        moveTo(tempNewPos, (x) => EasingFunctions.Linear(x, 0, 1, forwardSpeed * 1 / 2f), () => {
            moveTo(finalNewPos, (x) => EasingFunctions.Linear(x, 0, 1, forwardSpeed * 1 / 2f), () => {
                inAction = false;
                positionInMap = positionInMap + Isometric.getVector(currentForwardDirection) + Isometric.getVector(IsoDirection.Down);
            }, 0, forwardSpeed * 1 / 2f);
        }, 0, forwardSpeed * 1 / 2f);
    }

	/// <summary>
	/// Initialized all values
	/// </summary>
	/// <param name="map"></param>
	/// <param name="startPosInMap"></param>
    public void init(GenericGridMap<IsoObject> map, Vector3 startPosInMap) {
        this.map = map;
        this.positionInMap = startPosInMap;
    }

    //helper functions
    bool canMoveForward() {
        try {
            return frontTile() == null && frontFloorTile() != null;
        } catch (IndexOutOfRangeException) {
            return false;
        }
    }

    bool canJumpUp() {
        try {
            return topTile() == null && topFrontTile() == null && frontTile() != null;
        } catch (IndexOutOfRangeException) {
            return false;
        }
    }

    bool canJumpDown() {
        try {
            return frontTile() == null && frontFloorTile() == null && lowerFrontFloorTile() != null;
        } catch (IndexOutOfRangeException) {
            return false;
        }
    }

    IsoObject frontTile() {
        return map[positionInMap + Isometric.getVector(currentForwardDirection)];
    }
	IsoObject topFrontTile()
	{
        return map[positionInMap + Isometric.getVector(currentForwardDirection) + Isometric.getVector(IsoDirection.Up)];
    }

	IsoObject topTile()
	{
        return map[positionInMap + Isometric.getVector(IsoDirection.Up)];
    }

	IsoObject frontFloorTile()
	{
        return map[positionInMap + Isometric.getVector(currentForwardDirection) + Isometric.getVector(IsoDirection.Down)];
    }
	IsoObject lowerFrontFloorTile()
	{
        return map[positionInMap + Isometric.getVector(currentForwardDirection) + Isometric.getVector(IsoDirection.Down) * 2];
    }

   
    private Vector3 posInMapToWorldPos(Vector3 posInMap) {
        posInMap = Vector3.Scale(posInMap, map.tileSize);
        var z = map.tileSize.z / 2;
        posInMap += new Vector3(0, 0, z);

        return posInMap;
    }

    public void setSpeed(float value) {
		Queue<Action> tmp = new Queue<Action>();
		tmp.Enqueue(() => { forwardSpeed = value; });
		foreach(Action a in actionQueue) {
			tmp.Enqueue(a);
		}
		actionQueue = tmp;
		
    }
   
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,Isometric.vectorToIsoDirection(currentForwardDirection));
    }

}
