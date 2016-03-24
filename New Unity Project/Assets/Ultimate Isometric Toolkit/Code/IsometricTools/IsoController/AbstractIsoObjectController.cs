using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Abstract IsoObject controller class. It provides implementations for common functionalities.
/// Note: You may inherite from this class in order to write your own custom controller behaviour
/// </summary>
[RequireComponent(typeof(IsoObject))]
public abstract class AbstractIsoObjectController : MonoBehaviour{

    public delegate float EasingFunction(float currrentTime);

	[HideInInspector]
	public IsoObject isoObj;

	public IsoObject IsoObj
	{
		get
		{
			if (isoObj == null)
				isoObj = GetComponent<IsoObject>();
			return isoObj;
		}
		private set { isoObj = value; }
	}

    void Awake() {
        this.isoObj = this.GetOrAddComponent<IsoObject>();
    }

    /// <summary>
    /// Moves the Object from its current position to newPosition using a easingFunction providing a duration this transition should take.
    /// After the transition to newPosition is complete the callbackFunction is called.
    /// </summary>
    /// <param name="newPostion">the new position</param>
    /// <param name="easingFunction">easing function used for the transition</param> http://easings.net for further help
    /// <param name="callback">called upon finished move</param>
    /// <param name="delay">initial delay</param>
    protected void moveTo(Vector3 newPostion, EasingFunction easingFunction, Action callback, float delay, float duration) {
        StartCoroutine(customEasing(callback, newPostion, delay, duration, easingFunction));
    }


    private IEnumerator customEasing(Action cb, Vector3 to, float delay, float duration, EasingFunction function) {
        var from = isoObj.Position;
        yield return new WaitForSeconds(delay);
        float currentLerpTime = 0;

        while (currentLerpTime < duration) {
            currentLerpTime += Time.deltaTime;

            float x = (float)function(currentLerpTime);
            isoObj.Position = from + (to - from) * x;
            yield return null;
        }
        currentLerpTime = duration;
        isoObj.Position = to;

        cb();
    }


    /// <summary>
    /// Waits for duration (in seconds) then performs the callback. Simple Wrapper for Unity's waitForSeconds. 
    /// </summary>
    /// <param name="duration">time to wait</param> delay in seconds
    /// <param name="callback">called upon finish</param> 
    protected void waitForSeconds(float duration, Action callback) {
        StartCoroutine(_waitForSeconds(duration, callback));
    }
    protected void waitForSeconds(float duration) {
        StartCoroutine(_waitForSeconds(duration, () => { }));
    }

    private IEnumerator _waitForSeconds(float duration, Action cb) {
        yield return new WaitForSeconds(duration);

        cb();
    }
}
