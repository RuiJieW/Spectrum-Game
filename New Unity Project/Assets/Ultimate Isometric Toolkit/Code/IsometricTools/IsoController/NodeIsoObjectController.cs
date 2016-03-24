using UnityEngine;
using System.Collections;
/// <summary>
/// Node based Controller. 
/// Note: May use in a Tower Defense game for creeps.
/// </summary>
public class NodeIsoObjectController : AbstractIsoObjectController {
    //units/second
    public float speed = 2f;
    public bool loop = true;
    public Node[] nodes;

    public int nextNode = 0;

    void Start() {
        moveToNextNode();
    }
	public void moveToNextNode() {

        if (nodes.Length > 0 && nodes[nextNode] != null) {
            var newPos = nodes[nextNode].Position;
            float distance = (IsoObj.Position - newPos).magnitude;
			
			var duration = distance / speed;
           
			moveTo(newPos, (x) => EasingFunctions.Linear(x, 0, 1, duration), () => {
                if (nextNode < nodes.Length - 1) {
                    nextNode++;
                    moveToNextNode();
                } else {
                    if (loop) {
                        nextNode = 0;
                        moveToNextNode();
                    }
                }

            }, 0, duration);
        }
    }


    private void calcNextNode() {
        if (nextNode < nodes.Length - 1) {
            nextNode++;
        } else {
            if (loop) {
                nextNode = 0;
            }
        }

    }
}
