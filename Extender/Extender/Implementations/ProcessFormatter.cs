using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Extender.Abstractions;
using System.Diagnostics;
namespace Extender.Implementations
{
    public class ProcessFormatter:IProcessFormatter
    {
        public string GetInfo(Process process)
        {
            StringBuilder builder = new StringBuilder(256);
            builder.AppendLine($"{Resource.ProcessIdTitle}:{process.Id}");
            builder.AppendLine($"{Resource.ProcessNameTitle}:{process.ProcessName}");
            builder.AppendLine($"{Resource.ProcessMachineNameTitle}:{process.MachineName}");
            builder.AppendLine($"{Resource.ProcessMainWindowTitle}:{process.MainWindowTitle}");
            builder.AppendLine($"{Resource.ProcessRespondingTitle}:{process.Responding}");
            builder.AppendLine($"{Resource.VirtualMemoryTitle}:{process.VirtualMemorySize64}");
            builder.AppendLine($"{Resource.PhysicalMemoryTitle}:{process.WorkingSet64}");
            builder.AppendLine($"{Resource.ProcessThreadsTitle}:{process.Threads?.Count}");
            builder.AppendLine($"{Resource.ProcessCodeExecutionTitle}:{process.UserProcessorTime}");
            builder.AppendLine($"{Resource.ProcessTotalExecutionTitle}:{process.TotalProcessorTime}");
            builder.AppendLine($"{Resource.ProcessPriorityTitle}:{process.BasePriority}");
            try
            {
                builder.AppendLine($"{Resource.ProcessStartTimeTitle}:{process.StartTime}");
            }
            catch
            {
                builder.AppendLine($"{Resource.ProcessStartTimeTitle}:{Resource.NoAccess}");
            }
            try
            {
                builder.AppendLine($"{Resource.ProcessExitTimeTitle}:{process.ExitTime}");
            }
            catch
            {
                builder.AppendLine($"{Resource.ProcessExitTimeTitle}:{Resource.NoAccess}");
            }
            var enumerator = process?.Threads.GetEnumerator();
            enumerator.MoveNext();
            if (enumerator.Current != null)
            {
                foreach (ProcessThread thread in process.Threads)
                {
                    builder.AppendLine($"{Resource.ProcessThreadIdTitle}:{thread.Id}");
                    builder.AppendLine($"{Resource.ProcessThreadPriorityTitle}:{thread.BasePriority}");
                    builder.AppendLine($"{Resource.ProcessThreadStateTitle}:{thread.ThreadState}");
                    builder.AppendLine($"{Resource.ProcessThreadWaitReasonTitle}:{(thread.ThreadState == ThreadState.Wait ? thread.WaitReason.ToString() : "-")}");
                    builder.AppendLine($"{Resource.ProcessThreadCodeExecutionTitle}:{(thread.ThreadState != ThreadState.Running ? thread.UserProcessorTime.ToString() : "-")}");
                    builder.AppendLine($"{Resource.ProcessThreadTotalExecutionTitle}:{(thread.ThreadState != ThreadState.Running ? thread.TotalProcessorTime.ToString() : "-")}");
                }
            }
            return builder.ToString();
        }
    }
}
