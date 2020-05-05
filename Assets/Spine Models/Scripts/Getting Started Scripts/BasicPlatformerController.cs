using UnityEngine;
using UnityEngine.Events;

namespace Spine.Unity.Examples
{
    public class BasicPlatformerController : MonoBehaviour
    {
        public enum CharacterState
        {
            None,
            Idle,
            Run,
            Rise,
            Fall,
            Attack,
            Damage
        }

        [Header("Animation")]
        public SkeletonAnimationHandleExample animationHandle;

        // Events
        public event UnityAction OnJump, OnLand, OnHardLand;

        CharacterState previousState, currentState;

        public void Move(float moveH, float moveV, bool isGrounded, bool attacking)
        {
            if (moveH != 0)
            {
                animationHandle.SetFlip(moveH);
            }

            if (Mathf.Abs(moveH) > 0 && isGrounded)
            {
                currentState = CharacterState.Run;
            }
            else
            {
                currentState = CharacterState.Idle;
            }

            if (!isGrounded)
                currentState = moveV > 0 ? CharacterState.Rise : CharacterState.Fall;

            if (attacking)
            {
                currentState = CharacterState.Attack;
            }

            bool stateChanged = previousState != currentState;
            previousState = currentState;
            
            if (stateChanged)
                HandleStateChanged();
        }

        public void Damage()
        {
            currentState = CharacterState.Damage;
            bool stateChanged = previousState != currentState;
            previousState = currentState;

            if (stateChanged)
                HandleStateChanged();
        }

        public void Death(bool facingRight)
        {
            animationHandle.PlayOneShot(animationHandle.GetAnimationForState("deathB"), 0);
        }

        void HandleStateChanged()
        {
            string stateName = null;
            switch (currentState)
            {
                case CharacterState.Idle:
                    stateName = "idle";
                    break;
                case CharacterState.Run:
                    stateName = "run";
                    break;
                case CharacterState.Rise:
                    stateName = "rise";
                    break;
                case CharacterState.Fall:
                    stateName = "fall";
                    break;
                case CharacterState.Attack:
                    stateName = "attack";
                    break;
                case CharacterState.Damage:
                    stateName = "damage";
                    break;
                default:
                    break;
            }

            animationHandle.PlayAnimationForState(stateName, 0);
        }

    }
}
