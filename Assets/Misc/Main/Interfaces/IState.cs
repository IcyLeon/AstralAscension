using UnityEngine;

public interface IState
{
    void Enter();
    void Update();
    void Exit();
    void FixedUpdate();
    void OnCollisionEnter(Collision collision);
    void OnCollisionExit(Collision collision);
    void OnCollisionStay(Collision collision);
    void OnAnimationTransition();
}