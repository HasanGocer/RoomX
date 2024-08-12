using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class CharacterMove : MonoSingleton<CharacterMove>
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject targetTemp;
    [SerializeField] string floorName;
    [SerializeField] string interactiveName;
    [SerializeField] string paintingName;
    [SerializeField] string DoorName;
    [SerializeField] float interactionDistance = 1.0f;
    [SerializeField] float TurnSpeed = 4;
    [SerializeField] float navMeshSampleDistance = 1.0f;
    bool isMove = false;
    bool isPickUp = false;

    GameObject interactiveObject;

    public bool GetIsMove() { return isMove; }
    public void NavmeshAgentOff() { agent.enabled = false; }
    public void NavmeshAgentOn() { agent.enabled = true; }


    void Update()
    {
        CheckedMove();
        InteractiveFinish();

        if (Input.GetMouseButtonDown(0))
            if (!IsPointerOverUIElement())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    #region alan içinde mi kontrol
                    Vector3 targetPosition = hit.point;
                    NavMeshHit navHit;

                    if (NavMesh.SamplePosition(targetPosition, out navHit, navMeshSampleDistance, NavMesh.AllAreas))
                        targetPosition = navHit.position;
                    else
                        return;
                    #endregion

                    if (hit.collider.gameObject.CompareTag(floorName))
                    {
                        ResetMove(null);
                        SaveHit(hit.point);

                        StartCoroutine(CharacterAnim.Instance.TurnTargetIEnum(this.gameObject, interactiveObject.transform.position, TurnSpeed, () =>
                        {
                            agent.SetDestination(interactiveObject.transform.position);
                            LastMove();
                        }));
                    }
                }
            }
    }

    private void CheckedMove()
    {
        if (agent.velocity == Vector3.zero)
        {
            if (isMove)
            {
                isMove = false;

                if (!isPickUp)
                    CharacterAnim.Instance.IdleAnim();
                else
                    isPickUp = false;
            }
        }
        else
            if (!isMove)
            isMove = true;
    }
    private void InteractiveFinish()
    {
        if (interactiveObject != null)
        {
            float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(interactiveObject.transform.position.x, 0, interactiveObject.transform.position.z));
            if (distance <= interactionDistance)
            {
                agent.ResetPath();
                agent.velocity = Vector3.zero;
                isPickUp = true;
                CharacterAnim.Instance.IdleAnim();
                interactiveObject = null;
            }
        }
    }

    private bool IsPointerOverUIElement()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        for (int i = 0; i < results.Count; i++)
            if (results[i].gameObject.CompareTag("Clear UI")) results.RemoveAt(i);
        return results.Count > 0;
    }

    private void SaveHit(Vector3 hit)
    {
        GameObject hitGO = new GameObject("hit");
        hitGO.transform.position = hit;
        interactiveObject = hitGO;
    }
    private void ResetMove(GameObject tempInteractiveObject)
    {
        interactiveObject = tempInteractiveObject;
        agent.ResetPath();
        agent.velocity = Vector3.zero;
    }
    private void LastMove()
    {
        CharacterAnim.Instance.WalkAnim();
        agent.isStopped = false;
    }
}
