using UnityEngine;
using UnityEngine.Animations;

public class AnimatorEventHandler : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CharacterMove.Instance.NavmeshAgentOn();
        InteractiveManager.Instance.PerspectiveOn();
        InteractiveManager.Instance.GetTarget();
    }

}
