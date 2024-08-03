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
    [SerializeField] float interactionDistance = 1.0f;
    [SerializeField] int TurnSpeed = 4;
    bool isMove = false;
    bool isPickUp = false;

    GameObject interactiveObject;

    void Update()
    {
        CheckedMove();
        InteractiveFinish();

        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIElement())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.CompareTag(paintingName))
                    {
                        interactiveObject = hit.collider.gameObject;
                        agent.velocity = Vector3.zero;
                        agent.isStopped = true;

                        StartCoroutine(CharacterAnim.Instance.TurnTargetIEnum(this.gameObject, interactiveObject.transform.position, TurnSpeed, () =>
                        {
                            Vector3 tempPos = new Vector3(interactiveObject.transform.position.x, PlaneManager.Instance.GetTargetPlane().transform.position.y, interactiveObject.transform.position.z);
                            agent.SetDestination(tempPos);
                            CharacterAnim.Instance.WalkAnim();
                            agent.isStopped = false;
                        }));
                    }
                    if (hit.collider.gameObject.CompareTag(interactiveName))
                    {
                        interactiveObject = hit.collider.gameObject;
                        agent.velocity = Vector3.zero;
                        agent.isStopped = true;

                        StartCoroutine(CharacterAnim.Instance.TurnTargetIEnum(this.gameObject, interactiveObject.transform.position, TurnSpeed, () =>
                        {
                            Vector3 tempPos = new Vector3(interactiveObject.transform.position.x, PlaneManager.Instance.GetTargetPlane().transform.position.y, interactiveObject.transform.position.z);
                            agent.SetDestination(tempPos);
                            CharacterAnim.Instance.WalkAnim();
                            agent.isStopped = false;
                        }));
                    }
                    else if (hit.collider.gameObject.CompareTag(floorName))
                    {
                        interactiveObject = null;
                        agent.velocity = Vector3.zero;
                        agent.isStopped = true;

                        StartCoroutine(CharacterAnim.Instance.TurnTargetIEnum(this.gameObject, hit.point, TurnSpeed, () =>
                        {
                            Vector3 tempPos = new Vector3(hit.point.x, PlaneManager.Instance.GetTargetPlane().transform.position.y, hit.point.z);
                            agent.SetDestination(tempPos);
                            CharacterAnim.Instance.WalkAnim();
                            agent.isStopped = false;
                        }));
                    }
                }
            }
        }
    }

    public bool GetIsMove()
    {
        return isMove;
    }

    private void InteractiveFinish()
    {
        if (interactiveObject != null)
        {
            float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(interactiveObject.transform.position.x, 0, interactiveObject.transform.position.z));
            if (distance <= interactionDistance)
            {
                isPickUp = true;
                agent.velocity = Vector3.zero;
                agent.isStopped = true;
                SelectObject();
                interactiveObject = null;
            }
        }
    }

    private void SelectObject()
    {
        if (interactiveObject.CompareTag(floorName))
        {
            CharacterAnim.Instance.PickUpAnim();
            InteractiveManager.Instance.SetTarget(interactiveObject);
            interactiveObject = null;
        }
        else if (interactiveObject.CompareTag(paintingName))
        {
            CharacterAnim.Instance.IdleAnim();
            PaintingSearchSystem.Instance.SetCamera(interactiveObject);
            interactiveObject = null;
        }
    }

    private bool IsPointerOverUIElement()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
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
}
