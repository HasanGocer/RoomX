using UnityEngine;
using UnityEngine.Animations;

public class AnimatorEventHandler : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        InteractiveManager.Instance.PerspectiveOn();
        InteractiveManager.Instance.GetTarget();
    }

}
