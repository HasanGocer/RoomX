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
                        StartCoroutine(CharacterAnim.Instance.TurnTargetIEnum(this.gameObject, interactiveObject.transform.position, TurnSpeed, () => { agent.SetDestination(interactiveObject.transform.position); CharacterAnim.Instance.WalkAnim(); }));
                    }
                    else if (hit.collider.gameObject.CompareTag(floorName))
                    {
                        interactiveObject = null;
                        StartCoroutine(CharacterAnim.Instance.TurnTargetIEnum(this.gameObject, hit.point, TurnSpeed, () => { agent.SetDestination(hit.point); CharacterAnim.Instance.WalkAnim(); }));
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
        if (agent.velocity == Vector3.zero) if (isMove == true)
            {
                isMove = false;
                CharacterAnim.Instance.IdleAnim();
            }
            else { }
        else if (isMove == false) isMove = true;

    }
}
