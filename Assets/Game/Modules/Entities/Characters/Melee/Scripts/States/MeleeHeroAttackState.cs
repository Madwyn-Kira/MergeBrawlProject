using UnityEngine;

public class MeleeHeroAttackState : StateMachine
{
    private HeroController _controller;
    float distanceToHero = float.MaxValue;

    private bool _fireAlreadyStarted = false;

    public override void EnterState<T>(T controller)
    {
        _controller = controller as HeroController;
        _controller.NavAgent.isStopped = false;
        _controller.dragHandler.enabled = false;
        _controller.WeaponController.TargetFinder.StartFindCoroutine();
    }

    public override void ExitState()
    {
        _controller.WeaponController.TargetFinder.StopFindCoroutine();
        //_controller.dragHandler.enabled = true;
        _controller.WeaponController.StopFire();
    }

    public override void LocalUpdate()
    {
        if (_controller.WeaponController.TargetFinder.IsTargetFinded)
        {
            _controller.NavAgent.SetDestination(_controller.WeaponController.TargetFinder.GetTarget().transform.position);

            distanceToHero = Vector3.Distance(_controller.transform.position, _controller.WeaponController.TargetFinder.GetTarget().transform.position);

            if (distanceToHero <= _controller.NavAgent.stoppingDistance)
            {
                if (!_fireAlreadyStarted)
                {
                    _controller.WeaponController.StartFire();
                    _fireAlreadyStarted = true;
                }
            }
        }
    }
}
