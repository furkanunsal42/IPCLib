global using intptr_t = System.IntPtr;
global using size_t = System.UInt64;

global using process_handle_t =  System.IntPtr;

using System.Runtime.InteropServices;

namespace ipclibcs
{
    class ProcessHandle
    {
        [DllImport("ipclib.dll")]
        public static extern process_handle_t process_get_current_process_handle();

        [DllImport("ipclib.dll")]
        public static extern size_t process_get_current_process_handle_int();

        [DllImport("ipclib.dll")] public static extern process_handle_t process_get_current_process_handle_duplicate_h(process_handle_t target_process);

        [DllImport("ipclib.dll")] public static extern process_handle_t process_get_current_process_handle_duplicate_s(size_t target_process);

        [DllImport("ipclib.dll")] 
        public static extern size_t process_get_current_process_handle_duplicate_int_h(process_handle_t target_process);
        
        [DllImport("ipclib.dll")]
        public static extern size_t process_get_current_process_handle_duplicate_int_s(size_t target_process);
    }
}
