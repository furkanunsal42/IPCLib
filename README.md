# IPCLib
Interprocess Communication Library for C, C++, C# and Python using anonymous pipes and events, supporting Windows and Linux
# CPP Example
```cpp
void main_parent() {
	
	Pipe pipe_read;
	Pipe pipe_write;
	Event event_external;

	std::wstringstream ss;
	ss << "executable.exe " <<
		pipe_write.get_io_event()->get_handle_int() << " " <<
		event_external.get_handle_int() << " " <<
		pipe_write.get_read_handle_int() << " " <<
		pipe_read.get_write_handle_int();

	std::wstring command = ss.str();

	STARTUPINFO start_info = { sizeof(start_info) };
	PROCESS_INFORMATION proc_info = { 0 };
	::CreateProcessW(L"executable.exe", &command[0], nullptr, nullptr, true,
		 CREATE_NEW_CONSOLE, nullptr, nullptr, &start_info, &proc_info);
	::CloseHandle(proc_info.hThread);
	::CloseHandle(proc_info.hProcess);

	pipe_read.close_write();
	pipe_write.close_read();
	pipe_write.set_event_trigger_condition(Pipe::W);

	std::cout << "Parent: my rp:" << pipe_read <<
			" wp:" << pipe_write <<
			" event:" << *pipe_write.get_io_event() <<
			" event_external:" << event_external << std::endl;

	event_external.add_listener([&pipe_read, &pipe_write]() {
		std::string message;
		pipe_read.read(message);
		std::cout << "Parent: event is triggered, child sent: " << message << std::endl;
		
		if (message == "Hello There") {
			::Sleep(1000);
			pipe_write.write("Nice to see you Boy!");
		}

		if (message == "Its nice to talk to you too, Father!") {
			::Sleep(1000);
			pipe_write.write("Come join me Boy! We can rule the Universe as Father and Son!");
		}

		});

	std::cin.get();
}
```
```cpp
void main_child(char* argv[]) {
	
	std::stringstream ss;
	ss << argv[1] << " " << argv[2] << " " << argv[3] << " " << argv[4];
	size_t event_external_h;
	size_t event_h;
	size_t pipe_read_h;
	size_t pipe_write_h;

	ss >> event_external_h;
	ss >> event_h;
	ss >> pipe_read_h;
	ss >> pipe_write_h;

	Pipe pipe_read(0, pipe_read_h, nullptr);
	Pipe pipe_write(pipe_write_h, 0, event_h);
	Event event_external(event_external_h);
	
	pipe_write.set_event_trigger_condition(Pipe::WRITE);

	std::cout << "Child: my rp:" << pipe_read <<
		" wp:" << pipe_write <<
		" event:" << *pipe_write.get_io_event() <<
		" event_external:" << event_external << std::endl;
	
	event_external.add_listener([&pipe_read, &pipe_write]() {
		std::string message;
		pipe_read.read(message);
		std::cout << "Child: event is triggered, parent sent: " << message << std::endl;
	
		if (message == "Nice to see you Boy!") {
			::Sleep(1000);
			pipe_write.write("Its nice to talk to you too, Father!");
		}
	
		if (message == "Come join me Boy! We can rule the Universe as Father and Son!") {
			::Sleep(1000);
			pipe_write.write("No Father! You were supposed to bring balance to the Force not destroy it!");
		}
		});
	
	pipe_write.write("Hello There");

	std::cin.get();
}
```
# C# Example
```csharp 
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
```
