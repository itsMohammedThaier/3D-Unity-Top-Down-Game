using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public Transform whoWillMove;
    public float movingLerp;
    private GameObject target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        FollowThePlayer();
    }
    void FollowThePlayer()
    {
        Vector3 targetPosition = Vector3.Lerp(whoWillMove.transform.position, target.transform.position, movingLerp);
        whoWillMove.LookAt(targetPosition);
        whoWillMove.position = targetPosition;
    }
}
