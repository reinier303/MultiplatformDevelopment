using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public interface IGetTarget
{
    // Maybe we need to store information about what achievements are called
    //  for each platform
    //Dictionary<Achievement,string> SKU_Conversion;
    Vector2 GetTarget();
}

public class PCTargetGetter : IGetTarget
{
    public Vector2 GetTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector2 target = ray.GetPoint(100);
        return target;
    }
}

public class AndroidTargetGetter : IGetTarget
{
    Vector2 previousTarget;
    public Vector2 GetTarget()
    {
        if (Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Vector2 target = ray.GetPoint(100);
            previousTarget = target;
            return target;
        }
        else
        {
            return previousTarget;
        }
    }
}

public class PlayerMovementPreprocessor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
