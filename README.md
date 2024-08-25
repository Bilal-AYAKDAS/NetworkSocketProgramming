1. Form Interface:
InitializeComponent(): Used to initialize Windows Forms components and create interface elements (labels, text box, list box, buttons).
Label (label1): Label that tells the user to enter text.
TextBox (newText): Area where the user enters text.
ListBox (results): List that shows information such as messages from the server and server status.
Button (sendit, listen): Buttons used to start the server and send messages.
2. ButtonListenOnClick(object obj, EventArgs ea)
This is the method used to start the server and listen to clients on a specific port.
serverSocket creates a Socket object and accepts connections from all IP addresses (IPAddress.Any) using the TCP protocol.
The BeginAccept method starts accepting client connections asynchronously and triggers the callback method called AcceptConn when a client connects.
3. ButtonSendOnClick(object obj, EventArgs ea)
Receives the message entered by the user in the text box and sends it to all connected clients.
The message is converted to a byte array and sent asynchronously with the BeginSend method. When the message sending is completed, the SendData callback method is triggered.

4. AcceptConn(IAsyncResult iar)
Triggered when a new client connection is accepted.
The EndAccept method completes the connection with the client and creates a new client socket (clientSocket).
The client socket is added to the clientSockets list and the BeginReceive method is used to start receiving data from the client. This method receives data asynchronously and triggers the ReceiveData callback method.

5. Connected(IAsyncResult iar)
Triggered when the client is successfully connected.
The EndConnect method completes the connection and the connection status is displayed in the results list.
6. SendData(IAsyncResult iar)
Triggered when the message sending process is completed.
EndSend method returns the number of bytes sent.
7. ReceiveData(IAsyncResult iar)
Triggered when data is received from the client.
EndReceive method returns the number of bytes received. If the number of bytes received is 0, the client connection is closed.
The received data is converted to text and added to the results list.
If the received message is "bye", a "bye" message is sent back to the client, the client is removed from the list, and the connection is closed.
The received message is broadcasted to all connected clients again.
BeginReceive method is called to receive data again.
Additional Descriptions
clientSockets: A list that holds the sockets of all connected clients. Used to manage communication with clients.
serverSocket: Server socket. Used to accept client connections.
data: Byte array used to store received/sent data.
Summary
This server communicates with clients asynchronously over TCP. When a client connects to the server, the server receives messages from the client and broadcasts them to other connected clients. The user interface allows the user to start the server and send messages. Due to its asynchronous nature, the server can handle multiple clients simultaneously.
