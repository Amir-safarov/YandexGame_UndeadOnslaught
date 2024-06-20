using System;
using UnityEngine;

public class DeadZombiesCounter : MonoBehaviour
{
    public enum ScoringStates
    {
        FirstState,
        SecondState,
        ThirdState,
    }


    private int _currentDeadZombies;
    private int _totalDeadZombiesPP;
    private int _totalDeadZombiesYG;

    private const string TotalDeadZombiesCount = "TotalDeadZombiesCount";

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(TotalDeadZombiesCount))
            PlayerPrefs.SetInt(TotalDeadZombiesCount, 0);
        _totalDeadZombiesPP = PlayerPrefs.GetInt(TotalDeadZombiesCount);
        print($"������ ���� �� ��������� �� ������: {_totalDeadZombiesPP}");
        EventManager.TransferZombieDeathEvent.AddListener(RegistartionNewZombie);
        EventManager.RestartSceneEvent.AddListener(ResetCurrentDeadZombies);
    }

    public int GetCurrentDeadZombiesCount()
    {
        return _currentDeadZombies;
    }

    public int GetTotalDeadZombiesCount()
    {
        return _totalDeadZombiesPP;
    }

    private void RegistartionNewZombie()
    {
        _currentDeadZombies++;
        print($"����� ������ �����: {_currentDeadZombies}");
        TransferScoruingState();
        CheckDeadZombiesCount();
    }

    private void TransferScoruingState()
    {
        if (_currentDeadZombies < 10)
            EventManager.InvokeTransferScore(ScoringStates.FirstState);
        else if (_currentDeadZombies > 10 && _currentDeadZombies < 30)
            EventManager.InvokeTransferScore(ScoringStates.SecondState);
        else
            EventManager.InvokeTransferScore(ScoringStates.ThirdState);
    }

    private void CheckDeadZombiesCount()
    {
        if (_currentDeadZombies > _totalDeadZombiesPP)
        {
            PlayerPrefs.SetInt(TotalDeadZombiesCount, _currentDeadZombies);
            _totalDeadZombiesPP = PlayerPrefs.GetInt(TotalDeadZombiesCount);
            print($"����� ������ ���� �� ���������: {_totalDeadZombiesPP}");
        }
    }

    private void ResetCurrentDeadZombies()
    {
        _currentDeadZombies = 0;
    }
}
