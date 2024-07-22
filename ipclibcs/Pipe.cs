global using pipe_handle_t = System.IntPtr;
global using pipe_t = System.IntPtr;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ipclibcs
{
    class Pipe
    {
        public struct io_op_result
        {
            public io_op_result() { }

            public bool success = false;
            public size_t io_bytes = 0;
            public size_t total_bytes = 0;
            public bool is_total_bytes_learned = false;
        };

        [DllImport("ipclib.dll")]
        public static extern pipe_t pipe_create();
        [DllImport("ipclib.dll")]
        public static extern pipe_t pipe_create_hh(pipe_handle_t write_handle, pipe_handle_t read_handle);
        [DllImport("ipclib.dll")]
        public static extern pipe_t pipe_create_hhh(pipe_handle_t write_handle, pipe_handle_t read_handle, event_handle_t external_io_event);
        [DllImport("ipclib.dll")]
        public static extern pipe_t pipe_create_hhs(pipe_handle_t write_handle, pipe_handle_t read_handle, size_t external_io_event_int);
        [DllImport("ipclib.dll")]
        public static extern pipe_t pipe_create_ssh(size_t write_handle_int, size_t read_handle_int, event_handle_t external_io_event);
        [DllImport("ipclib.dll")]
        public static extern pipe_t pipe_create_sss(size_t write_handle_int, size_t read_handle_int, size_t external_io_event_int);

        [DllImport("ipclib.dll")] 
        public static extern void pipe_destroy(pipe_t pipe);

        [DllImport("ipclib.dll")] 
        public static extern void pipe_set_ownership(pipe_t pipe, bool is_read_handle_owned, bool is_write_handle_owned);
        [DllImport("ipclib.dll")]
        public static extern bool pipe_close(pipe_t pipe);
        [DllImport("ipclib.dll")]
        public static extern bool pipe_close_read(pipe_t pipe);
        [DllImport("ipclib.dll")]
        public static extern bool pipe_close_write(pipe_t pipe);

        [DllImport("ipclib.dll")]
        public static extern io_op_result pipe_clear(pipe_t pipe);
        [DllImport("ipclib.dll")]
        public static extern bool pipe_is_empty(pipe_t pipe);
        [DllImport("ipclib.dll")]
        public static extern size_t pipe_get_size(pipe_t pipe);

        [DllImport("ipclib.dll")]
        public static extern pipe_handle_t pipe_get_read_handle(pipe_t pipe);
        [DllImport("ipclib.dll")]
        public static extern pipe_handle_t pipe_get_write_handle(pipe_t pipe);
        [DllImport("ipclib.dll")]
        public static extern size_t pipe_get_read_handle_int(pipe_t pipe);
        [DllImport("ipclib.dll")]
        public static extern size_t pipe_get_write_handle_int(pipe_t pipe);

        //const Pipe::handle_t ipc_api pipe_get_read_handle_duplicate(void* pipe);
        //const Pipe::handle_t ipc_api pipe_get_write_handle_duplicate(void* pipe);
        //const size_t ipc_api pipe_get_read_handle_duplicate_int(void* pipe);
        //const size_t ipc_api pipe_get_write_handle_duplicate_int(void* pipe);

        [DllImport("ipclib.dll")]
        public static extern io_op_result pipe_write(pipe_t pipe, byte[] source_buffer, size_t buffer_size_in_byte, size_t offset_in_byte, string endline_character);
        [DllImport("ipclib.dll")]
        public static extern io_op_result pipe_read(pipe_t pipe, byte[] target_buffer, size_t buffer_size_in_byte, size_t offset_in_byte, string endline_character);
        [DllImport("ipclib.dll")]
        public static extern io_op_result pipe_peek(pipe_t pipe, byte[] target_buffer, size_t buffer_size_in_byte, size_t offset_in_byte, string endline_character);
        [DllImport("ipclib.dll")]
        public static extern io_op_result pipe_update(pipe_t pipe, byte[] source_buffer, size_t buffer_size_in_byte, size_t offset_in_byte, string endline_character);

        //Pipe::io_op_result ipc_api pipe_read_and_parse(std::vector<std::string>& out_vector, char* endline_character = "|");

        [DllImport("ipclib.dll")]
        public static extern event_t pipe_get_io_event(pipe_t pipe);
        [DllImport("ipclib.dll")]
        public static extern void pipe_set_io_event(pipe_t pipe, event_t external_event);
        [DllImport("ipclib.dll")]
        public static extern void pipe_set_event_trigger_condition(pipe_t pipe, int condition_flags);

        pipe_handle_t pipe_ptr = 0;


    }
}
