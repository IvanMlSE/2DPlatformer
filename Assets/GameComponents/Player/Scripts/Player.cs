using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField]
    private Text _textCountOfCherries;

    [SerializeField]
    private Text _textCountOfGems;

    private int _countOfCherries;

    private int _countOfGems;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _countOfCherries = 0;
        _countOfGems = 0;

        Spawn();
    }

    private void Update()
    {
        _textCountOfCherries.text = _countOfCherries.ToString();

        _textCountOfGems.text = _countOfGems.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Cherry cherry))
        {
            _countOfCherries++;

            Destroy(cherry.gameObject);
        }

        if (collision.TryGetComponent(out Gem gem))
        {
            _countOfGems++;

            Destroy(gem.gameObject);
        }
    }

    public void Spawn()
    {
        _rigidbody2D.position = _spawnPoint.position;
    }
}