# File Compression System

A client-server application that compresses files over a TCP connection. Built with C# and Windows Forms as part of a Network Programming course.

---

## Overview

The system consists of two parts:

- **Server** — a multi-threaded console app that receives files, compresses them using GZip, and sends them back
- **Client** — a Windows Forms app that lets the user pick a file, send it to the server, and save the compressed result

---

## How to Run

### Requirements
- Visual Studio 2019 or later
- .NET Framework 4.7+

### Steps
1. Clone or download the solution
2. Open the solution in Visual Studio
3. Right-click the **Solution** → **Properties** → **Multiple Startup Projects**
4. Set both `CompressionServer` and `CompressionClient` to **Start**
5. Press **F5**

The server console will start first. Then use the client form to connect, browse, and send files.

---

## How It Works

```
Client                          Server
  |                               |
  |-- connect (TCP port 5090) --> |
  |-- 8 bytes (file size) ------> |
  |-- file bytes ----------------> |
  |                               | compresses with GZip
  |<-- 8 bytes (compressed size)--|
  |<-- compressed bytes ----------|
  |                               |
  | saves .gz file                | closes connection
```

- File size is sent as a `long` (8 bytes) before the actual data so the receiver knows exactly how many bytes to expect
- The server spawns a new **thread per client**, so multiple clients can connect simultaneously
- The `ReadExactly` helper on both sides loops until all expected bytes are received, since TCP does not guarantee data arrives in one chunk

---

## Code Structure

```
CompressionServer/
└── Program.cs
    ├── Main()          — creates socket, binds to port 5090, accepts clients in a loop
    ├── HandleClient()  — receives file, compresses it, sends it back (runs on its own thread)
    ├── ReadExactly()   — helper to receive an exact number of bytes
    └── Compress()      — compresses bytes using GZipStream

CompressionClient/
└── Form1.cs
    ├── button1_Click()         — connects to server
    ├── browse_button_Click()   — opens file picker and loads file into memory
    ├── send_button_Click()     — sends file size + file bytes to server
    ├── receive_button_Click()  — receives compressed file and saves it as .gz
    └── ReceiveExactlyAsync()   — async helper to receive an exact number of bytes
```