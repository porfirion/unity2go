package main

import (
	"fmt"
	"net"
)

var (
	//listener    Listener
	tcpProtocol string = "tcp4"
	tcpAddr            = &net.TCPAddr{IP: net.IPv4(127, 0, 0, 1), Port: 25001}
	listener    net.Listener
	connection  net.Conn
)

func main() {
	listener, err := net.ListenTCP(tcpProtocol, tcpAddr)

	if err != nil {
		fmt.Println("Error opening listener: ", err)
		return
	}

	fmt.Println("Listening..", tcpAddr)
	for {
		connection, err := listener.AcceptTCP()
		if err != nil {
			fmt.Println(err)
		}

		fmt.Println("Connected! (", connection.RemoteAddr(), ")")

		/*var buffer []byte
		shouldClose := false
		for !shouldClose {
			buffer = make([]byte, 1000)
			n, err := connection.Read(buffer)

			if err != nil {
				fmt.Println("Received: ", n)
				fmt.Println("Error: ", err)
				shouldClose = true
			} else {
				fmt.Println("Received: ", n)
			}
		}
		connection.Close()
		fmt.Println("Connection closed")*/
		go wait(connection)
	}

	for {
	}
}

func wait(connection net.Conn) {
	defer connection.Close()

	var buffer []byte

	for {
		buffer = make([]byte, 1000)

		n, err := connection.Read(buffer)

		fmt.Println(n, "bytes read")
		if err == nil {
			fmt.Println("Received: ", string(buffer))
		} else {
			fmt.Println("Error: ", err)
			connection.Close()
			break
		}
	}

	fmt.Println("Waiting finished")
}
