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
		fmt.Println("Some error: ", err)
		return
	}

	fmt.Println("Listening..")
	for {
		connection, err := listener.AcceptTCP()
		if err != nil {
			fmt.Println(err)
		}

		fmt.Println("Connected! (", connection.RemoteAddr(), ")")
		go wait(connection)
	}
}

func wait(connection net.Conn) {
	var buffer []byte

	for {
		n, err := connection.Read(buffer)
		fmt.Println(n, "bytes read")
		if err == nil {
			fmt.Println("Received: ", buffer)
		} else {
			fmt.Println("Error: ", err)
			connection.Close()
			break
		}
	}

	fmt.Println("Waiting finished")
}
