using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 target;
    public float Speed = 1f;
    private Camera cam;
    public GameManager gameManager;
    public ObjectPooler objectPooler;
    private IGetTarget targetGetter;

    private void Awake()
    {
        gameManager = InstanceManager<GameManager>.GetInstance("GameManager");
        objectPooler = InstanceManager<ObjectPooler>.GetInstance("ObjectPooler");

        gameManager.Player = gameObject;

#if UNITY_ANDROID
            targetGetter = new AndroidTargetGetter();
#elif UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
            targetGetter = new PCTargetGetter();
#endif
    }


    private void Start()
    {
        cam = Camera.main;
        StartCoroutine(DropRadioactive());
    }

    private IEnumerator DropRadioactive()
    {
        objectPooler.SpawnFromPool("Radioactive", transform.position, transform.rotation);
        yield return new WaitForSeconds(Random.Range(0.4f, 1f));
        StartCoroutine(DropRadioactive());
    }

    private void Update()
    {

        target = targetGetter.GetTarget();
        if (Vector2.Distance(transform.position, target) > transform.localScale.x && !gameManager.isPaused)
        {
            Move();
            Rotate();
        }
    }

    private void Move()
    {
        float step = Speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    private void Rotate()
    {
        var dir = target - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
