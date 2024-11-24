using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class WanderController : MonoBehaviour {
    [Header("Grid fields")]
    //[SerializeField] private MazeSpawner m_Spawner;
    [SerializeField] public int[][] maze { set; get; }
    [SerializeField] private Grid m_Grid;
    private Stack<Vector2Int> path = new();

    private Vector3 nextdis;
    [SerializeField] private float gridTargetPrecision;

    [Header("Movement Variables")]
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    private bool WardenIsActive = false;
    private bool WardenIsSniffing = false;

    [Header("Sound Fields")]
    [SerializeField] private SoundEventEmitter SoundEventEmitter;
    [SerializeField] private AudioClip[] screamAudioClip;
    [SerializeField] private AudioClip[] seesPlayerClip;
    [SerializeField] private AudioClip[] hearsSomethingClip;
    private AudioSource _audioSource;

    [Header("Warden Model")]
    [SerializeField] private Animation wardenAnimation;

    [Header("Player Targeting")]
    [SerializeField] private int targetingWindow;
    [SerializeField] private float ChargeSpeed;
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRadius = 1;
    [SerializeField] private DetectionMethod detectionMethod;
    [SerializeField] private float GracePeriode;
    [SerializeField] private float DetectionChangePercent;
    private bool ChargingPlayer = false;

    enum DetectionMethod {
        StillInRangeAfterTime,
        RandomChange
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundEventEmitter.OnSoundEvent += HandleSoundEvent;
        _audioSource = GetComponent<AudioSource>();
    }

    private void HandleSoundEvent(SoundEvent @event) {
        if (maze == null) { return; }
        StopCoroutine("SniffForTarget");
        _audioSource.PlayOneShot(hearsSomethingClip[Random.Range(0, hearsSomethingClip.Length)]);
        ChangeAnimation("wardenrun.custom");
        WardenIsSniffing = false;
        speed = 2;
        turnSpeed = 2;

        var rounded = Vector3Int.RoundToInt(@event.eventPosition);
        var pos = m_Grid.WorldToCell(rounded+Vector3Int.one);
        //if (m_Spawner.maze[pos.x][pos.z] != 1) {
        if (maze[pos.x][pos.z] != 1) {
                SetPath(pos.x, pos.z);
            return;
        }

        var spaceModifierXTest = pos.x + (((rounded.x + 1) % 3) - 1);
        var spaceModifierZTest = pos.z + (((rounded.z + 1) % 3) - 1);

        var spaceModifierX = 0; 
        var spaceModifierZ = 0;
        //if (m_Spawner.maze[spaceModifierXTest][spaceModifierZTest] != 1) {
        if (maze[spaceModifierXTest][spaceModifierZTest] != 1) {
                spaceModifierX = ((rounded.x + 1) % 3) - 1;
            spaceModifierZ = ((rounded.z + 1) % 3) - 1;
        }
        //if (m_Spawner.maze[spaceModifierXTest][pos.z] != 1) {
        if (maze[spaceModifierXTest][pos.z] != 1) {
            spaceModifierX = ((rounded.x + 1) % 3) - 1;
        }
        //if (m_Spawner.maze[pos.x][spaceModifierZTest] != 1) {
        if (maze[pos.x][spaceModifierZTest] != 1) {
            spaceModifierZ = ((rounded.z + 1) % 3) - 1;
        }

        SetPath(pos.x + spaceModifierX, pos.z + spaceModifierZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (!WardenIsActive) { return; }

        if (!WardenIsSniffing && !ChargingPlayer && path.Count <= 0) {
            WardenIsSniffing = true;
            StartCoroutine("SniffForTarget");
        }

        if (!WardenIsSniffing && !ChargingPlayer && ((player.position+Vector3.up)-transform.position).magnitude < detectionRadius) {
            StartCoroutine("ChargePlayer");
        }
        if ((nextdis - transform.position).magnitude < 0.5f 
            || ((player.position + Vector3.up) - transform.position).magnitude > detectionRadius 
            && Physics.Linecast(transform.position, player.position + Vector3.up, out _, LayerMask.GetMask("Default"))) {
            ChargingPlayer = false;
        }

        if (WardenIsSniffing && path.Count <= 0) { return; }

        //if (path.Count <= 0) {
        //    //var x = Random.Range(0, m_Spawner.maze.Length - 1);
        //    var x = Random.Range(0, maze.Length - 1);
        //    //var y = Random.Range(0, m_Spawner.maze[0].Length - 1);
        //    var y = Random.Range(0, maze[0].Length - 1);
        //    SetPath(x, y);
        //}
        if (!ChargingPlayer && path.Count > 0) {
            nextdis = m_Grid.CellToWorld(Vector3Int.RoundToInt(new Vector3(path.Peek().x, 0, path.Peek().y)));
            nextdis.y = 1;
        }

        transform.position = transform.position + (speed * Time.deltaTime
            * ((nextdis - transform.position).normalized).normalized);
        // TODO make it turn toward it's target
        Quaternion toRotation = Quaternion.LookRotation(nextdis-transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation,toRotation,1*Time.deltaTime*turnSpeed);

        if (transform.forward != (nextdis - transform.position).normalized) {
            transform.position = transform.position + transform.forward * Time.deltaTime;
        }


        if (!ChargingPlayer && path.Count > 0 && (nextdis - transform.position).sqrMagnitude <= gridTargetPrecision) {
            path.Pop();
        }
    }

    void OnEnable() {
        //path.Clear();
        //if (path.Count <= 0) {
        //    var x = Random.Range(0, m_Spawner.maze.Length - 1);
        //    var y = Random.Range(0, m_Spawner.maze[0].Length - 1);
        //    SetPath(x,y);
        //}
    }

    private void SetPath(int x, int y) {
        path.Clear();
        var pos = m_Grid.WorldToCell(Vector3Int.RoundToInt(gameObject.transform.position)+Vector3Int.one);
        //var m = m_Spawner.maze;
        var m = maze;

        var disNode = AStar.ShortestPath(m, new int[] { pos.x, pos.z }, new int[] { x, y });
        Debug.Log(disNode.distance);
        while (disNode.parent != null) {
            path.Push(Vector2Int.RoundToInt(new Vector2(disNode.x, disNode.y)));
            disNode = disNode.parent;
        }

    }

    public void SpawnWarden() {
        var spawn = m_Grid.CellToWorld(Vector3Int.RoundToInt(new Vector3(maze.Length/2, 0, maze[0].Length/2)));
        spawn.y = 1;
        transform.position = spawn;
        WardenIsActive = true;
    }

    public IEnumerator SniffForTarget() {
        ChangeAnimation("wardenscream.custom");
        _audioSource.PlayOneShot(screamAudioClip[Random.Range(0, screamAudioClip.Length)]);
        yield return new WaitForSeconds(3);
        ChangeAnimation("wardenwalk.custom");
        WardenIsSniffing = false;
        var gridSpace = m_Grid.WorldToCell(Vector3Int.RoundToInt(player.position) + Vector3Int.one);
        //var x = Random.Range(0, maze.Length - 1);
        var x = Random.Range(System.Math.Max(gridSpace.x - targetingWindow + 1, 0), System.Math.Min(gridSpace.x + targetingWindow, maze.Length-1));
        //var y = Random.Range(0, maze[0].Length - 1);
        var y = Random.Range(System.Math.Max(gridSpace.z - targetingWindow + 1, 0), System.Math.Min(gridSpace.z + targetingWindow, maze.Length-1));
        SetPath(x, y);
        speed = 1;
        turnSpeed = 1;
    }

    private IEnumerator ChargePlayer() {
        if (!Physics.Linecast(transform.position, player.position + Vector3.up, out _, LayerMask.GetMask("Default"))) {
            if (detectionMethod == DetectionMethod.RandomChange && Random.value>DetectionChangePercent) {
                _audioSource.PlayOneShot(seesPlayerClip[Random.Range(0, seesPlayerClip.Length)]);
                ChargingPlayer = true;
                var target = player.position;
                target.y = 1;
                nextdis = target;
                ChangeAnimation("wardenrun.custom");
                speed = ChargeSpeed;
                turnSpeed = ChargeSpeed;
                path.Clear();
            }
            else if (detectionMethod == DetectionMethod.StillInRangeAfterTime) {
                yield return new WaitForSeconds(GracePeriode);
                if (!Physics.Linecast(transform.position, player.position + Vector3.up, out _, LayerMask.GetMask("Default"))) {
                    _audioSource.PlayOneShot(seesPlayerClip[Random.Range(0, seesPlayerClip.Length)]);
                    ChargingPlayer = true;
                    var target = player.position;
                    target.y = 1;
                    nextdis = target;
                    ChangeAnimation("wardenrun.custom");
                    speed = ChargeSpeed;
                    turnSpeed = ChargeSpeed;
                    path.Clear();
                }
            }
        }
    }

    private void ChangeAnimation(string animationName)
    {
        wardenAnimation.Play(animationName);
    }

    void OnDrawGizmosSelected() {
        var pos = m_Grid.WorldToCell(Vector3Int.RoundToInt(gameObject.transform.position)+Vector3Int.one);
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(m_Grid.CellToWorld(pos), Vector3.one * 3);
        if (nextdis!=null) {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(nextdis, Vector3.one * 3);
        }

        Gizmos.DrawLine(transform.position, player.position+Vector3.up);
    }
    private void OnDrawGizmos() {
        if (path.Count <= 0) return;
        var vec3 = path.Select(x => m_Grid.CellToWorld(Vector3Int.RoundToInt(new Vector3(x.x,0,x.y))));
        foreach (Vector3 cord in vec3) {
            Gizmos.DrawCube(cord, Vector3.one * 3);
        }
        Gizmos.DrawLineStrip(vec3.ToArray(), false);
    }
}
