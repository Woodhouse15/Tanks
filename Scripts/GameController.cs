using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour {
    public int level = 1;
    public int lives = 3;
    private List<EnemyTank> _enemies = new ();
    [SerializeField] public PlayerTank playerTank;
    bool gameOver;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _enemies = FindObjectsByType<EnemyTank>(FindObjectsSortMode.None).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemies.Count == 0) {
            gameOver = true;
        }

        if (!playerTank) {
            gameOver = true;
        }

        if (gameOver) {
            EndGame();
        }
    }

    void EndGame() {
        //TODO: Implement method
    }
}
