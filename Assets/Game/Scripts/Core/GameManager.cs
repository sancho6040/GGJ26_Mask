using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelGenerator _levelGeneratorInstance { get; private set; }
    public LevelGenerator LevelGenerator;

    public GameObject _playerInstance { get; private set; }
    public GameObject PlayerPrefab;

    public CameraFollow25D _playercameraInstance { get; private set; }
    public CameraFollow25D Playercamera;

    public async void Start()
    {
        _levelGeneratorInstance = Instantiate(LevelGenerator);

        while (_levelGeneratorInstance.SpawnPointInstance == null)
        {
            //wait for next frame
            await Task.Yield();
        }

        _playerInstance = Instantiate(PlayerPrefab, _levelGeneratorInstance.SpawnPointInstance.transform.position, Quaternion.identity);

        _playercameraInstance = Instantiate(Playercamera);
        _playercameraInstance.target = _playerInstance.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
