using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerCubeHolder : MonoBehaviour
{
    public event Action NewCubeAdd = delegate { };
    public event Action CubeLose = delegate { };

    [SerializeField]
    private CollectCubeEffectFactory _collectCubeEffect;
    [SerializeField]
    private List<PlayerCube> _playerCubes;

    void Start()
    {
        foreach (var cube in _playerCubes)
        {
            cube.GrabCube += OnGrabCube;
            cube.CubeWallCollide += OnCubeWallCollide;
        }
    }
    private void OnGrabCube(PlayerCube cube)
    {
        if (!_playerCubes.Contains(cube))
        {
            NewCubeAdd.Invoke();
            cube.gameObject.tag = "PlayerCube";
            
            cube.transform.position = transform.position + Vector3.up * _playerCubes.Count;
            _collectCubeEffect.CreateEffectAt(cube.transform.position);
            cube.transform.SetParent(transform);
            _playerCubes.Add(cube);

            cube.GrabCube += OnGrabCube;
            cube.CubeWallCollide += OnCubeWallCollide;
        }
    }

    private void OnCubeWallCollide(PlayerCube cube)
    {
        _playerCubes.Remove(cube);
        CubeLose.Invoke();
        cube.GrabCube -= OnGrabCube;
        cube.CubeWallCollide -= OnCubeWallCollide;
    }

    private void OnDestroy()
    {
        foreach (var cube in _playerCubes)
        {
            cube.GrabCube -= OnGrabCube;
            cube.CubeWallCollide -= OnCubeWallCollide;
        }
    }
}