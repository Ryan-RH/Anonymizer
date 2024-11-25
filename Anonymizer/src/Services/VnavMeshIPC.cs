using ECommons.EzIpcManager;
using System.Threading;

namespace Anonymizer.Services;

public class VnavMeshIPC
{
    // Example IPC functions
    [EzIPC("Nav.IsReady", true)] public Func<bool> Nav_IsReady = null!;
    [EzIPC("Nav.BuildProgress", true)] public Func<float> Nav_BuildProgress = null!;
    [EzIPC("Nav.Reload", true)] public Action Nav_Reload = null!;
    [EzIPC("Nav.Rebuild", true)] public Action Nav_Rebuild = null!;
    [EzIPC("Nav.Pathfind", true)] public Func<Vector3, Vector3, bool, Task<List<Vector3>>> Nav_Pathfind = null!;
    [EzIPC("Nav.PathfindCancelable", true)] public Func<Vector3, Vector3, bool, CancellationToken, Task<List<Vector3>>> Nav_PathfindCancelable = null!;
    [EzIPC("Nav.PathfindCancelAll", true)] public Action Nav_PathfindCancelAll = null!;
    [EzIPC("Nav.PathfindInProgress", true)] public Func<bool> Nav_PathfindInProgress = null!;
    [EzIPC("Nav.PathfindNumQueued", true)] public Func<int> Nav_PathfindNumQueued = null!;
    [EzIPC("Nav.IsAutoLoad", true)] public Func<bool> Nav_IsAutoLoad = null!;
    [EzIPC("Nav.SetAutoLoad", true)] public Action<bool> Nav_SetAutoLoad = null!;

    [EzIPC("Query.Mesh.NearestPoint", true)] public Func<Vector3, float, float, Vector3> Query_Mesh_NearestPoint = null!;
    [EzIPC("Query.Mesh.PointOnFloor", true)] public Func<Vector3, bool, float, Vector3> Query_Mesh_PointOnFloor = null!;

    [EzIPC("Path.MoveTo", true)] public Action<List<Vector3>, bool> Path_MoveTo = null!;
    [EzIPC("Path.Stop", true)] public Action Path_Stop = null!;
    [EzIPC("Path.IsRunning", true)] public Func<bool> Path_IsRunning = null!;
    [EzIPC("Path.NumWaypoints", true)] public Func<int> Path_NumWaypoints = null!;
    [EzIPC("Path.GetMovementAllowed", true)] public Func<bool> Path_GetMovementAllowed = null!;
    [EzIPC("Path.SetMovementAllowed", true)] public Action<bool> Path_SetMovementAllowed = null!;
    [EzIPC("Path.GetAlignCamera", true)] public Func<bool> Path_GetAlignCamera = null!;
    [EzIPC("Path.SetAlignCamera", true)] public Action<bool> Path_SetAlignCamera = null!;
    [EzIPC("Path.GetTolerance", true)] public Func<float> Path_GetTolerance = null!;
    [EzIPC("Path.SetTolerance", true)] public Action<float> Path_SetTolerance = null!;

    [EzIPC("SimpleMove.PathfindAndMoveTo", true)] public Func<Vector3, bool, bool> SimpleMove_PathfindAndMoveTo = null!;
    [EzIPC("SimpleMove.PathfindInProgress", true)] public Func<bool> SimpleMove_PathfindInProgress = null!;

    [EzIPC("Window.IsOpen", true)] public Func<bool> Window_IsOpen = null!;
    [EzIPC("Window.SetOpen", true)] public Action<bool> Window_SetOpen = null!;

    [EzIPC("DTR.IsShown", true)] public Func<bool> DTR_IsShown = null!;
    [EzIPC("DTR.SetShown", true)] public Action<bool> DTR_SetShown = null!;

    // Exeample IPC Constructor
    private VnavMeshIPC()
    {
        EzIPC.Init(this, "vnavmesh", SafeWrapper.AnyException);
    }
}
