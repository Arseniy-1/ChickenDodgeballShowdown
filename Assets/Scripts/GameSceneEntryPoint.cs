using System.Collections.Generic;
using Darchi.DodgeballShowdown.StateMashine;
using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _playerBehaviourPrefab;
    [SerializeField] private EnemyBehaviour _enemyBehaviourPrefab;

    [SerializeField] private int _playerCount = 1;
    [SerializeField] private int _enemyCount = 1;

    private List<EntityBehaviour> _enemyBehaviours = new();
    private List<EntityBehaviour> _playerBehaviours = new();

    private void Awake()
    {
        for (int i = 0; i < _playerCount; i++)
        {
            PlayerBehaviour player = Instantiate(_playerBehaviourPrefab);

            List<IState> playerStates = new List<IState>
            {
            new IdleState(player),
            new PlayerMoveState(player),
            new AttackState(player),
            new JumpState(player)
            };

            PlayerStateMashine playerStateMashine = new PlayerStateMashine(playerStates);
            player.Construct(playerStateMashine);

            _playerBehaviours.Add(player);
        }

        for (int i = 0; i < _enemyCount; i++)
        {
            EnemyBehaviour enemy = Instantiate(_enemyBehaviourPrefab);

            List<IState> enemyStates = new List<IState>
            {
            new IdleState(enemy),
            new EnemyMoveState(enemy),
            new AttackState(enemy),
            new JumpState(enemy)
            };

            EnemyStateMashine enemyStateMashine = new EnemyStateMashine(enemyStates);
            enemy.Construct(enemyStateMashine);

            _enemyBehaviours.Add(enemy);
        }

        List<IState> firstSquadStates = new List<IState>
        {
            new IdleSquadState(_playerBehaviours),
            new RunSquadState(_playerBehaviours),
            new AnyHasBallSquadState(_playerBehaviours)
        };

        List<IState> secondSquadStates = new List<IState>
        {
            new IdleSquadState(_enemyBehaviours),
            new RunSquadState(_enemyBehaviours),
            new AnyHasBallSquadState(_enemyBehaviours)
        };

        SquadStateMashine firstSquadStateMashine = new(firstSquadStates);
        SquadStateMashine secondSquadStateMashine = new(secondSquadStates);

        foreach (var state in firstSquadStates)
        {
            state.Initialize(firstSquadStateMashine);
        }

        foreach (var state in secondSquadStates)
        {
            state.Initialize(secondSquadStateMashine);
        }

        List<Squad> squad = new List<Squad>
        {
            new Squad(_playerBehaviours, firstSquadStateMashine),
            new Squad(_enemyBehaviours, secondSquadStateMashine)
        };

        Arena arena = new(squad);
    }
}
