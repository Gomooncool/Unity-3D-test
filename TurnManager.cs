using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    enum State
    {
        BattleStart,
        PlayerTurn,
        EnemyTurn,
        BattleIsWon,
        BattleIsLost,
        Exit
    }

    public int TurnNumber { get; private set; }
    private State CurrentState { get; set; }

    public TurnManager()
    {
        TurnNumber = 1;
        CurrentState = State.BattleStart;
    }

    public void PlayerActionPointsOver()
    {
        if (CurrentState == State.PlayerTurn)
            CurrentState = State.EnemyTurn;

        TurnNumber++;

    }


    public void EnemyActionPointsOver()
    {
        if (CurrentState == State.EnemyTurn)
            CurrentState = State.PlayerTurn;

    }

    public void EnemiesDestroyed()
    {
        if ((CurrentState == State.PlayerTurn) |  (CurrentState == State.EnemyTurn))
            CurrentState = State.BattleIsWon;

    }

    public void PlayerDestroyed()
    {
        if ((CurrentState == State.PlayerTurn) | (CurrentState == State.EnemyTurn))
            CurrentState = State.BattleIsLost;

    }

    public void ExitButtonPressed()
    {
        if ((CurrentState == State.BattleIsLost) | (CurrentState == State.BattleIsWon))
            CurrentState = State.Exit;

    }







}
