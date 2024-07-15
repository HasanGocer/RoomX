using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;

public class CharacterMove : MonoSingleton<CharacterMove>
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject targetTemp;
    [SerializeField] string floorName;
    [SerializeField] string interactiveName;
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
            if (!IsPointerOverUIElement())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                    if (hit.collider.gameObject.CompareTag(interactiveName))
                    {
                        interactiveObject = hit.collider.gameObject;
                        agent.velocity = Vector3.zero;
                        agent.Stop();

                        StartCoroutine(CharacterAnim.Instance.TurnTargetIEnum(this.gameObject, interactiveObject.transform.position, TurnSpeed, () =>
                        {
                            agent.SetDestination(interactiveObject.transform.position);
                            CharacterAnim.Instance.WalkAnim();
                            agent.Resume();
                        }));
                    }
                    else if (hit.collider.gameObject.CompareTag(floorName))
                    {
                        interactiveObject = null;
                        agent.velocity = Vector3.zero;
                        agent.Stop();

                        StartCoroutine(CharacterAnim.Instance.TurnTargetIEnum(this.gameObject, hit.point, TurnSpeed, () =>
                        {
                            agent.SetDestination(hit.point);
                            CharacterAnim.Instance.WalkAnim();
                            agent.Resume();
                        }));
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
            float distance = Vector3.Distance(transform.position, interactiveObject.transform.position);
            if (distance <= interactionDistance)
            {
                isPickUp = true;
                CharacterAnim.Instance.PickUpAnim();
                interactiveObject.GetComponent<InteractiveID>().TouchObject();
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
        return results.Count > 0;
    }
    private void CheckedMove()
    {
        if (agent.velocity == Vector3.zero)
            if (isMove)
            {
                isMove = false;

                if (!isPickUp)
                    CharacterAnim.Instance.IdleAnim();
                else
                    isPickUp = false;
            }
            else if (!isMove) isMove = true;
    }
}
