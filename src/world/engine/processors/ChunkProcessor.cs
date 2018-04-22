using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChunkProcessor : MonoBehaviour {

    static Thread[] processingThread = new Thread[12];
    static List<Process> processes = new List<Process>();

    static float timer, interval = 0.1f;

    public class Process {
        public Flag flag;
        public bool success;
        public string hash;
        public Chunk chunk;
    }
	
    public enum Flag {
        Pending, 
        Processing,
        Ready
    }

    public void Request(Chunk chunk){
        Process process = new Process();
        process.flag = Flag.Pending;
        process.chunk = chunk;
        process.hash = process.GetHashCode().ToString();
        processes.Add(process);
    }

	void Update () {
        timer += Time.deltaTime;
        if (timer < interval)
            return;

        timer = 0;

        for (int i = 0; i < processes.Count; i++){
            if (processes[i].flag == Flag.Pending){
                for (int j = 0; j < processingThread.Length; j++){
                    if ((processingThread[j] == null || !processingThread[j].IsAlive) && processes[i].flag == Flag.Pending ){
                        Thread.Sleep(1);

                        processingThread[j] = new Thread(new ThreadStart(delegate {
                                    ChunkGenerator.Generate(processingThread[j], processes[i]);
                                }
                            )
                        );
                        processingThread[j].Start();
                        break;
                    }
                }
            }
            if (processes[i].flag == Flag.Ready){
                processes[i].chunk.Create(processes[i]);
                processes.Remove(processes[i]);
            }
        }
	}
}
