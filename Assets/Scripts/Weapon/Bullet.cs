using Fusion;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    private const float Default_Flight_Distance = 100;
    private const float Start_Flight_Distance = 0;
    private const float Default_Speed = 20;

    private Vector3 _flightDirection;
    private Vector3 _initialPosition;
    private float _speed;
    private float _distanceTravelled;
    private float _maxDistance;

    public void Init(float flightDistance = Default_Flight_Distance, float speed = Default_Speed)
    {
        _speed = speed;
        _flightDirection = transform.forward;
        _initialPosition = transform.position;
        _maxDistance = flightDistance;
        _distanceTravelled = Start_Flight_Distance;
    }

    public override void FixedUpdateNetwork()
    {
        if (_distanceTravelled > _maxDistance)
            Runner.Despawn(Object);
        else
        {
            transform.position += _speed * _flightDirection * Runner.DeltaTime;
            _distanceTravelled = Vector3.Distance(_initialPosition, transform.position);
        }
    }
}