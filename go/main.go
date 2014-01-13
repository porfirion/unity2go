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
		if n, err := connection.Read(buffer); err == nil {
			fmt.Println(n, "bytes read")
			fmt.Println(buffer)
		} else {
			/*fmt.Println(n, "bytes read")
			fmt.Println("Error: ", err)
			break*/
		}
	}

	fmt.Println("Waiting finished")
}
