// 23.54
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    // 23.54 references to Variable of HandVisual
    public HandVisual RightHand;
    // 23.54
    public HandVisual LeftHand;

    // 23.54 reference to variable for index fingers
    public Transform RightIndexTip;
    // 23.54
    public Transform LeftIndexTip;

    // 23.54 material to be used while drawing with fingers pinched
    public Material DrawingMaterial;

    // 23.54 thickness of the pingers pinching drawing
    [Range(0.01f, 0.1f)]
    public float penWidth;

    // 23.54 to detect how much movement should be considered as valid for drawing
    [Range(0.001f, 0.05f)]
    public float precision;

    // 23.54 indexes to capture current position or trace current position of hand
    int indexR = 0;
    // 23.54
    int indexL = 0;

    // 23.54 dynamically created during drawing
    LineRenderer LeftLineRenderer;
    // 23.54
    LineRenderer RightLineRenderer;

    // 23.54 container to hold the line
    GameObject DrawingContainer;

    // Start is called before the first frame update
    void Start()
    {
        // 23.54 new game object called DrawingContainer initialized
        DrawingContainer = new GameObject("DrawingContainer");
    }

    // Update is called once per frame
    void Update()
    {
        // 23.54 check hand tracking
        // 23.54 for finger join and other functionalities
        if (RightHand.Hand.IsTrackedDataValid)
        {
            // 23.54 check indexfinger is pinching or not
            if (RightHand.Hand.GetIndexFingerIsPinching())
            {
                // 23.54 call the function with right index variable
                DrawRight(RightIndexTip);
            }
            // 23.54 when renderer is not null and we are not pinching, set it to null
            else if (RightLineRenderer != null)
            {
                // 23.54
                RightLineRenderer = null;
            }
        }
        // 23.54
        if (LeftHand.Hand.IsTrackedDataValid)
        {
            // 23.54
            if (LeftHand.Hand.GetIndexFingerIsPinching())
            {
                // 23.54
                DrawLeft(LeftIndexTip);
            }
            // 23.54
            else if (LeftLineRenderer != null)
            {
                // 23.54
                LeftLineRenderer = null;
            }
        }
    }

    // 23.54 function to drawwith right hand
    void DrawRight(Transform drawingPoint)
    {
        // 23.54 check if we are starting a new line
        if (RightLineRenderer == null)
        {
            // 23.54 we want line renderer on a gameobject
            // 23.54 so initiliazing game object and on
            // 23.54 that attaching line renderer and
            // 23.54 getting reference of the variable
            RightLineRenderer = new GameObject().AddComponent<LineRenderer>();
            // 23.54 material to generate while drawing
            RightLineRenderer.material = DrawingMaterial;
            // 23.54
            RightLineRenderer.positionCount = 1;
            // 23.54 starting and ending width of pen
            RightLineRenderer.startWidth = RightLineRenderer.endWidth = penWidth;
            // 23.54 set the first position
            RightLineRenderer.SetPosition(0, drawingPoint.position);
            // 23.54
            indexR = 0;
            // 23.54 while we create line renderer we need to bring it inside drawing container
            RightLineRenderer.transform.parent = DrawingContainer.transform;
        }
        // 23.54 if its not null logic
        else
        {
            // 23.54 distance between current index/position in indexR/indexL and the line renderer
            // 23.54 checking how much movement should be considered as valid for drawing with precision
            if (Vector3.Distance(RightLineRenderer.GetPosition(indexR), drawingPoint.position) >= precision)
            {
                // 23.54 incrementing position of hand tracing
                indexR++;
                // 23.54 incorrect code line causing the bug of starting line misbehaving
                // 23.54 RightLineRenderer.positionCount += indexR;
                // 23.54 incrementing position count
                RightLineRenderer.positionCount++;
                // 23.54 setting the position of hand tracing as our drawing point
                RightLineRenderer.SetPosition(indexR, drawingPoint.position);
            }
        }
    }

    // 23.54 function to draw with left hand
    void DrawLeft(Transform drawingPoint)
    {
        // 23.54
        if (LeftLineRenderer == null)
        {
            // 23.54
            LeftLineRenderer = new GameObject().AddComponent<LineRenderer>();
            // 23.54
            LeftLineRenderer.material = DrawingMaterial;
            // 23.54
            LeftLineRenderer.positionCount = 1;
            // 23.54
            LeftLineRenderer.startWidth = LeftLineRenderer.endWidth = penWidth;
            // 23.54
            LeftLineRenderer.SetPosition(0, drawingPoint.position);
            // 23.54
            indexL = 0;
            // 23.54
            LeftLineRenderer.transform.parent = DrawingContainer.transform;
        }
        // 23.54
        else
        {
            // 23.54
            if (Vector3.Distance(LeftLineRenderer.GetPosition(indexL), drawingPoint.position) >= precision)
            {
                // 23.54
                indexL++;
                // 23.54
                LeftLineRenderer.positionCount++;
                // 23.54
                LeftLineRenderer.SetPosition(indexL, drawingPoint.position);
            }
        }
    }

    // 23.55
    public void changeColor(Material material)
    {
        // 23.55
        DrawingMaterial = material;
    }

    // 23.56
    public void Undo()
    {
        // 23.56 the following condition is not in video but fixed by another dev
        // 23.56 hands of ovr get stuck so handling with condition DrawingContainer.transform.childCount
        if (DrawingContainer != null && (DrawingContainer.transform.childCount != 0))
            // 23.56 destroy most recent line renderer
            Destroy((DrawingContainer.transform.GetChild(DrawingContainer.transform.childCount - 1)).gameObject);
    }

    // 23.56
    public void EraseAll()
    {
        // 23.56 destroy existing gameobject
        Destroy(DrawingContainer);
        // 23.56 create new game object called DrawingContainer
        DrawingContainer = new GameObject("DrawingContainer");
    }
}
