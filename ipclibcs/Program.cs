using ipclibcs;

public class Program
{
    public static void Main(string[] args)
    {
        Event e = new Event();

        e.wait_timeout(1000 * 2);

        Console.WriteLine("Hello There");
        Console.WriteLine(ProcessHandle.process_get_current_process_handle());

        byte[] message_write = new byte[32];
        for (int i = 0; i < message_write.Length; i++)
            message_write[i] = (byte)i;

        pipe_t pipe = Pipe.pipe_create();

        Pipe.pipe_write(pipe, message_write, (size_t)message_write.Length * sizeof(byte), 0, "");

        byte[] message_read = new byte[32];
        Pipe.pipe_read(pipe, message_read, (size_t)message_write.Length * sizeof(byte), 0, "");

        for (int i = 0; i < message_write.Length; i++)
            Console.Write(message_read[i]);
        Console.WriteLine();

        Console.Read();
    }
}