using System.Diagnostics;
using UnityEngine;

public class LaunchServer : MonoBehaviour
{
    private Process _process;
    private void Awake()
    {
        var path = Application.streamingAssetsPath + "/netcoreapp3.1/OpenCvPuzzleHelper.exe";
        
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = path;
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;

        _process = new Process();
        _process.StartInfo = startInfo;
        _process.EnableRaisingEvents = true;
        
        _process.Start();
    }

    private void OnDestroy()
    {
        _process?.Kill();
    }
}
